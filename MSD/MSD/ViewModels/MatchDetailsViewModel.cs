using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MatchDetailsViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;

        public MatchDetailsViewModel(IApplicationController app)
        {
            _app = app;
        }
    }
}
