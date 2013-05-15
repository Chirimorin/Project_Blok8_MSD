using MSD.Controllers;
using MSD.ViewModels;
using MSD.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MSD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApplicationController
    {
        private LogInView _login;
        private LogInViewModel _loginVM;

        private MainWindow _main;
        private MainWindowModel _mainVM;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _login = new LogInView();
            _loginVM = new LogInViewModel();
            _login.Show();

            _login.DataContext = _loginVM;

            _main = new MainWindow();
            _mainVM = new MainWindowModel(this);
            _main.DataContext = _mainVM;
            MainWindow = _main;

            _main.Show();
            Models.Database database = new Models.Database();
        }


        public void ShowDocentKwalificatieView()
        {
            throw new NotImplementedException();
        }

        public void ShowDocentPersoonView()
        {
            throw new NotImplementedException();
        }

        public void ShowDocentView()
        {
            //_mainVM.Content = new DocentViewModel(this);
            Console.WriteLine("showdocentview");
        }

        public void ShowGebruikerAccountView()
        {
            throw new NotImplementedException();
        }

        public void ShowGebruikerContactView()
        {
            throw new NotImplementedException();
        }

        public void ShowGebruikerView()
        {
            throw new NotImplementedException();
        }

        public void ShowMatchDetailsView()
        {
            throw new NotImplementedException();
        }

        public void ShowMatchInvoerView()
        {
            throw new NotImplementedException();
        }

        public void ShowMatchMogelijkView()
        {
            throw new NotImplementedException();
        }

        public void ShowMatchSuccesView()
        {
            throw new NotImplementedException();
        }

        public void ShowStageView()
        {
            throw new NotImplementedException();
        }

        public void ShowStudentBedrijfView()
        {
            throw new NotImplementedException();
        }

        public void ShowStudentPersoonView()
        {
            throw new NotImplementedException();
        }

        public void ShowStudentView()
        {
            throw new NotImplementedException();
        }

        public void ShowWachtwoordView()
        {
            throw new NotImplementedException();
        }
        

    }
}
