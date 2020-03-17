﻿using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.Models
{
    class DirectoriesModel:BindableBase
    {
        private int id;
        private int parentId;
        private string directory;

        public int ID { get => id; set { id = value; RaisePropertiesChanged("ID"); } }
        public int ParentID { get => parentId; set { parentId = value; RaisePropertiesChanged("ParentID"); } }
        public string Directory { get => directory; set { directory = value; RaisePropertiesChanged("Directory"); } }
    }
}