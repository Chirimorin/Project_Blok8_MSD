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
        //Dit is een eerste opzet, SQL-Injection moet nog middels parameters of prepared-statements afgevangen worden.
        MySqlConnection myConnection = new MySqlConnection("Server=databases.aii.avans.nl;" + "Database=eavries_db2;" + "Uid=eavries;" + "Pwd=rd4qAS7j;");

        public Database()
        {
            //sqlConnection();
        }

        public void sqlConnection()
        {
            try
            {
                myConnection.Open();
                MySqlDataReader myReader = null;

                MySqlCommand myCommand = new MySqlCommand("SELECT * FROM student", myConnection);

                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {

                    MessageBox.Show("Studentnummer: " + myReader[0].ToString() + "\nNaam: " + myReader[1].ToString() + "\nAdres: " + myReader[2].ToString() + "\nWoonplaats: " + myReader[3].ToString());
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
