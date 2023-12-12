using GEditor.ViewModels;

namespace GEditor.Views
{
    /// <summary>
    /// CreateProjectView.xaml 的交互逻辑
    /// </summary>
    public partial class CreateProjectView
    {
        public CreateProjectView()
        {
            InitializeComponent();

            var viewModel = new CreateProjectViewModel();
            viewModel.CloseRequested += ViewModel_CloseRequested;
            DataContext = viewModel;
        }

        private void ViewModel_CloseRequested()
        {
            Close();
        }
    }
}
