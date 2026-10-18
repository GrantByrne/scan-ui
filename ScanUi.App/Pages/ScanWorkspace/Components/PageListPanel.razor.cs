using Microsoft.AspNetCore.Components;
using ScanUi.App.Pages.Workspace;

namespace ScanUi.App.Pages.ScanWorkspace.Components;

public partial class PageListPanel
{
    [Parameter, EditorRequired]
    public IReadOnlyList<WorkspacePage> Pages { get; set; } = Array.Empty<WorkspacePage>();
}
