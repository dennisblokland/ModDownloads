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
                    int downloadCount = downloads[key].Downloads - downloads[key - 1].Downloads;
                    TimeSpan time = downloads[key].Timestamp - downloads[key - 1].Timestamp;
                    if (Math.Round(time.TotalHours,1) != 2.0)
                    {
                        downloadCount = Convert.ToInt32(downloadCount / (Math.Round(time.TotalHours, 1) / 2));
                    }
                    dict.Add(downloads[key].Timestamp, downloadCount);
                }
            }
            return dict;
        }
    }
}
