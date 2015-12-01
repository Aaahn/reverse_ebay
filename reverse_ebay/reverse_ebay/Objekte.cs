using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    class Adresse
    {
        private int id { get; set; }
        private string str_nr { get; set; }
        private string plz { get; set; }
        private string ort { get; set; }
        private string land { get; set; }

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
        private Boolean rech_addr { get; set; }
        private Boolean lief_addr { get; set; }
        private string vname { get; set; }
        private string nname { get; set; }
        private string addr_zusatz { get; set; }
        private Adresse addresse { get; set; }

        BenutzerAdresse(Boolean rech_addr, Boolean lief_addr, string vname, string nname, string addr_zusatz, Adresse addresse)
        {
            this.rech_addr = rech_addr;
            this.lief_addr = lief_addr;
            this.vname = vname;
            this.nname = nname;
            this.addr_zusatz = addr_zusatz;
            this.addresse = addresse;
        }
    }
    class Benutzer
    {
        private int id { get; set; }
        public string name { get; set; }
        private string passwort { get; set; }
        private List<BenutzerAdresse> adressen { get; set; }

        Benutzer(int id, string name, string passwort, List<BenutzerAdresse> adressen)
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

        Artikel(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, float hoechstgebot, int bieter_id, int anbieter_id) 
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
