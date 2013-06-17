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
    public class Database
    {
        //Dit is een eerste opzet, SQL-Injection moet nog middels parameters of prepared-statements afgevangen worden.
        MySqlConnection myConnection = new MySqlConnection("Server=databases.aii.avans.nl;" + "Database=eavries_db;" + "Uid=eavries;" + "Pwd=rd4qAS7j;");
        /// <summary>
        /// Gebruik dit om data uit de database te halen
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>returned een mysqlDataAdapter</returns>
        public MySqlDataAdapter getData(MySqlCommand cmd)
        {
            myConnection.Open();
            cmd.Connection = myConnection;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            //MySqlDataReader reader = cmd.ExecuteReader();
            myConnection.Close();
            return adapter;
        }
        /// <summary>
        /// Gebruik dit om data in de database te zetten(update of insert)
        /// </summary>
        /// <param name="cmd"></param>
        public void setData(MySqlCommand cmd)
        {
            myConnection.Open();
            cmd.Connection = myConnection;
            cmd.ExecuteNonQuery();
            myConnection.Close();
        }
        

    }
}
