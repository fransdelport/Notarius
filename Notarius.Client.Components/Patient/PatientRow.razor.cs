using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Notarius.Client.Components.General;
using Notarius.Client.DataModel;
using Notarius.Client.Services.Schedule;
using Radzen;
using Radzen.Blazor;
//using IJSRuntime JSRuntime;

namespace Notarius.Client.Components.Patient
{
    public partial class PatientRowBase : ComponentBase
    {
       
        protected DialogBoxBase DeleteConfirmation { get; set; }

        protected Helptip Help { get; set; }


        [Parameter]
        public EventCallback<PatientUI> PatientDelete { get; set; }

        [Parameter]
        public EventCallback<PatientUI> PatientSelected { get; set; }

        public bool Selected { get; set; } = false;
        [Parameter]
        public bool SelectBTN { get; set; } = true;
        [Parameter]
        public bool DeleteBTN { get; set; } = false;
        [Parameter]
        public bool EditBTN { get; set; } = false;
        [Parameter]
        public bool NotEditable
        {
            set
            {
                SelectBTN = value == false;
                DeleteBTN = value == true;
                EditBTN = value == true;
            }
        }

        [Parameter]
        public string ParentUrl { get; set; } = "patientslist/";

        [Parameter]
        public PatientUI Patient { get; set; }

        protected override void OnInitialized()
        {
            Help = new Helptip();
            base.OnInitialized();
        }

        public void ShowToggle(bool showDetails)
        {
            Patient.ShowDetails = showDetails;
        }

        protected async void Delete()
        {
            await DeleteConfirmation.Show();
            //if (await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete this patient?"))
            //    return;
        }

        protected async void Select()
        {
            Selected = true;
            await PatientSelected.InvokeAsync(Patient);
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await PatientDelete.InvokeAsync(Patient);
                StateHasChanged();
            }
        }
        protected async void ShowHelp()
        {

            //////Help.ShowDialog = true;
            //////Help.Show();
        }

        protected async void HideHelp()
        {
            //////Help.Hide();
        }

    }
}
