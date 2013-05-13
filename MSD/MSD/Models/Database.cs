using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using MySql.Data.MySqlClient;

namespace MSD.Models
{
    class Database
    {
        MySqlConnection myConnection = new MySqlConnection("Server=databases.aii.avans.nl;" + "Database=eavries_db2;" + "Uid=eavries;" + "Pwd=rd4qAS7j;");

        public Database()
        {
            sqlConnection();
        }

        public void sqlConnection()
        {
            try
            {
                myConnection.Open();

                MySqlCommand myCommand = new MySqlCommand("SELECT * FROM docent", myConnection);

                MySqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    MessageBox.Show(myReader[0].ToString());
                }

                myConnection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
