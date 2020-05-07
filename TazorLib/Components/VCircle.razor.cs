using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace TazorLib.Components {
    public class VCircleBase:VItemComponent {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
