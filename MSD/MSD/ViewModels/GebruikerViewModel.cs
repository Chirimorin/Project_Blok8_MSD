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
using MSD.Entity;

namespace MSD.ViewModels
{
    public class GebruikerViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuweGebruikerCommand;
        private readonly RelayCommand _gebruikerAanpassenCommand;
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        private User _selectedItem;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                this.OnPropertyChanged("Users");
            }
        }

        public User SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public GebruikerViewModel(IApplicationController app)
        {
            _app = app;
            _nieuweGebruikerCommand = new RelayCommand(NieuweGebruiker);
            _gebruikerAanpassenCommand = new RelayCommand(GebruikerAanpassen);
        }

        private Database Database
        {
            get { return ModelFactory.Database; }
        }

        public RelayCommand NieuweGebruikerCommand { get { return _nieuweGebruikerCommand; } }
        public void NieuweGebruiker(object command)
        {
            GebruikerAccountViewModel vm = (GebruikerAccountViewModel)ViewFactory.getViewModel(_app, "gebruikerAccountViewModel");

            vm.Editing = false;
            vm.User = new User();

            vm.Password = "";
            vm.RepeatPassword = "";

            _app.ShowGebruikerAccountView();
        }

        public RelayCommand GebruikerAanpassenCommand { get { return _gebruikerAanpassenCommand; } }
        public void GebruikerAanpassen(object command)
        {
            GebruikerAccountViewModel vm = (GebruikerAccountViewModel)ViewFactory.getViewModel(_app, "gebruikerAccountViewModel");

            vm.Editing = true;
            vm.User = SelectedItem;

            vm.Password = "";
            vm.RepeatPassword = "";

            _app.ShowGebruikerAccountView();
        }
        /// <summary>
        /// Vult de gebruiker tabel met Users uit de Database
        /// </summary>
        public void fillUserTable()
        {
            Users.Clear();
            MySqlCommand cmd = new MySqlCommand("select * from gebruiker");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Users.Add(new User
                {
                    Name = table.Rows[RowNr][2].ToString(),
                    Email = table.Rows[RowNr][0].ToString()
                });
            }
        }
    }
}
