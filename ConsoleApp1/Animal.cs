﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal abstract class Animal
    {
        public abstract void makeSound();
        public void sleep() 
        {
            Console.WriteLine("zzzzZZzz");
        }
    }
}
