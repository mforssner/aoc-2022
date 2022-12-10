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
            public int Size
            {
                get { return currentSize; }
                set
                {
                    if (currentSize + value > 100000)
                    {
                        TooBig = true;
                        if (Parent != null) Parent.TooBig = true;
                    }
                    currentSize += value;
                    if (Parent != null) Parent.Size = value;
                }
            }
            private int currentSize = 0;
            public bool TooBig = false;
            public Folder[]? SubFolders { get; set; } = null;
            public Folder? Parent { get; set; }

            public void AddMoreFolders(Day7 day, List<string>? list)
            {
                var array = Array.Empty<Folder>();
                foreach (string name in list!)
                    array = array.Append(new Folder(day, name, this)).ToArray();
               
                SubFolders = array;
            }
        }
    }
    #endregion
}
