#region Imports
using Frontend.Codes;
using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
#endregion

namespace Frontend.Pages {
    public class ProdutosBase: BasePage {
        public List<EProduct> produtos = new List<EProduct>();   
        [Inject] public SProductsBlazorService productsService { get; set; }
        public ElementReference fileInput;
        [Inject]IJSRuntime JSRuntime { get; set; }

        #region OnInitializedAsync
        protected override async Task OnInitializedAsync() {
            produtos = await productsService.GetAll("ddff");
        }
        #endregion

        public void OnBtnAddClicked() {
            NavigationManager.NavigateTo(ClientDefines.RouteProductsEdit);
        }

        public void OnFileChanged(ChangeEventArgs args) {

        }

        public async void OnBtnImportCSVClicked() {
            await JSRuntime.InvokeVoidAsync("click", fileInput);
        }
    }
}
