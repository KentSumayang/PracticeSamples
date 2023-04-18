using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4
{ 
    public class Person
    {
        public string _firstName;
        public string _middleName;
        public string _lastName;

        public Person(string FirstName, string MiddleName, string LastName)
        {
            _firstName = FirstName;
            _middleName = MiddleName;
            _lastName = LastName;
        }
            public string FirstName
        {
            get 
            { 
                return _firstName; 
            }
            set
            {
                _firstName = value;
            }
        }
            public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set 
            { 
                _middleName = value;
            }
        }
            public string LastName
        {
            get 
            { 
                return _lastName; 
            }
            set
            {
                _lastName = value;
            }
        }

    
        
        public string Eat()
        {
            return " is eating.";
        }
        public string Sleep()
        {
            return " is sleeping.";
        }
        
    }
}