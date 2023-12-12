using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.Input;
using GEditor.Datas;
using GEditor.Events;
using GEditor.Helpers;
using GEditor.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace GEditor.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string _title;
        public string Title { get { return _title; }
            set { 
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }
        public ICommand CreateProjectCommand { get; set; }
        public ICommand OpenProjectCommand { get; set; }
        public ICommand LoadLayoutCommand { get; set; }
        public ICommand SaveLayoutCommand { get; set; }

        public MainViewModel() 
        {
            Title = "GEditor";

            CreateProjectCommand = new RelayCommand(CreateProjectExecute);
            OpenProjectCommand = new RelayCommand(OpenProjectExecute);
            LoadLayoutCommand = new RelayCommand(OnLoadLayout, CanLoadLayout);
            SaveLayoutCommand = new RelayCommand(OnSaveLayout);

            GProjectData.EventBus.Subscribe<OpenProjectEvent>(OnOpenProject);
        }

        private bool CanLoadLayout()
        {
            return File.Exists(@".\AvalonDock.Layout.config");
        }

        private void OnLoadLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(GProjectData.DockingManager);

            // Here I've implemented the LayoutSerializationCallback just to show
            //  a way to feed layout desarialization with content loaded at runtime
            // Actually I could in this case let AvalonDock to attach the contents
            // from current layout using the content ids
            // LayoutSerializationCallback should anyway be handled to attach contents
            // not currently loaded
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                //if (e.Model.ContentId == FileStatsViewModel.ToolContentId)
                //    e.Content = Workspace.This.FileStats;
                //else if (!string.IsNullOrWhiteSpace(e.Model.ContentId) &&
                //    File.Exists(e.Model.ContentId))
                //    e.Content = Workspace.This.Open(e.Model.ContentId);
            };
            layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
        }

        private void OnSaveLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(GProjectData.DockingManager);
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
        }

        private void OnOpenProject(OpenProjectEvent _) {
            Title = $"GEditor - {GProjectData.OpenProject.Name}";
        }

        private void OpenProjectExecute() {
            var folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK) {
                var selectedPath = folder.SelectedPath;
                var gproject = Path.Combine(selectedPath, ".gproject");
                if (!File.Exists(gproject)) {
                    System.Windows.MessageBox.Show("工程不存在, 请更换目录", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    GProjectData.OpenProject = SerializationHelper.DeserializeObject<Project>(gproject);
                    GProjectData.OpenProject.Path = selectedPath;
                    GProjectData.EventBus.Publish(new OpenProjectEvent());
                    System.Windows.MessageBox.Show("项目打开完成", "提示", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                }
                catch (Exception ex) {
                    System.Windows.MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
        }

        private void CreateProjectExecute() 
        {
            var createProjectView = new CreateProjectView();
            createProjectView.ShowDialog();
        }
    }
}
