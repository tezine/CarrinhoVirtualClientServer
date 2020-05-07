#region Imports
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Frontend.Codes;
using Frontend.Services;
using Syncfusion.Blazor;
#endregion

namespace Frontend {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM4MjcwQDMxMzgyZTMxMmUzMENJUDllQU5FZXRXd1NubmNiU2NoamFmRGtWUkdCVHpZWktGUW1DTFdDTkU9");

            //builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(ClientDefines.BaseURL) });
            AddClientSingletons(builder.Services);
            await builder.Build().RunAsync();
        }

        #region AddClientSingletons
        private static void AddClientSingletons(IServiceCollection services) {
            services.AddSyncfusionBlazor();
            services.AddSingleton<SCompaniesAdminUsersBlazorService>();
            services.AddSingleton<SProductsBlazorService>();
            services.AddSingleton<SUsersBlazorService>();
            
        }
        #endregion
    }

}
