using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    interface IFachkonzept
    {
        // Benutzer-Management
        Boolean erzeugeBenutzer(string name, string passwort);
        Boolean aendereBenutzer(string name, string passwort);
        Boolean loescheBenutzer(int id);
        Benutzer gibBenutzer(int id);
        Boolean einloggen();
        Boolean ausloggen();
        int eingeloggterUser();
        List<Adresse> meineAdressen();
        List<Artikel> meineArtikel();

        // Adressen-Management
        Boolean erzeugeAdresse(string str_nr, int plz, string stadt, string land);
        Boolean aendereAdresse(string str_nr, int plz, string stadt, string land);
        Boolean loescheAdresse(int id);

        // Artikel-Management
        Boolean erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot);
        Boolean aendereArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot);
        Boolean loescheArtikel(int id);
        Artikel gibArtikel(int id);

        // Sonstige Funktionen
        Boolean aufArtikelBieten(Artikel artikel, double gebot);
        List<Artikel> meineGeboteAnzeigen();
        Boolean istArtikelAktiv(int id);
        List<Artikel> gibArtikelListe(string suchstring);

    }
}

