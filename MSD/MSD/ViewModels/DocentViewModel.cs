using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using System.Data;

namespace MSD.ViewModels
{
    public class DocentViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _NieuweDocentCommand;
        private readonly RelayCommand _DocentAanpassenCommand;
        private ObservableCollection<Teacher> _teachers = new ObservableCollection<Teacher>();
        private ObservableCollection<KnowledgeArea> _knowledgeAreas = new ObservableCollection<KnowledgeArea>();
        private Teacher _selectedItem;

        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                this.OnPropertyChanged("Teachers");
            }
        }

        public ObservableCollection<KnowledgeArea> KnowledgeAreas
        {
            get { return _knowledgeAreas; }
            set
            {
                _knowledgeAreas = value;
                OnPropertyChanged("KnowledgeAreas");
            }
        }

        public Teacher SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("Name");
                OnPropertyChanged("Adress");
                OnPropertyChanged("Email");
                OnPropertyChanged("City");
                OnPropertyChanged("Phone");
                OnPropertyChanged("Preference");
                OnPropertyChanged("Hours");
                FillKnowledgeAreas();
            }
        }

        public DocentViewModel(IApplicationController app)
        {
            _app = app;
            _NieuweDocentCommand = new RelayCommand(NieuweDocent);
            _DocentAanpassenCommand = new RelayCommand(DocentAanpassen);
        }

        private Database Database
        {
            get { return ModelFactory.Database; }
        }

        public RelayCommand NieuweDocentCommand { get { return _NieuweDocentCommand; } }
        public void NieuweDocent(object command)
        {
            DocentPersoonViewModel vm = (DocentPersoonViewModel)ViewFactory.getViewModel(_app, "docentPersoonViewModel");
            DocentKwalificatieViewModel vm2 = (DocentKwalificatieViewModel)ViewFactory.getViewModel(_app, "docentKwalificatieViewModel");

            vm.Teacher = new Teacher();
            vm.Editing = false;

            vm2.Teacher = vm.Teacher;
            vm2.Editing = false;

            _app.ShowDocentPersoonView();
        }

        public RelayCommand DocentAanpassenCommand { get { return _DocentAanpassenCommand; } }
        public void DocentAanpassen(object command)
        {
            DocentPersoonViewModel vm = (DocentPersoonViewModel)ViewFactory.getViewModel(_app, "docentPersoonViewModel");
            DocentKwalificatieViewModel vm2 = (DocentKwalificatieViewModel)ViewFactory.getViewModel(_app, "docentKwalificatieViewModel");

            vm.Editing = true;
            vm.Teacher = SelectedItem;
            
            vm2.Editing = true;
            vm2.Teacher = vm.Teacher;

            _app.ShowDocentPersoonView();
        }

        /// <summary>
        /// Vult de Docent tabel met Teachers uit de Database
        /// </summary>
        public void fillTeacherTable()
        {
            Teachers.Clear();
            MySqlCommand cmd = new MySqlCommand("select * from docent");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Teachers.Add(new Teacher
                {
                    TeacherNo = Convert.ToInt32(table.Rows[RowNr][0].ToString()),
                    Name = table.Rows[RowNr][1].ToString(),
                    Email = table.Rows[RowNr][2].ToString(),
                    City = table.Rows[RowNr][3].ToString(),
                    Adress = table.Rows[RowNr][4].ToString(),
                    Phone = table.Rows[RowNr][5].ToString(),
                    Preference = table.Rows[RowNr][6].ToString(),
                    Hours = Convert.ToInt32(table.Rows[RowNr][7].ToString()),
                });
            }
        }

        /// <summary>
        /// Vult de kennisgebieden lijst met kenniesgebieden van de geselecteerde docent.
        /// </summary>
        public void FillKnowledgeAreas()
        {
            KnowledgeAreas.Clear();
            if (SelectedItem != null)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden WHERE KennisNr IN (SELECT Kennisgebieden_KennisNr FROM docent_has_kennisgebieden WHERE Docent_Docentnr = " + SelectedItem.TeacherNo + ")");
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = Database.getData(cmd);
                adapter.Fill(table);

                for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
                {
                    KnowledgeAreas.Add(new KnowledgeArea
                    {
                        Name = table.Rows[RowNr][1].ToString(),
                    });
                }
            }
        }

        public string Name
        {
            get 
            {
                if (SelectedItem != null)
                    return SelectedItem.Name;
                return "";
            }
        }

        public string Adress
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Adress;
                return "";
            }
        }

        public string City
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.City;
                return "";
            }
        }

        public string Email
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Email;
                return "";
            }
        }

        public int Hours
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Hours;
                return 0;
            }
        }

        public string Phone
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Phone;
                return "";
            }
        }

        public string Preference
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Preference;
                return "";
            }
        }
    }
}
