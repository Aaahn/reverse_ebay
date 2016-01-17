using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    class Fachkonzept1 : IFachkonzept
    {
        private Benutzer aktBenutzer;
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
        public bool erzeugeBenutzer(Benutzer benutzer)
        {
            if (datenhaltung.insertUser(benutzer.name, benutzer.passwort) != 0)
            {
                return true;
            }
            return false;
        }
        public bool aendereBenutzer(Benutzer benutzer)
        {
            if (datenhaltung.getUser(benutzer.id) != null)
            {
                if (datenhaltung.updateUser(benutzer.id, benutzer.name, benutzer.passwort))
                {
                    aktualisiereBenutzerdaten();
                    return true;
                }
            }
            return false;
        }
        public bool loescheBenutzer(Benutzer benutzer)
        {
            return datenhaltung.deleteUser(benutzer.id);
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
                Benutzer benutzer = datenhaltung.getUser(name);
                if (benutzer.passwort == passwort)
                {
                    aktBenutzer = benutzer;

                    return true;
                } else { return false; }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); return false; }
            
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
        private void aktualisiereBenutzerdaten()
        {
            Benutzer tempBenutzer = aktBenutzer;
            ausloggen();
            einloggen(tempBenutzer.name, tempBenutzer.passwort);
        }


        // BenutzerAdressen-Management
        public bool erzeugeBenutzerAdresse(BenutzerAdresse benutzeradresse)
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
        }
        public bool aendereBenutzerAdresse(BenutzerAdresse benutzeradresse)
        {
            Adresse temp_adresse = new Adresse();
            int adresse_id;
            try
            {
                temp_adresse = datenhaltung.getAddress(benutzeradresse.adresse.str_nr, benutzeradresse.adresse.plz, benutzeradresse.adresse.ort, benutzeradresse.adresse.land);
                adresse_id = temp_adresse.id;
                if (datenhaltung.updateUserAddress(benutzeradresse.benutzer_id,
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
            }
            catch
            {
                adresse_id = datenhaltung.insertAddress(benutzeradresse.adresse.str_nr,
                                                        benutzeradresse.adresse.plz,
                                                        benutzeradresse.adresse.ort,
                                                        benutzeradresse.adresse.land);
                if (adresse_id != 0)
                {
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
                }
                return false;
            }

            
        }
        public bool loescheBenutzerAdresse(BenutzerAdresse benutzeradresse)
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


        // Artikel-Management
        public bool erzeugeArtikel(Artikel artikel)
        {
            if (datenhaltung.insertItem(artikel.name, 
                                        artikel.kurzbeschr,
                                        artikel.langbeschr,
                                        artikel.ablaufdatum,
                                        artikel.hoechstgebot,
                                        0, 
                                        aktBenutzer.id) != 0)
            {
                return true;
            }
            return false;
        }
        public bool aendereArtikel(Artikel artikel)
        {
            if (datenhaltung.getItem(artikel.id) != null)
            {
                return datenhaltung.updateItem(artikel.id,
                                               artikel.name,
                                               artikel.kurzbeschr,
                                               artikel.langbeschr,
                                               artikel.ablaufdatum,
                                               artikel.hoechstgebot,
                                               artikel.bieter_id,
                                               aktBenutzer.id);
            }
            return false;
        }
        public bool loescheArtikel(Artikel artikel)
        {
            return datenhaltung.deleteItem(artikel.id);
        }
        public Artikel gibArtikel(int id)
        {
            try { return datenhaltung.getItem(id); }
            catch { return null; }
        }


        // Sonstige Funktionen
        public bool aufArtikelBieten(Artikel artikel, double gebot)
        {
            if (gebot >= 0 && gebot < artikel.hoechstgebot)
            {
                return datenhaltung.updateItem(artikel.id, artikel.name, artikel.kurzbeschr, artikel.langbeschr, artikel.ablaufdatum, gebot, aktBenutzer.id, artikel.anbieter_id);
            }
            return false;
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
        public bool istArtikelAktiv(Artikel artikel)
        {
            List<Artikel> artikelListe = datenhaltung.getItemList();
            foreach (Artikel art in artikelListe)
            {
                if (art.id == artikel.id && art.ablaufdatum > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Artikel> gibArtikelListe(string suchstring = "")
        {
            // Derzeit ist nur eine Volltextsuch auf die Kurzbeschreibung möglich
            List<Artikel> artikelListe = datenhaltung.getItemList();
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
