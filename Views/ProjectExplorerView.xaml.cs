using GEditor.ViewModels;
using System.Windows.Controls;

namespace GEditor.Views
{
    /// <summary>
    /// ProjectExplorerView.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectExplorerView : UserControl
    {
        public ProjectExplorerView()
        {
            InitializeComponent();

            DataContext = new ProjectExplorerViewModel();
        }
    }
}
