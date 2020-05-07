#region Imports
using Frontend.Codes;
using Frontend.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks; 
#endregion

namespace Frontend.Pages {
    public class LoginBase: BasePage {

        #region Fields;
        public ECompanyAdminUser e = new ECompanyAdminUser { Email = "bruno@tezine.com", Password = "tata" };
        [Inject] public SCompaniesAdminUsersBlazorService usersService { get; set; }
        #endregion

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync() {

        } 
        #endregion

        #region OnBtnLoginClicked
        protected async Task OnBtnLoginClicked() {
            try {
                //var eUser = await httpClient.PostJsonAsync<EUser>(SDefines.BaseURL + "/api/v1/SUsers/Authenticate", e);
                var eUser = await usersService.Authenticate(e);
                if (eUser != null) {
                    NavigationManager.NavigateTo(ClientDefines.RouteDashboard);
                }
            } catch (Exception ex) {
                ClientLogger.LogError(ex);
            }
        } 
        #endregion
    }
}
