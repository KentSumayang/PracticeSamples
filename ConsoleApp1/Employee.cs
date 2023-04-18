using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Employee
    {
        string firstName { get; set; } 
        string middleName { get; set; }
        string lastName { get; set; }
        double salary { get; set; }
        string position { get; set; }
        string fullname { get; set; }

        public Employee(string firstName, string middleName, string lastName, double salary, string position)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.salary = salary;
            this.position = position;
            
        }
        public void introduceSelf()
        {
            fullname = firstName + " " + middleName + " " + lastName;

            Console.WriteLine("Hello, my name is " + fullname + ". I am an " + position + ". My salary is " + salary +".");
        }
    }
}
