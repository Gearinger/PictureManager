using DevExpress.Mvvm;
using PictureManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureManager.ViewModel
{
    class PictureManageViewModel: ViewModelBase
    {
        #region 全局变量
        string currentDirectory = Environment.SpecialFolder.MyPictures.ToString();
        private ObservableCollection<PictureItemModel> pictureList = new ObservableCollection<PictureItemModel>();
        private ObservableCollection<DirectoriesModel> DirectoryList = new ObservableCollection<DirectoriesModel>();

        private PictureItemModel selectPicture = new PictureItemModel();

        #endregion

        #region MyRegion
        public ObservableCollection<DirectoriesModel> DirectoriesTree { get => DirectoryList;set { DirectoryList = value;RaisePropertiesChanged("DirectoriesTree"); } }

        internal ObservableCollection<PictureItemModel> PictureList { get => pictureList; set { pictureList = value; RaisePropertiesChanged("PictureList"); } }

        internal PictureItemModel SelectPicture { get => selectPicture; set { selectPicture = value; RaisePropertiesChanged("SelectPicture"); } }

        #endregion

        #region Command
        public DelegateCommand CreateDirectoryCommand => new DelegateCommand(CreateDirectoryEvent);


        private void CreateDirectoryEvent()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description="请选择项目文件夹";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.ShowDialog();
            currentDirectory = folderBrowserDialog.SelectedPath;
            bool isOver = false;
            InitDirectoryList(currentDirectory, ref isOver);
        }


        #endregion

        #region Func
        /// <summary>
        /// 初始化图片列表
        /// </summary>
        private void InitPictureList()
        {

        }

        /// <summary>
        /// 初始化文件夹列表
        /// </summary>
        private void InitDirectoryList(string directory, ref bool isOver)
        {
            if (isOver) return;
            int parentID = DirectoryList.ToList().Find(p => p.Directory == directory)==null?0: DirectoryList.ToList().Find(p => p.Directory == directory).ID;
            //foreach (var item in Directory.GetFiles(directory))
            //{
            //    if (Utility.Utility.IsPathHidden(item)) continue;
            //    DirectoryList.Add(new DirectoriesModel() { Directory = item, ID = DirectoryList.Count + 1, ParentID = parentID, Name = Path.GetFileName(item) });
            //    if (DirectoryList.Count == 1000)
            //    {
            //        isOver = MessageBox.Show("所选择项目文件已超过1000条，是否继续?", "", MessageBoxButtons.YesNo)==DialogResult.No;
            //    }
            //    if (isOver) break;
            //}
            foreach (var item in Directory.GetDirectories(directory))
            {
                if (Utility.Utility.IsPathHidden(item)) continue;
                DirectoryList.Add(new DirectoriesModel() { Directory = item, ID = DirectoryList.Count + 1, ParentID = parentID, Name = Path.GetFileName(item) });
                if (DirectoryList.Count == 1000)
                {
                    isOver = MessageBox.Show("所选择项目文件已超过1000条，是否继续?", "", MessageBoxButtons.YesNo).Equals(DialogResult.No);
                }
                if (isOver) break;
                InitDirectoryList(item, ref isOver);
            }
        }


        #endregion

    }
}
