namespace ScanUi.App.Pages.Workspace;

public sealed record WorkspacePage(int Number, string Title, int Dpi);

public partial class ScanWorkspace
{
    private static readonly IReadOnlyList<WorkspacePage> SamplePages =
    [
        new(1, "Invoice", 300),
        new(2, "Agreement", 300),
        new(3, "Visit notes", 300)
    ];
}
