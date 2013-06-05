using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Entity;
using System.Collections.ObjectModel;

namespace MSD.ViewModels
{
    public class StageBedrijfViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _saveCommand;
        private readonly RelayCommand _backCommand;
        private Database _database;

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; this.OnPropertyChanged("Title"); }
        }

        private string _company;

        public string Company
        {
            get { return _company; }
            set { _company = value; this.OnPropertyChanged("Company"); }
        }
        private string _website;

        public string Website
        {
            get { return _website; }
            set { _website = value; this.OnPropertyChanged("Website"); }
        }
        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set { _adress = value; this.OnPropertyChanged("Adress"); }
        }
        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; this.OnPropertyChanged("City"); }
        }
        private string _zip;

        public string Zip
        {
            get { return _zip; }
            set { _zip = value; this.OnPropertyChanged("Zip"); }
        }
        private string _contact;

        public string Contact
        {
            get { return _contact; }
            set { _contact = value; this.OnPropertyChanged("Contact"); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; this.OnPropertyChanged("Email"); }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; this.OnPropertyChanged("Phone"); }
        }
        private string _branch;

        public string Branch
        {
            get { return _branch; }
            set { _branch = value; this.OnPropertyChanged("Branch"); }
        }
        

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
            _database.getData(mycommand);
        }

        public RelayCommand BackCommand { get { return _backCommand; } }
        public void Back(object command)
        {
            _app.ShowBedrijfOverzichtView();
        }
    }
}
