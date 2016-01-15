using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    class Fachkonzept1 : IFachkonzept
    {
        private Benutzer aktBenutzer { get; set; }
        private IDatenhaltung datenhaltung;

        public Fachkonzept1(IDatenhaltung _datenhaltung)
        {
            this.datenhaltung = _datenhaltung;
            this.aktBenutzer = null;
        }

        // Ausgabe des aktuellen Nutzers
        public Benutzer gibAktBenutzer()
        {
            return aktBenutzer;
        }


        // Benutzer-Management
        public Boolean erzeugeBenutzer(string name, string passwort)
        {
            return datenhaltung.insertUser(name, passwort);
        }
        public Boolean aendereBenutzer(int id, string name = null, string passwort = null)
        {
            Benutzer benutzer = datenhaltung.getUser(id);
            if (name == null) { name = benutzer.name; }
            if (passwort == null) { passwort = benutzer.passwort; }
            return datenhaltung.updateUser(id, name, passwort);
        }
        public Boolean loescheBenutzer(int id)
        {
            return datenhaltung.deleteUser(id);
        }
        public Benutzer gibBenutzer(int id)
        {
            try { return datenhaltung.getUser(id); }
            catch { return null; }
        }
        public Boolean einloggen(string name, string passwort)
        {
            Benutzer benutzer = datenhaltung.getUserByName(name);
            try
            {
                if (benutzer.passwort == passwort)
                {
                    aktBenutzer = benutzer;
                    return true;
                } else { return false; }
            }
            catch { return false; }
            
        }
        public Boolean ausloggen()
        {
            aktBenutzer = null;
            if (aktBenutzer == null) { return true; } else { return false; }
        }
        public List<BenutzerAdresse> meineAdressen()
        {
            List<BenutzerAdresse> benutzeradressen = null;
            try
            {
                benutzeradressen = aktBenutzer.adressen;
                return benutzeradressen;
            } catch { return null; }
        }
        public List<Artikel> meineArtikel()
        {
            List<Artikel> benutzerartikel = null;
            try
            {
                benutzerartikel = datenhaltung.getItemList();
                foreach (Artikel artikel in benutzerartikel)
                {
                    if(artikel.anbieter_id != aktBenutzer.id)
                    {
                        benutzerartikel.Remove(artikel);
                    }
                }
                return benutzerartikel;
            }
            catch { return null; }

        }


        // Adressen-Management
        public Boolean erzeugeAdresse(string str_nr, string plz, string ort, string land)
        {
            return datenhaltung.insertAddress(str_nr, plz, ort, land);
        }
        public Boolean aendereAdresse(int id, string str_nr = null, string plz = null, string ort = null, string land = null)
        {
            Adresse adresse = datenhaltung.getAddress(id);
            if (str_nr == null) { str_nr = adresse.str_nr; }
            if (plz == null) { plz = adresse.plz; }
            if (ort == null) { ort = adresse.ort; }
            if (land == null) { land = adresse.land; }
            return datenhaltung.updateAddress(id, str_nr, plz, ort, land);
        }
        public Boolean loescheAdresse(int id)
        {
            return datenhaltung.deleteAddress(id);
        }


        // Artikel-Management
        public Boolean erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double mindestgebot)
        {
            return datenhaltung.insertItem(name, kurzbeschr, langbeschr, ablaufdatum, mindestgebot, bieter_id, anbieter_id);
        }
        public Boolean aendereArtikel(int id, string name = null, string kurzbeschr = null, string langbeschr = null, DateTime ablaufdatum = default(DateTime))
        {
            Artikel artikel = datenhaltung.getItem(id);
            if (name == null) { name = artikel.name; }
            if (kurzbeschr == null) { kurzbeschr = artikel.kurzbeschr; }
            if (langbeschr == null) { kurzbeschr = artikel.langbeschr; }
            return datenhaltung.updateItem(id, name, kurzbeschr, langbeschr, artikel.ablaufdatum, artikel.hoechstgebot, artikel.bieter_id, artikel.anbieter_id);
        }
        public Boolean loescheArtikel(int id)
        {
            return datenhaltung.deleteItem(id);
        }
        public Artikel gibArtikel(int id)
        {
            try { return datenhaltung.getItem(id); }
            catch { return null; }
        }


        // Sonstige Funktionen
        public Boolean aufArtikelBieten(Artikel artikel, double gebot)
        {
            return datenhaltung.updateItem(artikel.id, artikel.name, artikel.kurzbeschr, artikel.langbeschr, artikel.ablaufdatum, gebot, aktBenutzer.id, artikel.anbieter_id);
        }
        public List<Artikel> meineGeboteAnzeigen()
        {
            List<Artikel> artikelListe = datenhaltung.getItemList();
            foreach (Artikel artikel in artikelListe)
            {
                if(artikel.bieter_id != aktBenutzer.id)
                {
                    artikelListe.Remove(artikel);
                }
            }
            return artikelListe;
        }
        public Boolean istArtikelAktiv(int artikel_id)
        {
            Artikel artikel = datenhaltung.getItem(artikel_id);
            if (artikel.ablaufdatum > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }
        public List<Artikel> gibArtikelListe(string suchstring = null)
        {
            // Derzeit ist nur eine Volltextsuch auf die Kurzbeschreibung möglich
            List<Artikel> artikelListe = datenhaltung.getItemList();
            if (suchstring != null)
            {
                foreach (Artikel artikel in artikelListe)
                {
                    if (artikel.kurzbeschr != suchstring)
                    {
                        artikelListe.Remove(artikel);
                    }
                }
            }
            return artikelListe;
        }

    }
}
