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
        private string guid;
        private DateTime createDare;
        private string author;
        private String other;

        public string Name { get => name; set { name = value; RaisePropertiesChanged("Name"); } }
        public string Description { get => description; set { description = value; RaisePropertiesChanged("Description"); } }
        public string FileName { get => fileName; set { fileName = value; RaisePropertiesChanged("FileName"); } }
        public string PicturePath { get => picturePath; set { picturePath = value; RaisePropertiesChanged("PicturePath"); } }
        public string Guid { get => guid; set { guid = value; RaisePropertiesChanged("Guid"); } }
        public DateTime CreateDare { get => createDare; set { createDare = value; RaisePropertiesChanged("CreateDare"); } }
        public string Author { get => author; set { author = value; RaisePropertiesChanged("Author"); } }
        public string Other { get => other; set { other = value; RaisePropertiesChanged("Other"); } }
    }
}
