using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace GEditor.Datas
{
    internal class ExplorerTreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<ExplorerTreeNode> Children { get; set; }

        public ICommand RenameCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ExplorerTreeNode() 
        { 
            Children = new ObservableCollection<ExplorerTreeNode>();

            RenameCommand = new RelayCommand(Rename);
            DeleteCommand = new RelayCommand(Delete);
        }


        private void Rename()
        {
        }

        private void Delete()
        {
        }
    }
}
