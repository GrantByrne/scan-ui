using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components;

public partial class Breadcrumb
{
    [Parameter, EditorRequired]
    public IReadOnlyList<string> Items { get; set; } = Array.Empty<string>();
}
