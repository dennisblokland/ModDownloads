using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModDownloads.Shared.Entities
{
    public class Download
    {
        public int ID { get; set; }
        public int Downloads { get; set; }
        public virtual Mod Mod { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
