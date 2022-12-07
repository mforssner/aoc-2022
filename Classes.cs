using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public class Day7
    {
        public List<Folder>? List { get; set; }
        public int TotalSize { get; set; }
        public Day7()
        {
            List = new List<Folder>();
        }

        public class Folder
        {
            public Folder(Day7 day, string name, Folder? parent = null)
            {
                Name = name;
                Parent = parent;
                day.List.Add(this);
            }

            public string Name { get; set; }
            public int TotalSize
            {
                get
                {
                    return totalSize;
                }
                set
                {
                    if (TooBig) return;
                    else if (totalSize + value > 100000)
                    {
                        TooBig = true;
                        totalSize = 0;
                    }
                    else
                    {
                        totalSize += value;
                        if (Parent != null && !Parent.TooBig) Parent.TotalSize += value;
                    }
                }
            }
            private int totalSize = 0;
            public bool TooBig = false;
            public Folder[]? SubFolders { get; set; } = null;
            public Folder? Parent { get; set; }

            public void AddMoreFolders(Day7 day, List<string>? list)
            {
                if (list == null) return;
                var array = new Folder[0];
                foreach (string name in list)
                {
                    var folder = new Folder(day, name, this);
                    array = array.Append(folder).ToArray();
                }
                SubFolders = array;
            }
        }
    }
}
