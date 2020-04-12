using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowTreeView();
        }

        private const string FOLDER_ICON = @"";//Icon路径
        private const string EDITABLE_ICON = @"";//Icon路径
        private const string TAG_ICON = @"";//Icon路径

        private void ShowTreeView()
        {
            List<PropertyNodeItem> itemList = new List<PropertyNodeItem>();
            PropertyNodeItem node1 = new PropertyNodeItem()
            {
                DisplayName = "Node No.1",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = FOLDER_ICON,
            };
            PropertyNodeItem node1tag1 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.1",
                Name = "This is the discription of Tag 1. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON
            };
            node1.Children.Add(node1tag1);
            PropertyNodeItem node1tag2 = new PropertyNodeItem()
            {
                DisplayName = "Tag No.2",
                Name = "This is the discription of Tag 2. This is a tag.",
                Icon = TAG_ICON,
                EditIcon = EDITABLE_ICON
            };
            node1.Children.Add(node1tag2);
            itemList.Add(node1);

            PropertyNodeItem node2 = new PropertyNodeItem()
            {
                DisplayName = "Node No.2",
                Name = "This is the discription of Node2. This is a folder.",
                Icon = FOLDER_ICON,
            };
            itemList.Add(node2);


            treeView.ItemsSource = itemList;

        }
    }


    internal class PropertyNodeItem
    {
        public string Icon { get; set; }
        public string EditIcon { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public List<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {
            Children = new List<PropertyNodeItem>();
        }
    }
}
