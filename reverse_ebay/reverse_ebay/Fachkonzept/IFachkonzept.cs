using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    interface IFachkonzept
    {
        // Benutzer-Management
        bool erzeugeBenutzer(string name, string passwort);
        bool aendereBenutzer(int id, string name, string passwort);
        bool loescheBenutzer(int id);
        Benutzer gibBenutzer(int id);
        bool einloggen(string name, string passwort);
        bool ausloggen();
        int eingeloggterUser();
        List<BenutzerAdresse> meineAdressen();
        List<Artikel> meineArtikel();

        // Adressen-Management
        bool erzeugeAdresse(string str_nr, int plz, string stadt, string land);
        bool aendereAdresse(int id, string str_nr, int plz, string stadt, string land);
        bool loescheAdresse(int id);

        // Artikel-Management
        bool erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot);
        bool aendereArtikel(int id, string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot);
        bool loescheArtikel(int id);
        Artikel gibArtikel(int id);

        // Sonstige Funktionen
        bool aufArtikelBieten(Artikel artikel, double gebot);
        List<Artikel> meineGeboteAnzeigen();
        bool istArtikelAktiv(int id);
        List<Artikel> gibArtikelListe(string suchstring);

    }
}

