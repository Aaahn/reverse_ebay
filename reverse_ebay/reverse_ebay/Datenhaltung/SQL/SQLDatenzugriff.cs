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
                sqlCommand.Parameters.Clear();
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
                List<BenutzerAdresse> test = getUserAdressList();
                foreach (BenutzerAdresse useradress in test)
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
            catch (Exception e) { Console.WriteLine(e.ToString()); return null; }
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
                List<int> idList = new List<int>();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        idList.Add(Convert.ToInt32(sqlDataReader.GetValue(0)));
                    }
                }

                closeSQL();

                foreach (int id in idList)
                {
                    userList.Add(getUser(id));
                }

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

                sqlCommand = new SqlCommand("SELECT @@IDENTITY AS 'new_id';", sqlConnection);
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
                List<int> idList = new List<int>();
                while (sqlDataReader.Read())
                {
                    idList.Add(Convert.ToInt32(sqlDataReader.GetValue(0)));
                }

                closeSQL();

                foreach (int id in idList)
                {
                    addressList.Add(getAddress(id));
                }

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
            catch (Exception e) { Console.WriteLine(e.ToString()); return false; }
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
                sqlDataReader = sqlCommand.ExecuteReader();
                Adresse tempAddress = null;
                while (sqlDataReader.Read())
                {
                    tempAddress = new Adresse(Convert.ToInt32(sqlDataReader["id"]),
                                              Convert.ToString(sqlDataReader["str_nr"]).Trim(),
                                              Convert.ToString(sqlDataReader["plz"]).Trim(),
                                              Convert.ToString(sqlDataReader["ort"]).Trim(),
                                              Convert.ToString(sqlDataReader["land"]).Trim());
                }

                closeSQL();

                openSQL();

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_BenutzerAdresse] WHERE [benutzer_id] = @param1 AND [adresse_id] = @param2;", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);

                sqlDataReader = sqlCommand.ExecuteReader();
                BenutzerAdresse tempUserAddress = null;
                while (sqlDataReader.Read())
                {
                    tempUserAddress = new BenutzerAdresse(Convert.ToBoolean(sqlDataReader["rech_addr"]),
                                                          Convert.ToBoolean(sqlDataReader["lief_addr"]),
                                                          Convert.ToString(sqlDataReader["vname"]).Trim(),
                                                          Convert.ToString(sqlDataReader["nname"]).Trim(),
                                                          Convert.ToString(sqlDataReader["addr_zusatz"]).Trim(),
                                                          Convert.ToInt32(sqlDataReader["benutzer_id"]),
                                                          tempAddress);
                }

                closeSQL();

                return tempUserAddress;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); return null; }
        }
        public List<BenutzerAdresse> getUserAdressList()
        {
            try
            {
                List<BenutzerAdresse> userAddressList = new List<BenutzerAdresse>();
                openSQL();

                sqlCommand = new SqlCommand("SELECT [benutzer_id], [adresse_id] FROM [dbo].[T_BenutzerAdresse];", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                List<int> userIDList = new List<int>();
                List<int> addressIDList = new List<int>();
                while (sqlDataReader.Read())
                {
                    userIDList.Add(Convert.ToInt32(sqlDataReader.GetValue(0)));
                    addressIDList.Add(Convert.ToInt32(sqlDataReader.GetValue(1)));
                }

                closeSQL();

                foreach (int userID in userIDList)
                {
                    foreach (int addressID in addressIDList)
                    {
                        userAddressList.Add(getUserAddress(userID, addressID));
                    }
                }

                return userAddressList;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); return new List<BenutzerAdresse>(); }
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
                param6.Value = anbieter_id;

                openSQL();

                sqlCommand = new SqlCommand("INSERT INTO [dbo].[T_Artikel] (name, kurzbeschr, langbeschr, ablaufdatum, hoechstgebot, anbieter_id)" +
                                            "VALUES(@param1, @param2, @param3, @param4, @param5, @param6);", sqlConnection);
                sqlCommand.Parameters.Add(param1);
                sqlCommand.Parameters.Add(param2);
                sqlCommand.Parameters.Add(param3);
                sqlCommand.Parameters.Add(param4);
                sqlCommand.Parameters.Add(param5);
                sqlCommand.Parameters.Add(param6);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand("SELECT @@IDENTITY AS 'new_id';", sqlConnection);
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
                    int tempPurchID;
                    if (sqlDataReader["bieter_id"].Equals(null))
                    {
                        tempPurchID = Convert.ToInt32(sqlDataReader["bieter_id"]);
                    } else { tempPurchID = 0; }
                    tempItem = new Artikel(Convert.ToInt32(sqlDataReader["id"]),
                                           (Convert.ToString(sqlDataReader["name"])).Trim(),
                                           (Convert.ToString(sqlDataReader["kurzbeschr"])).Trim(),
                                           (Convert.ToString(sqlDataReader["langbeschr"])).Trim(),
                                           Convert.ToDateTime(sqlDataReader["ablaufdatum"]),
                                           Convert.ToDouble(sqlDataReader["hoechstgebot"]),
                                           tempPurchID,
                                           Convert.ToInt32(sqlDataReader["anbieter_id"]));
                }

                closeSQL();

                return tempItem;
            }
            catch { return null; }
        }
        public List<Artikel> getItemList()
        {
            try
            {
                List<Artikel> itemList = new List<Artikel>();
                openSQL();

                sqlCommand = new SqlCommand("SELECT * FROM [dbo].[T_Artikel];", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                List<int> idList = new List<int>();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        idList.Add(Convert.ToInt32(sqlDataReader.GetValue(0)));
                    }
                }

                closeSQL();

                foreach (int id in idList)
                {
                    itemList.Add(getItem(id));
                }

                return itemList;
            }
            catch { return new List<Artikel>(); }
        }
    }
}
