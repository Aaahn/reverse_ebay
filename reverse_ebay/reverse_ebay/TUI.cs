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
                Console.WriteLine("({0}) {1}", zaehler, artikel.name);
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
                        ArtikelMenue(aktuelleArtikel[auswahl]);
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
                    //die nächsten Wünsche
                    if (aktuelleArtikel.Count == maxAnzahl)
                    {
                        runde++;
                    }
                    break;
                case "V":
                case "v":
                    //die vorherigen Wünsche
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
                alleArtikel = fachkonzept.gibArtikelListe("");
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

        public void ArtikelMenue(Artikel artikel)
        {
            string eingabe;
            Benutzer suchender = fachkonzept.gibBenutzer(artikel.anbieter_id);
            Benutzer bieter = fachkonzept.gibBenutzer(artikel.bieter_id);
            Console.Clear();
            Console.WriteLine("Artikelmenü");
            Console.WriteLine("-----------");
            Console.WriteLine();
            Console.WriteLine("Name:             {0}", artikel.name);
            Console.WriteLine("Kurzbeschreibung: {0}", artikel.kurzbeschr);
            Console.WriteLine("Langbeschreibung: {0}", artikel.langbeschr);
            Console.WriteLine("Höchstgebot:      {0} EUR", artikel.hoechstgebot.ToString("0,00"));
            Console.WriteLine("Ablaufdatum:      {0}", artikel.ablaufdatum);
            Console.WriteLine("Suchender:        {0}", suchender.name);
            Console.WriteLine("Aktueller Bieter: {0}", bieter.name);
            Console.WriteLine();
            if (fachkonzept.eingeloggterUser() != artikel.anbieter_id)
            {
                Console.WriteLine("[B] - Niedrigeres Gebot abgeben");
            }
            if (fachkonzept.eingeloggterUser() == artikel.anbieter_id)
            {
                Console.WriteLine("[A] - Artikel ändern");
                Console.WriteLine("[E] - Auktion beenden");
            }
            Console.WriteLine("[Z] - Zurück zum Hauptmenü");
            Console.WriteLine();
            Console.Write("Ihre Auswahl: ");
            eingabe = Console.ReadLine();
            switch (eingabe)
            {
                case "B":
                case "b":
                    // Bieten
                    break;
                case "A":
                case "a":
                    // Ändern
                    break;
                case "E":
                case "e":
                    //Auktion beenden
                    break;
                case "Z":
                case "z":
                    hauptmenue();
                    break;
                default:
                    ArtikelMenue(artikel);
                    break;
            }
        }
        private void BietenMenue(Artikel artikel)
        {
            string eingabe;
            double gebotNeu = 0f;
            Console.Clear();
            Console.WriteLine("Bieten");
            Console.WriteLine("-----------");
            Console.WriteLine();
            Console.WriteLine("Artikel:          {0}", artikel.name);
            Console.WriteLine("Kurzbeschreibung: {0}", artikel.kurzbeschr);
            Console.WriteLine("Aktuelles Gebot:  {0} EUR", artikel.hoechstgebot.ToString("0,00"));
            Console.WriteLine();
            Console.Write("Ihr Gebot: ");
            eingabe = Console.ReadLine();
            try
            {
                gebotNeu = Convert.ToDouble(eingabe);
                if (!fachkonzept.aufArtikelBieten(artikel, gebotNeu))
                {
                    throw new Exception();
                }
                ArtikelMenue(artikel);
            } catch (Exception e)
            {
                Console.Write("Das Gebot ist ungültig. Bitte versuchen Sie es erneut!");
                Console.Read();
                BietenMenue(artikel);
            }

        }
    }
}