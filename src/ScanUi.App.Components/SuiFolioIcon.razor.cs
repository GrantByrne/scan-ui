using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components;

public enum FolioIconName
{
    Scan,
    Grid,
    File,
    Clock,
    Settings,
    Plus,
    Chevron,
    ChevronRight,
    Rotate,
    Spark,
    Crop,
    Text,
    Trash,
    ArrowUp,
    Download,
    Import,
    Minus,
    Max,
    Check,
    Info
}

public partial class SuiFolioIcon
{
    [Parameter, EditorRequired]
    public FolioIconName Name { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    protected string IconClass => string.IsNullOrWhiteSpace(CssClass) ? "icon" : $"icon {CssClass}";

    protected string SymbolHref => Name switch
    {
        FolioIconName.Scan => "#ic-scan",
        FolioIconName.Grid => "#ic-grid",
        FolioIconName.File => "#ic-file",
        FolioIconName.Clock => "#ic-clock",
        FolioIconName.Settings => "#ic-settings",
        FolioIconName.Plus => "#ic-plus",
        FolioIconName.Chevron => "#ic-chevron",
        FolioIconName.ChevronRight => "#ic-chevron-right",
        FolioIconName.Rotate => "#ic-rotate",
        FolioIconName.Spark => "#ic-spark",
        FolioIconName.Crop => "#ic-crop",
        FolioIconName.Text => "#ic-text",
        FolioIconName.Trash => "#ic-trash",
        FolioIconName.ArrowUp => "#ic-arrow-up",
        FolioIconName.Download => "#ic-download",
        FolioIconName.Import => "#ic-import",
        FolioIconName.Minus => "#ic-minus",
        FolioIconName.Max => "#ic-max",
        FolioIconName.Check => "#ic-check",
        FolioIconName.Info => "#ic-info",
        _ => throw new ArgumentOutOfRangeException(nameof(Name), Name, null)
    };
}
