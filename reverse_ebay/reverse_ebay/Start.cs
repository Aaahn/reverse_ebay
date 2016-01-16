using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    class Start
    {
        static void Main(string[] args)
        {
            var programm = new TUI(new Fachkonzept2(new XMLDatenzugriff()));

            programm.start();
        }
    }
}
