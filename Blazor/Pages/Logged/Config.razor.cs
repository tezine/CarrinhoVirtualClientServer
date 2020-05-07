#region Imports
using Microsoft.AspNetCore.Components;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Frontend.Pages.Logged {

    public class ConfigBase:ComponentBase {
        public List<EAdminUser> adminUsers = new List<EAdminUser>();
        //[Inject] public SAdminUsersService adminUsersService { get; set; }

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync() {
          //  adminUsers =  adminUsersService.GetAll();
        }
        #endregion

        public void OnBtnSalvarClicked() {

        }

    }
}
