using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using MSD.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace MSD.ViewModels
{
    class LogInViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _ShowMainWindow;
        private readonly RelayCommand _WachtwoordVergeten;
        private Database _database;
        private string _email;

        private PasswordBox _passwordBox;

        public LogInViewModel(IApplicationController app)
        {
            _app = app;
            this._ShowMainWindow = new RelayCommand(ShowMainWindow);
            this._WachtwoordVergeten = new RelayCommand(ShowWachtwoordView);
            _database = ModelFactory.Database;
        }


        public PasswordBox PasswordBox
        {
            get { return _passwordBox; }
            set { _passwordBox = value; }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get
            {
                return _passwordBox.Password;
            }
        }

        public RelayCommand ShowMainWindowCommand { get { return _ShowMainWindow; } }

        public void ShowMainWindow(object command)
        {
            string query = "SELECT mailadres,wachtwoord,naam FROM gebruiker WHERE '" + _email + "' = mailadres;";
            DataTable data = new DataTable();
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = _database.getData(mycommand);
            adapter.Fill(data);
            //als er waardes terug komen
            if (data.Rows.Count != 0)
            {
                //kijkt of het ingevoerde wachtwoord juist is
                string wachtwoord = data.Rows[0][1].ToString();
                if (Password != null)
                {
                    if (MD5Encryptor.CompareString(Password, wachtwoord))
                    {
                        //set de username die bij het emailadres hoort
                        MainWindowModel mainWindowModel = (MainWindowModel)ViewFactory.getViewModel(_app, "mainWindowModel");
                        mainWindowModel.UserName = data.Rows[0][2].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Het ingevoerde wachtwoord is niet juist");
                    }
                }
                else
                {
                    MessageBox.Show("U heeft geen wachtwoord ingevoerd");
                }

            }
            else
            {
                MessageBox.Show("Het ingevoerde e-mailadres komt niet voor in ons bestand");
            }
        }

        public RelayCommand ShowWachtwoordVergeten { get { return _WachtwoordVergeten; } }

        public void ShowWachtwoordView(object command)
        {
            WachtwoordViewModel wachtwoordview = (WachtwoordViewModel)ViewFactory.getViewModel(_app, "wachtwoordViewModel");
        }
    }
}
