using MSD.Controllers;
using MSD.Models;
using MSD.ViewModels;
using MSD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Factories
{
    public abstract class ViewFactory
    {
        private static DocentKwalificatieViewModel _docentKwalificatieView;
        private static DocentPersoonViewModel _docentPersoonView;
        private static DocentViewModel _docentView;
        private static GebruikerAccountViewModel _gebruikerAccountView;
        private static GebruikerContactViewModel _gebruikerContactView;
        private static GebruikerViewModel _gebruikerView;
        private static LogInViewModel _logInView;
        private static MainWindowModel _mainWindow;
        private static MatchDetailsViewModel _matchDetailsView;
        private static MatchInvoerViewModel _matchInvoerView;
        private static MatchMogelijkViewModel _matchMogelijkView;
        private static MatchSuccesViewModel _matchSuccesView;
        private static StageViewModel _stageView;
        private static StudentBedrijfViewModel _studentBedrijfView;
        private static StudentPersoonViewModel _studentPersoonView;
        private static StudentViewModel _studentView;
        private static WachtwoordViewModel _wachtwoordView;

        /// <summary>
        /// Returns the viewModel. It will be made if it doesn't exist yet.
        /// </summary>
        /// <param name="app">The applicationcontroller</param>
        /// <param name="name">Name of the viewModel to be returned</param>
        /// <returns>The requested viewModel or null if the value is wrong</returns>
        public static PropertyChangedBase getViewModel(IApplicationController app, string name)
        {
            switch (name)
            {
                case ("docentKwalificatieViewModel"):
                    if (_docentKwalificatieView == null)
                        _docentKwalificatieView = new DocentKwalificatieViewModel(app);
                    return _docentKwalificatieView;
                case ("docentPersoonViewModel"):
                    if (_docentPersoonView == null)
                        _docentPersoonView = new DocentPersoonViewModel(app);
                    return _docentPersoonView;
                case ("docentViewModel"):
                    if (_docentView == null)
                        _docentView = new DocentViewModel(app);
                    return _docentView;
                case ("gebruikerAccountViewModel"):
                    if (_gebruikerAccountView == null)
                        _gebruikerAccountView = new GebruikerAccountViewModel(app);
                    return _gebruikerAccountView;
                case ("gebruikerContactViewModel"):
                    if (_gebruikerContactView == null)
                        _gebruikerContactView = new GebruikerContactViewModel(app);
                    return _gebruikerContactView;
                case ("gebruikerViewModel"):
                    if (_gebruikerView == null)
                        _gebruikerView = new GebruikerViewModel(app);
                    return _gebruikerView;
                case ("matchDetailsViewModel"):
                    if (_matchDetailsView == null)
                        _matchDetailsView = new MatchDetailsViewModel(app);
                    return _matchDetailsView;
                case ("matchInvoerViewModel"):
                    if (_matchInvoerView == null)
                        _matchInvoerView = new MatchInvoerViewModel(app);
                    return _matchInvoerView;
                case ("matchMogelijkViewModel"):
                    if (_matchMogelijkView == null)
                        _matchMogelijkView = new MatchMogelijkViewModel(app);
                    return _matchMogelijkView;
                case ("matchSuccesViewModel"):
                    if (_matchSuccesView == null)
                        _matchSuccesView = new MatchSuccesViewModel(app);
                    return _matchSuccesView;
                case ("stageViewModel"):
                    if (_stageView == null)
                        _stageView = new StageViewModel(app);
                    return _stageView;
                case ("studentBedrijfViewModel"):
                    if (_studentBedrijfView == null)
                        _studentBedrijfView = new StudentBedrijfViewModel(app);
                    return _studentBedrijfView;
                case ("studentPersoonViewModel"):
                    if (_studentPersoonView == null)
                        _studentPersoonView = new StudentPersoonViewModel(app);
                    return _studentPersoonView;
                case ("studentViewModel"):
                    if (_studentView == null)
                        _studentView = new StudentViewModel(app);
                    return _studentView;
                case ("wachtwoordViewModel"):
                    if (_wachtwoordView == null)
                        _wachtwoordView = new WachtwoordViewModel(app);
                    return _wachtwoordView;
                case ("mainWindowModel"):
                    if (_mainWindow == null)
                    {
                        _mainWindow = new MainWindowModel(app);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        mainWindow.DataContext = _mainWindow;
                        mainWindow.Closed += app.OnMainWindowClose;
                    }
                    return _mainWindow;
                case ("logInViewModel"):
                    if (_logInView == null)
                    {
                        _logInView = new LogInViewModel(app);
                        LogInView loginView = new LogInView();
                        loginView.Show();
                        loginView.DataContext = _logInView;
                    }
                    return _logInView;
                default:
                    return null;
            }
        }


    }
}
