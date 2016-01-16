﻿using System;
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
        public bool erzeugeBenutzer(string name, string passwort)
        {
            return datenhaltung.insertUser(name, passwort);
        }
        public bool aendereBenutzer(int id, string name = null, string passwort = null)
        {
            Benutzer benutzer = datenhaltung.getUser(id);
            if (name == null) { name = benutzer.name; }
            if (passwort == null) { passwort = benutzer.passwort; }
            return datenhaltung.updateUser(id, name, passwort);
        }
        public bool loescheBenutzer(int id)
        {
            return datenhaltung.deleteUser(id);
        }
        public Benutzer gibBenutzer(int id)
        {
            try { return datenhaltung.getUser(id); }
            catch { return null; }
        }
        public bool einloggen(string name, string passwort)
        {
            try
            {
                Benutzer benutzer = datenhaltung.getUserByName(name);
                if (benutzer.passwort == passwort)
                {
                    aktBenutzer = benutzer;

                    return true;
                } else { return false; }
            }
            catch { return false; }
            
        }
        public bool ausloggen()
        {
            aktBenutzer = null;
            if (aktBenutzer == null) { return true; } else { return false; }
        }
        public List<BenutzerAdresse> meineAdressen()
        {
            List<BenutzerAdresse> benutzeradressen = new List<BenutzerAdresse>();
            try
            {
                benutzeradressen = aktBenutzer.adressen;
                return benutzeradressen;
            } catch { return null; }
        }
        public List<Artikel> meineArtikel(bool nuroffen)
        {
            
            List<Artikel> artikelListe = datenhaltung.getItemList();
            List<Artikel> benutzerArtikelListe = new List<Artikel>();
            foreach (Artikel artikel in artikelListe)
            {
                if(artikel.anbieter_id == aktBenutzer.id)
                {
                    if (nuroffen)
                    {
                        if (artikel.ablaufdatum < DateTime.Now)
                        {
                            benutzerArtikelListe.Add(artikel);
                        }
                    }
                    else
                    {
                        benutzerArtikelListe.Add(artikel);
                    }

                }
            }
            return benutzerArtikelListe;
        }


        // BenutzerAdressen-Management
        public bool erzeugeBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            bool status = false;
            Adresse temp_adresse = benutzeradresse.adresse;
            int adresse_id = 0;

            List<Adresse> adressliste = datenhaltung.getAddressList();
            foreach (Adresse adresse in adressliste)
            {
                if (temp_adresse.str_nr == adresse.str_nr
                    && temp_adresse.plz == adresse.plz
                    && temp_adresse.ort == adresse.ort
                    && temp_adresse.land == adresse.land)
                {
                    adresse_id = adresse.id;
                }
            }

            if (adresse_id == 0)
            {
                datenhaltung.insertAddress(temp_adresse.str_nr, temp_adresse.plz, temp_adresse.ort, temp_adresse.land);
            }
            
            if (datenhaltung.insertAddress(temp_adresse.str_nr, temp_adresse.plz, temp_adresse.ort, temp_adresse.land))
            {
                
                status = datenhaltung.insertUserAddress(benutzeradresse.benutzer_id,
                                                        adresse_id, 
                                                        benutzeradresse.vname, 
                                                        benutzeradresse.nname,
                                                        benutzeradresse.addr_zusatz, 
                                                        benutzeradresse.rech_addr, 
                                                        benutzeradresse.lief_addr);
            }
            
            return status;
        }
        public bool aendereBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            return datenhaltung.updateUserAddress(benutzer_id, adresse_id, vname, nname, addr_zusatz, rech_addr, lief_addr);
        }
        public bool loescheBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            return datenhaltung.deleteUserAddress(benutzer_id, adresse_id);
        }


        // Artikel-Management
        public bool erzeugeArtikel(string name, string kurzbeschr, string langbeschr, int anbieter_id, int bieter_id, DateTime ablaufdatum, double mindestgebot)
        {
            return datenhaltung.insertItem(name, kurzbeschr, langbeschr, ablaufdatum, mindestgebot, bieter_id, anbieter_id);
        }
        public bool aendereArtikel(int id, string name = null, string kurzbeschr = null, string langbeschr = null, DateTime ablaufdatum = default(DateTime))
        {
            Artikel artikel = datenhaltung.getItem(id);
            if (name == null) { name = artikel.name; }
            if (kurzbeschr == null) { kurzbeschr = artikel.kurzbeschr; }
            if (langbeschr == null) { kurzbeschr = artikel.langbeschr; }
            return datenhaltung.updateItem(id, name, kurzbeschr, langbeschr, artikel.ablaufdatum, artikel.hoechstgebot, artikel.bieter_id, artikel.anbieter_id);
        }
        public bool loescheArtikel(int id)
        {
            return datenhaltung.deleteItem(id);
        }
        public Artikel gibArtikel(int id)
        {
            try { return datenhaltung.getItem(id); }
            catch { return null; }
        }


        // Sonstige Funktionen
        public bool aufArtikelBieten(Artikel artikel, double gebot)
        {
            return datenhaltung.updateItem(artikel.id, artikel.name, artikel.kurzbeschr, artikel.langbeschr, artikel.ablaufdatum, gebot, aktBenutzer.id, artikel.anbieter_id);
        }
        public List<Artikel> meineGeboteAnzeigen()
        {
            List<Artikel> artikelListe = datenhaltung.getItemList();
            List<Artikel> meineGeboteListe = new List<Artikel>();
            foreach (Artikel artikel in artikelListe)
            {
                if(artikel.bieter_id == aktBenutzer.id)
                {
                    meineGeboteListe.Add(artikel);
                }
            }
            return meineGeboteListe;
        }
        public bool istArtikelAktiv(int artikel_id)
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
        public List<Artikel> gibArtikelListe(string suchstring = "")
        {
            // Derzeit ist nur eine Volltextsuch auf die Kurzbeschreibung möglich
            List<Artikel> artikelListe = datenhaltung.getItemList();
            List<Artikel> meineArtikelListe = new List<Artikel>();
            if ((suchstring != "") && (artikelListe.Count > 0))
            {
                foreach (Artikel artikel in artikelListe)
                {
                    if (artikel.kurzbeschr == suchstring)
                    {
                        meineArtikelListe.Add(artikel);
                    }
                }
                return meineArtikelListe;
            }
            else
            {
                return artikelListe;
            }
        }

    }
}
