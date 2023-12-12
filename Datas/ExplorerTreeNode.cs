using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEditor.Datas
{
    internal class ExplorerTreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<ExplorerTreeNode> Children { get; set; }

        public ExplorerTreeNode() { 
            Children = new ObservableCollection<ExplorerTreeNode>();
        }
    }
}
