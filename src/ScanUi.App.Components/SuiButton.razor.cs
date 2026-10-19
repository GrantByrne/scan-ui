using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ScanUi.App.Components;

public partial class SuiButton
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? StartContent { get; set; }
    [Parameter] public RenderFragment? EndContent { get; set; }
    [Parameter] public SuiButtonType Type { get; set; } = SuiButtonType.Button;
    [Parameter] public SuiButtonVariant? Variant { get; set; }
    [Parameter] public SuiButtonColor? Color { get; set; }
    [Parameter] public SuiButtonSize? Size { get; set; }
    [Parameter] public SuiButtonRadius? Radius { get; set; }
    [Parameter] public bool Disabled { get; set; } = false;
    [Parameter] public bool FullWidth { get; set; } = false;
    [Parameter] public bool IconOnly { get; set; } = false;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private string HtmlType => Type.ToString().ToLowerInvariant();

    private string ButtonClass
    {
        get
        {
            var classes = new List<string> { "sui-button" };

            if (Variant is { } variant)
            {
                classes.Add($"sui-button-variant-{variant.ToString().ToLowerInvariant()}");
            }

            if (Color is { } color)
            {
                classes.Add($"sui-button-color-{color.ToString().ToLowerInvariant()}");
            }

            if (Size is { } size)
            {
                classes.Add($"sui-button-size-{size.ToString().ToLowerInvariant()}");
            }

            if (Radius is { } radius)
            {
                classes.Add($"sui-button-radius-{radius.ToString().ToLowerInvariant()}");
            }

            if (FullWidth)
            {
                classes.Add("sui-button-full-width");
            }

            if (IconOnly)
            {
                classes.Add("sui-button-icon-only");
            }

            if (!string.IsNullOrWhiteSpace(Class))
            {
                classes.Add(Class);
            }

            return string.Join(" ", classes);
        }
    }

    private Task HandleClickAsync(MouseEventArgs args) =>
        Disabled ? Task.CompletedTask : OnClick.InvokeAsync(args);
}
