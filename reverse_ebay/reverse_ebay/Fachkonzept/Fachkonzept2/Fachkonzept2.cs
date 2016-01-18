using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    /* 
     * Das Fachkonzept 2 gibt die erhaltene Datenhaltung an die Facade, damit 
     * diese eine Verbindung zu den Daten erhält. Ansonsten benötigt es keine weiteren
     * Attribute, da sie für das Fachkonzept nicht notwendig sind. 
     * Die Manipulation der Daten über die Datenhaltung wird ausschließlich in der
     * Facade verwaltet, inklusive sämtlicher Einschränkungen.
     * Für die Ausgabe der Daten stellt die Facade eigene Attribute zur 
     * Verfügung (dazu mehr in der Datei reverseEbayFacade.cs). Wie die Daten dann 
     * verwendet werden, also was der Benutzer (in unserem Fall das Fachkonzept 2)
     * mit den Daten macht, ist für die Facade nicht relevant. Sie stellt die Daten
     * nur zur Verfügung.
     */
    class Fachkonzept2 : IFachkonzept
    {
        private ReverseEbayFacade reverseEbayFacade;

        public Fachkonzept2(IDatenhaltung _datenhaltung)
        {
            this.reverseEbayFacade = new ReverseEbayFacade(_datenhaltung);
        }

        // Ausgabe des aktuellen Nutzers
        public Benutzer gibAktBenutzer()
        {
            return reverseEbayFacade.gibAktBenutzer();
        }


        // Benutzer-Management
        public bool erzeugeBenutzer(Benutzer benutzer)
        {
            return reverseEbayFacade.erzeugeBenutzer(benutzer);
        }
        public bool aendereBenutzer(Benutzer benutzer)
        {
            return reverseEbayFacade.aendereBenutzer(benutzer);
        }
        public bool loescheBenutzer(Benutzer benutzer)
        {
            return reverseEbayFacade.loescheBenutzer(benutzer);
        }
        public Benutzer gibBenutzer(int id)
        {
            try
            {
                foreach(Benutzer benutzer in reverseEbayFacade.gibAlleBenutzerListe())
                {
                    if (benutzer.id == id)
                    {
                        return benutzer;
                    }
                }
                return null;
            }
            catch { return null; }
        }
        public bool einloggen(string name, string passwort)
        {
            Benutzer tempBenutzer = new Benutzer();
            tempBenutzer.name = name;
            tempBenutzer.passwort = passwort;
            return reverseEbayFacade.oeffneBenutzer(tempBenutzer);
        }
        public bool ausloggen()
        {
            return reverseEbayFacade.schliesseBenutzer();
        }
        public List<BenutzerAdresse> meineAdressen()
        {
            return reverseEbayFacade.gibAktBenutzerAdressenListe();
        }
        public List<Artikel> meineArtikel(bool nuroffen)
        {
            List<Artikel> tempArtikelListe = reverseEbayFacade.gibAktBenutzerArtikelListe();
            List<Artikel> benutzerArtikelListe = new List<Artikel>();
           
            if (nuroffen)
            {
                foreach (Artikel artikel in tempArtikelListe)
                {
                    if (artikel.ablaufdatum < DateTime.Now)
                    {
                        benutzerArtikelListe.Add(artikel);
                    }
                }
            }
            else
            {
                benutzerArtikelListe = tempArtikelListe;
            }

            return benutzerArtikelListe;
        }


        // BenutzerAdressen-Management
        public bool erzeugeBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            return reverseEbayFacade.erzeugeBenutzerAdresse(benutzeradresse);
        }
        public bool aendereBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            return reverseEbayFacade.aendereBenutzerAdresse(benutzeradresse);
        }
        public bool loescheBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            return reverseEbayFacade.loescheBenutzerAdresse(benutzeradresse);
        }


        // Artikel-Management
        public bool erzeugeArtikel(Artikel artikel)
        {
            return reverseEbayFacade.erzeugeBenutzerArtikel(artikel);
        }
        public bool aendereArtikel(Artikel artikel)
        {
            return reverseEbayFacade.aendereBenutzerArtikel(artikel);
        }
        public bool loescheArtikel(Artikel artikel)
        {
            return reverseEbayFacade.loescheBenutzerArtikel(artikel);
        }
        public Artikel gibArtikel(int id)
        {
            try
            {
                foreach (Artikel artikel in reverseEbayFacade.gibAlleArtikelListe())
                {
                    if (artikel.id == id)
                    {
                        return artikel;
                    }
                }
                return null;
            }
            catch { return null; }
        }


        // Sonstige Funktionen
        public bool aufArtikelBieten(Artikel artikel, double gebot)
        {
            foreach (Artikel tempArtikel in reverseEbayFacade.gibAlleArtikelListe())
            {
                if (gebot >= 0 && gebot < artikel.hoechstgebot)
                {
                    artikel.hoechstgebot = gebot;
                    artikel.bieter_id = reverseEbayFacade.gibAktBenutzer().id;
                    return reverseEbayFacade.aendereArtikel(artikel);
                }
                return false;
            }
            return false;
        }
        public List<Artikel> meineGeboteAnzeigen()
        {
            List<Artikel> meineGeboteListe = new List<Artikel>();
            foreach (Artikel tempArtikel in reverseEbayFacade.gibAlleArtikelListe())
            {
                if (tempArtikel.bieter_id == reverseEbayFacade.gibAktBenutzer().id)
                {
                    meineGeboteListe.Add(tempArtikel);
                }
            }
            return meineGeboteListe;
        }
        public bool istArtikelAktiv(Artikel artikel)
        {
            foreach (Artikel tempArtikel in reverseEbayFacade.gibAlleArtikelListe())
            {
                if (tempArtikel.id == artikel.id && tempArtikel.ablaufdatum > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Artikel> gibArtikelListe(string suchstring = "")
        {
            // Derzeit ist nur eine Volltextsuch auf die Kurzbeschreibung möglich
            List<Artikel> artikelListe = reverseEbayFacade.gibAlleArtikelListe();
            List<Artikel> artikelSuchListe = new List<Artikel>();
            if ((suchstring != "") && (artikelListe.Count > 0))
            {
                foreach (Artikel artikel in artikelListe)
                {
                    if (artikel.kurzbeschr == suchstring)
                    {
                        artikelSuchListe.Add(artikel);
                    }
                }
                return artikelSuchListe;
            }
            else
            {
                return artikelListe;
            }
        }
    }
}
