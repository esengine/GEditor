using CommunityToolkit.Mvvm.Input;
using GEditor.Datas;
using GEditor.Events;
using GEditor.Helpers;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace GEditor.ViewModels
{
    internal class CreateProjectViewModel : ViewModelBase
    {
        public string ProjectName { get; set; }

        private string _selectProjectPath;
        public string SelectProjectPath { 
            get { return _selectProjectPath; } 
            set { 
                _selectProjectPath = value; 
                RaisePropertyChanged(nameof(SelectProjectPath)); 
            } 
        }
        public ICommand CreateProjectCommand { get; set; }
        public ICommand BrowserProjectCommand { get; set; }

        public event Action CloseRequested;

        public CreateProjectViewModel() {
            CreateProjectCommand = new RelayCommand(CreateProjectExecute);
            BrowserProjectCommand = new RelayCommand(BrowserProjectExecute);
        }

        private void BrowserProjectExecute() 
        {
            var folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = true;
            var dialog = folder.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                SelectProjectPath = folder.SelectedPath;
            }
        }

        private void CreateProjectExecute() 
        {
            var project = new Project();
            project.Name = ProjectName;
            project.Path = SelectProjectPath;
            try {
                SerializationHelper.SerializeObject(project, Path.Combine(SelectProjectPath, ".gproject"));
                GProjectData.OpenProject = project;
                GProjectData.EventBus.Publish(new OpenProjectEvent());
                CloseRequested?.Invoke();
                MessageBox.Show("项目创建完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) { MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }
    }
}
