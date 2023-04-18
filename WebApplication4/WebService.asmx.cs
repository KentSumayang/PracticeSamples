using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;

namespace WebApplication4
{
    /// <summary>
    /// Summary description for WebService2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
            
        }
        

        [WebMethod]
        public string Validation (string age)
        {
            try
            {
                int convertedAge = Convert.ToInt32(age);

                if (convertedAge == 10)
                {
                    return "Not valid to vote. Not a Senior Citizen";
                }
                else if (convertedAge == 20)
                {
                    return "Valid to vote. Not a Senior Citizen";
                }
                else if (convertedAge == 65)
                {
                    return "Valid to vote. A Senior Citizen";
                }
                else
                {
                    return "Invalid Value";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                

            }


        }
        [WebMethod]
        public string Person(string fname,string mname, string lname)
        {
            try
            {
                Person person = new Person("", "", "");

                string fullname = fname + mname + lname;

                if (fname != "" || mname != "" || lname != "")
                {
                    return fullname;
                }
                else
                {
                    return "Error";
                }

                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        [WebMethod]
        public string Eat (string name)
        {
            try
            {
                Person person = new Person("", "", "");
                string action = name + person.Eat();

                if (name != "")
                {
                    return action;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        [WebMethod]
        public string Sleep (string name)
        {
            try
            {
                Person person = new Person("","","");
                string action = name + person.Sleep();

                if (name != "")
                {
                    return action;
                }
                else
                {
                    return "Error";
                }
            }
                
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [WebMethod]
        public int Add(int x , int y)
        {
            try
            {
                int result = Calculator.Add(x, y);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        [WebMethod]
        public bool  Honors (string name, string major, int gpa )
        {
            Student student1 = new Student(name,major,gpa);

            return student1.HasHonors();
        }
        [WebMethod]
        public string Movie(string title, string director, string rating)
        {
            Movie mov1 = new Movie(title,director,rating);

            return title + " " + director + " " + mov1.Rating;
        }
        [WebMethod]
        public string Song (string title, string artist, string duration)
        {
            Song likey = new Song(title,artist,duration);
            Song maji = new Song(title, artist, duration);

            return likey.Title;
        }
    }
}
