using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Entity
{
    public class Student : PropertyChangedBase, IDataErrorInfo
    {
        private string _initials;

        public string Initials
        {
            get { return _initials; }
            set { _initials = value; }
        }
        private string _firstname;

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        private string _tussenvoegsel;

        public string Tussenvoegsel
        {
            get { return _tussenvoegsel; }
            set { _tussenvoegsel = value; }
        }
        private string _lastname;

        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
        private string _dateofbirth;

        public string Dateofbirth
        {
            get { return _dateofbirth; }
            set { _dateofbirth = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _enrolYear;

        public string EnrolYear
        {
            get { return _enrolYear; }
            set { _enrolYear = value; }
        }
        private string _graduationYear;

        public string GraduationYear
        {
            get { return _graduationYear; }
            set { _graduationYear = value; }
        }
        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private string _adres;

        public string Adres
        {
            get { return _adres; }
            set { _adres = value; }
        }
        private string _zip;

        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        static readonly string[] ValidatedProp =
        {
            "Initials",
            "Firstname",
            "Tussenvoegsel",
            "Lastname",
            "Dateofbirth",
            "Email",
            "Enrolyear",
            "Graduationyear",
            "City",
            "Adres",
            "Postcode",
            "Telefoonnr",
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
                case "Initials":
                    error = this.ValidateAbbreviation();
                    break;
                case "Firstname":
                    error = this.ValidateDescription();
                    break;
                case "Tussenvoegsel":
                    error = this.ValidateDescription();
                    break;
                case "Lastname":
                    error = this.ValidateDescription();
                    break;
                default:
                    break;
            }
            return error;
        }
        //TODO: controleren van alle properties
        private string ValidateAbbreviation()
        {
            if (IsStringMissing(Abbreviation))
            {
                return "Afkorting moet worden ingevuld";
            }
            return "";
        }

        private string ValidateDescription()
        {
            if (IsStringMissing(Description))
            {
                return "Beschrijving moet worden ingevuld";
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
