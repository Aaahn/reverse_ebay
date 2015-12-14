using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    class Fachkonzept1: IFachkonzept
    {
        public Fachkonzept1()
        {
        }

        public bool aendereAdresse(string str_nr, int plz, string stadt, string land)
        {
            throw new NotImplementedException();
        }

        public bool aendereAdresse(int id, string str_nr, int plz, string stadt, string land)
        {
            throw new NotImplementedException();
        }

        public bool aendereArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot)
        {
            throw new NotImplementedException();
        }

        public bool aendereArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, int gebot)
        {
            throw new NotImplementedException();
        }

        public bool aendereArtikel(int id, string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot)
        {
            throw new NotImplementedException();
        }

        public bool aendereBenutzer(string name, string passwort)
        {
            throw new NotImplementedException();
        }

        public bool aendereBenutzer(int id, string name, string passwort)
        {
            throw new NotImplementedException();
        }

        public bool aufArtikelBieten(int gebot)
        {
            throw new NotImplementedException();
        }

        public bool aufArtikelBieten(Artikel artikel, double gebot)
        {
            throw new NotImplementedException();
        }

        public bool ausloggen()
        {
            throw new NotImplementedException();
        }

        public int eingeloggterUser()
        {
            throw new NotImplementedException();
        }

        public bool einloggen()
        {
            throw new NotImplementedException();
        }

        public bool einloggen(string name, string passwort)
        {
            throw new NotImplementedException();
        }

        public bool erzeugeAdresse(string str_nr, int plz, string stadt, string land)
        {
            throw new NotImplementedException();
        }

        public bool erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot)
        {
            throw new NotImplementedException();
        }

        public bool erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, int gebot)
        {
            throw new NotImplementedException();
        }

        public bool erzeugeBenutzer(string name, string passwort)
        {
            throw new NotImplementedException();
        }

        public Artikel gibArtikel(int id)
        {
            throw new NotImplementedException();
        }

        public List<Artikel> gibArtikelListe(string suchstring)
        {
            throw new NotImplementedException();
        }

        public Benutzer gibBenutzer(int id)
        {
            throw new NotImplementedException();
        }

        public bool istArtikelAktiv(int id)
        {
            throw new NotImplementedException();
        }

        public bool loescheAdresse(int id)
        {
            throw new NotImplementedException();
        }

        public bool loescheArtikel(int id)
        {
            throw new NotImplementedException();
        }

        public bool loescheBenutzer(int id)
        {
            throw new NotImplementedException();
        }

        public List<Adresse> meineAdressen()
        {
            throw new NotImplementedException();
        }

        public List<Artikel> meineArtikel()
        {
            throw new NotImplementedException();
        }

        public List<Artikel> meineGeboteAnzeigen()
        {
            throw new NotImplementedException();
        }

        List<BenutzerAdresse> IFachkonzept.meineAdressen()
        {
            throw new NotImplementedException();
        }
    }
}
