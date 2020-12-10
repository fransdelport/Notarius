using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Notarius.Client.Components.General
{
    public partial class HomePageLinkBase :ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public string homepageCSS { get; set; } = "circle";

       [Parameter]
        public string ImageSRC { get; set; }
       

        protected void MouseOver(MouseEventArgs e)
        {
            homepageCSS = "cirlesquare";
        }
        protected void MouseOut(MouseEventArgs e)
        {
            homepageCSS = "circle";
        }
        protected void MouseDown(MouseEventArgs e)
        {
            homepageCSS = "cirleclick";
        }

        protected void MouseUp(MouseEventArgs e)
        {
            homepageCSS = "circlesquare";
        }
    }
}
