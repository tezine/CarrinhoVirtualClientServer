using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TazorLib.Components {
    public class VLabelBase:VItemComponent {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string TextAlign { get; set; }
        [Parameter] public string Color { get; set; }
        [Parameter] public string FontSize { get; set; } = "auto";
        [Parameter] public string FontWeight { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public string LineHeight { get; set; }
        [Parameter] public bool VerticalAlign { get; set; }


        protected override void OnInitialized() {
            if (this.VerticalAlign) {
                //if(!this.height)Logger.logError(this, 'please set the height in order to vertical center');
                this.LineHeight = this.Height;
            }
            if ((this.TextAlign == TazorLib.Enums.TextAlign.Right || this.TextAlign == TazorLib.Enums.TextAlign.Center) && (String.IsNullOrEmpty(this.Width))) {
                //Logger.logError(this,'please set the width in order to text-align ');
            }
        }
    }
}
