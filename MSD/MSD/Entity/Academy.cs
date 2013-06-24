using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Entity
{
    public class Academy : PropertyChangedBase
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
    }
}
