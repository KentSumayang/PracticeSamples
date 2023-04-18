using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Song likey = new Song("Likey","TWICE",200);
            //Console.WriteLine(Song.songCount);
            //Song typa = new Song("Typa Girl","BLACKPINK",300);
            //Console.WriteLine(Song.songCount);

            //UsefulTools.SayHi("Kent");

            //Chef chef = new Chef();
            //chef.MakeSpecialDish();

            //ItalianChef italianChef = new ItalianChef();
            //italianChef.MakeSpecialDish();
            //italianChef.MakePasta();

            //User user = new User(10,"Kent James",20);
            //Console.WriteLine(user.Name);

            //Animal dog = new Dog();

            //dog.makeSound();
            //dog.sleep();

            //IDog dog = new IDog();
            //dog.makeSound();
            //dog.walk();
            //ICat cat = new ICat();
            //cat.makeSound();
            //cat.walk();
            //IBird bird = new IBird();
            //bird.makeSound();
            //bird.walk();
            //bird.fly();

            //Employee[] employees = new Employee[5];
            //employees[0] = new Employee("Kent", "James", "Sumayang", 2000, "Intern");
            //employees[1] = new Employee("Angela", "Mae", "Endrina", 2000, "Intern");

            //foreach (Employee employee in employees)
            //{
            //    employee.introduceSelf();
            //}
            //Animal[] animals = new Animal[5];
            //animals[0] = new Pig();
            //animals[1] = new Dog();
            //animals[2] = new Dog();
            //animals[3] = new Dog();
            //animals[4] = new Pig();
            //for (int i = 0; i < animals.Length; i++)
            //{
            //    animals[i].makeSound();
            //}
            string[] names = { };

            string[,] users = {
                {   "Kent",   "kent123"    },
                {   "Angela", "angge123"   },
                {   "Joann",  "bokya123"   }
            };

            string[,] user = new string[6,2];
            
            Console.ReadLine();
        }
        //abstract class Animal
        //{
        //    public abstract void makeSound();
        //}
        //class Dog : Animal
        //{
        //    public override void makeSound()
        //    {
        //        Console.WriteLine("Arf Arf");
        //    }
        //}
        //class Pig : Animal
        //{
        //    public override void makeSound()
        //    {
        //        Console.WriteLine("Oink Oink");
        //    }
        //}
    }
}
