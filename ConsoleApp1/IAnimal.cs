using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface IAnimal
    {
        void makeSound();

    }
    interface ILandAnimal
    {
        void walk();
    }
    interface IAirAnimal : ILandAnimal
    {
        void fly();
    }
    class IDog : IAnimal, ILandAnimal
    {
        public void makeSound()
        {
            Console.WriteLine("Arf");
        }
        public void walk()
        {
            Console.WriteLine(" Dog is walking.");
        }
    }
    class ICat : IAnimal, ILandAnimal
    {
        public void makeSound()
        {
            Console.WriteLine("Meow");
        }
        public void walk()
        {
            Console.WriteLine("Cat is walking.");
        }
    }
    class IBird : IAnimal, IAirAnimal
    {
        public void makeSound()
        {
            Console.WriteLine("Twit");
        }
        public void walk()
        {
            Console.WriteLine("Bird is walking.");
        }
        public void fly()
        {
            Console.WriteLine("Bird is flying.");
        }
    }

    
}
