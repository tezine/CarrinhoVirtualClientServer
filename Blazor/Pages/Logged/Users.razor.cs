#region Imports
using Microsoft.AspNetCore.Components;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Frontend.Pages {
    public class ClientesBase:ComponentBase {
        public List<EUser> clientes = new List<EUser>();
        //[Inject] public SUsersService ClientsService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync() {
            //clientes = ClientsService.GetAll("fdkdhd");
        }
        #endregion

        public void OnToolbarClicked() {
            navigationManager.NavigateTo("ClienteInfo");
        }
    }
}
