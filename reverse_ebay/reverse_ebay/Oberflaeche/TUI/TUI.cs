using System;
using System.Collections.Generic;

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
            if (fachkonzept.gibAktBenutzer() == null)
            {
                Console.WriteLine("[L] - Anmelden");
                Console.WriteLine("[R] - Registrieren");
            } else
            {
                Console.WriteLine("[A] - Abmelden");
                Console.WriteLine("[M] - meine Seite");
            }
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
                    if (fachkonzept.gibAktBenutzer() == null)
                    {
                        LoginMenue();
                    }
                    break;
                case "R":
                case "r":
                    //registrieren
                    if (fachkonzept.gibAktBenutzer() == null)
                    {
                        RegistrierenMenue();
                    }
                    break;
                case "A":
                case "a":
                    //Abmelden
                    if (!fachkonzept.ausloggen())
                    {
                        Console.WriteLine("Abmelden nicht erfolgreich, bitte versuchen Sie es erneut.");
                        Console.Read();
                    }
                    break;
                case "M":
                case "m":
                    // meine Seite
                    if (fachkonzept.gibAktBenutzer() == null)
                    {
                        UserMenue(fachkonzept.gibAktBenutzer());
                    }
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
            int versatz = runde * maxAnzahl, anzahl;
            try {anzahl = alleArtikel.Count; } // TODO
            catch {anzahl = 0; }
            if (anzahl == 0)
            {
                alleArtikel = fachkonzept.gibArtikelListe("");
            }
            artikel.Clear();
            if ((anzahl >= versatz) && (anzahl < versatz + maxAnzahl))
            {
                for (int i = versatz; i < anzahl; i++)
                {
                    artikel.Add(alleArtikel[i]);
                }
            }
            else if (anzahl >= versatz + maxAnzahl)
            {
                for (int i = versatz; i < versatz + maxAnzahl; i++)
                {
                    artikel.Add(alleArtikel[i]);
                }
            }
            return artikel;
        }

        private void ArtikelMenue(Artikel artikel)
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
            if (fachkonzept.gibAktBenutzer().id != artikel.anbieter_id)
            {
                Console.WriteLine("[B] - Niedrigeres Gebot abgeben");
            }
            if (fachkonzept.gibAktBenutzer().id == artikel.anbieter_id)
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
                    if (fachkonzept.gibAktBenutzer().id != artikel.anbieter_id)
                    {
                        BietenMenue(artikel);
                    }
                    break;
                case "A":
                case "a":
                    // Ändern
                    ArtikelAendernMenue(artikel);
                    break;
                case "E":
                case "e":
                    //Auktion beenden
                    if (!BeendeAuktion(artikel))
                    {
                        Console.WriteLine("Auktion beenden nicht erfolgreich. Bitte versuchen Sie es erneut.");
                    }
                    else
                    {
                        Console.WriteLine("Auktion erfolgreich beendet.");
                    }
                    Console.Read();
                    ArtikelMenue(artikel);
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
            } catch
            {
                Console.Write("Das Gebot ist ungültig. Bitte versuchen Sie es erneut!");
                Console.Read();
                BietenMenue(artikel);
            }

        }

        private void ArtikelAendernMenue (Artikel artikel)
        {
            string eingabe;
            Console.Clear();
            Console.WriteLine("Artikel ändern");
            Console.WriteLine("--------------");
            Console.WriteLine();
            Console.WriteLine("Name:             {0}", artikel.name);
            Console.WriteLine("Kurzbeschreibung: {0}", artikel.kurzbeschr);
            Console.WriteLine("Langbeschreibung: {0}", artikel.langbeschr);
            Console.WriteLine();
            Console.WriteLine("[N] - Name ändern");
            Console.WriteLine("[K] - Kurzbeschreibung ändern");
            Console.WriteLine("[L] - Langbeschreibung ändern");
            Console.WriteLine("[A] - zurück zum Artikelmenü");
            Console.WriteLine("[Z] - Zurück zum Hauptmenü");
            Console.WriteLine();
            Console.Write("Ihre Auswahl: ");
            eingabe = Console.ReadLine();
            switch (eingabe)
            {
                case "N":
                case "n":
                    //Name ändern
                    Console.WriteLine();
                    Console.WriteLine("Name alt: {0}", artikel.name);
                    Console.Write("Name neu: ");
                    if (!AendereArtikelName(artikel, Console.ReadLine()))
                    {
                        Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                    } else
                    {
                        Console.WriteLine("Ändern erfolgreich.");
                    }
                    Console.Read();
                    ArtikelAendernMenue(artikel);
                    break;
                case "K":
                case "k":
                    //Kurzbeschreibung ändern
                    Console.WriteLine();
                    Console.WriteLine("Kurzbeschreibung alt: {0}", artikel.kurzbeschr);
                    Console.Write("Kurzbeschreibung neu: ");
                    if (!AendereArtikelName(artikel, Console.ReadLine()))
                    {
                        Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                    }
                    else
                    {
                        Console.WriteLine("Ändern erfolgreich.");
                    }
                    Console.Read();
                    ArtikelAendernMenue(artikel);
                    break;
                case "L":
                case "l":
                    //Langebschreibung ändern
                    Console.WriteLine();
                    Console.WriteLine("Langbeschreibung alt: {0}", artikel.kurzbeschr);
                    Console.Write("Langbeschreibung neu: ");
                    if (!AendereArtikelName(artikel, Console.ReadLine()))
                    {
                        Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                    }
                    else
                    {
                        Console.WriteLine("Ändern erfolgreich.");
                    }
                    Console.Read();
                    ArtikelAendernMenue(artikel);
                    break;
                case "A":
                case "a":
                    //Artikelmenü
                    ArtikelMenue(artikel);
                    break;
                case "Z":
                case "z":
                    // Hauptmenü
                    hauptmenue();
                    break;
                default:
                    ArtikelAendernMenue(artikel);
                    break;
            }
        }
        private bool AendereArtikelName(Artikel artikel, string name)
        {
            if (!name.Equals(artikel.name))
            {
                if (fachkonzept.aendereArtikel(artikel.id, name, artikel.kurzbeschr, artikel.langbeschr,artikel.ablaufdatum))
                {
                    artikel.name = name;
                    return true;
                }
            }
            return false;
        }
        private bool AendereArtikelKurzbeschr(Artikel artikel, string kurzbeschr)
        {
            if (!kurzbeschr.Equals(artikel.kurzbeschr))
            {
                if (fachkonzept.aendereArtikel(artikel.id, artikel.name, kurzbeschr, artikel.langbeschr, artikel.ablaufdatum))
                {
                    artikel.kurzbeschr = kurzbeschr;
                    return true;
                }
            }
            return false;
        }
        private bool AendereArtikelLangbeschr(Artikel artikel, string langbeschr)
        {
            if (!langbeschr.Equals(artikel.langbeschr))
            {
                if (fachkonzept.aendereArtikel(artikel.id, artikel.name, artikel.kurzbeschr, langbeschr, artikel.ablaufdatum))
                {
                    artikel.langbeschr = langbeschr;
                    return true;
                }
            }
            return false;
        }

        private bool BeendeAuktion (Artikel artikel)
        {
            DateTime jetzt = DateTime.Now;
            if (artikel.ablaufdatum > jetzt)
            {
                if (fachkonzept.aendereArtikel(artikel.id, artikel.name, artikel.kurzbeschr, artikel.langbeschr, artikel.ablaufdatum))
                {
                    return true;
                }
            }
            return false;
        }

        private void LoginMenue()
        {
            string name,passwort;
            Console.Clear();
            Console.WriteLine("Login");
            Console.WriteLine("-----");
            Console.WriteLine();
            Console.Write("Name:     ");
            name = Console.ReadLine();
            Console.Write("Passwort: ");
            passwort = Console.ReadLine();

            if (!fachkonzept.einloggen(name, passwort))
            {
                Console.WriteLine("Login nicht erfolgreich. Bitte versuchen Sie es erneut.");
                Console.Read();
            }
            hauptmenue();
        }

        private void RegistrierenMenue()
        {
            string name, passwort;
            Console.Clear();
            Console.WriteLine("Registrieren");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.Write("Name:     ");
            name = Console.ReadLine();
            Console.Write("Passwort: ");
            passwort = Console.ReadLine();

            if (!fachkonzept.erzeugeBenutzer(name, passwort))
            {
                Console.WriteLine("Registrierung nicht erfolgreich. Bitte versuchen Sie es erneut.");
                Console.Read();
            }
            hauptmenue();
        }

        private void UserMenue(Benutzer benutzer)
        {
            string eingabe;
            int zaehler = 0, auswahl;
            List<Artikel> meineArtikel = fachkonzept.meineArtikel();
            Console.Clear();
            Console.WriteLine("Benutzermenü");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.WriteLine("Name:             {0}", benutzer.name);
            Console.WriteLine();
            Console.WriteLine("Meine Wunschliste");
            foreach (Artikel artikel in meineArtikel)
            {
                Console.WriteLine("({0}) {1}", zaehler, artikel.name);
                zaehler++;
            }

            Console.WriteLine();
            Console.WriteLine("    - Zahl eingeben um Details zu sehen");
            Console.WriteLine("[A] - Adressen verwalten");
            Console.WriteLine("[N] - Name ändern");
            Console.WriteLine("[P] - Passwort ändern");
            Console.WriteLine("[Z] - Zurück zum Hauptmenü");
            Console.WriteLine();
            Console.Write("Ihre Auswahl: ");
            eingabe = Console.ReadLine();
            try
            {
                auswahl = Convert.ToInt32(eingabe);
                if ((auswahl < meineArtikel.Count)&& (auswahl >= 0))
                {
                    ArtikelMenue(meineArtikel[auswahl]);
                }
            }
            catch
            {
                switch (eingabe)
                {
                    case "A":
                    case "a":
                        // Adressen verwalten
                        break;
                    case "N":
                    case "n":
                        // Name ändern
                        Console.WriteLine();
                        Console.WriteLine("Name alt: {0}", benutzer.name);
                        Console.Write("Name neu: ");
                        if (!AendereBenutzerName(benutzer, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case "P":
                    case "p":
                        //Passwort ändern
                        Console.WriteLine();
                        Console.WriteLine("Passwort alt: {0}", benutzer.passwort);
                        Console.Write("Passwort neu: ");
                        if (!AendereBenutzerPasswort(benutzer, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case "Z":
                    case "z":
                        hauptmenue();
                        break;
                }
            }
            UserMenue(benutzer);
        }
        private bool AendereBenutzerName(Benutzer benutzer, string name)
        {
            if (!name.Equals(benutzer.name))
            {
                if (fachkonzept.aendereBenutzer(benutzer.id, name, benutzer.passwort))
                {
                    benutzer.name = name;
                    return true;
                }
            }
            return false;
        }
        private bool AendereBenutzerPasswort(Benutzer benutzer, string passwort)
        {
            if (!passwort.Equals(benutzer.passwort))
            {
                if (fachkonzept.aendereBenutzer(benutzer.id, benutzer.name, passwort))
                {
                    benutzer.passwort = passwort;
                    return true;
                }
            }
            return false;
        }

        private void AdressMenue(Benutzer benutzer)
        {
            string eingabe;
            int zaehler = 0, auswahl;
            List<BenutzerAdresse> meineAdressen = fachkonzept.meineAdressen();
            Console.Clear();
            Console.WriteLine("Adressen");
            Console.WriteLine("--------");
            foreach (BenutzerAdresse adresse in meineAdressen)
            {
                Console.WriteLine("({0}) {1} {2}", zaehler, adresse.vname, adresse.nname);
                Console.WriteLine("    {1}", adresse.addr_zusatz);
                Console.WriteLine("    {1}", adresse.adresse.str_nr);
                Console.WriteLine("    {1} {2}", adresse.adresse.plz, adresse.adresse.ort);
                Console.WriteLine("    {1}", adresse.adresse.land);
                if (adresse.rech_addr)
                {
                    Console.WriteLine("    # Rechnungsadresse");
                }
                if (adresse.lief_addr)
                {
                    Console.WriteLine("    # Lieferadresse");
                }
                zaehler++;
            }

            Console.WriteLine();
            Console.WriteLine("    - Zahl eingeben um zu bearbeiten");
            Console.WriteLine("[M] - Zurück zum Benutzermenü");
            Console.WriteLine("[Z] - Zurück zum Hauptmenü");
            Console.WriteLine();
            Console.Write("Ihre Auswahl: ");
            eingabe = Console.ReadLine();
            try
            {
                auswahl = Convert.ToInt32(eingabe);
                if ((auswahl < meineAdressen.Count) && (auswahl >= 0))
                {
                    AdressMgtMenue(meineAdressen[auswahl]);
                }
            }
            catch
            {
                switch (eingabe)
                {
                    case "M":
                    case "m":
                        //Zurück zum BenutzerMenü
                        UserMenue(benutzer);
                        break;
                    case "Z":
                    case "z":
                        //Zurück zum HauptMenü
                        hauptmenue();
                        break;
                }
            }
            UserMenue(benutzer);
        }
        private void AdressMgtMenue (BenutzerAdresse benutzerAdresse)
        {

        }
    }
}