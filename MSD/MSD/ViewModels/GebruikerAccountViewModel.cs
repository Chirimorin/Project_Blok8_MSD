using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private bool _editing;
        private string _password;
        private string _repeatPassword;

        private User _user;
        private string _mailOld;

        public GebruikerAccountViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Opslaan);
            _terugCommand = new RelayCommand(Terug);
            _database = ModelFactory.Database;
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        /// <summary>
        /// Opslaan van gegevens
        /// </summary>
        /// <param name="command"></param>
        public void Opslaan(object command)
        {
            if (Name != "" && Email != "" && (Password != "" || Editing))
            {
                if (Password == RepeatPassword)
                {
                    //encrypt het wachtwoord
                    string passwordMD5 = MD5Encryptor.ConvertString(Password);
                    string query;
                    if (Editing)
                    {
                        if (Password == "")
                        {
                            query = "UPDATE gebruiker SET Naam = '" + Name + "', Mailadres = '" + Email + "' WHERE Mailadres = '" + _mailOld + "';";
                        }
                        else
                        {
                            query = "UPDATE gebruiker SET Naam = '" + Name + "', Wachtwoord = '" + passwordMD5 + "', Mailadres = '" + Email + "' WHERE Mailadres = '" + _mailOld + "';";
                        }
                        Debug.WriteLine(query);
                    }
                    else
                    {
                        query = "INSERT INTO gebruiker (mailadres,wachtwoord,naam) VALUES('" + Email + "','" + passwordMD5 + "','" + Name + "');";
                    }
                    MySqlCommand mycommand = new MySqlCommand(query);
                    _database.setData(mycommand);
                    _app.ShowGebruikerView();
                }
                else
                {
                    MessageBox.Show("Wachtwoorden komen niet overeen");
                }
            }
            else
            {
                string message = "De volgende gegevens zijn niet ingevuld: \n";
                if (Name == "")
                {
                    message += " - Naam \n";
                }
                if (Email == "")
                {
                    message += " - Email \n";
                }
                if (Password == "")
                {
                    message += " - Wachtwoord \n";
                }

                MessageBox.Show(message);
            }
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowGebruikerView();
        }

        public User User
        {
            get { return _user; }
            set 
            {
                _user = value;
                _mailOld = _user.Email;
            }
        }

        public bool Editing
        {
            get { return _editing; }
            set 
            {
                _editing = value;
                OnPropertyChanged(Title);
            }
        }

        public string Title
        {
            get
            {
                if (Editing) return "Gebruiker Aanpassen";
                else return "Nieuwe Gebruiker";
            }
        }

        
        public string Name
        {
            get
            {
                if (User.Name == null) return "";
                return User.Name;
            }
            set
            {
                User.Name = value;
                OnPropertyChanged("Name");
            }
        }

        
        public string Email
        {
            get
            {
                if (User.Email == null) return "";
                return User.Email;
            }
            set
            {
                User.Email = value;
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
