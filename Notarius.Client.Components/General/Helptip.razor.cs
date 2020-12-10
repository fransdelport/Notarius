using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace Notarius.Client.Components.General
{
    public partial class HelptipBase : ComponentBase
    {


        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool ShowDialog { get; set; }

        [Parameter]
        public string Text { get; set; }

        public void Show()
        {
            ShowDialog = true;

            StateHasChanged();
        }

        public void Hide()
        {
            ShowDialog = false;
            StateHasChanged();
        }

    }
}
