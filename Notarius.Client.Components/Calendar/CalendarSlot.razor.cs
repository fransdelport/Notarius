using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Notarius.Client.Components.Patient;
using Notarius.Client.DataModel;
using Notarius.Client.Services.Schedule;

namespace Notarius.Client.Components.Calendar
{
    public partial class CalendarSlotBase : ComponentBase
    {

        [Inject]
        private IScheduleService ScheduleService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseCallback { get; set; }

      
        protected SelectPatientDialog SelectPatientDialog { get; set; } = new SelectPatientDialog();


        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public DateTime ScheduleDate { get; set; } = DateTime.Now;

        [Parameter]
        public string Day { get; set; } = "Monday";

        [Parameter]
        public int DayOfWeek { get; set; }

        [Parameter]
        public ScheduleUI Scheduler { get; set; } = new ScheduleUI();

        public string CSSClass = "schedulebutton";

        protected string Text 
        {
            get { return Scheduler.ToString(); } 
        }

        protected string ID 
        {
            get { return Day + ScheduleTime; }
        }

        protected string ScheduleTime 
        { 
            get 
            {
                DateTime dt;
                DateTime.TryParse(Scheduler.ScheduleTime, out dt);
                return dt.ToShortTimeString();
            } 
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected void ScheduleClicked(MouseEventArgs e,  string ID)
        {
            SelectPatientDialog.Show();
           
            CSSClass = "schedulebuttonBooked";
            StateHasChanged();
        }
        protected async void SelectAddDialog_OnDialogClose()
        {
            StateHasChanged();
        }
        protected async void SelectCallback(PatientUI p)
        {
            Scheduler.patient = p;
            Scheduler.MRN = p.MRN;
            Scheduler.ScheduleTime = ScheduleDate.ToString();
            Console.WriteLine(Scheduler.patient.Firstname);
            Console.WriteLine(Scheduler.MRN);
            Console.WriteLine(Scheduler.ScheduleTime);
            //We need to save the schedule here to the database
            await ScheduleService.Update(Scheduler);
            StateHasChanged();
        }
    }
}
