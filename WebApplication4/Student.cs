using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class Student
    {
        public string _name;
        public string _major;
        public int _gpa;
        public Student(string name, string major, int gpa)
        {
            _name = name;
            _major = major;
            _gpa = gpa;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Major
        {
            get { return _major; }
            set { _major = value; }
        }
        public int Gpa
        {
            get { return _gpa; }
            set { _gpa = value; }
        }
        
        public bool HasHonors()
        {
            if (Gpa >= 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}