using Bunit;
using ScanUi.App.Components;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiBreadcrumbTests
{
    [Fact]
    public void Renders_each_segment_and_marks_the_current_page()
    {
        using var context = new BunitContext();
        var rendered = context.Render<SuiBreadcrumb>(parameters => parameters
            .Add(component => component.Items, new[] { "Workspace", "Scans", "Scan documents" }));

        var breadcrumb = rendered.Find("nav[aria-label='Breadcrumb']");
        Assert.Equal(
            new[] { "Workspace", "Scans" },
            breadcrumb.QuerySelectorAll("span").Select(item => item.TextContent.Trim()));
        Assert.Equal("Scan documents", breadcrumb.QuerySelector("strong[aria-current='page']")!.TextContent.Trim());
        Assert.Equal(2, breadcrumb.QuerySelectorAll("svg[aria-hidden='true']").Length);
    }
}
