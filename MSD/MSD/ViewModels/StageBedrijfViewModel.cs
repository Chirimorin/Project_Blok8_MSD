using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    public class StageBedrijfViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _saveCommand;
        private readonly RelayCommand _backCommand;
        private Database _database;

        private string _company;
        private string _website;
        private string _adress;
        private string _city;
        private string _zip;
        private string _contact;
        private string _email;
        private string _phone;
        private string _branch;


        public StageBedrijfViewModel(IApplicationController app)
        {
            _app = app;
            _saveCommand = new RelayCommand(Save);
            _backCommand = new RelayCommand(Back);
            _database = ModelFactory.Database;
        }

        public RelayCommand SaveCommand { get { return _saveCommand; } }
        public void Save(object command)
        {
            string query = "INSERT INTO stagebedrijf(bedrijfnr,naam,plaats,straat,postcode,telefoonnr,website,contactpersoon,email,branch) VALUES(" + "," +_company + "," + _city + "," + _adress + "," + _zip + "," + _phone + "," + _website + "," + _contact + "," + _email + "," + _branch + ")";
            MySqlCommand mycommand = new MySqlCommand(query);
            _database.executeQuery(mycommand);
        }

        public RelayCommand BackCommand { get { return _backCommand; } }
        public void Back(object command)
        {
            _app.ShowBedrijfOverzichtView();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(Title);
            }
        }

        
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }

        
        public string Website
        {
            get
            {
                return _website;
            }
            set
            {
                _website = value;
                OnPropertyChanged("Website");
            }
        }

        
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged("Adress");
            }
        }

        
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                OnPropertyChanged("Zip");
            }
        }

        
        public string Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                OnPropertyChanged("Contact");
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

        
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        
        public string Branch
        {
            get
            {
                return _branch;
            }
            set
            {
                _branch = value;
                OnPropertyChanged("Branch");
            }
        }

    }
}
