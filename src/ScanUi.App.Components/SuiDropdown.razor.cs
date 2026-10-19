using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components;

public partial class SuiDropdown
{
    [Parameter, EditorRequired]
    public string Id { get; set; } = null!;

    [Parameter, EditorRequired]
    public IReadOnlyList<SuiDropdownOption> Options { get; set; } = null!;

    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string DropdownClass => string.IsNullOrWhiteSpace(CssClass) ? "sui-dropdown-select" : $"sui-dropdown-select {CssClass}";

    private Task HandleChangeAsync(ChangeEventArgs args) =>
        ValueChanged.InvokeAsync(args.Value?.ToString() ?? string.Empty);
}
