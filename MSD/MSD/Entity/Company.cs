using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Entity
{
    class Company : PropertyChangedBase, IDataErrorInfo
    {
        private string _company;
        private string _website;
        private string _adress;
        private string _city;
        private string _zip;
        private string _contact;
        private string _email;
        private string _phone;
        private string _branch;
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


        public string Companyname
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

        public string Error
        {
            get { return "Er is een fout opgetreden"; }
        }
        public string this[string columnName]
        {
            get
            {
                return this.GetResult(columnName);
            }
        }

        private string GetResult(string columnName)
        {
            PropertyInfo info = this.GetType().GetProperty(columnName);
            if (info != null)
            {
                string value= info.GetValue(this, null) as string;
                if (string.IsNullOrEmpty(value))
                    return string.Format("{0} moet worden ingevuld", info.Name);
                else if(value.Length > 0 || value.Length < 45  )
                    return string.Format("{0} moet tussen de 0 en 45 karakters bevatten", info.Name);
            }
            return null;
        }
    }
}
