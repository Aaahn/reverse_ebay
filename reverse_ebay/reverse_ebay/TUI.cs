using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    class TUI : IOberflaeche
    {
        private IFachkonzept fachkonzept;
        private int runde, maxAnzahl;
        private List<Artikel> alleArtikel;

        public TUI(IFachkonzept _fachkonzept)
        {
            this.fachkonzept = _fachkonzept;
            this.alleArtikel = new List<Artikel>();
            this.runde = 0;
            this.maxAnzahl = 10;
        }

        public void start()
        {
            hauptmenue();
        }

        private void hauptmenue()
        {
            int zaehler = 0;

            Console.Clear();

            Console.WriteLine("Willkommen bei reverse-ebay");
            Console.WriteLine();
            Console.WriteLine("Aktuelle Wunschliste");
            List<Artikel> aktuelleArtikel = holeAnzahlAnArtikeln();
            foreach (Artikel artikel in aktuelleArtikel)
            {
                Console.WriteLine("({0}) {1}", zaehler, artikel);
                zaehler++;
            }
            Console.WriteLine();
            Console.WriteLine("    - Zahl eingeben um Details zu sehen");
            Console.WriteLine("[L] - Anmelden");
            Console.WriteLine("[R] - Registrieren");
            Console.WriteLine("[W] - Wunsch eintragen");
            if (aktuelleArtikel.Count == maxAnzahl)
            {
                Console.WriteLine("[N] - Die nächsten 10 Wünsche");
            }
            if (runde != 0)
            {
                Console.WriteLine("[V] - Die vorherigen 10 Wünsche");
            }
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
                    int auswahl = Convert.ToInt32(eingabe);
                    if (auswahl < aktuelleArtikel.Count)
                    {
                        //artikelDetails(aktuelleArtikel[auswahl];
                    }
                    break;
                case "L":
                case "l":
                    //anmelden
                    break;
                case "R":
                case "r":
                    //registrieren
                    break;
                case "W":
                case "w":
                    //Wunsch eintragen
                    break;
                case "N":
                case "n":
                    //die nächsten 10 Wünsche
                    if (aktuelleArtikel.Count == maxAnzahl)
                    {
                        runde++;
                    }
                    break;
                case "V":
                case "v":
                    //die vorherigen 10 Wünsche
                    if (runde != 0)
                    {
                        runde--;
                    }
                    break;
                case "Q":
                case "q":
                    //Beenden
                    Environment.Exit(0);
                    break;
            }
            hauptmenue();
        }

        private List<Artikel> holeAnzahlAnArtikeln()
        {
            List<Artikel> artikel = new List<Artikel>();
            int versatz = runde * maxAnzahl;

            if (alleArtikel.Count == 0)
            {
                alleArtikel = fachkonzept.sucheArtikel("","","");
            }
            artikel.Clear();
            if ((alleArtikel.Count >= versatz) && (alleArtikel.Count < versatz + maxAnzahl))
            {
                for (int i = versatz; i < alleArtikel.Count; i++)
                {
                    artikel.Add(alleArtikel[i]);
                }
            }
            else if (alleArtikel.Count >= versatz + maxAnzahl)
            {
                for (int i = versatz; i < versatz + maxAnzahl; i++)
                {
                    artikel.Add(alleArtikel[i]);
                }
            }
            return artikel;
        }
    }
}