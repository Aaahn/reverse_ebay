using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    /* *** Dokumentation / Hinweise ***
     * Das Fachkonzept erzeugt beim Aufruf der Methode 'einloggen' einen Benutzer, 
     * der zur Laufzeit dauerhaft verfügbar ist. Mit der Methode 'gibAktBenutzer wird
     * dieser Benutzer als Kopie ausgegeben. Eine Änderung an dem Objekt ist nicht
     * möglich, aber über den Aufruf 'gibAktBenutzer().id' lässt sich zum Beispiel die 
     * Nutzer-ID auslesen. Aber auch alle anderen Daten des Nutzers wie Name und Passwort.
     * Mit der Methode 'ausloggen()' wird der Benutzer wieder geleert. Es kann immer nur
     * ein Nutzer aktiv sein.
     */
    interface IFachkonzept
    {
        // Benutzer-Management
        bool erzeugeBenutzer(string name, string passwort);
        bool aendereBenutzer(int id, string name, string passwort);
        bool loescheBenutzer(int id);
        Benutzer gibBenutzer(int id);
        bool einloggen(string name, string passwort);
        bool ausloggen();
        Benutzer gibAktBenutzer();
        List<BenutzerAdresse> meineAdressen();
        List<Artikel> meineArtikel(bool nuroffen);

        // Adressen-Management
        bool erzeugeAdresse(string str_nr, string plz, string stadt, string land);
        bool aendereAdresse(int id, string str_nr, string plz, string stadt, string land);
        bool loescheAdresse(int id);

        // BenutzerAdressen-Management


        // Artikel-Management
        bool erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double gebot);
        bool aendereArtikel(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum);
        bool loescheArtikel(int id);
        Artikel gibArtikel(int id);

        // Sonstige Funktionen
        bool aufArtikelBieten(Artikel artikel, double gebot);
        List<Artikel> meineGeboteAnzeigen();
        bool istArtikelAktiv(int id);
        List<Artikel> gibArtikelListe(string suchstring);

    }
}

