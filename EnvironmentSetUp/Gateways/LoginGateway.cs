using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace EnvironmentSetUp.Gateways
{
    public class LoginGateway
    {
        public bool Login(int Id, string pass)
        {
            string SQLQuery = "SELECT Password FROM NovaRoomRegistrationTest.User where NNumber like " + Convert.ToString(Id) + " and Password Like '" + Convert.ToString(pass) + "'";
            string output = "";
            using (MySqlConnection connection = new MySqlConnection(SQL_String.GetRDSConnectionString()))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQLQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output = reader.GetString(0);
                        }
                    }
                }

            }
            if (output != "")
                return true;
            else
                return false;
        }

    }
}
