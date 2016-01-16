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
        bool erzeugeBenutzer(Benutzer benutzer);
        bool aendereBenutzer(Benutzer benutzer);
        bool loescheBenutzer(Benutzer benutzer);
        Benutzer gibBenutzer(int id);
        bool einloggen(string name, string passwort);
        bool ausloggen();
        Benutzer gibAktBenutzer();
        List<BenutzerAdresse> meineAdressen();
        List<Artikel> meineArtikel(bool nuroffen);

        // BenutzerAdressen-Management
        bool erzeugeBenutzerAdresse(BenutzerAdresse benutzeradresse);
        bool aendereBenutzerAdresse(BenutzerAdresse benutzeradresse);
        bool loescheBenutzerAdresse(BenutzerAdresse benutzeradresse);

        // Artikel-Management
        bool erzeugeArtikel(Artikel artikel);
        bool aendereArtikel(Artikel artikel);
        bool loescheArtikel(Artikel artikel);
        Artikel gibArtikel(int id);

        // Sonstige Funktionen
        bool aufArtikelBieten(Artikel artikel, double gebot);
        List<Artikel> meineGeboteAnzeigen();
        bool istArtikelAktiv(Artikel artikel);
        List<Artikel> gibArtikelListe(string suchstring);

    }
}

