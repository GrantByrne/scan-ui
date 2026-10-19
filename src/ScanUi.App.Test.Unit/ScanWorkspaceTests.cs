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
        Assert.Empty(rendered.FindAll(".pill-concept"));
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

    [Fact]
    public void Migrated_buttons_preserve_labels_accessible_names_and_visual_hooks()
    {
        using var context = new BunitContext();
        var rendered = context.Render<App>();

        Assert.Equal(16, rendered.FindAll("button.sui-button").Count);

        void AssertButton(
            string selector,
            string visibleText,
            string? title,
            string? accessibleName,
            params string[] expectedClasses)
        {
            var button = rendered.Find(selector);
            Assert.Equal(visibleText, button.TextContent.Trim());
            Assert.Equal(title, button.GetAttribute("title"));
            Assert.Equal(accessibleName, button.GetAttribute("aria-label"));

            var classes = button.GetAttribute("class")!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Assert.Contains("sui-button", classes);
            foreach (var expectedClass in expectedClasses)
            {
                Assert.Contains(expectedClass, classes);
            }
        }

        AssertButton("button.tool[title='Auto enhance']", "Enhance", "Auto enhance", null,
            "tool", "sui-button-variant-ghost", "sui-button-color-default");
        AssertButton("button.tool[title='Auto crop']", "Crop", "Auto crop", null,
            "tool", "sui-button-variant-ghost", "sui-button-color-default");
        AssertButton("button.tool[title='Rotate 90 degrees']", "", "Rotate 90 degrees", "Rotate 90 degrees",
            "tool", "sui-button-variant-ghost", "sui-button-color-default", "sui-button-icon-only");
        AssertButton("button.tool.active[title='Toggle searchable text']", "", "Toggle searchable text",
            "Toggle searchable text", "tool", "active", "sui-button-variant-ghost",
            "sui-button-color-default", "sui-button-icon-only");

        AssertButton(".zoomtools button[title='Zoom out']", "", "Zoom out", "Zoom out",
            "sui-button-variant-light", "sui-button-color-default", "sui-button-icon-only");
        AssertButton(".zoomtools button[title='Zoom in']", "", "Zoom in", "Zoom in",
            "sui-button-variant-light", "sui-button-color-default", "sui-button-icon-only");
        AssertButton(".zoomtools button[title='Reset zoom']", "", "Reset zoom", "Reset zoom",
            "sui-button-variant-light", "sui-button-color-default", "sui-button-icon-only");
        AssertButton("button.addpage", "Add another page", null, null,
            "addpage", "sui-button-variant-outlined", "sui-button-color-default", "sui-button-full-width");

        for (var pageNumber = 1; pageNumber <= 3; pageNumber++)
        {
            AssertButton(
                $"button.tinybtn[aria-label='Move page {pageNumber} up']",
                "",
                "Move page up",
                $"Move page {pageNumber} up",
                "tinybtn", "sui-button-variant-ghost", "sui-button-color-default", "sui-button-icon-only");
            AssertButton(
                $"button.tinybtn[aria-label='Remove page {pageNumber}']",
                "",
                "Remove page",
                $"Remove page {pageNumber}",
                "tinybtn", "sui-button-variant-ghost", "sui-button-color-default", "sui-button-icon-only");
        }

        AssertButton("button.scanprimary", "Scan pages", null, null,
            "primary", "scanprimary", "sui-button-variant-solid", "sui-button-color-primary",
            "sui-button-full-width");
        AssertButton("button.exportprimary", "Export document", null, null,
            "primary", "exportprimary", "sui-button-variant-solid", "sui-button-color-primary");
    }
}
