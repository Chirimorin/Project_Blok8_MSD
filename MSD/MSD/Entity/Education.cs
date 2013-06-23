using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSD.Models;

namespace MSD.Entity
{
    public class Education : PropertyChangedBase
    {
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _abbreviation;

        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }
    }
}
