using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components;

public partial class Pill
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    [Parameter]
    public string CssClass { get; set; } = string.Empty;
}
