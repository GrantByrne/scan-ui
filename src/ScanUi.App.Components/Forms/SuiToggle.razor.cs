using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components.Forms;

public partial class SuiToggle
{
    [Parameter, EditorRequired]
    public string Label { get; set; } = null!;

    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string ToggleClass =>
        string.IsNullOrWhiteSpace(CssClass) ? "sui-toggle" : $"sui-toggle {CssClass}";

    private Task ToggleAsync() => Disabled ? Task.CompletedTask : ValueChanged.InvokeAsync(!Value);
}
