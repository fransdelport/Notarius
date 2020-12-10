using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notarius.Client.Services;
using Notarius.Client.Services.Schedule;
using Radzen;

namespace Notarius.ClientApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<Notarius.Client.App>("app");

            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://127.0.0.1:25214/") });//builder.HostEnvironment.BaseAddress

            builder.Services.AddHttpClient<IPatientService, PatientService>(client => client.BaseAddress = new Uri("http://127.0.0.1:25214"));
            builder.Services.AddHttpClient<IScheduleService, ScheduleService>(client => client.BaseAddress = new Uri("http://127.0.0.1:25214"));
            //Radzen Services
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            await builder.Build().RunAsync();
        }
    }
}
