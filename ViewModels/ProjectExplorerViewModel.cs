using GEditor.Datas;
using GEditor.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GEditor.ViewModels
{
    internal class ProjectExplorerViewModel : ViewModelBase
    {
        public ObservableCollection<ExplorerTreeNode> Nodes { get; set; }

        private FileSystemWatcher _fileWatcher;

        public ProjectExplorerViewModel() 
        {
            Nodes = new ObservableCollection<ExplorerTreeNode>();

            GProjectData.EventBus.Subscribe<OpenProjectEvent>(OnOpenProject);
        }

        private void OnOpenProject(OpenProjectEvent _) {
            SetupFileSystemWatcher();
        }

        private void SetupFileSystemWatcher() 
        {
            _fileWatcher = new FileSystemWatcher {
                Path = GProjectData.OpenProject.Path,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*"
            };

            _fileWatcher.Created += _fileWatcher_Created;
            _fileWatcher.Deleted += _fileWatcher_Deleted;
            _fileWatcher.Renamed += _fileWatcher_Renamed;

            _fileWatcher.EnableRaisingEvents = true;
        }

        private void _fileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => {
                var item = Nodes.FirstOrDefault(n => n.Name == e.OldName);
                if (item != null)
                {
                    item.Name = e.Name;
                }
            });
        }

        private void _fileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var item = Nodes.FirstOrDefault(n => n.Name == e.Name);
                if (item != null) { 
                    Nodes.Remove(item);
                }
            });
        }

        private void _fileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => {
                Nodes.Add(new ExplorerTreeNode { Name = e.Name });
            });
        }
    }
}
