using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.Components.Patient;
using Notarius.Client.DataModel;
using Notarius.Client.Services;

namespace Notarius.Client.Pages
{
    public partial class PatientsBase: ComponentBase
    {
        protected Notarius.Client.Components.Patient.PatientsList patientList = new Notarius.Client.Components.Patient.PatientsList();   


    }
}
