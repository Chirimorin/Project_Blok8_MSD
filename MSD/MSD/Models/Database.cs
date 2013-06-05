﻿using System;
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
        MySqlConnection myConnection = new MySqlConnection("Server=databases.aii.avans.nl;" + "Database=eavries_db2;" + "Uid=eavries;" + "Pwd=rd4qAS7j;");

        public MySqlDataAdapter getData(MySqlCommand cmd)
        {
            myConnection.Open();
            cmd.Connection = myConnection;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            //MySqlDataReader reader = cmd.ExecuteReader();
            myConnection.Close();
            return adapter;
        }

        public void setData(MySqlCommand cmd)
        {
            myConnection.Open();
            cmd.Connection = myConnection;
            cmd.ExecuteNonQuery();
            myConnection.Close();
        }
        

    }
}
