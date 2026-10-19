using Bunit;
using ScanUi.App.Components;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiPillTests
{
    [Fact]
    public void Renders_content_with_shared_and_custom_classes()
    {
        using var context = new BunitContext();
        var rendered = context.Render<SuiPill>(parameters => parameters
            .Add(component => component.CssClass, "pill-concept")
            .Add(component => component.ChildContent, content => content.AddContent(0, "Interactive concept")));

        var pill = rendered.Find("span.pill.pill-concept");
        Assert.Equal("Interactive concept", pill.TextContent);
    }
}
