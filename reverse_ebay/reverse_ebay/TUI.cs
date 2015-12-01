using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    class TUI:IOberflaeche  
    {
        private IFachkonzept fachkonzept;
        private int round;

        public TUI(IFachkonzept _fachkonzept)
        {
            this.fachkonzept = _fachkonzept;
        }

        public void start()
        {
            mainmenue();
        }

        private void mainmenue()
        {
            Console.WriteLine("Willkommen bei reverse-ebay");
            Console.WriteLine();
            Console.WriteLine("Aktuelle Wunschliste");
            // fachkonzept.getItemNames(10)
            Console.WriteLine();
            Console.WriteLine("    - Zahl eingeben um Details zu sehen");
            Console.WriteLine("[L] - Anmelden");
            Console.WriteLine("[R] - Registrieren");
            Console.WriteLine("[W] - Wunsch eintragen");
            Console.WriteLine("[N] - Die nächsten 10 Wünsche");
            Console.WriteLine("[Q] - Beenden");
            Console.WriteLine("");
            Console.Write("Ihre Auswahl: ");
            string eingabe = Console.ReadLine();

            switch (eingabe)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    //GetItemByID(Convert.ToInt32(eingabe),round);
                    break;
                case "L":
                    //GetItemByID(0,round);
                    break;
            }
            
            Console.WriteLine("\n"+eingabe);
            Console.Read();

        }
    }
}
