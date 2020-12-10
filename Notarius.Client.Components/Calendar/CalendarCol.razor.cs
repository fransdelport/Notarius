using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.DataModel;
using Notarius.Client.DataModel.General;
using Notarius.Client.Services.Schedule;

namespace Notarius.Client.Components.Calendar
{
    public partial class CalendarColBase:ComponentBase
    {
        [Inject]
        protected IScheduleService ScheduleService { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public CalendarSlot CalSlot { get; set; } = new CalendarSlot();

        public IEnumerable<ScheduleUI> Schedules { get; set; }

        [Parameter]
        public string Day { get; set; } = "Monday";

        [Parameter]
        public int DayOfWeek { get; set; }

        [Parameter]
        public DateTime Date { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadSchedules();
            base.OnInitializedAsync();
        }

        protected async Task<IEnumerable<ScheduleUI>> LoadSchedules()
        {
            //Load the schedules from the database
            DateTime dt = DateFunctions.GetFirstDayOfWeek(DateTime.Now);
            dt = dt.AddDays(DayOfWeek);
            Day = Day + ":" + dt.ToShortDateString();
            DateTime d;
            DateTime.TryParse(dt.ToShortDateString(), out d);
            Date = d;
            Schedules = await ScheduleService.GetAll(dt);
            return Schedules;

        }

    }
}
