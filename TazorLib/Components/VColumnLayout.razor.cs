using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using TazorLib.Codes;

namespace TazorLib.Components {
    public class VColumnLayoutBase : VItemComponent {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool CenterHorizontally { get; set; } = false;
        [Parameter] public string VerticalAlignItems { get; set; } = TazorLib.Enums.VerticalAlignItems.Top;

        protected override void OnInitialized() {
            if (this.CenterHorizontally) {
                if (String.IsNullOrEmpty(this.Width)) Logger.LogError(this, "Unable to center horizontally since width is no set");
                if (!String.IsNullOrEmpty(this.Margin)) Logger.LogWarning(this, "replacing margin");
                this.Margin = "0 auto";
            }
        }
    }
}
