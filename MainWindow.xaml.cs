using AvalonDock.Themes;
using GEditor.Datas;
using GEditor.ViewModels;
using System.IO;
using System.Windows;

namespace GEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            GProjectData.DockingManager = dockManager;
            dockManager.Theme = new Vs2013DarkTheme();

            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            Unloaded += new RoutedEventHandler(MainWindow_Unloaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.LayoutSerializationCallback += (s, args) =>
            {
                args.Content = args.Content;
            };

            if (File.Exists(@".\AvalonDock.config"))
                serializer.Deserialize(@".\AvalonDock.config");
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            var serializer = new AvalonDock.Layout.Serialization.XmlLayoutSerializer(dockManager);
            serializer.Serialize(@".\AvalonDock.config");
        }
    }
}
