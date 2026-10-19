using Bunit;
using AngleSharp.Html.Dom;
using ScanUi.App;
using Xunit;

namespace ScanUi.App.Test.Unit;

public class ScanWorkspaceTests
{
    [Fact]
    public void Root_route_displays_workspace_samples_settings_and_accessible_controls()
    {
        using var context = new BunitContext();
        var rendered = context.Render<App>();

        Assert.Equal("Scan workspace", rendered.Find("h1").TextContent.Trim());
        Assert.Equal("Interactive concept", rendered.Find(".pill-concept").TextContent.Trim());
        Assert.Equal("03", rendered.Find(".pill-count").TextContent.Trim());
        Assert.Equal("PDF", rendered.Find(".pill-format").TextContent.Trim());
        var breadcrumb = rendered.Find("nav[aria-label='Breadcrumb']");
        Assert.Equal("Workspace", breadcrumb.QuerySelector("span")!.TextContent.Trim());
        Assert.Equal(
            "Scan documents",
            breadcrumb.QuerySelector("strong[aria-current='page']")!.TextContent.Trim());
        Assert.Empty(rendered.FindAll("aside"));
        Assert.NotNull(rendered.Find("section[aria-label='Scanner workspace']"));
        Assert.NotNull(rendered.Find("section[aria-label='Export']"));
        Assert.Equal(
            new[] { "Invoice · 300 DPI", "Agreement · 300 DPI", "Visit notes · 300 DPI" },
            rendered.FindAll(".pageitem-sub").Select(page => page.TextContent.Trim()));
        Assert.Equal("$730.00", rendered.Find(".doc-total b").TextContent.Trim());

        var settings = new Dictionary<string, string>
        {
            ["scanner"] = "Brother DS-740D",
            ["source"] = "Document feeder",
            ["color"] = "Color",
            ["dpi"] = "300 DPI",
            ["papersize"] = "Auto detect"
        };
        foreach (var (id, value) in settings)
        {
            Assert.NotNull(rendered.Find($"label[for='{id}']"));
            Assert.Equal(value, ((IHtmlSelectElement)rendered.Find($"select#{id}")).Value);
        }

        Assert.Equal("true", rendered.Find("button[aria-label='Double-sided scanning']").GetAttribute("aria-pressed"));
        Assert.Equal("true", rendered.Find("button[aria-label='Automatically deskew']").GetAttribute("aria-pressed"));
        Assert.Equal("false", rendered.Find("button[aria-label='Remove blank pages']").GetAttribute("aria-pressed"));

        var iconOnlyButtons = rendered.FindAll("button")
            .Where(button => string.IsNullOrWhiteSpace(button.TextContent))
            .ToList();
        Assert.NotEmpty(iconOnlyButtons);
        Assert.All(iconOnlyButtons, button =>
            Assert.False(string.IsNullOrWhiteSpace(button.GetAttribute("aria-label"))));
    }
}
