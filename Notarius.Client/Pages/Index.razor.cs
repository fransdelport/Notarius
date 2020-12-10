using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.Components.General;

namespace Notarius.Client.Pages
{
    public partial class IndexBase:ComponentBase
    {
        public HomePageLink PatientHomeLink { get; set; } = new HomePageLink();

        protected string PatientSrc { get; set; } = "/Images/patients.jpg";

        protected override Task OnInitializedAsync()
        {
           // PatientHomeLink.ImageSRC = "/Images/patients.jpg";

            return base.OnInitializedAsync();
        }

    }
}
