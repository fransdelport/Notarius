using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.Components.UITheme;
using Notarius.Client.DataModel;
using Notarius.Client.Services;

namespace Notarius.Client.Components.Patient
{
    public partial class AddPatientDialogBase:ComponentBase
    {

        [CascadingParameter]
        public Theme ThemeClass { get; set; }
        
        [Parameter]
        public EventCallback<bool> CloseCallback { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public PatientUI Patient { get; set; } = new PatientUI { MRN = "New", Firstname = "", Address = "", Lastname = "", City = "", State = "", Zip = "" };

        [Inject]
        private IPatientService PatientService { get; set; }

      

        public bool ShowDialog { get; set; }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }
        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        public void ResetDialog()
        {
            Patient = new PatientUI { MRN = "New", Firstname = "", Address = "", Lastname = "", City = "", State = "", Zip = "" };
        }

        protected async Task HandleValidSubmit()
        {
            await PatientService.AddPatient(Patient);
            ShowDialog = false;

            await CloseCallback.InvokeAsync(true);
            StateHasChanged();
        }

    }
}
