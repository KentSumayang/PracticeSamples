using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class Song
    {
        public string _title;
        public string _artist;
        public string _duration;
        public static int songCount = 0;
        public Song(string title, string artist, string duration)
        {
            _title = title;
            _artist = artist;
            _duration = duration;
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }
        public string Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
    }
    
}