using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MSD.Factories;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data;

namespace MSD.ViewModels
{
    public class GebruikerViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuweGebruikerCommand;
        private readonly RelayCommand _gebruikerAanpassenCommand;
        private Database _database;
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                this.OnPropertyChanged("Users");
            }
        }

        public GebruikerViewModel(IApplicationController app)
        {
            _app = app;
            _nieuweGebruikerCommand = new RelayCommand(NieuweGebruiker);
            _gebruikerAanpassenCommand = new RelayCommand(GebruikerAanpassen);
            _database = db.getInstance();
            this.fillUserTable();
        }

        public RelayCommand NieuweGebruikerCommand { get { return _nieuweGebruikerCommand; } }
        public void NieuweGebruiker(object command)
        {
            GebruikerAccountViewModel vm = (GebruikerAccountViewModel)ViewFactory.getViewModel(_app, "gebruikerAccountViewModel");
            vm.Title = "Nieuwe Gebruiker";
            _app.ShowGebruikerAccountView();
        }

        public RelayCommand GebruikerAanpassenCommand { get { return _gebruikerAanpassenCommand; } }
        public void GebruikerAanpassen(object command)
        {
            GebruikerAccountViewModel vm = (GebruikerAccountViewModel)ViewFactory.getViewModel(_app, "gebruikerAccountViewModel");
            vm.Title = "Gebruiker Aanpasen";
            _app.ShowGebruikerAccountView();
        }

        private void fillUserTable()
        {
            MySqlCommand cmd = new MySqlCommand("select * from gebruiker");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = _database.executeQuery(cmd);
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                users.Add(new User{ Naam = table.Rows[i][0].ToString(),
                                    Email = table.Rows[i][1].ToString()});
            }
        }

        private void initLoad()
        {

        }
    }
}
