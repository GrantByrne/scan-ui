using Microsoft.AspNetCore.Components;

namespace ScanUi.App.Components;

public partial class SuiForms
{
    [Parameter, EditorRequired]
    public string Label { get; set; } = null!;

    [Parameter, EditorRequired]
    public string For { get; set; } = null!;

    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    [Parameter]
    public string? HelpText { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    private string FormClass => string.IsNullOrWhiteSpace(CssClass) ? "sui-forms" : $"sui-forms {CssClass}";

    private string HelpTextId => $"{For}-help";
}
