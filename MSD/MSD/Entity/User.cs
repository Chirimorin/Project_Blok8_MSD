using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;
using MySql.Data.MySqlClient;

namespace MSD.Entity
{
    public class User : PropertyChangedBase
    {
        private string _naam;

        public string Naam
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
    }
}
