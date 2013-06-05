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
    class MatchMogelijkViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _showDetailsCommand;
        private readonly RelayCommand _matchenCommand;
        private Database _database;
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();

        public ObservableCollection<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                teachers = value;
                this.OnPropertyChanged("Teachers");
            }
        }

        public MatchMogelijkViewModel(IApplicationController app)
        {
            _app = app;
            _showDetailsCommand = new RelayCommand(ShowDetails);
            _matchenCommand = new RelayCommand(Matchen);
            _database = ModelFactory.Database;
        }

        public RelayCommand ShowDetailsCommand { get { return _showDetailsCommand; } }
        public void ShowDetails(object command)
        {
            _app.ShowMatchDetailsView();
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            _app.ShowMatchSuccesView();
        }

        public void MogelijkeMatch(int stagenr, string periodenaam)
        {
            teachers.Clear();
            string query = "SELECT d.naam,k.kennisnaam FROM docent d JOIN docent_has_kennisgebieden dk ON d.docentnr = dk.docent_docentnr JOIN kennisgebieden k ON dk.kennisgebieden_kennisnr = k.kennisnr JOIN kennisgebieden_has_stageopdracht ks ON k.kennisnr = ks.kennisgebieden_kennisnr WHERE ks.stageopdracht_stagenr = '"+ stagenr + "' AND ks.stageopdracht_periode_periodenaam = '" + periodenaam + "'";
            MySqlCommand mycommand = new MySqlCommand(query);
            DataTable data = new DataTable();
            MySqlDataAdapter adapter = _database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                for (int RowNr = 0; RowNr < data.Rows.Count; RowNr++)
                {
                    teachers.Add(new Teacher
                    {
                        Naam = data.Rows[RowNr][0].ToString()
                        
                    });
                }
            }

        }
    }
}
