using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;
using Notarius.Client.Components.Calendar;

namespace Notarius.Client.Pages
{
    public partial class CalendarBase: ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public CalendarCol CalCol { get; set; } = new CalendarCol();

    }
}
