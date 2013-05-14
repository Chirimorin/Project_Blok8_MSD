using MSD.Services;
using MSD.Views;
using MSD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Controllers
{
    class ApplicationController : IApplicationController
    {
        private LogInView _login;
        private LogInViewModel _loginVM;

        private MainWindow _main;
        private MainWindowModel _mainVM;

        public ApplicationController()
        {

            _login = new LogInView();
            _loginVM = new LogInViewModel();
            _login.Show();

            _login.DataContext = _loginVM;

            _main = new MainWindow();
            _mainVM = new MainWindowModel();
            _main.DataContext = _mainVM;

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
            _mainVM.Content = new DocentViewModel();
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
