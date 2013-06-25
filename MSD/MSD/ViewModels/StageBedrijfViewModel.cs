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
using System.Diagnostics;
using System.Data;
using System.Windows;

namespace MSD.ViewModels
{
    public class StageBedrijfViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _saveCommand;
        private readonly RelayCommand _backCommand;
        private Database _database;

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
            if (!HasEmptyProperties())
            {
                string query = null;
                if (Wijzig == true)
                {
                    query = "UPDATE stagebedrijf SET naam = '" + CompanyName + "', plaats = '" + City + "', straat = '" + Adress + "', postcode = '" + Zip + "', telefoonnr = " + Phone + ", website = '" + Website + "', contactpersoon = '" + Contact + "', email = '" + Email + "', branch = '" + Branch + "' WHERE bedrijfnr = " + Company.ID;

                }
                if (Wijzig == false)
                {
                    int bedrijfnr = getexecuteQuery("SELECT MAX(bedrijfnr) FROM stagebedrijf") + 1;
                    query = "INSERT INTO stagebedrijf(bedrijfnr,naam,plaats,straat,postcode,telefoonnr,website,contactpersoon,email,branch) VALUES(" + bedrijfnr + ",'" + CompanyName + "','" + City + "','" + Adress + "','" + Zip + "'," + Phone + ",'" + Website + "','" + Contact + "','" + Email + "','" + Branch + "')";
                }
                if (query != null)
                {
                    executeQuery(query);
                }
                _app.ShowBedrijfOverzichtView();
            }
            
        }

        public bool HasEmptyProperties()
        {
            bool HasEmptyProperty = false;
            string message = "De volgende gegevens zijn niet ingevuld: \n";
            if (String.IsNullOrEmpty(CompanyName))
            {
                message += " - Naam \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(City))
            {
                message += " - Plaats \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Adress))
            {
                message += " - Adres \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Zip))
            {
                message += " - Postcode \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Phone))
            {
                message += " - Telefoonnr \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Website))
            {
                message += " - Website \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Contact))
            {
                message += " - Contactpersoon \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Email))
            {
                message += " - Emailadres \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Branch))
            {
                message += " - Branch \n";
                HasEmptyProperty = true;
            }
            if (HasEmptyProperty)
            {
                MessageBox.Show(message);
            }
            return HasEmptyProperty;
        }

        public void executeQuery(string query)
        {
            Debug.WriteLine(query);
            MySqlCommand mycommand = new MySqlCommand(query);
            mycommand = new MySqlCommand(query);
            _database.setData(mycommand);
        }
        public int getexecuteQuery(string query)
        {
            Debug.WriteLine(query);
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = _database.getData(mycommand);
            adapter.Fill(data);
            return (int)data.Rows[0][0];
        }

        public RelayCommand BackCommand { get { return _backCommand; } }
        public void Back(object command)
        {
            _app.ShowBedrijfOverzichtView();
        }

        private bool _wijzig;

        public bool Wijzig
        {
            get { return _wijzig; }
            set { _wijzig = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; this.OnPropertyChanged("Title"); }
        }

        private Company _company;

        public Company Company
        {
            get { return _company; }
            set { _company = value;}
        }
        public string CompanyName
        {
            get
            {
                if (Company != null) return Company.Companyname;
                else return "";
            }
            set { Company.Companyname = value; this.OnPropertyChanged("CompanyName"); }
        }

        public string Website
        {
            get
            {
                if (Company != null) return Company.Website;
                else return "";
            }
            set { Company.Website = value; this.OnPropertyChanged("Website"); }
        }

        public string Adress
        {
            get
            {
                if (Company != null) return Company.Adress;
                else return "";
            }
            set { Company.Adress = value; this.OnPropertyChanged("Adress"); }
        }

        public string City
        {
            get
            {
                if (Company != null) return Company.City;
                else return "";
            }
            set { Company.City = value; this.OnPropertyChanged("City"); }
        }

        public string Zip
        {
            get
            {
                if (Company != null) return Company.Zip;
                else return "";
            }
            set { Company.Zip = value; this.OnPropertyChanged("Zip"); }
        }

        public string Contact
        {
            get
            {
                if (Company != null) return Company.Contact;
                else return "";
            }
            set { Company.Contact = value; this.OnPropertyChanged("Contact"); }
        }

        public string Email
        {
            get
            {
                if (Company != null) return Company.Email;
                else return "";
            }
            set { Company.Email = value; this.OnPropertyChanged("Email"); }
        }

        public string Phone
        {
            get
            {
                if (Company != null) return Company.Phone;
                else return "";
            }
            set { Company.Phone = value; this.OnPropertyChanged("Phone"); }
        }

        public string Branch
        {
            get
            {
                if (Company != null) return Company.Branch;
                else return "";
            }
            set { Company.Branch = value; this.OnPropertyChanged("Branch"); }
        }
        

        
    }
}
