using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using TazorLib.Codes;

namespace TazorLib.Components {
    public class VRowLayoutBase :VItemComponent{
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool CenterHorizontally { get; set; } = false;
        [Parameter] public string HorizontalAlignItems { get; set; } = TazorLib.Enums.HorizontalAlignItems.Center;

        protected override void OnInitialized() {
            if (this.CenterHorizontally) { 
                if (String.IsNullOrEmpty(this.Width)) Logger.LogError(this, "Unable to center horizontally since width is no set");
                if (!String.IsNullOrEmpty(this.Margin)) Logger.LogWarning(this, "replacing margin");
                this.Margin = "0 auto";
            }
        }
    }
}
