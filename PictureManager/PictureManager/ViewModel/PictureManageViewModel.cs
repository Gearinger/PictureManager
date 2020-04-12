using PictureManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PictureManager.DelegateCommandNameSpace;
using System.Security.AccessControl;
using PictureManager.Utility;

namespace PictureManager.ViewModel
{
    class PictureManageViewModel: INotifyPropertyChanged
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
        private string mainBackGroundIMG;
        private int pWidth = 1190;

        /// <summary>
        /// 项目中文件夹数量
        /// </summary>
        public int DirCount { get; set; }
        /// <summary>
        /// 项目中图片数量
        /// </summary>
        public int PicCount { get; set; }

        #endregion

        #region 属性
        /// <summary>
        /// 左侧文件夹树列表
        /// </summary>
        public ObservableCollection<DirectoryModel> DirectoriesTree { get => directoryList; set { directoryList = value; NotifyPropertyChanged("DirectoriesTree"); } }

        /// <summary>
        /// 图片列表
        /// </summary>
        public ObservableCollection<PictureItemModel> PictureList { get => pictureList; set { pictureList = value; NotifyPropertyChanged("PictureList"); } }

        /// <summary>
        /// 选择的图片
        /// </summary>
        public PictureItemModel SelectPicture { get => selectPicture; set { selectPicture = value; NotifyPropertyChanged("SelectPicture"); } }

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
                NotifyPropertyChanged("SelectDirectory"); 
            } }

        public string CurrentDirectory { 
            get => currentDirectory; 
            set {
                if (currentDirectory != value)
                {
                    InitPictureList(value);
                    SortPicture();
                }
                currentDirectory = value;
                NotifyPropertyChanged("CurrentDirectory"); 
            } 
        }

        public string MainBackGroundIMG { get => mainBackGroundIMG;set { mainBackGroundIMG = value; NotifyPropertyChanged("MainBackGroundIMG"); } }

        public int PWidth { get=>pWidth; set {pWidth=value; NotifyPropertyChanged("PWidth"); } }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
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
            GetDirectoryListFromDir(DirectoriesTree,defaultDirectory);
            InitPictureList(defaultDirectory);
            SortPicture();
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
            GetDirectoryListFromDir(DirectoriesTree,defaultDirectory);
            InitPictureList(defaultDirectory);
            SortPicture();
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
                var rate = 1.0/GetWidthDevideHeight(item.PicturePath);
                item.Height = Convert.ToInt32(item.Width * rate);
            }
        }

        /// <summary>
        /// 读取路径，构建DirectoryModelList
        /// </summary>
        /// <param name="directory">目标路径</param>
        /// <param name="isOver">是否结束寻找下级文件夹</param>
        private void GetDirectoryListFromDir(ObservableCollection<DirectoryModel> DirList, string directory)
        {
            //路径不可访问
            if (Common.IsPathHidden(directory)) return;

            if (DirList.Count==0)
            {
                DirList.Add(new DirectoryModel() { Directory = directory, GUID = Guid.NewGuid(), Name = Path.GetFileName(directory) });
            }

            foreach (var item in Directory.GetDirectories(directory))
            {
                if (Common.IsPathHidden(item)) continue;
                DirList.Add(new DirectoryModel() { Directory = item, GUID = Guid.NewGuid(), Name = Path.GetFileName(item) });
                if (DirCount >= 1000)
                {
                    if (MessageBox.Show("所选择项目文件夹已超过1000条，是否继续?", "", MessageBoxButtons.YesNo).Equals(DialogResult.No)) return;
                }
                GetDirectoryListFromDir(DirList[DirList.Count-1].DirectoryList, item);
            }
        }
        
        private double GetWidthDevideHeight(string picPath)
        {
            Bitmap pic = new Bitmap(picPath);
            return pic.Width / (double)pic.Height;
        }

        private void SortPicture()
        {
            if (PictureList.Count==0)
            {
                return;
            }
            int loopNum = PWidth / (PictureList[0].Width+10);
            List<Point> seriesList = new List<Point>();

            PictureList[0].X = 0;
            PictureList[0].Y = 0;
            seriesList.Add(new Point(PictureList[0].X, PictureList[0].Y + PictureList[0].Height + 5));

            for (int i = 1; i < PictureList.Count; i++)
            {
                if (i < loopNum)
                {
                    PictureList[i].X = PictureList[i - 1].X + PictureList[i - 1].Width + 5;
                    PictureList[i].Y = 0;
                    PictureList[i].Margin = $"{PictureList[i].X},{PictureList[i].Y},0,0";
                    seriesList.Add(new Point(PictureList[i].X, PictureList[i].Y + PictureList[i].Height));
                }
                else
                {
                    PictureList[i].X = (int)seriesList[0].X;
                    PictureList[i].Y = (int)seriesList[0].Y;
                    PictureList[i].Margin = $"{PictureList[i].X},{PictureList[i].Y},0,0";
                    seriesList[0] = new Point(PictureList[i].X, PictureList[i].Y + PictureList[i].Height+5);
                }
                seriesList.Sort((x, y) => Convert.ToInt32(x.Y - y.Y));
            }
        }

        #endregion

    }
}
