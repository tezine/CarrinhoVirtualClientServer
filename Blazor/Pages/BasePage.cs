#region Imports
using Frontend.Codes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks; 
#endregion

namespace Frontend.Pages {
    public class BasePage: ComponentBase {
        [Inject] public HttpClient httpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized() {
            base.OnInitialized();
            //httpClient.BaseAddress = new Uri(NavigationManager.BaseUri);
            httpClient.BaseAddress = new Uri(ClientDefines.BaseURL);
        }
    }
}
