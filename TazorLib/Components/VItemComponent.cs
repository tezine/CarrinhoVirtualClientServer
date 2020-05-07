using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TazorLib.Enums;

namespace TazorLib.Components {
    public class VItemComponent:ComponentBase {
        [Parameter] public string ID { get; set; }
        [Parameter] public string Width { get; set; } = "auto";
        [Parameter] public string Height { get; set; } = "auto";
        [Parameter] public string BackgroundColor { get; set; } = "auto";
        [Parameter] public string Padding { get; set; }
        [Parameter] public string PaddingTop { get; set; }
        [Parameter] public string PaddingBottom { get; set; }
        [Parameter] public string PaddingLeft { get; set; }
        [Parameter] public string PaddingRight { get; set; }
        [Parameter] public string Margin { get; set; }
        [Parameter] public string MarginLeft { get; set; }
        [Parameter] public string MarginRight { get; set; }
        [Parameter] public string MarginTop { get; set; }
        [Parameter] public string MarginBottom { get; set; }
        [Parameter] public string Display { get; set; } = "block";

        [Parameter] public string GridLayoutColumnStart { get; set; }
        [Parameter] public string GridLayoutColumnEnd { get; set; }
        [Parameter] public string GridLayoutRowEnd { get; set; }

        [Parameter] public string CursorType { get; set; } = TazorLib.Enums. CursorType.Auto;
    }
}
