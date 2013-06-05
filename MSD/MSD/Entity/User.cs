using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;
using MySql.Data.MySqlClient;

namespace MSD.Entity
{
    public class User : PropertyChangedBase, IDataErrorInfo
    {
        private string _naam;

        public string Name
        {
            get { return _naam; }
            set {   _naam = value;
                this.OnPropertyChanged("Naam");
            }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value;
                this.OnPropertyChanged("Email");
            }
        }

        static readonly string[] ValidatedProp =
        {
            "Name",
            "Email",
        };

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }

        /// <summary>
        /// returned true als object geen errors heeft
        /// </summary>
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
            if (Array.IndexOf(ValidatedProp, propertyName) < 0)
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
                    error = this.ValidateEmail();
                    break;
                default:
                    break;
            }
            return error;
        }
        //TODO: controleren van alle properties
        private string ValidateName()
        {
            if (IsStringMissing(_naam))
            {
                return "Naam moet worden ingevuld";
            }
            return "";
        }

        private string ValidateEmail()
        {
            if (IsStringMissing(_email))
            {
                return "Email moet worden ingevuld";
            }
            return "";
        }


        /// <summary>
        /// Kijk of de string leeg is
        /// </summary>
        /// <param name="value">Waarde die moet worden gecontroleerd</param>
        /// <returns></returns>
        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }
    }
}
