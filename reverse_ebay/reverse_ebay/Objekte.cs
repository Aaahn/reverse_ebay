using System;
using System.Collections.Generic;

namespace reverse_ebay
{
    class Adresse
    {
        public int id { get; set; }
        public string str_nr { get; set; }
        public string plz { get; set; }
        public string ort { get; set; }
        public string land { get; set; }

        public Adresse(int id, string str_nr, string plz, string ort, string land = "Deutschland")
        {
            this.id = id;
            this.str_nr = str_nr;
            this.plz = plz;
            this.ort = ort;
            this.land = land;
        }
    }

    class BenutzerAdresse
    {
        public Boolean rech_addr { get; set; }
        public Boolean lief_addr { get; set; }
        public string vname { get; set; }
        public string nname { get; set; }
        public string addr_zusatz { get; set; }
        public int benutzer_id { get; set; }
        public Adresse adresse { get; set; }

        public BenutzerAdresse(Boolean rech_addr, Boolean lief_addr, string vname, string nname, string addr_zusatz, int benutzer_id, Adresse addresse)
        {
            this.rech_addr = rech_addr;
            this.lief_addr = lief_addr;
            this.vname = vname;
            this.nname = nname;
            this.addr_zusatz = addr_zusatz;
            this.benutzer_id = benutzer_id;
            this.adresse = adresse;
        }
    }

    class Benutzer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string passwort { get; set; }
        public List<BenutzerAdresse> adressen { get; set; }

        public Benutzer(int id, string name, string passwort, List<BenutzerAdresse> adressen)
        {
            this.id = id;
            this.name = name;
            this.passwort = passwort;
            this.adressen = adressen;
        }
    }

    class Artikel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string kurzbeschr { get; set; }
        public string langbeschr { get; set; }
        public DateTime ablaufdatum { get; set; }
        public float hoechstgebot { get; set; }
        public int bieter_id { get; set; }
        public int anbieter_id { get; set; }

        public Artikel(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, float hoechstgebot, int bieter_id, int anbieter_id) 
        {
            this.id = id;
            this.name = name;
            this.kurzbeschr = kurzbeschr;
            this.langbeschr = langbeschr;
            this.ablaufdatum = ablaufdatum;
            this.hoechstgebot = hoechstgebot;
            this.bieter_id = bieter_id;
            this.anbieter_id = anbieter_id;
        }
    }
}
