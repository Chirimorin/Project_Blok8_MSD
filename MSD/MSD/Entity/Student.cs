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
        private string _studentNumber;

        public string StudentNo
        {
            get { return _studentNumber; }
            set { _studentNumber = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }        
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _education;
        public string Education
        {
            get { return _education; }
            set { _education = value; }
        }

        static readonly string[] ValidatedProp =
        {
            /*"Initials",
            "Name",
            "Dateofbirth",
            "Email",
            "Enrolyear",
            "Graduationyear",
            "City",
            "Adres",
            "Postcode",
            "Telefoonnr",*/
        };

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }

        private string GetValidationError(string propertyName)
        {
            throw new NotImplementedException();
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
