using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Entity
{
    public class Teacher
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
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

        private string _hours;
        public string Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }

        private string _preference;
        public string Preference
        {
            get { return _preference; }
            set { _preference = value; }
        }
    }
}
