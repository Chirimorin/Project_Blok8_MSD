using MSD.Controllers;
using MSD.Entity;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StageopdrachtViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;
        private readonly RelayCommand _terugCommand;
        private Assignment _assignment;

        public StageopdrachtViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Save);
            _terugCommand = new RelayCommand(Back);
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentPersoonView();
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Save(object command)
        {


            _app.ShowStudentView();
        }

        public Assignment Assignment
        {
            get { return _assignment; }
            set
            {
                
                _assignment = value;
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(Title);
            }
        }

        public string Description
        {
            get
            {
                if(Assignment != null) 
                    return Assignment.Description;
                return "";
            }
            set
            {
                Assignment.Description = value;
            }
        }
        public string Name
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Name;
                return "";
            }
            set
            {
                Assignment.Name = value;
            }
        }

        public string Comments
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Comments;
                return "";
            }
            set
            {
                Assignment.Comments = value;
            }
        }
        public Company Company
        {
            get
            {
                return Assignment.Company;
            }
            set
            {
                Assignment.Company = value;
            }
        }
        public string Period
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Period;
                return "";
            }
            set
            {
                Assignment.Period = value;
            }
        }

        public bool Accepted
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Accepted;
                return false;
            }
            set
            {
                Assignment.Accepted = value;
            }
        }

        public bool TempPermission
        {
            get
            {
                if (Assignment != null)
                    return Assignment.TempPermission;
                return false;
            }
            set
            {
                Assignment.TempPermission = value;
            }
        }

        public bool Permission
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Permission;
                return false;
            }
            set
            {
                Assignment.Permission = value;
            }
        }
        public string KnowLedgeItem
        {
            get
            {
                return Assignment.KnowLedgeItem;
            }
            set
            {
                Assignment.KnowLedgeItem = value;
                Assignment.Knowledge.Add(Assignment.KnowLedgeItem);
            }
        }

    }
}
