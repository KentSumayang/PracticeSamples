using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class Movie
    {
        public string _title;
        public string _director;
        private string _rating;

        public Movie(string title, string director, string rating)
        {
            _title = title;
            _director = director;
            Rating = rating;
        }
        public string Rating
        {
            get { return _rating; }
            set
            {
                if (value == "G" || value == "PG" || value == "PG-13" || value == "R" || value == "NR")
                {
                    _rating = value;
                }
                else
                {
                    _rating = "NR";
                }
            }
        }
    }
}