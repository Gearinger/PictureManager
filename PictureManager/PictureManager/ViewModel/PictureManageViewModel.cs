using DevExpress.Mvvm;
using PictureManager.Models;
using System;
using System.Collections.Generic;
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
        private List<PictureItemModel> PictureList = new List<PictureItemModel>();
        private List<DirectoriesModel> DirectoryList = new List<DirectoriesModel>();

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
        private void InitDirectoryList()
        {

        }

        #endregion

    }
}
