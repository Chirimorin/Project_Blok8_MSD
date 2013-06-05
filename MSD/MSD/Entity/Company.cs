using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Entity
{
    //TODO alle entiteiten maken zoals deze..
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

        public string Companyname
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                OnPropertyChanged("Companyname");
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

        static readonly string[] ValidatedProp =
        {
            "Companyname",
            "Website",
            "Adress",
            "City",
            "Zip",
            "Contact",
            "Email",
            "Phone",
            "Branch",
        };

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }

        //Geeft true als dit object geen errors heeft
        public bool isValid
        {
            get
            {
                foreach (string prop in ValidatedProp)
                {
                    if (GetValidationError(prop) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //kijkt naar de propertyName bijv Companyname, 
        //valideerd deze met this.Validate{property} methode en geeft bijbehorende foutmelding
        private string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProp,propertyName)< 0)
            {
                return null;
            }

            string error = null;

            switch (propertyName)
            {
                case "Companyname":
                    error = this.ValidateName();
                    break;
                case "Website":
                    error = this.ValidateWebsite();
                    break;
                case "Adress":
                    error = this.ValidateAdress();
                    break;
                case "City":
                    error = this.ValidateCity();
                    break;
                case "Zip":
                    error = this.ValidateZip();
                    break;
                case "Contact": 
                    error = this.ValidateContact();
                    break;
                case "Email":
                    error = this.ValidateEmail();
                    break;
                case "Phone":
                    error = this.ValidatePhone();
                    break;
                case "Branch":
                    error = this.ValidateBranch();
                    break;
                default:
                    break;
            }
            return error;
        }
        //TODO: controleren van alle properties
        private string ValidateBranch()
        {
            throw new NotImplementedException();
        }

        private string ValidatePhone()
        {
            throw new NotImplementedException();
        }

        private string ValidateEmail()
        {
            throw new NotImplementedException();
        }

        private string ValidateContact()
        {
            throw new NotImplementedException();
        }

        private string ValidateZip()
        {
            throw new NotImplementedException();
        }

        private string ValidateCity()
        {
            throw new NotImplementedException();
        }

        private string ValidateAdress()
        {
            throw new NotImplementedException();
        }

        private string ValidateWebsite()
        {
            throw new NotImplementedException();
        }

        private string ValidateName()
        {
            throw new NotImplementedException();
        }

        //Kijkt of er wel wat is ingevuld
        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }
        /// <summary>
        /// Kijkt of het emailadres valid is door middel van Regex
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        static bool isValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
            {
                return false;
            }
            string legalpattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            return Regex.IsMatch(email, legalpattern, RegexOptions.IgnoreCase);
        }
    }
}
