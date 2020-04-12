using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.Models
{
    class DirectoryModel: INotifyPropertyChanged
    {
        //构造函数
        public DirectoryModel()
        {
            DirectoryList = new ObservableCollection<DirectoryModel>();
        }

        private Guid guid = Guid.NewGuid();
        private string directory;
        private string name;
        private ObservableCollection<DirectoryModel> directoryList;

        public Guid GUID { get => guid; set { guid = value; NotifyPropertyChanged(); } }
        public string Directory { get => directory; set { directory = value; NotifyPropertyChanged(); } }
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        public ObservableCollection<DirectoryModel> DirectoryList
        { get => directoryList;set { directoryList = value;NotifyPropertyChanged(); } }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
