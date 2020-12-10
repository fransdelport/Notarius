using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.Components.Patient;
using Notarius.Client.DataModel;
using Notarius.Client.Services;

namespace Notarius.Client.Components
{
    public partial class PatientsListBase: ComponentBase
    {


        [Parameter]
        public EventCallback<bool> CloseCallback { get; set; }

        [Parameter]
        public EventCallback<bool> SelectCallback { get; set; }


        public IEnumerable<PatientUI> Patients { get; set; } = null;
        protected AddPatientDialog AddPatientDialog { get; set; }

        [Parameter]
        public bool NotEditAble { get; set; }

        [Inject]
        public IPatientService PatientService { get; set; }

    
        protected async override Task OnInitializedAsync()
        {
              Patients = await PatientService.GetAllPatients();
        }
        protected void QuickAddPatient()
        {
            AddPatientDialog.Show();
        }
        public async void PatientAddDialog_OnDialogClose()
        {

            Patients = (await PatientService.GetAllPatients());
            StateHasChanged();
        }

        protected async void PatientDeleted(PatientUI patient)
        {
            await  PatientService.DeletePatient(patient.MRN);
            Patients = (await PatientService.GetAllPatients());
            StateHasChanged();
        }
       

        public bool ShowDialog { get; set; }

        public void Show()
        {
            ShowDialog = true;
            Reset();
            StateHasChanged();
        }

        private void Reset()
        {
            Patients = List<PatientUI>();
        }

        private IEnumerable<T> List<T>()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
    }
}
