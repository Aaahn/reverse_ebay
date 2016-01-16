using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverse_ebay
{
    interface IDatenhaltung
    {
        // Benutzer-Zugriff
        bool insertUser(string name, string passwort);
        bool updateUser(int id, string name, string passwort);
        bool deleteUser(int id);
        Benutzer getUser(int id);
        Benutzer getUser(string name);
        List<Benutzer> getUserList();

        // Adressen-Zugriff
        bool insertAddress(string str_nr, string plz, string ort, string land);
        bool updateAddress(int id, string str_nr, string plz, string ort, string land);
        bool deleteAddress(int id);
        Adresse getAddress(int id);
        Adresse getAddress(string str_nr, string plz, string ort, string land);
        List<Adresse> getAddressList();

        // BenutzerAdressen-Zugriff
        bool insertUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, bool rech_addr, bool lief_addr);
        bool updateUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, bool rech_addr, bool lief_addr);
        bool deleteUserAddress(int user_id, int address_id);
        BenutzerAdresse getUserAddress(int user_id, int address_id);
        List<BenutzerAdresse> getUserAdressList();

        //Artikel-Zugriff
        bool insertItem(string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id);
        bool updateItem(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id);
        bool deleteItem(int id);
        Artikel getItem(int id);
        List<Artikel> getItemList();
    }
}
