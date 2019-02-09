using ModDownloads.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModDownloads.Server
{
    public static class DownloadsHelper
    {
        public static Dictionary<DateTime, int> GetDownloadsIncrease(List<Download> downloads)
        {
            Dictionary<DateTime, int> dict = new Dictionary<DateTime, int>();

            for (int key = 0; key < downloads.Count; ++key)
            {
                if (key != 0)
                {
                    dict.Add(downloads[key].Timestamp, downloads[key].Downloads - downloads[key - 1].Downloads);
                }
            }
            return dict;
        }
    }
}
