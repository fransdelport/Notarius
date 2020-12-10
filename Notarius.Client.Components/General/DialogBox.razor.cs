using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Notarius.Client.Components.UITheme;

namespace Notarius.Client.Components.General
{
    public class DialogBoxBase:ComponentBase
    {

        protected bool ShowConfirmation { get; set; } = false;

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirm Delete";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure you want to delete";
        public bool ShowDialog { get; set; }
        public async Task<bool> Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
            
            return true;
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }

    }
}
