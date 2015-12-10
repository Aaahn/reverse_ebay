using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    class Program
    {
        static void Main(string[] args)
        {
            var programm = new GUI(new Fachkonzept1());

            programm.start();
        }
    }
}
