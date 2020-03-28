using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.Models
{
    class PictureItemModel: BindableBase
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

        /// <summary>
        /// 文件名（不带后缀）
        /// </summary>
        public string Name { get => name; set { name = value; RaisePropertiesChanged("Name"); } }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string Description { get => description; set { description = value; RaisePropertiesChanged("Description"); } }
        /// <summary>
        /// 文件名（带后缀）
        /// </summary>
        public string FileName { get => fileName; set { fileName = value; RaisePropertiesChanged("FileName"); } }
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string PicturePath { get => picturePath; set { picturePath = value; RaisePropertiesChanged("PicturePath"); } }
        /// <summary>
        /// 
        /// </summary>
        public string Guid { get => guid; set { guid = value; RaisePropertiesChanged("Guid"); } }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get => createDate; set { createDate = value; RaisePropertiesChanged("CreateDate"); } }
        /// <summary>
        /// 
        /// </summary>
        public string Author { get => author; set { author = value; RaisePropertiesChanged("Author"); } }
        /// <summary>
        /// 
        /// </summary>
        public string Other { get => other; set { other = value; RaisePropertiesChanged("Other"); } }

        public double PixelWidth { get => pixelWidth; set { pixelWidth = value; RaisePropertiesChanged("PixelWidth"); } }
        public double PixelHeight { get => pixelHeight; set { pixelHeight = value; RaisePropertiesChanged("PixelHeight"); } }

    }
}
