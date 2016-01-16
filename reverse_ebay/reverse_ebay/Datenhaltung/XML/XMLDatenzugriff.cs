using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace reverse_ebay
{
    class XMLDatenzugriff : IDatenhaltung
    {
        private XElement UserXML;
        private XElement AddressXML;
        private XElement UserAddressXML;
        private XElement ItemXML;
        private string speicherort;

        public XMLDatenzugriff()
        {
            this.speicherort = @"C:\reverse_ebay\xml\";
        }

        // Benutzer-Zugriff
        public Boolean insertUser(string name, string passwort)
        {
            loadUserFile();
            int new_id = (int)UserXML.Attribute("lastUsedID") + 1;
            if (!userExists(new_id) & !userNameExists(name))
            {
                XElement user = new XElement("user");
                user.Add(new XElement("id", new_id));
                user.Add(new XElement("name", name));
                user.Add(new XElement("passwort", passwort));

                UserXML.Add(user);
                UserXML.SetAttributeValue("lastUsedID", new_id);

                saveUserFile();
                return true;
            } else
            {
                return false;
            }
            
        }
        public Boolean updateUser(int id, string name, string passwort)
        {
            loadUserFile();
            if (userExists(id))
            {
                XElement user = null;
                IEnumerable<XElement> users =
                    from el in UserXML.Elements("user")
                    where (int)el.Element("id") == id
                    select el;
                
                foreach (XElement el in users) { user = el; }

                user.Remove();
                user.SetElementValue("name", name);
                user.SetElementValue("passwort", passwort);
                UserXML.Add(user);

                saveUserFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean deleteUser(int id)
        {
            loadUserFile();
            if (userExists(id))
            {
                XElement user = null;
                IEnumerable<XElement> users =
                    from el in UserXML.Elements("user")
                    where (int)el.Element("id") == id
                    select el;

                foreach (XElement el in users) { user = el; }

                user.Remove();

                if (deleteItemByVendorID(id))
                {
                    if (deleteUserAddressByUserID(id))
                    {
                        saveUserFile();
                        return true;
                    }
                }
                return false;
                
            }
            else
            {
                return false;
            }
        }
        public Benutzer getUser(int id)
        {
            loadUserFile();
            loadUserAddressFile();
            if (userExists(id))
            {
                XElement user = null;
                IEnumerable<XElement> users = 
                    from el in UserXML.Elements("user")
                    where (int)el.Element("id") == id
                    select el;
                foreach (XElement el in users) { user = el; }

                List<BenutzerAdresse> adressen = new List<BenutzerAdresse>();
                IEnumerable<XElement> useraddresses =
                    from el in UserAddressXML.Elements("user_address")
                    where (int)el.Element("user_id") == id
                    select el;
                foreach (XElement el in useraddresses)
                {
                    adressen.Add(getUserAddress((int)el.Element("user_id"), (int)el.Element("address_id")));
                }

                Benutzer benutzer = new Benutzer(
                    (int)user.Element("id"),
                    (string)user.Element("name"),
                    (string)user.Element("passwort"),
                    adressen);

                return benutzer;
            }
            else
            {
                return null;
            }
        }
        public Benutzer getUserByName(string name)
        {
            loadUserFile();
            loadUserAddressFile();
            if (userNameExists(name))
            {
                XElement user = null;
                IEnumerable<XElement> users =
                    from el in UserXML.Elements("user")
                    where (string)el.Element("name") == name
                    select el;
                foreach (XElement el in users) { user = el; }

                List<BenutzerAdresse> adressen = new List<BenutzerAdresse>();
                IEnumerable<XElement> useraddresses =
                    from el in UserAddressXML.Elements("user_address")
                    where (int)el.Element("user_id") == (int)user.Element("id")
                    select el;
                foreach (XElement el in useraddresses)
                {
                    adressen.Add(getUserAddress((int)el.Element("user_id"), (int)el.Element("address_id")));
                }

                Benutzer benutzer = new Benutzer(
                    (int)user.Element("id"),
                    (string)user.Element("name"),
                    (string)user.Element("passwort"),
                    adressen);

                return benutzer;
            }
            else
            {
                return null;
            }
        }
        public List<Benutzer> getUserList()
        {
            loadUserFile();
            List<Benutzer> userList = null;
            IEnumerable<XElement> users =
                from el in UserXML.Elements("user")
                select el;
            foreach (XElement el in users)
            {
                userList.Add(getUser((int)el.Element("id")));
            }
            return userList;

        }
        private Boolean userExists(int id)
        {
            loadUserFile();
            IEnumerable<XElement> userList = null;
            try { 
                userList = UserXML.Elements();
            } catch { return true; }

            foreach (XElement user in userList)
            {
                if ((int)user.Element("id") == id)
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean userNameExists(string name)
        {
            loadUserFile();
            IEnumerable<XElement> userList = null;
            try
            {
                userList = UserXML.Elements();
            }
            catch { return true; }

            foreach (XElement user in userList)
            {
                if ((string)user.Element("name") == name)
                {
                    return true;
                }
            }
            return false;
        }
        private void loadUserFile()
        {
            try
            {
                UserXML = XElement.Load(speicherort + "user.xml");
            }
            catch
            {
                UserXML = new XElement("users");
                UserXML.SetAttributeValue("lastUsedID", 0);
                UserXML.Save(speicherort + "user.xml");
            }
            
        }
        private void saveUserFile()
        {
            UserXML.Save(speicherort + "user.xml");
        }

      
        // Adressen-Zugriff
        public Boolean insertAddress(string str_nr, string plz, string ort, string land)
        {
            loadAddressFile();
            int new_id = (int)AddressXML.Attribute("lastUsedID") + 1;
            if (!addressExists(new_id) & !addressExists(str_nr, plz, ort, land))
            {
                XElement address = new XElement("address");

                address.Add(new XElement("id", new_id));
                address.Add(new XElement("str_nr", str_nr));
                address.Add(new XElement("plz", plz));
                address.Add(new XElement("ort", ort));
                address.Add(new XElement("land", land));

                AddressXML.Add(address);
                AddressXML.SetAttributeValue("lastUsedID", new_id);

                saveAddressFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean updateAddress(int id, string str_nr, string plz, string ort, string land)
        {
            loadAddressFile();
            if (addressExists(id))
            {
                XElement address = null;
                IEnumerable<XElement> addresses =
                    from el in AddressXML.Elements("address")
                    where (int)el.Element("id") == id
                    select el;

                foreach (XElement el in addresses) { address = el; }

                address.Remove();
                address.SetElementValue("str_nr", str_nr);
                address.SetElementValue("plz", plz);
                address.SetElementValue("ort", ort);
                address.SetElementValue("land", land);
                AddressXML.Add(address);

                saveAddressFile();
                return true;
            }
            else if (addressExists(str_nr, plz, ort, land))
            {
                XElement address = null;
                IEnumerable<XElement> addresses =
                    from el in AddressXML.Elements("address")
                    where (((string)el.Element("str_nr") == str_nr)
                         & ((string)el.Element("plz") == plz)
                         & ((string)el.Element("ort") == ort)
                         & ((string)el.Element("land") == land))
                    select el;

                foreach (XElement el in addresses) { address = el; }

                address.Remove();
                address.SetElementValue("str_nr", str_nr);
                address.SetElementValue("plz", plz);
                address.SetElementValue("ort", ort);
                address.SetElementValue("land", land);
                AddressXML.Add(address);

                saveAddressFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean deleteAddress(int id)
        {
            loadAddressFile();
            if (addressExists(id))
            {
                XElement address = null;
                IEnumerable<XElement> addresses =
                    from el in AddressXML.Elements("address")
                    where (int)el.Element("id") == id
                    select el;

                foreach (XElement el in addresses) { address = el; }

                address.Remove();
                deleteUserAddressByAddressID(id);

                saveAddressFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Adresse getAddress(int id)
        {
            loadAddressFile();
            if (addressExists(id))
            {
                XElement address = null;
                IEnumerable<XElement> addresses =
                    from el in AddressXML.Elements("address")
                    where (int)el.Element("id") == id
                    select el;

                foreach (XElement el in addresses) { address = el; }

                List<BenutzerAdresse> adressen = new List<BenutzerAdresse>();

                Adresse adresse = new Adresse(
                    (int)address.Element("id"),
                    (string)address.Element("str_nr"),
                    (string)address.Element("plz"),
                    (string)address.Element("stadt"),
                    (string)address.Element("land"));

                return adresse;
            }
            else
            {
                return null;
            }
        }
        public List<Adresse> getAddressList()
        {
            loadAddressFile();
            List<Adresse> addressList = null;
            IEnumerable<XElement> addresses =
                from el in AddressXML.Elements("address")
                select el;
            foreach (XElement el in addresses)
            {
                addressList.Add(getAddress((int)el.Element("id")));
            }
            return addressList;

        }
        private Boolean addressExists(int id)
        {
            loadUserFile();
            IEnumerable<XElement> addressList = null;
            try {
                addressList = AddressXML.Elements();
            } catch { return true; }

            foreach (XElement adress in addressList)
            {
                if ((int)adress.Element("id") == id)
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean addressExists(string str_nr, string plz, string ort, string land)
        {
            loadUserFile();
            IEnumerable<XElement> addressList = null;
            try
            {
                addressList = AddressXML.Elements();
            }
            catch { return true; }

            foreach (XElement adress in addressList)
            {
                
                if (((string)adress.Element("str_nr") == str_nr) 
                  & ((string)adress.Element("plz") == plz)
                  & ((string)adress.Element("ort") == ort)
                  & ((string)adress.Element("land") == land))
                {
                    return true;
                }
            }
            return false;
        }
        private void loadAddressFile()
        {
            try
            {
                AddressXML = XElement.Load(speicherort + "address.xml");
            }
            catch
            {
                AddressXML = new XElement("addresses");
                AddressXML.SetAttributeValue("lastUsedID", 0);
                AddressXML.Save(speicherort + "address.xml");
            }
        }
        private void saveAddressFile()
        {
            AddressXML.Save(speicherort + "address.xml");
        }
        

        // BenutzerAdressen-Zugriff
        public Boolean insertUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, Boolean rech_addr, Boolean lief_addr)
        {
            loadUserAddressFile();
            if (!useraddressExists(user_id, address_id))
            {
                XElement useraddress = new XElement("user_address");
                useraddress.Add(new XElement("user_id", user_id));
                useraddress.Add(new XElement("address_id", address_id));
                useraddress.Add(new XElement("vname", vname));
                useraddress.Add(new XElement("nname", nname));
                useraddress.Add(new XElement("addr_zusatz", addr_zusatz));
                useraddress.Add(new XElement("rech_addr", rech_addr));
                useraddress.Add(new XElement("lief_addr", lief_addr));

                UserAddressXML.Add(useraddress);

                saveUserAddressFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean updateUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, Boolean rech_addr, Boolean lief_addr)
        {
            loadUserAddressFile();
            if (useraddressExists(user_id, address_id))
            {
                XElement useraddress = null;
                IEnumerable<XElement> useraddresses =
                    from el in UserAddressXML.Elements("user_address")
                    where (int)el.Element("user_id") == user_id & (int)el.Element("address_id") == address_id
                    select el;

                foreach (XElement el in useraddresses) { useraddress = el; }

                useraddress.Remove();
                useraddress.SetElementValue("vname", vname);
                useraddress.SetElementValue("nname", nname);
                useraddress.SetElementValue("addr_zusatz", addr_zusatz);
                useraddress.SetElementValue("rech_addr", rech_addr);
                useraddress.SetElementValue("lief_addr", lief_addr);
                UserAddressXML.Add(useraddress);

                saveUserAddressFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean deleteUserAddress(int user_id, int address_id)
        {
            Boolean status = false;
            loadUserAddressFile();
            if (user_id != 0 & address_id != 0)
            {
                if (useraddressExists(user_id, address_id))
                {
                    XElement useraddress = null;
                    IEnumerable<XElement> useraddresses =
                        from el in AddressXML.Elements("user_address")
                        where (int)el.Element("user_id") == user_id & (int)el.Element("address_id") == address_id
                        select el;

                    foreach (XElement el in useraddresses) { useraddress = el; }

                    useraddress.Remove();

                    saveUserAddressFile();
                    status = true;
                }
            }
            else if (user_id == 0 & address_id != 0)
            {
                IEnumerable<XElement> useraddresses =
                        from el in AddressXML.Elements("user_address")
                        where (int)el.Element("address_id") == address_id
                        select el;

                foreach (XElement el in useraddresses) { el.Remove(); }

                saveUserAddressFile();
                status = true;
            }
            else if (user_id != 0 & address_id == 0)
            {
                IEnumerable<XElement> useraddresses =
                        from el in AddressXML.Elements("user_address")
                        where (int)el.Element("user_id") == user_id
                        select el;

                foreach (XElement el in useraddresses) { el.Remove(); }

                saveUserAddressFile();
                status = true;
            }

            return status;

        }
        public BenutzerAdresse getUserAddress(int user_id, int address_id)
        {
            loadUserAddressFile();
            if (useraddressExists(user_id, address_id))
            {
                XElement useraddress = null;
                IEnumerable<XElement> useraddresses =
                    from el in UserAddressXML.Elements("user_address")
                    where (int)el.Element("user_id") == user_id & (int)el.Element("address_id") == address_id
                    select el;

                foreach (XElement el in useraddresses) { useraddress = el; }

                Adresse adresse = getAddress(address_id);

                BenutzerAdresse benutzeradresse = new BenutzerAdresse(
                    (bool)useraddress.Element("rech_addr"),
                    (bool)useraddress.Element("lief_addr"),
                    (string)useraddress.Element("vname"),
                    (string)useraddress.Element("nname"),
                    (string)useraddress.Element("addr_zusatz"),
                    user_id,
                    adresse);

                return benutzeradresse;
            }
            else
            {
                return null;
            }
        }
        private Boolean useraddressExists(int user_id, int address_id)
        {
            loadUserAddressFile();
            IEnumerable<XElement> useraddressList = null;
            try {
                useraddressList = UserAddressXML.Elements();
            } catch { return true; }
            
            foreach (XElement useraddress in useraddressList)
            {
                if (((int)useraddress.Element("user_id") == user_id) & ((int)useraddress.Element("address_id") == address_id))
                {
                    return true;
                }
            }
            return false;
        }
        private void loadUserAddressFile()
        {
            try
            {
                UserAddressXML = XElement.Load(speicherort + "user_address.xml");
            }
            catch
            {
                UserAddressXML = new XElement("useraddresses");
                UserAddressXML.Save(speicherort + "user_address.xml");
            }
        }
        private void saveUserAddressFile()
        {
            UserAddressXML.Save(speicherort + "user_address.xml");
        }
        private Boolean deleteUserAddressByUserID(int user_id)
        {
            return deleteUserAddress(user_id, 0);
        }
        private Boolean deleteUserAddressByAddressID(int address_id)
        {
            return deleteUserAddress(0, address_id);
        }


        //Artikel-Zugriff
        public Boolean insertItem(string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id)
        {
            loadItemFile();
            int new_id = (int)ItemXML.Attribute("lastUsedID") + 1;
            if (!itemExists(new_id))
            {
                XElement item = new XElement("item");
                item.Add(new XElement("id", new_id));
                item.Add(new XElement("name", name));
                item.Add(new XElement("kurzbeschr", kurzbeschr));
                item.Add(new XElement("langbeschr", langbeschr));
                item.Add(new XElement("ablaufdatum", ablaufdatum));
                item.Add(new XElement("hoechstgebot", hoechstgebot));
                item.Add(new XElement("bieter_id", bieter_id));
                item.Add(new XElement("anbieter_id", anbieter_id));


                ItemXML.Add(item);
                ItemXML.SetAttributeValue("lastUsedID", new_id);

                saveItemFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean updateItem(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id)
        {
            loadItemFile();
            if (itemExists(id))
            {
                XElement item = null;
                IEnumerable<XElement> items =
                    from el in ItemXML.Elements("item")
                    where (int)el.Element("id") == id
                    select el;

                foreach (XElement el in items) { item = el; }

                item.Remove();
                item.SetElementValue("name", name);
                item.SetElementValue("kurzbeschr", kurzbeschr);
                item.SetElementValue("langbeschr", langbeschr);
                item.SetElementValue("ablaufdatum", ablaufdatum);
                item.SetElementValue("hoechstgebot", hoechstgebot);
                item.SetElementValue("bieter_id", bieter_id);
                item.SetElementValue("anbieter_id", anbieter_id);
                ItemXML.Add(item);

                saveItemFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean deleteItem(int id)
        {
            loadItemFile();
            if (itemExists(id))
            {
                XElement item = null;
                IEnumerable<XElement> items =
                    from el in ItemXML.Elements("item")
                    where (int)el.Element("id") == id
                    select el;

                foreach (XElement el in items) { item = el; }

                item.Remove();

                saveItemFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Artikel getItem(int id)
        {
            loadItemFile();
            if (itemExists(id))
            {
                XElement item = null;
                IEnumerable<XElement> items =
                    from el in ItemXML.Elements("item")
                    where (int)el.Element("id") == id
                    select el;
                foreach (XElement el in items) { item = el; }

                Artikel artikel = new Artikel(
                    (int)item.Element("id"),
                    (string)item.Element("name"),
                    (string)item.Element("kurzbeschr"),
                    (string)item.Element("langbeschr"),
                    (DateTime)item.Element("ablaufdatum"),
                    (double)item.Element("hoechstgebot"),
                    (int)item.Element("bieter_id"),
                    (int)item.Element("anbieter_id"));

                return artikel;
            }
            else
            {
                return null;
            }
        }
        public List<Artikel> getItemList()
        {
            try {
                loadItemFile();
                List<Artikel> itemList = null;
                IEnumerable<XElement> items =
                    from el in ItemXML.Elements("item")
                    select el;
                foreach (XElement el in items)
                {
                    itemList.Add(getItem((int)el.Element("id")));
                }
                return itemList;
            }
            catch { return null; }

        }
        private Boolean itemExists(int id)
        {
            loadItemFile();
            IEnumerable<XElement> itemList = null;
            try
            {
                itemList = ItemXML.Elements();
            }
            catch { return true; }

            foreach (XElement item in itemList)
            {
                if ((int)item.Element("id") == id)
                {
                    return true;
                }
            }
            return false;
        }
        private void loadItemFile()
        {
            try
            {
                ItemXML = XElement.Load(speicherort + "item.xml");
            }
            catch
            {
                ItemXML = new XElement("items");
                ItemXML.SetAttributeValue("lastUsedID", 0);
                ItemXML.Save(speicherort + "item.xml");
            }
        }
        private void saveItemFile()
        {
            ItemXML.Save(speicherort + "item.xml");
        }
        private Boolean deleteItemByVendorID(int vendor_id)
        {
            loadItemFile();

            IEnumerable<XElement> items =
                from el in ItemXML.Elements("item")
                where (int)el.Element("anbieter_id") == vendor_id
                select el;

            foreach (XElement el in items)
            {
                el.Remove();
            }

            saveItemFile();
            return true;

        }
    }
}
