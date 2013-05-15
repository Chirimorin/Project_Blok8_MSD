using MSD.Factories;
using MSD.ViewModels;
using MSD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Controllers
{
    class ApplicationController : IApplicationController
    {

        public ApplicationController()
        {
            ViewFactory.getViewModel(this, "mainWindowModel");
            ViewFactory.getViewModel(this, "logInViewModel");

            Models.Database database = new Models.Database();
        }

        public void OnMainWindowClose(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void ShowDocentKwalificatieView()
        {
            ShowView("docentKwalificatieViewModel");
        }

        public void ShowDocentPersoonView()
        {
            ShowView("docentPersoonViewModel");
        }

        public void ShowDocentView()
        {
            ShowView("docentViewModel");
        }

        public void ShowGebruikerAccountView()
        {
            ShowView("gebruikerAccountViewModel");
        }

        public void ShowGebruikerContactView()
        {
            ShowView("gebruikerContactViewModel");
        }

        public void ShowGebruikerView()
        {
            ShowView("gebruikerViewModel");
        }

        public void ShowMatchDetailsView()
        {
            ShowView("matchDetailsViewModel");
        }

        public void ShowMatchInvoerView()
        {
            ShowView("matchInvoerViewModel");
        }

        public void ShowMatchMogelijkView()
        {
            ShowView("matchMogelijkViewModel");
        }

        public void ShowMatchSuccesView()
        {
            ShowView("matchSuccesViewModel");
        }

        public void ShowStageView()
        {
            ShowView("stageViewModel");
        }

        public void ShowStudentBedrijfView()
        {
            ShowView("studentBedrijfViewModel");
        }

        public void ShowStudentPersoonView()
        {
            ShowView("studentPersoonViewModel");
        }

        public void ShowStudentView()
        {
            ShowView("studentViewModel");
        }

        public void ShowWachtwoordView()
        {
            ShowView("wachtwoordViewModel");
        }
        
        /// <summary>
        /// Will show the view that belongs to the given view model name
        /// </summary>
        /// <param name="viewModelName">Name of the viewModel that belongs to the view.</param>
        private void ShowView(string viewModelName)
        {
            MainWindowModel mainWindowModel = (MainWindowModel)ViewFactory.getViewModel(this, "mainWindowModel");
            mainWindowModel.Contents = ViewFactory.getViewModel(this, viewModelName);
        }

    }
}
