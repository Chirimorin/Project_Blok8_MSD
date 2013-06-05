using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MSD.Entity;
using MySql.Data.MySqlClient;
using System.Data;

namespace MSD.ViewModels
{
    class StagebedrijfOverzichtViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuwBedrijfCommand;
        private readonly RelayCommand _bedrijfAanpassenCommand;
        private Database _database;
        private ObservableCollection<Company> _companys = new ObservableCollection<Company>();

        public StagebedrijfOverzichtViewModel(IApplicationController app)
        {
            _app = app;
            _nieuwBedrijfCommand = new RelayCommand(NieuwBedrijf);
            _bedrijfAanpassenCommand = new RelayCommand(BedrijfAanpassen);
            this.FillCompanyTable();
        }

        public ObservableCollection<Company> Companys
        {
            get { return _companys; }
            set
            {
                _companys = value;
                this.OnPropertyChanged("Companys");
            }
        }

        public RelayCommand NieuwBedrijfCommand { get { return _nieuwBedrijfCommand;}}
        public void NieuwBedrijf(object command)
        {
            StageBedrijfViewModel vm = (StageBedrijfViewModel)ViewFactory.getViewModel(_app, "stageBedrijfViewModel");
            vm.Title = "Nieuw Bedrijf";
            vm.Adress = "";
            vm.Branch = "";
            vm.City = "";
            vm.Company = "";
            vm.Contact = "";
            vm.Email = "";
            vm.Phone = "";
            vm.Website = "";

            _app.ShowBedrijfView();
        }

        public RelayCommand BedrijfAanpassenCommand { get { return _bedrijfAanpassenCommand; } }
        public void BedrijfAanpassen(object command)
        {
            StageBedrijfViewModel vm = (StageBedrijfViewModel)ViewFactory.getViewModel(_app, "stageBedrijfViewModel");
            vm.Title = "Bedrijf Aanpassen";
            
            //TODO: Gegevens invullen zoals in de database!
            vm.Adress = "";
            vm.Branch = "";
            vm.City = "";
            vm.Company = "";
            vm.Contact = "";
            vm.Email = "";
            vm.Phone = "";
            vm.Website = "";

            _app.ShowBedrijfView();
        }
        /// <summary>
        /// Vult de observable collection met Companys
        /// </summary>
        private void FillCompanyTable()
        {
            MySqlCommand cmd = new MySqlCommand("select * from stagebedrijf");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Companys.Add(new Company
                {
                    Companyname = table.Rows[RowNr][1].ToString(),
                    City = table.Rows[RowNr][2].ToString(),
                    Adress = table.Rows[RowNr][3].ToString(),
                    Zip = table.Rows[RowNr][4].ToString(),
                    Phone = table.Rows[RowNr][5].ToString(),
                    Website = table.Rows[RowNr][6].ToString(),
                    Contact = table.Rows[RowNr][7].ToString(),
                    Email = table.Rows[RowNr][8].ToString(),
                    Branch = table.Rows[RowNr][9].ToString(),
                });
            }
        }
    }
}
