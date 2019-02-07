using System;
using System.Collections.Generic;
using System.Text;

namespace ModDownloads.Shared.Entities
{
    public class Mod
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public virtual ICollection<Download> Downloads { get; set; }
    }
}
