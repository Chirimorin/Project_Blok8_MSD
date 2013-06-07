using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Entity
{
    class Assignment : PropertyChangedBase
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
        private List<string> _knowledge;
        public List<string> Knowledge
        {
            get
            {
                return _knowledge;
            }
            
        }
        private string _knowledgeItem;
        public string KnowLedgeItem
        {
            get
            {
                return _knowledgeItem;
            }
            set
            {
                _knowledgeItem = value;
                Knowledge.Add(_knowledgeItem);
            }
        }
    

    }
}
