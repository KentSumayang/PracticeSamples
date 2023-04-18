using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class ItalianChef : Chef
    {
        public void MakePasta()
        {
            Console.WriteLine("The chef makes pasta.");
        }
        public override void MakeSpecialDish()
        {
            Console.WriteLine("The ched makes chicken parm.");
        }
    }
}
