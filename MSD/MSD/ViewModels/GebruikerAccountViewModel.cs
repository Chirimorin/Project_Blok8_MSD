using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MSD.ViewModels
{
    class GebruikerAccountViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;
        private readonly RelayCommand _terugCommand;
        private Database _database;

        private string _title;
        private string _name;
        private string _email;
        private string _password;
        private string _repeatPassword;

        public GebruikerAccountViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Opslaan);
            _terugCommand = new RelayCommand(Terug);
            _database = ModelFactory.Database;
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Opslaan(object command)
        {
            //opslaan van gegevens
            if (Password == RepeatPassword)
            {
                //encrypt het wachtwoord
                string password = MD5Encryptor.ConvertString(Password);
                //MessageBox.Show(password);
                string query = "INSERT INTO gebruiker (mailadres,wachtwoord,naam) VALUES('" + Email + "','" + password + "','" + Name + "');";
                MySqlCommand mycommand = new MySqlCommand(query);
                _database.setData(mycommand);

                _app.ShowGebruikerView();

            }
            else
            {
                MessageBox.Show("Wachtwoorden komen niet overeen");
            }
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowGebruikerView();
        }

        
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(Title);
            }
        }

        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
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
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        
        public string RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged("RepeatPassword");
            }
        }

    }
}
