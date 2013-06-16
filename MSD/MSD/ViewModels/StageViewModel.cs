using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StageViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;

        private bool _afstuderen;

        public StageViewModel(IApplicationController app)
        {
            _app = app;
            string query;
            if (Afstuderen)
            {
                query = "SELECT s.* FROM student s WHERE type = 'Afstuderen'";
            }
            else
            {
                query = "SELECT s.* FROM student s WHERE type = 'Stage'";
            }
            
            FillTable(query);
        }
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                this.OnPropertyChanged("Students");
            }
        }

        public bool Afstuderen
        {
            get { return _afstuderen; }
            set 
            {
                _afstuderen = value;
                OnPropertyChanged("Title");
            }
        }

        public string Title
        {
            get
            {
                if (Afstuderen) return "Afstuderen";
                else return "Stages";
            }
        }

        /// <summary>
        /// Vult de ObservableCollection met Student objecten
        /// </summary>
        /// <param name="query">De uit te voeren query</param>
        public void FillTable(string query)
        {
            students.Clear();
            MySqlCommand mycommand = new MySqlCommand(query);
            DataTable data = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                for (int RowNr = 0; RowNr < data.Rows.Count; RowNr++)
                {
                    students.Add(new Student
                    {
                        StudentNo = data.Rows[RowNr][0].ToString(),
                        Name = data.Rows[RowNr][1].ToString(),
                        Email = data.Rows[RowNr][2].ToString(),
                        Education = data.Rows[RowNr][3].ToString(),

                        Assignment = new Assignment
                        {
                            Name = data.Rows[RowNr][4].ToString(),
                            Period = data.Rows[RowNr][9].ToString(),
                            Company = data.Rows[RowNr][8].ToString(),
                        }


                    });
                }
            }
        }

    }
}
