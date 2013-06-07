using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MSD.Entity
{
    public class Teacher : IDataErrorInfo
    {
        public Teacher()
        {
            KnowledgeAreas = new string[5];
        }

        private int _teacherNo;
        public int TeacherNo
        {
            get { return _teacherNo; }
            set { _teacherNo = value; }
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

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }

        private string _preference;
        public string Preference
        {
            get 
            {
                if (_preference == null)
                    return "";
                return _preference; 
            }
            set { _preference = value; }
        }

        private string[] _knowledgeAreas;
        public string[] KnowledgeAreas
        {
            get { return _knowledgeAreas; }
            set { _knowledgeAreas = value; }
        }

        static readonly string[] ValidatedProp =
        {
            "Name",
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
        /// Kijkt naar de property en validate deze vervolgens
        /// </summary>
        /// <param name="propertyName">property van User</param>
        /// <returns>returned errors als die gevonden zijn</returns>
        private string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProp, propertyName) < 0)
            {
                return null;
            }

            string error = null;

            switch (propertyName)
            {
                case "Name":
                    error = this.ValidateName();
                    break;
                default:
                    break;
            }
            return error;
        }

        private string ValidateName()
        {
            throw new NotImplementedException();
        }
    }
}
