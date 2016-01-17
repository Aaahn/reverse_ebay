using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    class Start
    {
        static void Main(string[] args)
        {
            var programm = new GUI(new Fachkonzept1(new XMLDatenzugriff()));

            programm.start();
        }
    }
}
