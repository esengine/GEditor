using AvalonDock;
using GEditor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEditor.Datas
{
    internal static class GProjectData
    {
        public static Project OpenProject { get; set; }
        public static EventBus EventBus { get; set; } = new EventBus();
        public static DockingManager DockingManager { get; set; }
    }
}
