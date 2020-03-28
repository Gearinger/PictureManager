using DevExpress.Mvvm;
using PictureManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureManager.ViewModel
{
    class PictureManageViewModel: ViewModelBase
    {
        public PictureManageViewModel()
        {

        }

        #region 全局变量
        string defaultDirectory = Environment.SpecialFolder.MyPictures.ToString();
        private ObservableCollection<PictureItemModel> pictureList = new ObservableCollection<PictureItemModel>();
        private ObservableCollection<DirectoryModel> directoryList = new ObservableCollection<DirectoryModel>();

        private DirectoryModel selectDirectory = new DirectoryModel();
        private PictureItemModel selectPicture = new PictureItemModel();
        private string currentDirectory;


        #endregion

        #region 属性
        /// <summary>
        /// 左侧文件夹树列表
        /// </summary>
        public ObservableCollection<DirectoryModel> DirectoriesTree { get => directoryList; set { directoryList = value;RaisePropertiesChanged("DirectoriesTree"); } }

        /// <summary>
        /// 图片列表
        /// </summary>
        public ObservableCollection<PictureItemModel> PictureList { get => pictureList; set { pictureList = value; RaisePropertiesChanged("PictureList"); } }

        /// <summary>
        /// 选择的图片
        /// </summary>
        public PictureItemModel SelectPicture { get => selectPicture; set { selectPicture = value; RaisePropertiesChanged("SelectPicture"); } }

        /// <summary>
        /// 选择的路径
        /// </summary>
        public DirectoryModel SelectDirectory { 
            get => selectDirectory; 
            set { 
                selectDirectory = value;
                if (selectDirectory!=null)
                {
                    CurrentDirectory = selectDirectory?.Directory;
                }
                RaisePropertiesChanged("SelectDirectory"); 
            } }

        public string CurrentDirectory { 
            get => currentDirectory; 
            set {
                if (currentDirectory != value)
                {
                    InitPictureList(value);
                }
                currentDirectory = value; 
                RaisePropertiesChanged("CurrentDirectory"); 
            } 
        }

        #endregion

        #region Command
        #region 新建
        public DelegateCommand CreateDirectoryCommand => new DelegateCommand(CreateDirectoryEvent);
        private void CreateDirectoryEvent()
        {
            DirectoriesTree?.Clear();
            PictureList?.Clear();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择项目文件夹";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.ShowDialog();
            defaultDirectory = folderBrowserDialog.SelectedPath;
            bool isOver = false;
            InitDirectoryList(defaultDirectory, ref isOver);
            InitPictureList(defaultDirectory);
            currentDirectory = defaultDirectory;
        }
        #endregion

        #region 打开
        public DelegateCommand OpenDirectoryCommand => new DelegateCommand(OpenDirectoryEvent);

        private void OpenDirectoryEvent()
        {
            DirectoriesTree?.Clear();
            PictureList?.Clear();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择项目文件夹";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.ShowDialog();
            defaultDirectory = folderBrowserDialog.SelectedPath;
            bool isOver = false;
            InitDirectoryList(defaultDirectory, ref isOver);
            InitPictureList(defaultDirectory);
            currentDirectory = defaultDirectory;
        }
        #endregion

        #region 退出
        public DelegateCommand CloseWinCommand => new DelegateCommand(CloseWinEvent);

        private void CloseWinEvent()
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
        #endregion

        #region Func
        /// <summary>
        /// 初始化图片列表
        /// </summary>
        /// <param name="directory">输入图片路径</param>
        private void InitPictureList(string directory)
        {
            if (PictureList.Count != 0) PictureList.Clear();
            foreach (var picture in Directory.GetFiles(directory, "*.jpg", SearchOption.AllDirectories))
            {
                PictureList.Add(new PictureItemModel() {
                    Author="", CreateDate = DateTime.Now, Description="", FileName = Path.GetFileName(picture), Name=Path.GetFileNameWithoutExtension(picture), PicturePath = picture
                });
            }

            //初始化显示大小
            foreach (var item in PictureList)
            {
                item.PixelHeight = 300;
                item.PixelWidth = item.PixelHeight * GetWidthDevideHeight(item.PicturePath);
            }
        }

        /// <summary>
        /// 初始化文件夹列表
        /// </summary>
        /// <param name="directory">目标路径</param>
        /// <param name="isOver">是否结束寻找下级文件夹</param>
        private void InitDirectoryList(string directory, ref bool isOver)
        {
            if (DirectoriesTree.Count==0)
            {
                DirectoriesTree.Add(new DirectoryModel() { Directory = directory, ID = DirectoriesTree.Count + 1, ParentID = 0, Name = Path.GetFileName(directory) });
            }
            if (isOver) return;
            int parentID = DirectoriesTree.ToList().Find(p => p.Directory == directory)==null?1: DirectoriesTree.ToList().Find(p => p.Directory == directory).ID;
            foreach (var item in Directory.GetDirectories(directory))
            {
                if (Utility.Utility.IsPathHidden(item)) continue;
                DirectoriesTree.Add(new DirectoryModel() { Directory = item, ID = DirectoriesTree.Count + 1, ParentID = parentID, Name = Path.GetFileName(item) });
                if (DirectoriesTree.Count == 1000)
                {
                    isOver = MessageBox.Show("所选择项目文件已超过1000条，是否继续?", "", MessageBoxButtons.YesNo).Equals(DialogResult.No);
                }
                if (isOver) break;
                InitDirectoryList(item, ref isOver);
            }
        }
        
        private double GetWidthDevideHeight(string picPath)
        {
            Bitmap pic = new Bitmap(picPath);
            return pic.Width / (double)pic.Height;
        }

        #endregion

    }
}
