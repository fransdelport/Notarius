using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.DataModel;
using Notarius.Client.Services;

namespace Notarius.Client.Components.Patient
{
    public partial class PatientDetailsBase : ComponentBase
    {
        [Parameter]
        public PatientUI Patient { get; set; }


        [Inject]
        private IPatientService PatientService { get; set; }

        public bool Saved { get; set; } = false;
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;

        protected bool SaveButtonDisabled { get; set; } = true;

        protected async Task HandleValidSubmit()
        {

        }

        [Parameter]
        public EventCallback<bool> OnShowToggle { get; set; }

        protected async Task HandleInvalidSubmit()
        {

        }
        public void SaveClick()
        {
            PatientService.UpdatePatient(Patient);
            SaveButtonDisabled = true;
        }
        public void DataChanged()
        {
            SaveButtonDisabled = false;
        }
    }
}
