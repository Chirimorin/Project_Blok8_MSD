using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Controllers
{
    public interface IApplicationController
    {
        void OnWindowClose(object sender, EventArgs e);
        void ShowBedrijfView();
        void ShowBedrijfOverzichtView();
        void ShowDocentKwalificatieView();
        void ShowDocentPersoonView();
        void ShowDocentView();
        void ShowGebruikerAccountView();
        void ShowGebruikerContactView();
        void ShowGebruikerView();
        void ShowMatchDetailsView();
        void ShowMatchInvoerView();
        void ShowMatchMogelijkView();
        void ShowMatchSuccesView();
        void ShowStageView();
        void ShowStageopdrachtView();
        void ShowStudentPersoonView();
        void ShowStudentView();
        void ShowWachtwoordView();

        void ShowMainWindow();
        void ShowLoginView();
    }
}
