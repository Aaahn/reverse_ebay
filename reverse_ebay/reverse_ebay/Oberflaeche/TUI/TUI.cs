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
            int anzahlArtikel;

            Console.Clear();

            Console.WriteLine("Willkommen bei reverse-ebay");
            Console.WriteLine();
            Console.WriteLine("Aktuelle Wunschliste");
            List<Artikel> aktuelleArtikel = holeAnzahlAnArtikeln();
            try
            {
                anzahlArtikel = aktuelleArtikel.Count;
            }
            catch
            {
                anzahlArtikel = 0;
            }
            if (anzahlArtikel != 0)
            {
                foreach (Artikel artikel in aktuelleArtikel)
                {
                    Console.WriteLine("({0}) {1}", zaehler, artikel.name);
                    zaehler++;
                }
                Console.WriteLine();
                Console.WriteLine("    - Zahl eingeben um Details zu sehen");
            }
            else
            {
                Console.WriteLine(" Keine Artikel vorhanden");
                Console.WriteLine();
            }
            if (fachkonzept.gibAktBenutzer() == null)
            {
                Console.WriteLine("[L] - Anmelden");
                Console.WriteLine("[R] - Registrieren");
            } else
            {
                Console.WriteLine("[A] - Abmelden");
                Console.WriteLine("[M] - meine Seite");
                Console.WriteLine("[W] - Wunsch eintragen");
            }
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

            try
            {
                int auswahl = Convert.ToInt32(eingabe);
                if (auswahl < anzahlArtikel)
                {
                    //artikelDetails(aktuelleArtikel[auswahl];
                    ArtikelMenue(aktuelleArtikel[auswahl]);
                }
            }
            catch
            {

                switch (eingabe)
                {
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
                        if (fachkonzept.gibAktBenutzer() != null)
                        {
                            UserMenue(fachkonzept.gibAktBenutzer());
                        }
                        break;
                    case "W":
                    case "w":
                        //Wunsch eintragen
                        ArtikelEinfuegen(fachkonzept.gibAktBenutzer());
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
            }
            hauptmenue();
        }

        private List<Artikel> holeAnzahlAnArtikeln()
        {
            List<Artikel> artikel = new List<Artikel>();
            int versatz = runde * maxAnzahl, anzahl;
            alleArtikel = fachkonzept.gibArtikelListe("");
            try { anzahl = alleArtikel.Count; } // TODO
            catch { anzahl = 0; }

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
            if (bieter != null)
            {
                Console.WriteLine("Mindestgebot:      {0} EUR", artikel.hoechstgebot.ToString("0,00"));
                Console.WriteLine("Aktueller Bieter: {0}", bieter.name);
            }
            else
            {
                if (artikel.hoechstgebot != 0)
                {
                    Console.WriteLine("Preisobergrenze:  {0}", artikel.hoechstgebot);
                }
                Console.WriteLine("Noch keine Gebote vorhanden.");
            }
            Console.WriteLine("Ablaufdatum:      {0}", artikel.ablaufdatum);
            Console.WriteLine("Suchender:        {0}", suchender.name);
            
            Console.WriteLine();
            if (fachkonzept.gibAktBenutzer() != null)
            {
                if (fachkonzept.gibAktBenutzer().id != artikel.anbieter_id)
                {
                    Console.WriteLine("[B] - Niedrigeres Gebot abgeben");
                }
                if (fachkonzept.gibAktBenutzer().id == artikel.anbieter_id)
                {
                    Console.WriteLine("[A] - Artikel ändern");
                    Console.WriteLine("[E] - Auktion beenden");
                }
                Console.WriteLine("[M] - zurück zu Meine Seite");
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
                case "M":
                case "m":
                    // Ändern
                    UserMenue(fachkonzept.gibAktBenutzer());
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

        private void ArtikelEinfuegen (Benutzer benutzer)
        {
            Artikel neuerArtikel = new Artikel();
            Console.Clear();
            Console.WriteLine("Wunsch eintragen");
            Console.WriteLine("----------------");
            Console.Write("Name:             ");
            neuerArtikel.name = Console.ReadLine();
            while (neuerArtikel.name.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie einen Namen ein.");
                Console.Write("Name:             ");
                neuerArtikel.name = Console.ReadLine();
            }
            Console.Write("Kurzbeschreibung: ");
            neuerArtikel.kurzbeschr = Console.ReadLine();
            while (neuerArtikel.kurzbeschr.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie eine Kurzbeschreibung ein.");
                Console.Write("Kurzbeschreibung: ");
                neuerArtikel.kurzbeschr = Console.ReadLine();
            }
            Console.Write("Langbeschreibung: ");
            neuerArtikel.langbeschr = Console.ReadLine();
            while (neuerArtikel.langbeschr.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie eine Langbeschreibung ein.");
                Console.Write("Langbeschreibung: ");
                neuerArtikel.langbeschr = Console.ReadLine();
            }
            Console.Write("Preisobergrenze:      ");
            neuerArtikel.hoechstgebot = ValidateDouble(Console.ReadLine());
            
            neuerArtikel.anbieter_id = fachkonzept.gibAktBenutzer().id; //TODO
            neuerArtikel.ablaufdatum = DateTime.Now.AddDays(14);

            Console.WriteLine();
            if (fachkonzept.erzeugeArtikel(neuerArtikel))
            {
                Console.WriteLine("Anlegen erfolgreich!");
            }
            else
            {
                Console.WriteLine("Anlegen nicht erfolgreich!");
            }
        }
        private double ValidateDouble (string eingabe)
        {
            double number;
            try
            {
                number = Convert.ToDouble(eingabe);
                return number;
            }
            catch
            {
                Console.WriteLine("Bitte geben Sie ein gültiges Höchstgebot im Format 0.00 an.");
                Console.Write("Preisobergrenze:      ");
                return ValidateDouble(Console.ReadLine());
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
                Artikel andererArtikel = artikel;
                andererArtikel.name = name; 
                if (fachkonzept.aendereArtikel(andererArtikel))
                {
                    artikel =  andererArtikel;
                    return true;
                }
            }
            return false;
        }
        private bool AendereArtikelKurzbeschr(Artikel artikel, string kurzbeschr)
        {
            if (!kurzbeschr.Equals(artikel.kurzbeschr))
            {
                Artikel andererArtikel = artikel;
                andererArtikel.kurzbeschr = kurzbeschr;
                if (fachkonzept.aendereArtikel(andererArtikel))
                {
                    artikel = andererArtikel;
                    return true;
                }
            }
            return false;
        }
        private bool AendereArtikelLangbeschr(Artikel artikel, string langbeschr)
        {
            if (!langbeschr.Equals(artikel.langbeschr))
            {
                Artikel andererArtikel = artikel;
                andererArtikel.langbeschr = langbeschr;
                if (fachkonzept.aendereArtikel(andererArtikel))
                {
                    artikel = andererArtikel;
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
                Artikel andererArtikel = new Artikel(artikel.id, artikel.name, artikel.kurzbeschr, artikel.langbeschr, DateTime.Now, artikel.hoechstgebot, artikel.bieter_id, artikel.anbieter_id);
                if (fachkonzept.aendereArtikel(andererArtikel))
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
            Benutzer neuerBenutzer = new Benutzer();
            Console.Clear();
            Console.WriteLine("Registrieren");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.Write("Name:     ");
            neuerBenutzer.name = Console.ReadLine();
            while (neuerBenutzer.name.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie einen Namen ein.");
                Console.Write("Name:     ");
                neuerBenutzer.name = Console.ReadLine();
            }
            Console.Write("Passwort: ");
            neuerBenutzer.passwort = Console.ReadLine();
            if (!fachkonzept.erzeugeBenutzer(neuerBenutzer))
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
            List<Artikel> meineArtikel = fachkonzept.meineArtikel(false);
            Console.Clear();
            Console.WriteLine("Benutzermenü");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.WriteLine("Name:             {0}", benutzer.name);
            Console.WriteLine();
            if (meineArtikel != null)
            {
                Console.WriteLine("Meine Wunschliste");
                foreach (Artikel artikel in meineArtikel)
                {
                    Console.WriteLine("({0}) {1}", zaehler, artikel.name);
                    zaehler++;
                }
                Console.WriteLine();
                Console.WriteLine("    - Zahl eingeben um Details zu sehen");
            }
            else
            {
                Console.WriteLine("Noch keine Wunschliste vorhanden");
                Console.WriteLine();
            }
            Console.WriteLine("[W] - Wunsch eintragen");
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
                        AdressMenue(benutzer);
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
                    case "W":
                    case "w":
                        // Wunsch eintragen
                        ArtikelEinfuegen(benutzer);
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
            if ((!name.Equals(benutzer.name)) && (!name.Equals("")))
            {
                Benutzer andererBenutzer = benutzer;
                andererBenutzer.name = name;
                if (fachkonzept.aendereBenutzer(andererBenutzer))
                {
                    benutzer = andererBenutzer;
                    return true;
                }
            }
            return false;
        }
        private bool AendereBenutzerPasswort(Benutzer benutzer, string passwort)
        {
            if (!passwort.Equals(benutzer.passwort))
            {
                Benutzer andererBenutzer = benutzer;
                andererBenutzer.passwort = passwort;
                if (fachkonzept.aendereBenutzer(andererBenutzer))
                {
                    benutzer = andererBenutzer;
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
                Console.WriteLine("    {0}", adresse.addr_zusatz);
                Console.WriteLine("    {0}", adresse.adresse.str_nr);
                Console.WriteLine("    {0} {1}", adresse.adresse.plz, adresse.adresse.ort);
                Console.WriteLine("    {0}", adresse.adresse.land);
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
            Console.WriteLine("[N] - Neue Adresse anlegen");
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
                    case "N":
                    case "n":
                        //Neue Adresse anlegen
                        AdresseEinfuegen(benutzer);
                        break;
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
            AdressMenue(benutzer);
        }
        private void AdresseEinfuegen(Benutzer benutzer)
        {
            BenutzerAdresse neueBenutzerAdresse = new BenutzerAdresse();
            neueBenutzerAdresse.adresse = new Adresse();
            neueBenutzerAdresse.benutzer_id = benutzer.id;
            Console.Clear();
            string rech, lief;
            Console.WriteLine("Adresse einfügen");
            Console.WriteLine("----------------");
            Console.WriteLine();
            Console.Write("Vorname:      ");
            neueBenutzerAdresse.vname = Console.ReadLine();
            while (neueBenutzerAdresse.vname.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie einen Vornamen ein.");
                Console.Write("Vorname:      ");
                neueBenutzerAdresse.vname = Console.ReadLine();
            }
            Console.Write("Nachname:     ");
            neueBenutzerAdresse.nname = Console.ReadLine();
            while (neueBenutzerAdresse.nname.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie einen Nachnamen ein.");
                Console.Write("Nachname:     ");
                neueBenutzerAdresse.nname = Console.ReadLine();
            }
            Console.Write("Adresszusatz: ");
            neueBenutzerAdresse.addr_zusatz = Console.ReadLine();
            Console.Write("Straße, Nr.:  ");
            neueBenutzerAdresse.adresse.str_nr = Console.ReadLine();
            while (neueBenutzerAdresse.adresse.str_nr.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie Straße und Hausnummer ein.");
                Console.Write("Straße, Nr.:  ");
                neueBenutzerAdresse.adresse.str_nr = Console.ReadLine();
            }
            Console.Write("Postleitzahl: ");
            neueBenutzerAdresse.adresse.plz = Console.ReadLine();
            while (neueBenutzerAdresse.adresse.plz.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie eine Postleitzahl ein.");
                Console.Write("Postleitzahl: ");
                neueBenutzerAdresse.adresse.plz = Console.ReadLine();

            }
            Console.Write("Ort:          ");
            neueBenutzerAdresse.adresse.ort = Console.ReadLine();
            while (neueBenutzerAdresse.adresse.ort.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie einen Ort ein.");
                Console.Write("Ort:          ");
                neueBenutzerAdresse.adresse.ort = Console.ReadLine();
            }
            Console.Write("Land:         ");
            neueBenutzerAdresse.adresse.land = Console.ReadLine();
            while (neueBenutzerAdresse.adresse.land.Equals(""))
            {
                Console.WriteLine("Bitte geben Sie ein Land ein.");
                Console.Write("Land:         ");
                neueBenutzerAdresse.adresse.land = Console.ReadLine();
            }
            Console.Write("Rechnungsadresse? [J/N]: ");
            rech = Console.ReadLine();
            while ((!rech.Equals("J")) && (!rech.Equals("N"))){
                Console.WriteLine("Bitte geben Sie gültige Zeichen (J für \"Ja\" oder N für \"Nein\") ein.");
                Console.Write("Rechnungsadresse? [J/N]: ");
                rech = Console.ReadLine();
            }
            neueBenutzerAdresse.rech_addr = (rech.Equals("J") ? true : false);

            Console.Write("Lieferadresse? [J/N]:    ");
            lief = Console.ReadLine();
            while ((!lief.Equals("J")) && (!lief.Equals("N")))
            {
                Console.WriteLine("Bitte geben Sie gültige Zeichen (J für \"Ja\" oder N für \"Nein\") ein.");
                Console.Write("Lieferadresse? [J/N]:    ");
                lief = Console.ReadLine();
            }
            neueBenutzerAdresse.lief_addr = (lief.Equals("J") ? true : false);


            if (fachkonzept.erzeugeBenutzerAdresse(neueBenutzerAdresse))
            {
                Console.WriteLine("Erstellen erfolgreich!");
                Console.Read();

            }
        }
        private void AdressMgtMenue (BenutzerAdresse benutzerAdresse)
        {
            string eingabe;
            int auswahl;
            Console.Clear();
            Console.WriteLine("Adresse ändern");
            Console.WriteLine("--------------");
            Console.WriteLine();
            Console.WriteLine("[0] Vorname:      {0}", benutzerAdresse.vname);
            Console.WriteLine("[1] Nachname:     {0}", benutzerAdresse.nname);
            Console.WriteLine("[2] Adresszusatz: {0}", benutzerAdresse.addr_zusatz);
            Console.WriteLine("[3] Straße, Nr.:  {0}", benutzerAdresse.adresse.str_nr);
            Console.WriteLine("[4] Postleitzahl: {0}", benutzerAdresse.adresse.plz);
            Console.WriteLine("[5] Ort:          {0}", benutzerAdresse.adresse.ort);
            Console.WriteLine("[6] Land:         {0}", benutzerAdresse.adresse.land);
            Console.Write("[7] Rechnungsadresse: ");
            Console.WriteLine((benutzerAdresse.rech_addr ? "Ja" : "Nein"));
            Console.Write("[8] Lieferadresse:    ");
            Console.WriteLine((benutzerAdresse.lief_addr ? "Ja" : "Nein"));
            Console.WriteLine();
            Console.WriteLine("    - Zahl eingeben um zu bearbeiten");
            Console.WriteLine("[L] - Adresse löschen");
            Console.WriteLine("[A] - Zurück zum Adressmenü");
            Console.WriteLine("[M] - Zurück zum Benutzermenü");
            Console.WriteLine("[Z] - Zurück zum Hauptmenü");
            Console.WriteLine();
            Console.Write("Ihre Auswahl: ");
            eingabe = Console.ReadLine();
            try
            {
                auswahl = Convert.ToInt32(eingabe);
                switch (auswahl)
                {
                    case 0:
                        //Vornamen ändern
                        Console.WriteLine();
                        Console.WriteLine("Vorname alt: {0}", benutzerAdresse.vname);
                        Console.Write("Vorname neu: ");
                        if (!AendereVorname(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 1:
                        //Nachnamen ändern
                        Console.WriteLine();
                        Console.WriteLine("Nachname alt: {0}", benutzerAdresse.nname);
                        Console.Write("Nachname neu: ");
                        if (!AendereNachname(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 2:
                        //Adresszusätze ändern
                        Console.WriteLine();
                        Console.WriteLine("Adresszusatz alt: {0}", benutzerAdresse.addr_zusatz);
                        Console.Write("Adresszusatz neu: ");
                        if (!AendereAdresszusatz(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 3:
                        //Str. und Nr. ändern
                        Console.WriteLine();
                        Console.WriteLine("Straße, Nr. alt: {0}", benutzerAdresse.adresse.str_nr);
                        Console.Write("Straße, Nr. neu: ");
                        if (!AendereStrNr(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 4:
                        //PLZ ändern
                        Console.WriteLine();
                        Console.WriteLine("Postleitzahl alt: {0}", benutzerAdresse.adresse.plz);
                        Console.Write("Postleitzahl neu: ");
                        if (!AenderePLZ(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 5:
                        //Ort ändern
                        Console.WriteLine();
                        Console.WriteLine("Ort alt: {0}", benutzerAdresse.adresse.ort);
                        Console.Write("Ort neu: ");
                        if (!AendereOrt(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 6:
                        //Land ändern
                        Console.WriteLine();
                        Console.WriteLine("Land alt: {0}", benutzerAdresse.adresse.land);
                        Console.Write("Land neu: ");
                        if (!AendereLand(benutzerAdresse, Console.ReadLine()))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 7:
                        string rech;
                        //Rechnungsadresse ändern
                        Console.WriteLine();
                        Console.Write("Rechnungsadresse: ");
                        Console.WriteLine((benutzerAdresse.rech_addr ? "Ja" : "Nein"));
                        Console.Write("Rechnungsadresse? [J/N]: ");
                        rech = Console.ReadLine();
                        while ((!rech.Equals("J")) && (!rech.Equals("N")))
                        {
                            Console.WriteLine("Bitte geben Sie gültige Zeichen (J für \"Ja\" oder N für \"Nein\") ein.");
                            Console.Write("Rechnungsadresse? [J/N]: ");
                            rech = Console.ReadLine();
                        }
                        if (!AendereRechnungsadresse(benutzerAdresse, (rech.Equals("J") ? true : false)))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                    case 8:
                        string lief;
                        //Lieferadresse ändern
                        Console.WriteLine();
                        Console.Write("Lieferadresse: ");
                        Console.WriteLine((benutzerAdresse.lief_addr ? "Ja" : "Nein"));
                        Console.Write("Lieferadresse? [J/N]: ");
                        lief = Console.ReadLine();
                        while ((!lief.Equals("J")) && (!lief.Equals("N")))
                        {
                            Console.WriteLine("Bitte geben Sie gültige Zeichen (J für \"Ja\" oder N für \"Nein\") ein.");
                            Console.Write("Lieferadresse? [J/N]: ");
                            lief = Console.ReadLine();
                        }
                        if (!AendereRechnungsadresse(benutzerAdresse, (lief.Equals("J") ? true : false)))
                        {
                            Console.WriteLine("Ändern nicht erfolgreich. Bitte versuchen Sie es erneut.");
                        }
                        else
                        {
                            Console.WriteLine("Ändern erfolgreich.");
                        }
                        Console.Read();
                        break;
                }
            }
            catch
            {
                switch (eingabe)
                {
                    case "L":
                    case "l":
                        //Zurück zum Adressmenü
                        if (fachkonzept.loescheBenutzerAdresse(benutzerAdresse))
                        {
                            AdressMenue(fachkonzept.gibAktBenutzer());
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Das Löschen war nicht erfolgreich.");
                            Console.Read();
                            AdressMgtMenue(benutzerAdresse);
                        }
                        break;
                    case "A":
                    case "a":
                        //Zurück zum Adressmenü
                        AdressMenue(fachkonzept.gibAktBenutzer());
                        break;
                    case "M":
                    case "m":
                        //Zurück zum BenutzerMenü
                        UserMenue(fachkonzept.gibAktBenutzer());
                        break;
                    case "Z":
                    case "z":
                        //Zurück zum HauptMenü
                        hauptmenue();
                        break;
                }
            }
            AdressMgtMenue(benutzerAdresse);
        }
        private bool AendereVorname(BenutzerAdresse adresse, string vname) 
        {
            if ((!vname.Equals(adresse.vname)) && (!vname.Equals("")))
            {
                BenutzerAdresse andereBenutzeradresse = adresse;
                andereBenutzeradresse.vname = vname;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzeradresse))
                {
                    adresse = andereBenutzeradresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereNachname(BenutzerAdresse adresse, string nname) 
        {
            if ((!nname.Equals(adresse.nname)) && (nname != ""))
            {
                BenutzerAdresse andereBenutzeradresse = adresse;
                andereBenutzeradresse.nname = nname;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzeradresse))
                {
                    adresse = andereBenutzeradresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereAdresszusatz(BenutzerAdresse adresse, string zusatz) 
        {
            if (!zusatz.Equals(adresse.addr_zusatz))
            {
                BenutzerAdresse andereBenutzeradresse = adresse;
                andereBenutzeradresse.addr_zusatz = zusatz;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzeradresse))
                {
                    adresse= andereBenutzeradresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereRechnungsadresse(BenutzerAdresse adresse, bool istRechAddr)
        {
            if (istRechAddr != adresse.rech_addr)
            {
                BenutzerAdresse andereBenutzeradresse = adresse;
                andereBenutzeradresse.rech_addr = istRechAddr;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzeradresse))
                {
                    adresse= andereBenutzeradresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereLieferadresse(BenutzerAdresse adresse, bool istLiefAddr)
        {
            if (istLiefAddr != adresse.lief_addr)
            {
                BenutzerAdresse andereBenutzeradresse = adresse;
                andereBenutzeradresse.lief_addr = istLiefAddr;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzeradresse))
                {
                    adresse = andereBenutzeradresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereStrNr(BenutzerAdresse adresse, string strNr) 
        {
            if ((!strNr.Equals(adresse.adresse.str_nr)) && (strNr != ""))
            {
                BenutzerAdresse andereBenutzerAdresse = adresse;
                Adresse andereAdresse = adresse.adresse;
                andereAdresse.str_nr = strNr;
                andereBenutzerAdresse.adresse = andereAdresse;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzerAdresse))
                {
                    adresse = andereBenutzerAdresse;
                    return true;
                }
            }
            return false;
        }
        private bool AenderePLZ(BenutzerAdresse adresse, string plz) 
        {
            if ((!plz.Equals(adresse.adresse.str_nr)) && (plz != ""))
            {
                BenutzerAdresse andereBenutzerAdresse = adresse;
                Adresse andereAdresse = adresse.adresse;
                andereAdresse.plz = plz;
                andereBenutzerAdresse.adresse = andereAdresse;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzerAdresse))
                {
                    adresse = andereBenutzerAdresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereOrt(BenutzerAdresse adresse, string ort) 
        {
            if ((!ort.Equals(adresse.adresse.str_nr)) && (ort != ""))
            {
                BenutzerAdresse andereBenutzerAdresse = adresse;
                Adresse andereAdresse = adresse.adresse;
                andereAdresse.ort = ort;
                andereBenutzerAdresse.adresse = andereAdresse;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzerAdresse))
                {
                    adresse = andereBenutzerAdresse;
                    return true;
                }
            }
            return false;
        }
        private bool AendereLand(BenutzerAdresse adresse, string land) // TODO
        {
            if ((!land.Equals(adresse.adresse.str_nr)) && (land != ""))
            {
                BenutzerAdresse andereBenutzerAdresse = adresse;
                Adresse andereAdresse = adresse.adresse;
                andereAdresse.land = land;
                andereBenutzerAdresse.adresse = andereAdresse;
                if (fachkonzept.aendereBenutzerAdresse(andereBenutzerAdresse))
                {
                    adresse = andereBenutzerAdresse;
                    return true;
                }
            }
            return false;
        }
    }
}