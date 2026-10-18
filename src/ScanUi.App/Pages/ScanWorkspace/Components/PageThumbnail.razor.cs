using Microsoft.AspNetCore.Components;
using ScanUi.App.Pages.Workspace;

namespace ScanUi.App.Pages.ScanWorkspace.Components;

public partial class PageThumbnail
{
    [Parameter, EditorRequired]
    public WorkspacePage PageData { get; set; } = null!;

    [Parameter]
    public bool IsSelected { get; set; }
}
