using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StageViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;

        private bool _afstuderen;

        public StageViewModel(IApplicationController app)
        {
            _app = app;
        }

        public bool Afstuderen
        {
            get { return _afstuderen; }
            set { _afstuderen = value; }
        }

    }
}
