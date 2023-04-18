using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class User
    {
        private int _id;
        private string _name;
        private int _age;

        public int Id 
        {
            get 
            { 
                return _id; 
            } 
            set 
            {
                if (value >= 1000) 
                _id = value;
                else
                {
                    Console.WriteLine("Error. ID must be greater than 1000");
                }
            } 
        }
        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
            set 
            { 
                _name = value; 
            } 
        }
        public int Age 
        { 
            get 
            {
                return _age; 
            } 
            set 
            {
                _age = value; 
            } 
        }

        public User(int id, string name, int age) 
        {
            Id = id;
            Name = name;
            Age = age;
        }

         
    }


}
