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
        Boolean insertUser(string name, string passwort);
        Boolean updateUser(int id, string name, string passwort);
        Boolean deleteUser(int id);
        Benutzer getUser(int id);
        Benutzer getUserByName(string name);
        List<Benutzer> getUserList();

        // Adressen-Zugriff
        Boolean insertAddress(string str_nr, string plz, string ort, string land);
        Boolean updateAddress(int id, string str_nr, string plz, string ort, string land);
        Boolean deleteAddress(int id);
        Adresse getAddress(int id);
        List<Adresse> getAddressList();

        // BenutzerAdressen-Zugriff
        Boolean insertUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, Boolean rech_addr, Boolean lief_addr);
        Boolean updateUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, Boolean rech_addr, Boolean lief_addr);
        Boolean deleteUserAddress(int user_id, int address_id);
        BenutzerAdresse getUserAddress(int user_id, int address_id);

        //Artikel-Zugriff
        Boolean insertItem(string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id);
        Boolean updateItem(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id);
        Boolean deleteItem(int id);
        Artikel getItem(int id);
        List<Artikel> getItemList();
    }
}
