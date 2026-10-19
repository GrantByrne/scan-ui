using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components;

public partial class SuiBreadcrumb
{
    [Parameter, EditorRequired]
    public IReadOnlyList<string> Items { get; set; } = Array.Empty<string>();
}
