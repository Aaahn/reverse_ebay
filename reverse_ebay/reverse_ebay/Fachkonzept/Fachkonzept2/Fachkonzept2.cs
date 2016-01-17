//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace reverse_ebay
//{
//    class Fachkonzept2
//    {
//        private ReverseEbayFacade reverseEbayFacade;

//        public Fachkonzept2(IDatenhaltung _datenhaltung)
//        {
//            this.reverseEbayFacade = new ReverseEbayFacade(_datenhaltung);
//        }

//        // Ausgabe des aktuellen Nutzers
//        public Benutzer gibAktBenutzer()
//        {
//            return reverseEbayFacade.gibAktBenutzer();
//        }


//        // Benutzer-Management
//        public bool erzeugeBenutzer(Benutzer benutzer)
//        {
//            return reverseEbayFacade.erzeugeBenutzer(benutzer);
//        }
//        public bool aendereBenutzer(Benutzer benutzer)
//        {
//            return reverseEbayFacade.aendereBenutzer(benutzer);
//        }
//        public bool loescheBenutzer(Benutzer benutzer)
//        {
//            return reverseEbayFacade.loescheBenutzer(benutzer);
//        }
//        public Benutzer gibBenutzer(int id)
//        {
//            try
//            {
//                foreach(Benutzer benutzer in reverseEbayFacade.gibAlleBenutzerListe())
//                {
//                    if (benutzer.id == id)
//                    {
//                        return benutzer;
//                    }
//                }
//                return null;
//            }
//            catch { return null; }
//        }
//        public bool einloggen(string name, string passwort)
//        {
//            Benutzer tempBenutzer = new Benutzer();
//            tempBenutzer.name = name;
//            tempBenutzer.passwort = passwort;
//            return reverseEbayFacade.oeffneBenutzer(tempBenutzer);
//        }
//        public bool ausloggen()
//        {
//            return reverseEbayFacade.schliesseBenutzer();
//        }
//        public List<BenutzerAdresse> meineAdressen()
//        {
//            return reverseEbayFacade.gibAktBenutzerAdressenListe();
//        }
//        public List<Artikel> meineArtikel(bool nuroffen)
//        {
//            List<Artikel> tempArtikelListe = reverseEbayFacade.gibAktBenutzerArtikelListe();
//            List<Artikel> benutzerArtikelListe = new List<Artikel>();
           
//            if (nuroffen)
//            {
//                foreach (Artikel artikel in tempArtikelListe)
//                {
//                    if (artikel.ablaufdatum < DateTime.Now)
//                    {
//                        benutzerArtikelListe.Add(artikel);
//                    }
//                }
//            }
//            else
//            {
//                benutzerArtikelListe = tempArtikelListe;
//            }

//            return benutzerArtikelListe;
//        }


//        // BenutzerAdressen-Management
//        public bool erzeugeBenutzerAdresse(BenutzerAdresse benutzeradresse)
//        {
//            return reverseEbayFacade.erzeugeBenutzerAdresse(benutzeradresse);
//        }
//        public bool aendereBenutzerAdresse(BenutzerAdresse benutzeradresse)
//        {
//            return reverseEbayFacade.aendereBenutzerAdresse(benutzeradresse);
//        }
//        public bool loescheBenutzerAdresse(BenutzerAdresse benutzeradresse)
//        {
//            return reverseEbayFacade.loescheBenutzerAdresse(benutzeradresse);
//        }


//        // Artikel-Management
//        public bool erzeugeArtikel(Artikel artikel)
//        {
//            return reverseEbayFacade.erzeugeBenutzerArtikel(artikel);
//        }
//        public bool aendereArtikel(Artikel artikel)
//        {
//            return reverseEbayFacade.aendereBenutzerArtikel(artikel);
//        }
//        public bool loescheArtikel(Artikel artikel)
//        {
//            return reverseEbayFacade.loescheBenutzerArtikel(artikel);
//        }
//        public Artikel gibArtikel(int id)
//        {
//            try
//            {
//                foreach (Artikel artikel in reverseEbayFacade.gibAlleArtikelListe())
//                {
//                    if (artikel.id == id)
//                    {
//                        return artikel;
//                    }
//                }
//                return null;
//            }
//            catch { return null; }
//        }


//        // Sonstige Funktionen
//        public bool aufArtikelBieten(Artikel artikel, double gebot)
//        {
//            foreach (Artikel tempArtikel in reverseEbayFacade.gibAlleArtikelListe())
//            {
//                if (gebot >= 0 && gebot < artikel.hoechstgebot)
//                {
//                    artikel.hoechstgebot = gebot;
//                    artikel.bieter_id = reverseEbayFacade.gibAktBenutzer().id;
//                    return reverseEbayFacade.aendereArtikel(artikel);
//                }
//                return false;
//            }
//            return false;
//        }
//        public List<Artikel> meineGeboteAnzeigen()
//        {
//            List<Artikel> artikelListe = datenhaltung.getItemList();
//            List<Artikel> meineGeboteListe = new List<Artikel>();
//            foreach (Artikel artikel in artikelListe)
//            {
//                if (artikel.bieter_id == aktBenutzer.id)
//                {
//                    meineGeboteListe.Add(artikel);
//                }
//            }
//            return meineGeboteListe;
//        }
//        public bool istArtikelAktiv(Artikel artikel)
//        {
//            List<Artikel> artikelListe = datenhaltung.getItemList();
//            foreach (Artikel art in artikelListe)
//            {
//                if (art.id == artikel.id && art.ablaufdatum > DateTime.Now)
//                {
//                    return true;
//                }
//            }
//            return false;
//        }
//        public List<Artikel> gibArtikelListe(string suchstring = "")
//        {
//            // Derzeit ist nur eine Volltextsuch auf die Kurzbeschreibung möglich
//            List<Artikel> artikelListe = datenhaltung.getItemList();
//            List<Artikel> meineArtikelListe = new List<Artikel>();
//            if ((suchstring != "") && (artikelListe.Count > 0))
//            {
//                foreach (Artikel artikel in artikelListe)
//                {
//                    if (artikel.kurzbeschr == suchstring)
//                    {
//                        meineArtikelListe.Add(artikel);
//                    }
//                }
//                return meineArtikelListe;
//            }
//            else
//            {
//                return artikelListe;
//            }
//        }
//    }
//}
