using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    #region Day 7
    public class Day7
    {
        public List<Folder> List { get; set; }
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
            public double Size
            {
                get
                {
                    return size;
                }
                set
                {
                    if (size + value > 100000)
                    {
                        TooBig = true;
                        if (Parent != null)
                            Parent.TooBig = true;
                    }
                    size = value;
                    if (Parent != null) Parent.Size += value;
                }
            }
            private double size { get; set; } = 0;
            public bool TooBig = false;
            public Folder[]? SubFolders { get; set; } = null;
            public Folder? Parent { get; set; }

            public void AddMoreFolders(Day7 day, List<string>? list)
            {
                if (list == null) return;
                var array = Array.Empty<Folder>();
                foreach (string name in list)
                {
                    var folder = new Folder(day, name, this);
                    array = array.Append(folder).ToArray();
                }
                SubFolders = array;
            }
        }
    }
    #endregion
}
