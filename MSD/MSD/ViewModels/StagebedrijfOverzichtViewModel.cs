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
        private Company _selectedCompany;

        public StagebedrijfOverzichtViewModel(IApplicationController app)
        {
            _app = app;
            _nieuwBedrijfCommand = new RelayCommand(NieuwBedrijf);
            _bedrijfAanpassenCommand = new RelayCommand(BedrijfAanpassen);
            
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

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                this.OnPropertyChanged("SelectedCompany");

            }
        }

        public RelayCommand NieuwBedrijfCommand { get { return _nieuwBedrijfCommand;}}
        public void NieuwBedrijf(object command)
        {
            StageBedrijfViewModel vm = (StageBedrijfViewModel)ViewFactory.getViewModel(_app, "stageBedrijfViewModel");
            vm.Title = "Nieuw Bedrijf";
            vm.Company = new Company();
            vm.Wijzig = false;

            _app.ShowBedrijfView();
        }

        public RelayCommand BedrijfAanpassenCommand { get { return _bedrijfAanpassenCommand; } }
        public void BedrijfAanpassen(object command)
        {
            StageBedrijfViewModel vm = (StageBedrijfViewModel)ViewFactory.getViewModel(_app, "stageBedrijfViewModel");
            vm.Title = "Bedrijf Aanpassen";
            vm.Company = SelectedCompany;
            vm.Wijzig = true;

            _app.ShowBedrijfView();
        }
        public int getNumberStudents(int id)
        {
            string query = "SELECT COUNT(s.stagebedrijf_bedrijfnr) AS aantal FROM stageopdracht s WHERE s.stagebedrijf_bedrijfnr = " + id;
            MySqlCommand cmd = new MySqlCommand(query);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);
            if (table.Rows.Count != 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Vult de bedrijven tabel met companys uit de Database
        /// </summary>
        public void FillCompanyTable()
        {
            Companys.Clear();
            MySqlCommand cmd = new MySqlCommand("select * from stagebedrijf");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Companys.Add(new Company
                {
                    ID = (int)table.Rows[RowNr][0],
                    Companyname = table.Rows[RowNr][1].ToString(),
                    City = table.Rows[RowNr][2].ToString(),
                    Adress = table.Rows[RowNr][3].ToString(),
                    Zip = table.Rows[RowNr][4].ToString(),
                    Phone = table.Rows[RowNr][5].ToString(),
                    Website = table.Rows[RowNr][6].ToString(),
                    Contact = table.Rows[RowNr][7].ToString(),
                    Email = table.Rows[RowNr][8].ToString(),
                    Branch = table.Rows[RowNr][9].ToString(),
                    Amount = getNumberStudents((int)table.Rows[RowNr][0])
                });
            }
        }
    }
}
