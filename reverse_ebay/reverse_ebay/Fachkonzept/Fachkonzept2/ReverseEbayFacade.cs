﻿using System.Collections.Generic;

namespace reverse_ebay
{
    /*
     * Die Facade beinhaltet neben der Datenhaltung noch 5 Attribute,
     * die alle Daten beinhalten sollen, die das Fachkonzept benötigt.
     * Neben dem aktuell aktiven Benutzer sind das noch:
     * - eine Liste aller Artikel des Benutzers
     * - eine Liste aller Adressen des Benutzers
     * - eine Liste aller Artikel
     * - eine Liste aller Benutzer
     * Alle nicht-benutzerabhängigen Daten werden schon beim Initialisieren
     * der Facade geladen, alle benutzerspezifischen Daten in der Methode
     * oeffneBenutzer().
     * Nach jeder Änderung von Daten wird die Funktion aktualisiereBenutzerdaten()
     * ausgeführt, um die geänderten Daten dem Fachkonzept zur Verfügung zu stellen.
     * Dabei wird der Benutzer einfach einmal geschlossen und neu geöffnet.
     * In den einzelnen Methoden wird dann auf die Datenhaltung zugegriffen, 
     * ohne dass das Fachkonzept weiß, wie das durchgeführt wird.
     */
    class ReverseEbayFacade
    {
        private IDatenhaltung datenhaltung;

        private Benutzer aktBenutzer;
        private List<Artikel> aktBenutzer_ArtikelListe;
        private List<BenutzerAdresse> aktBenutzer_AdressenListe;
        private List<Artikel> alleArtikelListe;
        private List<Benutzer> alleBenutzerListe;

        public ReverseEbayFacade(IDatenhaltung _datenhaltung)
        {
            this.datenhaltung = _datenhaltung;
            this.aktBenutzer_ArtikelListe = new List<Artikel>();
            this.aktBenutzer_AdressenListe = new List<BenutzerAdresse>();
            this.alleArtikelListe = datenhaltung.getItemList();
            this.alleBenutzerListe = datenhaltung.getUserList();
        }

        // Laden / Entladen der Facade
        public bool oeffneBenutzer(Benutzer benutzer)
        {
            try
            {
                Benutzer tempBenutzer = datenhaltung.getUser(benutzer.name);
                if (tempBenutzer.passwort == benutzer.passwort)
                {
                    alleArtikelListe = datenhaltung.getItemList();
                    alleBenutzerListe = datenhaltung.getUserList();

                    // Aktueller Benutzer
                    aktBenutzer = tempBenutzer;

                    // Artikel-Liste des aktuellen Benutzers
                    foreach (Artikel artikel in alleArtikelListe)
                    {
                        if (artikel.anbieter_id == aktBenutzer.id)
                        {
                            aktBenutzer_ArtikelListe.Add(artikel);
                        }
                    }

                    // Adressen-Liste des aktuellen Benutzers
                    foreach (BenutzerAdresse benutzerAdresse in datenhaltung.getUserAdressList())
                    {
                        if (benutzerAdresse.benutzer_id == aktBenutzer.id)
                        {
                            aktBenutzer_AdressenListe.Add(benutzerAdresse);
                        }
                    }

                    return true;
                }
                return false;
            } catch { return false; }
        }
        public bool schliesseBenutzer()
        {
            try
            {
                aktBenutzer = null;
                aktBenutzer_ArtikelListe = new List<Artikel>();
                aktBenutzer_AdressenListe = new List<BenutzerAdresse>();
                return true;
            }
            catch { return false; }
        }
        private void aktualisiereBenutzerdaten()
        {
            Benutzer tempBenutzer = aktBenutzer;
            schliesseBenutzer();
            oeffneBenutzer(tempBenutzer);       
        }

        // Zugriffsfunktionen
        public Benutzer gibAktBenutzer()
        {
            return aktBenutzer;
        }
        public List<Artikel> gibAktBenutzerArtikelListe()
        {
            aktBenutzer_ArtikelListe.Sort();
            return aktBenutzer_ArtikelListe;
        }
        public List<Artikel> gibAlleArtikelListe()
        {
            alleArtikelListe.Sort();
            return alleArtikelListe;
        }
        public List<Benutzer> gibAlleBenutzerListe()
        {
            alleBenutzerListe.Sort();
            return alleBenutzerListe;
        }
        public List<BenutzerAdresse> gibAktBenutzerAdressenListe()
        {
            aktBenutzer_AdressenListe.Sort();
            return aktBenutzer_AdressenListe;
        }

        // Benutzer-Management
        public bool erzeugeBenutzer(Benutzer benutzer)
        {
            try
            {
                if (datenhaltung.insertUser(benutzer.name, benutzer.passwort) != 0)
                {
                    return true;
                }
                return false;
            } catch { return false; }
            
        }
        public bool aendereBenutzer(Benutzer benutzer)
        {
            try
            {
                if (datenhaltung.updateUser(aktBenutzer.id, benutzer.name, benutzer.passwort))
                {
                    aktBenutzer.name = benutzer.name;
                    aktBenutzer.passwort = benutzer.passwort;
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            } catch { return false; }
        }
        public bool loescheBenutzer(Benutzer benutzer)
        {
            try
            {
                bool status = datenhaltung.deleteUser(aktBenutzer.id);
                schliesseBenutzer();
                return status;
            } catch { return false; }
        }

        // Artikel-Management
        public bool erzeugeBenutzerArtikel(Artikel artikel)
        {
            try
            {
                int artikel_id = datenhaltung.insertItem(artikel.name,
                                                         artikel.kurzbeschr,
                                                         artikel.langbeschr,
                                                         artikel.ablaufdatum,
                                                         artikel.hoechstgebot,
                                                         0,
                                                         aktBenutzer.id);
                if (artikel_id != 0 && datenhaltung.getItem(artikel_id) != null)
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            } catch { return false; }
        }
        public bool aendereBenutzerArtikel(Artikel artikel)
        {
            try
            {
                if (datenhaltung.updateItem(artikel.id,
                                            artikel.name,
                                            artikel.kurzbeschr,
                                            artikel.langbeschr,
                                            artikel.ablaufdatum,
                                            artikel.hoechstgebot,
                                            artikel.bieter_id,
                                            aktBenutzer.id))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            } catch { return false; }
        }
        public bool aendereArtikel(Artikel artikel)
        {
            try
            {
                if (datenhaltung.updateItem(artikel.id,
                                            artikel.name,
                                            artikel.kurzbeschr,
                                            artikel.langbeschr,
                                            artikel.ablaufdatum,
                                            artikel.hoechstgebot,
                                            artikel.bieter_id,
                                            artikel.anbieter_id))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
        public bool loescheBenutzerArtikel(Artikel artikel)
        {
            try
            {
                if (datenhaltung.deleteItem(artikel.id))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        // Benutzer-Adressen-Management
        public bool erzeugeBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            try
            {
                Adresse temp_adresse = datenhaltung.getAddress(benutzeradresse.adresse.str_nr,
                                                               benutzeradresse.adresse.plz,
                                                               benutzeradresse.adresse.ort,
                                                               benutzeradresse.adresse.land);

                int adresse_id;
                if (temp_adresse == null)
                {
                    adresse_id = datenhaltung.insertAddress(benutzeradresse.adresse.str_nr,
                                                            benutzeradresse.adresse.plz,
                                                            benutzeradresse.adresse.ort,
                                                            benutzeradresse.adresse.land);
                }
                else
                {
                    adresse_id = temp_adresse.id;
                }

                if (datenhaltung.insertUserAddress(benutzeradresse.benutzer_id,
                                                   adresse_id,
                                                   benutzeradresse.vname,
                                                   benutzeradresse.nname,
                                                   benutzeradresse.addr_zusatz,
                                                   benutzeradresse.rech_addr,
                                                   benutzeradresse.lief_addr))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            } catch { return false; }
        }
        public bool aendereBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            try
            {
                if (datenhaltung.updateUserAddress(benutzeradresse.benutzer_id, 
                                                   benutzeradresse.adresse.id,
                                                   benutzeradresse.vname,
                                                   benutzeradresse.nname,
                                                   benutzeradresse.addr_zusatz,
                                                   benutzeradresse.rech_addr,
                                                   benutzeradresse.lief_addr))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            } catch { return false; }
        }
        public bool loescheBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            try
            {
                if (benutzeradresse.adresse.id == 0)
                {
                    benutzeradresse.adresse = datenhaltung.getAddress(benutzeradresse.adresse.str_nr, benutzeradresse.adresse.plz, benutzeradresse.adresse.ort, benutzeradresse.adresse.land);
                }
                if (datenhaltung.deleteUserAddress(benutzeradresse.benutzer_id, benutzeradresse.adresse.id))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
    }
}
