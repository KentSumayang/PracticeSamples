using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Song
    {
        public string _title;
        public string _artist;
        public int _duration;
        public static int songCount = 0;

        public Song (string title, string artist, int duration)
        {
            _title = title;
            _artist = artist;
            _duration = duration;
            songCount++;
        }
        public int getSongCount()
        {
            return songCount;
        }
    }
}
