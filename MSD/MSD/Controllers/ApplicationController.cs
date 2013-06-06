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
            
            ViewFactory.getViewModel(this, "logInViewModel");

            Models.Database database = new Models.Database();
        }

        public void OnWindowClose(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void ShowBedrijfView()
        {
            ShowView("stageBedrijfViewModel");
        }

        public void ShowBedrijfOverzichtView()
        {
            ShowView("stageBedrijfOverzichtViewModel");
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
            StageViewModel stageViewModel = (StageViewModel)ViewFactory.getViewModel(this, "stageViewModel");
            stageViewModel.Afstuderen = false;
        }

        public void ShowAfstuderenView()
        {
            ShowView("stageViewModel");
            StageViewModel stageViewModel = (StageViewModel)ViewFactory.getViewModel(this, "stageViewModel");
            stageViewModel.Afstuderen = true;
        }

        public void ShowStageopdrachtView()
        {
            ShowView("stageopdrachtViewModel");
        }

        public void ShowStudentPersoonView()
        {
            ShowView("studentPersoonViewModel");
        }

        public void ShowStudentView()
        {
            ShowView("studentViewModel");
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



        public void ShowMainWindow()
        {
            ViewFactory.getViewModel(this, "mainWindowModel");

        }

        public void ShowLoginView()
        {
            ViewFactory.getViewModel(this, "logInViewModel");
        }

        public void ShowWachtwoordView()
        {
            ViewFactory.getViewModel(this, "wachtwoordViewModel");
        }
    }
}
