using System;

namespace GEditor.Datas
{
    [Serializable]
    internal class Project
    {
        public string Name { get; set; }

        [NonSerialized]
        public string Path;
    }
}
