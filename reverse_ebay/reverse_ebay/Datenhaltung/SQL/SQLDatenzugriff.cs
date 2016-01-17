using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace reverse_ebay
{
    class SQLDatenzugriff : IDatenhaltung
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;

        public SQLDatenzugriff()
        {
            
        }

        // SQL Verbindung öffnen / schließen
        private void openSQL()
        {
            sqlConnection = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Initial Catalog=ReverseEbaySQL;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void closeSQL()
        {
            try
            {
                sqlCommand = null;
                sqlDataReader = null;
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Benutzer-Zugriff
        public int insertUser(string name, string passwort)
        {
            try
            {
                int new_id = 0;

                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.NVarChar);
                param1.Value = name;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = passwort;

                openSQL();

                sqlCommand = new SqlCommand("INSERT INTO [dbo].[T_Benutzer] (name, passwort)" +
                                            "VALUES (@param1, @param2);", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.ExecuteNonQuery();
                
                sqlCommand = new SqlCommand("SELECT @@IDENTITY AS 'new_id';", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        new_id = Convert.ToInt32(sqlDataReader["new_id"]);
                    }
                }
                
                closeSQL();

                return new_id;
            } catch {  return 0; }

        }
        public bool updateUser(int id, string name, string passwort)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = name;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = passwort;

                openSQL();

                sqlCommand = new SqlCommand("UPDATE [dbo].[T_Benutzer] SET [name] = @param2, [passwort] = @param3 WHERE[id] = @param1; ", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            } catch { return false; }

        }
        public bool deleteUser(int id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;

                openSQL();

                sqlCommand = new SqlCommand("DELETE FROM [dbo].[T_Benutzer] WHERE [id] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public Benutzer getUser(int id)
        {
            try
            {
                List<BenutzerAdresse> tempUserAddressList = new List<BenutzerAdresse>();
                foreach (BenutzerAdresse useradress in getUserAdressList())
                {
                    if (useradress.benutzer_id == id)
                    {
                        tempUserAddressList.Add(useradress);
                    }
                }

                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;

                openSQL();

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_Benutzer] WHERE [id] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);

                sqlDataReader = sqlCommand.ExecuteReader();
                Benutzer tempUser = null;
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {                        
                        tempUser = new Benutzer((int)sqlDataReader["id"],
                                                ((string)sqlDataReader["name"]).Trim(),
                                                ((string)sqlDataReader["passwort"]).Trim(),
                                                tempUserAddressList);
                    }
                }
                else

                closeSQL();

                return tempUser;
            }
            catch { return null; }
        }
        public Benutzer getUser(string name)
        {
            try
            {
                return getUser(getUserIDbyName(name));
            }
            catch { return null; }
        }
        public List<Benutzer> getUserList()
        {
            List<Benutzer> userList = new List<Benutzer>();
            try
            {
                openSQL();

                sqlCommand = new SqlCommand("SELECT [id] FROM [dbo].[T_Benutzer];", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        userList.Add(getUser((int)sqlDataReader["id"]));
                    }
                }

                closeSQL();

                return userList;
            }
            catch { return userList; }
        }
        private int getUserIDbyName(string name)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.NVarChar);
                param1.Value = name;

                openSQL();

                sqlCommand = new SqlCommand("SELECT [id] FROM [dbo].[T_Benutzer] WHERE [name] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);

                sqlDataReader = sqlCommand.ExecuteReader();
                int user_id = 0;
                while (sqlDataReader.Read())
                {
                    user_id = (int)sqlDataReader["id"];
                }

                closeSQL();

                return user_id;
            }
            catch { return 0; }
        }


        // Adressen-Zugriff
        public int insertAddress(string str_nr, string plz, string ort, string land)
        {
            try
            {
                int new_id = 0;

                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.NVarChar);
                param1.Value = str_nr;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = plz;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = ort;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.NVarChar);
                param4.Value = land;

                openSQL();

                sqlCommand = new SqlCommand("INSERT INTO [dbo].[T_Adresse] (str_nr, plz, ort, land)" +
                                            "VALUES(@param1, @param2, @param3, @param4);", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand("SELECT @@IDENTITY AS 'new_id';");
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    new_id = Convert.ToInt32(sqlDataReader["new_id"]);
                }

                closeSQL();

                closeSQL();

                return new_id;
            }
            catch { return 0; }
        }
        public bool updateAddress(int id, string str_nr, string plz, string ort, string land)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = str_nr;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = plz;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.NVarChar);
                param4.Value = ort;
                SqlParameter param5 = new SqlParameter("@param5", SqlDbType.NVarChar);
                param5.Value = land;

                openSQL();

                sqlCommand = new SqlCommand("UPDATE [dbo].[T_Adresse]" +
                                            "SET [str_nr] = @param2, [plz] = @param3, [ort] = @param4, [land] = @param5" +
                                            "WHERE[id] = @param1; ", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.Parameters.Add(param5);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public bool deleteAddress(int id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;

                openSQL();

                sqlCommand = new SqlCommand("DELETE FROM [dbo].[T_Adresse] WHERE [id] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public Adresse getAddress(int id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;

                openSQL();

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_Adresse] WHERE [id] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.ExecuteNonQuery();

                sqlDataReader = sqlCommand.ExecuteReader();
                Adresse tempAddress = null;
                while (sqlDataReader.Read())
                {
                    tempAddress = new Adresse((int)sqlDataReader["id"],
                                              ((string)sqlDataReader["str_nr"]).Trim(),
                                              ((string)sqlDataReader["plz"]).Trim(),
                                              ((string)sqlDataReader["ort"]).Trim(),
                                              ((string)sqlDataReader["land"]).Trim());
                }

                closeSQL();

                return tempAddress;
            }
            catch { return null; }
        }
        public Adresse getAddress(string str_nr, string plz, string ort, string land)
        {
            try
            {
                return getAddress(getAddressIDbyAddressData(str_nr, plz, ort, land));
            }
            catch { return null; }
        }
        public List<Adresse> getAddressList()
        {
            List<Adresse> addressList = new List<Adresse>();
            try
            {
                openSQL();

                sqlCommand = new SqlCommand("SELECT [id] FROM [dbo].[T_Adresse];", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    addressList.Add(getAddress((int)sqlDataReader["id"]));
                }

                closeSQL();

                return addressList;
            }
            catch { return addressList; }
        }
        private int getAddressIDbyAddressData(string str_nr, string plz, string ort, string land)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.NVarChar);
                param1.Value = str_nr;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = plz;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = ort;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.NVarChar);
                param4.Value = land;

                openSQL();

                sqlCommand = new SqlCommand("SELECT [id] FROM [dbo].[T_Adresse]" +
                                            "WHERE [str_nr] = @param1" +
                                            "AND [plz] = @param2" +
                                            "AND [ort] = @param3" +
                                            "AND [land] = @param4;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlDataReader = sqlCommand.ExecuteReader();
                int user_id = 0;
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        user_id = (int)sqlDataReader["id"];
                    }
                }

                closeSQL();

                return user_id;
            }
            catch { return 0; }
        }


        // BenutzerAdressen-Zugriff
        public bool insertUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, bool rech_addr, bool lief_addr)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = user_id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.Int);
                param2.Value = address_id;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = vname;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.NVarChar);
                param4.Value = nname;
                SqlParameter param5 = new SqlParameter("@param5", SqlDbType.NVarChar);
                param5.Value = addr_zusatz;
                SqlParameter param6 = new SqlParameter("@param6", SqlDbType.Bit);
                param6.Value = rech_addr;
                SqlParameter param7 = new SqlParameter("@param7", SqlDbType.Bit);
                param7.Value = lief_addr;

                openSQL();

                sqlCommand = new SqlCommand("INSERT INTO [dbo].[T_BenutzerAdresse] (benutzer_id, adresse_id, vname, nname, addr_zusatz, rech_addr, lief_addr)" +
                                            "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7);", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.Parameters.Add(param5);
                sqlCommand.Parameters.Add(param6);
                sqlCommand.Parameters.Add(param7);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public bool updateUserAddress(int user_id, int address_id, string vname, string nname, string addr_zusatz, bool rech_addr, bool lief_addr)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = user_id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.Int);
                param2.Value = address_id;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = vname;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.NVarChar);
                param4.Value = nname;
                SqlParameter param5 = new SqlParameter("@param5", SqlDbType.NVarChar);
                param5.Value = addr_zusatz;
                SqlParameter param6 = new SqlParameter("@param6", SqlDbType.Bit);
                param6.Value = rech_addr;
                SqlParameter param7 = new SqlParameter("@param7", SqlDbType.Bit);
                param7.Value = lief_addr;

                openSQL();

                sqlCommand = new SqlCommand("UPDATE [dbo].[T_BenutzerAdresse]" +
                                            "SET [vname] = @param3, [nname] = @param4, [addr_zusatz] = @param5, [rech_addr] = @param6, [lief_Addr] = @param7" +
                                            "WHERE [benutzer_id] = @param1 AND [adresse_id] = @param2; ", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.Parameters.Add(param5);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public bool deleteUserAddress(int user_id, int address_id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = user_id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.Int);
                param2.Value = address_id;

                openSQL();

                sqlCommand = new SqlCommand("DELETE FROM [dbo].[T_BenutzerAdresse] WHERE [benutzer_id] = @param1 AND [adresse_id] = @param2;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public BenutzerAdresse getUserAddress(int user_id, int address_id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = user_id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.Int);
                param2.Value = address_id;

                openSQL();

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_Adresse] WHERE [id] = @param2;", sqlConnection);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.ExecuteNonQuery();
                Adresse tempAddress = null;
                while (sqlDataReader.Read())
                {
                    tempAddress = new Adresse((int)sqlDataReader["id"],
                                              ((string)sqlDataReader["str_nr"]).Trim(),
                                              ((string)sqlDataReader["plz"]).Trim(),
                                              ((string)sqlDataReader["ort"]).Trim(),
                                              ((string)sqlDataReader["land"]).Trim());
                }

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_BenutzerAdresse] WHERE [benutzer_id] = @param1 AND [adresse_id] = @param2;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.ExecuteNonQuery();

                sqlDataReader = sqlCommand.ExecuteReader();
                BenutzerAdresse tempUserAddress = null;
                while (sqlDataReader.Read())
                {
                    tempUserAddress = new BenutzerAdresse((bool)sqlDataReader["rech_addr"],
                                                          (bool)sqlDataReader["lief_addr"],
                                                          ((string)sqlDataReader["vname"]).Trim(),
                                                          ((string)sqlDataReader["nname"]).Trim(),
                                                          ((string)sqlDataReader["addr_zusatz"]).Trim(),
                                                          (int)sqlDataReader["benutzer_id"],
                                                          tempAddress);
                }

                closeSQL();

                return tempUserAddress;
            }
            catch { return null; }
        }
        public List<BenutzerAdresse> getUserAdressList()
        {
            List<BenutzerAdresse> userAddressList = new List<BenutzerAdresse>();
            try
            {
                openSQL();

                sqlCommand = new SqlCommand("SELECT [benutzer_id], [adresse_id] FROM [dbo].[T_BenutzerAdresse];", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                        userAddressList.Add(getUserAddress((int)sqlDataReader["benutzer_id"], (int)sqlDataReader["adresse_id"]));
                }

                closeSQL();

                return userAddressList;
            }
            catch { return userAddressList; }
        }


        //Artikel-Zugriff
        public int insertItem(string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id)
        {
            try
            {
                int new_id = 0;

                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.NVarChar);
                param1.Value = name;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = kurzbeschr;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.Text);
                param3.Value = langbeschr;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.DateTime);
                param4.Value = ablaufdatum;
                SqlParameter param5 = new SqlParameter("@param5", SqlDbType.Decimal);
                param5.Value = hoechstgebot;
                SqlParameter param6 = new SqlParameter("@param6", SqlDbType.Int);
                param6.Value = bieter_id;
                SqlParameter param7 = new SqlParameter("@param7", SqlDbType.Int);
                param7.Value = anbieter_id;

                openSQL();

                sqlCommand = new SqlCommand("INSERT INTO [dbo].[T_Artikel] (name, kurzbeschr, langbeschr, ablaufdatum, hoechstgebot, bieter_id, anbieter_id)" +
                                            "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7);", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.Parameters.Add(param5);
                sqlCommand.Parameters.Add(param6);
                sqlCommand.Parameters.Add(param7);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand("SELECT @@IDENTITY AS 'new_id';");
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    new_id = Convert.ToInt32(sqlDataReader["new_id"]);
                }

                closeSQL();

                return new_id;
            }
            catch { return 0; }
        }
        public bool updateItem(int id, string name, string kurzbeschr, string langbeschr, DateTime ablaufdatum, double hoechstgebot, int bieter_id, int anbieter_id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;
                SqlParameter param2 = new SqlParameter("@param2", SqlDbType.NVarChar);
                param2.Value = name;
                SqlParameter param3 = new SqlParameter("@param3", SqlDbType.NVarChar);
                param3.Value = kurzbeschr;
                SqlParameter param4 = new SqlParameter("@param4", SqlDbType.Text);
                param4.Value = langbeschr;
                SqlParameter param5 = new SqlParameter("@param5", SqlDbType.DateTime);
                param5.Value = ablaufdatum;
                SqlParameter param6 = new SqlParameter("@param6", SqlDbType.Decimal);
                param6.Value = hoechstgebot;
                SqlParameter param7 = new SqlParameter("@param7", SqlDbType.Int);
                param7.Value = bieter_id;
                SqlParameter param8 = new SqlParameter("@param8", SqlDbType.Int);
                param8.Value = anbieter_id;

                openSQL();

                sqlCommand = new SqlCommand("UPDATE [dbo].[T_Artikel]" +
                                            "SET [name] = @param2, [kurzbeschr] = @param3, [langbeschr] = @param4, [ablaufdatum] = @param5, [hoechstgebot] = @param6, [bieter_id] = @param7, [anbieter_id] = @param8" +
                                            "WHERE[id] = @param1; ", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.Parameters.Add(param5);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public bool deleteItem(int id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;

                openSQL();

                sqlCommand = new SqlCommand("DELETE FROM [dbo].[T_Artikel] WHERE [id] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.ExecuteNonQuery();

                closeSQL();

                return true;
            }
            catch { return false; }
        }
        public Artikel getItem(int id)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@param1", SqlDbType.Int);
                param1.Value = id;

                openSQL();

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_Artikel] WHERE [id] = @param1;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.ExecuteNonQuery();

                sqlDataReader = sqlCommand.ExecuteReader();
                Artikel tempItem = null;
                while (sqlDataReader.Read())
                {
                    tempItem = new Artikel((int)sqlDataReader["id"],
                                           ((string)sqlDataReader["name"]).Trim(),
                                           ((string)sqlDataReader["kurzbeschr"]).Trim(),
                                           ((string)sqlDataReader["langbeschr"]).Trim(),
                                           (DateTime)sqlDataReader["ablaufdatum"],
                                           (double)sqlDataReader["hoechstgebot"],
                                           (int)sqlDataReader["bieter_id"],
                                           (int)sqlDataReader["anbieter_id"]);
                }

                closeSQL();

                return tempItem;
            }
            catch { return null; }
        }
        public List<Artikel> getItemList()
        {
            List<Artikel> itemList = new List<Artikel>();
            try
            {
                openSQL();

                sqlCommand = new SqlCommand("SELECT [id] FROM [dbo].[T_Artikel];", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    itemList.Add(getItem((int)sqlDataReader["id"]));
                }

                closeSQL();

                return itemList;
            }
            catch { return itemList; }
        }
    }
}
