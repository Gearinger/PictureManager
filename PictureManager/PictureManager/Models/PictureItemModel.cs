using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.Models
{
    class PictureItemModel : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private string fileName;
        private string picturePath;
        private string guid = System.Guid.NewGuid().ToString();
        private DateTime createDate;
        private string author;
        private String other;
        private double pixelWidth;
        private double pixelHeight;
        private int width = 200;
        private int height;
        private int x;
        private int y;
        private string margin;

        /// <summary>
        /// 文件名（不带后缀）
        /// </summary>
        public string Name { get => name; set { name = value; NotifyPropertyChanged("Name"); } }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string Description { get => description; set { description = value; NotifyPropertyChanged("Description"); } }
        /// <summary>
        /// 文件名（带后缀）
        /// </summary>
        public string FileName { get => fileName; set { fileName = value; NotifyPropertyChanged("FileName"); } }
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string PicturePath { get => picturePath; set { picturePath = value; NotifyPropertyChanged("PicturePath"); } }
        /// <summary>
        /// 
        /// </summary>
        public string Guid { get => guid; set { guid = value; NotifyPropertyChanged("Guid"); } }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get => createDate; set { createDate = value; NotifyPropertyChanged("CreateDate"); } }
        /// <summary>
        /// 
        /// </summary>
        public string Author { get => author; set { author = value; NotifyPropertyChanged("Author"); } }
        /// <summary>
        /// 
        /// </summary>
        public string Other { get => other; set { other = value; NotifyPropertyChanged("Other"); } }

        public double PixelWidth { get => pixelWidth; set { pixelWidth = value; NotifyPropertyChanged(); } }
        public double PixelHeight { get => pixelHeight; set { pixelHeight = value; NotifyPropertyChanged(); } }

        public int Width { get => width; set { width = value; NotifyPropertyChanged(); } }
        public int Height { get => height; set { height = value; NotifyPropertyChanged(); } }
        public int X { get => x; set { x = value; NotifyPropertyChanged(); } }
        public int Y { get => y; set { y = value; NotifyPropertyChanged(); } }
        public string Margin { get => margin; set { margin = value; NotifyPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String info="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
