#region Imports
using Microsoft.AspNetCore.Components;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Frontend.Pages.Logged {
    public class ClienteInfoBase:ComponentBase {
        public string clienteID = "";
        public EUser Client = new EUser();
        //[Inject] public SUsersService ClientsService { get; set; }

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync() {
          //  Client = ClientsService.GetByID(clienteID);  
        }
        #endregion
    }
}
