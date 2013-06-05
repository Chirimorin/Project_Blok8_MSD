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
        private static StageBedrijfViewModel _stageBedrijfView;
        private static StagebedrijfOverzichtViewModel _stageBedrijfOverzichtView;
        private static DocentKwalificatieViewModel _docentKwalificatieView;
        private static DocentPersoonViewModel _docentPersoonView;
        private static DocentViewModel _docentView;
        private static GebruikerAccountViewModel _gebruikerAccountView;
        private static GebruikerViewModel _gebruikerView;
        private static LogInViewModel _logInViewModel;
        private static MainWindowModel _mainWindowModel;
        private static MatchDetailsViewModel _matchDetailsView;
        private static MatchInvoerViewModel _matchInvoerView;
        private static MatchMogelijkViewModel _matchMogelijkView;
        private static MatchSuccesViewModel _matchSuccesView;
        private static StageViewModel _stageView;
        private static StageopdrachtViewModel _stageopdrachtView;
        private static StudentPersoonViewModel _studentPersoonView;
        private static StudentViewModel _studentView;
        private static WachtwoordViewModel _wachtwoordViewModel;

        private static LogInView _loginView;
        private static MainWindow _mainWindow;
        private static WachtwoordView _wachtwoordView;

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
                case ("stageBedrijfViewModel"):
                    if (_stageBedrijfView == null)
                        _stageBedrijfView = new StageBedrijfViewModel(app);
                    return _stageBedrijfView;
                case ("stageBedrijfOverzichtViewModel"):
                    if (_stageBedrijfOverzichtView == null)
                        _stageBedrijfOverzichtView = new StagebedrijfOverzichtViewModel(app);
                    return _stageBedrijfOverzichtView;
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
                case ("gebruikerViewModel"):
                    if (_gebruikerView == null)
                        _gebruikerView = new GebruikerViewModel(app);
                    _gebruikerView.fillUserTable();
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
                case ("stageopdrachtViewModel"):
                    if (_stageopdrachtView == null)
                        _stageopdrachtView = new StageopdrachtViewModel(app);
                    return _stageopdrachtView;
                case ("studentPersoonViewModel"):
                    if (_studentPersoonView == null)
                        _studentPersoonView = new StudentPersoonViewModel(app);
                    return _studentPersoonView;
                case ("studentViewModel"):
                    if (_studentView == null)
                        _studentView = new StudentViewModel(app);
                    return _studentView;
                case ("wachtwoordViewModel"):
                    _wachtwoordViewModel = new WachtwoordViewModel(app);
                    _wachtwoordView = new WachtwoordView();
                    _wachtwoordView.DataContext = _wachtwoordViewModel;
                    _wachtwoordView.Show();
                    return _wachtwoordViewModel;
                case ("mainWindowModel"):
                    if (_mainWindowModel == null)
                    {
                        _mainWindowModel = new MainWindowModel(app);
                        _mainWindow = new MainWindow();
                        _mainWindow.DataContext = _mainWindowModel;
                        _mainWindow.Closed += app.OnWindowClose;
                    }
                    if (_loginView != null)
                    {
                        _loginView.Hide();
                    }
                    _mainWindow.Show();
                    return _mainWindowModel;
                case ("logInViewModel"):
                    if (_logInViewModel == null)
                    {
                        _logInViewModel = new LogInViewModel(app);
                        _loginView = new LogInView();
                        _loginView.DataContext = _logInViewModel;
                        _loginView.Closed += app.OnWindowClose;
                        _logInViewModel.PasswordBox = _loginView.PasswordBox;
                    }
                    if (_mainWindow != null)
                    {
                        _mainWindow.Hide();
                    }
                    _loginView.Show();
                    return _logInViewModel;
                default:
                    return null;
            }
        }
    }
}
