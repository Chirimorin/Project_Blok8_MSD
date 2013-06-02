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
    }
}
