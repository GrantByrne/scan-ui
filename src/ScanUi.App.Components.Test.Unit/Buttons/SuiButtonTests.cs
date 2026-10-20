using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ScanUi.App.Components;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiButtonTests
{
    [Fact]
    public void Renders_content_slots_in_start_child_end_order()
    {
        using var context = new BunitContext();
        RenderFragment startContent = builder => builder.AddContent(0, "Start");
        RenderFragment childContent = builder => builder.AddContent(0, "Action");
        RenderFragment endContent = builder => builder.AddContent(0, "End");
        var rendered = context.Render<SuiButton>(parameters =>
            parameters
                .Add(button => button.StartContent, startContent)
                .Add(button => button.ChildContent, childContent)
                .Add(button => button.EndContent, endContent)
        );

        Assert.Equal("StartActionEnd", rendered.Find("button").TextContent);
    }

    [Fact]
    public void Composes_options_and_forwards_attributes_to_a_native_button()
    {
        using var context = new BunitContext();
        var rendered = context.Render<SuiButton>(parameters =>
            parameters
                .Add(button => button.Variant, SuiButtonVariant.Ghost)
                .Add(button => button.Color, SuiButtonColor.Default)
                .Add(button => button.Size, SuiButtonSize.Medium)
                .Add(button => button.Radius, SuiButtonRadius.Full)
                .Add(button => button.FullWidth, true)
                .Add(button => button.IconOnly, true)
                .Add(button => button.Class, "tool")
                .Add(button => button.Style, "outline: 0;")
                .Add(
                    button => button.AdditionalAttributes,
                    new Dictionary<string, object>
                    {
                        ["title"] = "Enhance",
                        ["aria-label"] = "Enhance document",
                        ["data-action"] = "enhance",
                    }
                )
        );
        var button = rendered.Find("button");

        Assert.Equal("button", button.GetAttribute("type"));
        Assert.Equal(
            "sui-button sui-button-variant-ghost sui-button-color-default "
                + "sui-button-size-medium sui-button-radius-full sui-button-full-width "
                + "sui-button-icon-only tool",
            button.GetAttribute("class")
        );
        Assert.Equal("outline: 0;", button.GetAttribute("style"));
        Assert.Equal("Enhance", button.GetAttribute("title"));
        Assert.Equal("Enhance document", button.GetAttribute("aria-label"));
        Assert.Equal("enhance", button.GetAttribute("data-action"));
    }

    [Fact]
    public async Task Component_parameters_override_conflicting_unmatched_attributes()
    {
        using var context = new BunitContext();
        var invocationCount = 0;
        var rendered = context.Render<SuiButton>(parameters =>
            parameters
                .Add(button => button.Type, SuiButtonType.Submit)
                .Add(button => button.Class, "owned")
                .Add(button => button.Style, "outline: 0;")
                .Add(
                    button => button.OnClick,
                    EventCallback.Factory.Create<MouseEventArgs>(this, _ => invocationCount++)
                )
                .Add(
                    button => button.AdditionalAttributes,
                    new Dictionary<string, object>
                    {
                        ["type"] = "reset",
                        ["disabled"] = true,
                        ["class"] = "forwarded",
                        ["style"] = "color: red;",
                        ["onclick"] = "untrusted",
                    }
                )
        );
        var button = rendered.Find("button");

        Assert.Equal("submit", button.GetAttribute("type"));
        Assert.False(button.HasAttribute("disabled"));
        Assert.Equal("sui-button owned", button.GetAttribute("class"));
        Assert.Equal("outline: 0;", button.GetAttribute("style"));
        await button.ClickAsync();
        Assert.Equal(1, invocationCount);
    }

    [Fact]
    public async Task Invokes_enabled_click_and_suppresses_disabled_click()
    {
        using var context = new BunitContext();
        var invocationCount = 0;
        var enabled = context.Render<SuiButton>(parameters =>
            parameters.Add(
                button => button.OnClick,
                EventCallback.Factory.Create<MouseEventArgs>(this, _ => invocationCount++)
            )
        );

        await enabled.Find("button").ClickAsync();

        Assert.Equal(1, invocationCount);

        var disabled = context.Render<SuiButton>(parameters =>
            parameters
                .Add(button => button.Disabled, true)
                .Add(
                    button => button.OnClick,
                    EventCallback.Factory.Create<MouseEventArgs>(this, _ => invocationCount++)
                )
        );
        var disabledButton = disabled.Find("button");

        Assert.True(disabledButton.HasAttribute("disabled"));
        await disabledButton.ClickAsync();

        Assert.Equal(1, invocationCount);
    }
}
