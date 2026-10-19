using ScanUi.App.Components;

namespace ScanUi.App.Pages.ScanWorkspace.Components;

public partial class ScanSettingsPanel
{
    private static readonly IReadOnlyList<SuiDropdownOption> ScannerOptions =
    [
        new("Brother DS-740D", "Brother DS-740D"),
        new("Choose a scanner…", "Choose a scanner…")
    ];

    private static readonly IReadOnlyList<SuiDropdownOption> SourceOptions =
    [
        new("Document feeder", "Document feeder"),
        new("Manual single sheet", "Manual single sheet"),
        new("Flatbed", "Flatbed")
    ];

    private static readonly IReadOnlyList<SuiDropdownOption> ColorOptions =
    [
        new("Color", "Color"),
        new("Grayscale", "Grayscale"),
        new("Black & white", "Black & white")
    ];

    private static readonly IReadOnlyList<SuiDropdownOption> ResolutionOptions =
    [
        new("300 DPI", "300 DPI"),
        new("150 DPI", "150 DPI"),
        new("600 DPI", "600 DPI")
    ];

    private static readonly IReadOnlyList<SuiDropdownOption> PaperSizeOptions =
    [
        new("Auto detect", "Auto detect"),
        new("US Letter", "US Letter"),
        new("A4", "A4"),
        new("Legal", "Legal"),
        new("Receipt", "Receipt")
    ];

    private string _selectedScanner = "Brother DS-740D";
    private string _selectedSource = "Document feeder";
    private string _selectedColor = "Color";
    private string _selectedResolution = "300 DPI";
    private string _selectedPaperSize = "Auto detect";
    private bool _doubleSided = true;
    private bool _automaticallyDeskew = true;
    private bool _removeBlankPages;
}
