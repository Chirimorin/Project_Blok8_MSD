using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Entity
{
    public class Academy : PropertyChangedBase, IDataErrorInfo
    {
        private string _abbreviation;

        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; this.OnPropertyChanged("Abbreviation"); }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; this.OnPropertyChanged("Description"); }
        }

        static readonly string[] ValidatedProp =
        {
            "Abbreviation",
            "Description",
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
                case "Abbreviation":
                    error = this.ValidateAbbreviation();
                    break;
                case "Description":
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
