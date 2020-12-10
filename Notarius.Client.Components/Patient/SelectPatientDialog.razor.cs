using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.DataModel;
using Notarius.Client.Services;

namespace Notarius.Client.Components.Patient
{
    public partial class SelectPatientDialogBase : ComponentBase
    {

        [Inject]
        public IPatientService PatientService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseCallback { get; set; }

        [Parameter]
        public EventCallback<PatientUI> SelectCallback { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }


        public IEnumerable<PatientUI> Patients { get; set; } = null;

        [Parameter]
        public PatientUI SelectedPatient { get; set; }

        public bool ShowDialog { get; set; }

        public void Show()
        {
            ShowDialog = true;
            Reset();
            StateHasChanged();
        }

        private void Reset()
        {
          //  patientList = new PatientsList();
        }
        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
        protected async override Task OnInitializedAsync()
        {
            Patients = await PatientService.GetAllPatients();
        }


        public async void PatientSelect(PatientUI patient)
        {
            await SelectCallback.InvokeAsync(patient);

            await CloseCallback.InvokeAsync(true);

            ShowDialog = false;
            StateHasChanged();

        }
    }
}
