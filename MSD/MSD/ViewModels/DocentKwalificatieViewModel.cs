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
using System.Windows;

namespace MSD.ViewModels
{
    class DocentKwalificatieViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;
        private readonly RelayCommand _terugCommand;
        private string[] _knowledgeAreas;

        private bool _editing;
        private Teacher _teacher;

        public DocentKwalificatieViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Opslaan);
            _terugCommand = new RelayCommand(Terug);
        }

        public string[] KnowledgeAreas
        {
            get { return _knowledgeAreas; }
            set
            {
                _knowledgeAreas = value;
                OnPropertyChanged("KnowledgeAreas");
            }
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Opslaan(object command)
        {
            if (!String.IsNullOrEmpty(Teacher.Name) &&
                !String.IsNullOrEmpty(Teacher.Email) &&
                !String.IsNullOrEmpty(Teacher.Phone) &&
                !String.IsNullOrEmpty(Teacher.Adress) &&
                !String.IsNullOrEmpty(Teacher.City))
            {
                ModelFactory.Database.setData(new MySqlCommand("DELETE FROM docent_has_kennisgebieden WHERE Docent_Docentnr = " + Teacher.TeacherNo));

                string query;
                 int afkorting = getexecuteQuery("SELECT afkorting FROM opleiding WHERE omschrijving = '" + Teacher.Education + "';");
           
                if (Editing)
                {
                    
                    query = "UPDATE docent SET Naam = '" + Teacher.Name + "', Mailadres = '" + Teacher.Email + "', Plaats = '" + Teacher.City + "', Adres = '" + Teacher.Adress + "', Telefoonnr = '" + Teacher.Phone + "', Voorkeur = '" + Teacher.Preference + "', Uren = '" + Teacher.Hours + "' WHERE Docentnr = '" + Teacher.TeacherNo + "';";
                    ModelFactory.Database.setData(new MySqlCommand(query));
                    query = "UPDATE docent_has_opleiding SET docent_docentnr = " + Teacher.TeacherNo + ", opleiding_afkorting = " + afkorting + ", opleiding_academie_afkorting = '" + Teacher.Academie + "' WHERE docent_docentnr = " + Teacher.TeacherNo + ";";
                    ModelFactory.Database.setData(new MySqlCommand(query));
                }
                else
                {
                   
                    Teacher.TeacherNo = getexecuteQuery("select max(Docentnr) from docent")+1;

                    query = "INSERT INTO docent (Docentnr, Naam, Mailadres, Plaats, Adres, Telefoonnr, Voorkeur, Uren) VALUES('" + Teacher.TeacherNo + "','" + Teacher.Name + "','" + Teacher.Email + "','" + Teacher.City + "','" + Teacher.Adress + "','" + Teacher.Phone + "','" + Teacher.Preference + "','" + Teacher.Hours + "');";
                    ModelFactory.Database.setData(new MySqlCommand(query));
                    query = "INSERT INTO docent_has_opleiding (docent_docentnr, opleiding_afkorting, opleiding_academie_afkorting) VALUES("+ Teacher.TeacherNo +"," + afkorting +",'" + Teacher.Academie + "');";
                    ModelFactory.Database.setData(new MySqlCommand(query));
                }
               



                foreach (string knowledgeArea in Teacher.KnowledgeAreas)
                {
                    if (knowledgeArea != "" && knowledgeArea != null)
                        ModelFactory.Database.setData(new MySqlCommand("INSERT INTO docent_has_kennisgebieden (Docent_Docentnr, Kennisgebieden_KennisNr) SELECT " + Teacher.TeacherNo + ", (SELECT KennisNr FROM kennisgebieden WHERE KennisNaam = '" + knowledgeArea +"')"));
                }

                _app.ShowDocentView();
            }
            else
            {
                string message = "De volgende gegevens zijn niet ingevuld: \n";
                if (String.IsNullOrEmpty(Teacher.Name))
                {
                    message += " - Naam \n";
                }
                if (String.IsNullOrEmpty(Teacher.Email))
                {
                    message += " - Email \n";
                }
                if (String.IsNullOrEmpty(Teacher.Phone))
                {
                    message += " - Telefoonnummer \n";
                }
                if (String.IsNullOrEmpty(Teacher.Adress))
                {
                    message += " - Adres \n";
                }
                if (String.IsNullOrEmpty(Teacher.Phone))
                {
                    message += " - Woonplaats \n";
                }

                MessageBox.Show(message);
            }
        }
        public int getexecuteQuery(string query)
        {
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = ModelFactory.Database.getData(mycommand);
            adapter.Fill(data);
            return (int)data.Rows[0][0];
        }

        public void FillKnowledgeAreas()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            KnowledgeAreas = new string[table.Rows.Count];

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                KnowledgeAreas[RowNr] = table.Rows[RowNr][1].ToString();
            }
        }

        private void UpdateTeacherKnowledgeAreas()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden WHERE KennisNr IN (SELECT Kennisgebieden_KennisNr FROM docent_has_kennisgebieden WHERE Docent_Docentnr = " + Teacher.TeacherNo + ")");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            int NumRows = table.Rows.Count;

            if (NumRows >= 1)
                KnowledgeArea1 = table.Rows[0][1].ToString();
            else
                KnowledgeArea1 = "";
            if (NumRows >= 2)
                KnowledgeArea2 = table.Rows[1][1].ToString();
            else
                KnowledgeArea2 = "";
            if (NumRows >= 3)
                KnowledgeArea3 = table.Rows[2][1].ToString();
            else
                KnowledgeArea3 = "";
            if (NumRows >= 4)
                KnowledgeArea4 = table.Rows[3][1].ToString();
            else
                KnowledgeArea4 = "";
            if (NumRows >= 5)
                KnowledgeArea5 = table.Rows[4][1].ToString();
            else
                KnowledgeArea5 = "";
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowDocentPersoonView();
        }

        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                OnPropertyChanged("Title");
                FillKnowledgeAreas(); //Update knowledge dropdowns on pressing new or edit
            }
        }
        
        public string Title
        {
            get
            {
                if (Editing) return "Docent Aanpassen";
                else return "Nieuwe Docent";
            }
        }

        public Teacher Teacher
        {
            get { return _teacher; }
            set 
            {
                _teacher = value;
                UpdateTeacherKnowledgeAreas();
            }
        }

        public int Hours
        {
            get { return Teacher.Hours; }
            set
            {
                Teacher.Hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public string Preference
        {
            get { return Teacher.Preference; }
            set
            {
                Teacher.Preference = value;
                OnPropertyChanged("Preference");
            }
        }

        public string KnowledgeArea1
        {
            get { return Teacher.KnowledgeAreas[0]; }
            set
            {
                Teacher.KnowledgeAreas[0] = value;
                OnPropertyChanged("KnowledgeArea1");
            }
        }

        public string KnowledgeArea2
        {
            get { return Teacher.KnowledgeAreas[1]; }
            set
            {
                Teacher.KnowledgeAreas[1] = value;
                OnPropertyChanged("KnowledgeArea2");
            }
        }

        public string KnowledgeArea3
        {
            get { return Teacher.KnowledgeAreas[2]; }
            set
            {
                Teacher.KnowledgeAreas[2] = value;
                OnPropertyChanged("KnowledgeArea3");
            }
        }

        public string KnowledgeArea4
        {
            get { return Teacher.KnowledgeAreas[3]; }
            set
            {
                Teacher.KnowledgeAreas[3] = value;
                OnPropertyChanged("KnowledgeArea4");
            }
        }

        public string KnowledgeArea5
        {
            get { return Teacher.KnowledgeAreas[4]; }
            set
            {
                Teacher.KnowledgeAreas[4] = value;
                OnPropertyChanged("KnowledgeArea5");
            }
        }

    }
}
