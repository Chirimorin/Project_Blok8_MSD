using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Controllers
{
    public interface IApplicationController
    {
        void OnMainWindowClose(object sender, EventArgs e);
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
        void ShowStudentBedrijfView();
        void ShowStudentPersoonView();
        void ShowStudentView();
        void ShowWachtwoordView();
    }
}
