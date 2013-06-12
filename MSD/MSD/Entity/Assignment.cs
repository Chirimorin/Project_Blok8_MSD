using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Entity
{
    public class Assignment : PropertyChangedBase
    {

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _comments;
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        private bool _accepted;
        public bool Accepted
        {
            get
            {
                return _accepted;
            }
            set
            {
                _accepted = value;
            }
        }

        private bool _tempPermission;
        public bool TempPermission
        {
            get
            {
                return _tempPermission;
            }
            set
            {
                _tempPermission = value;
            }
        }

        private bool _permission;
        public bool Permission
        {
            get
            {
                return _permission;
            }
            set
            {
                _permission = value;
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private string _company;
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
            }
        }
        private string _period;
        public string Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
            }
        }
        private string[] _knowledge;
        public string[] Knowledge
        {
            get { return _knowledge; }
            set { _knowledge = value; }
            
        }
       
    

    }
}
