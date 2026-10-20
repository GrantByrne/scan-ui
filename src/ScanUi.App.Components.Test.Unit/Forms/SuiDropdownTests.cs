using Bunit;
using ScanUi.App.Components.Forms;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiDropdownTests
{
    [Fact]
    public void Renders_the_selected_option_and_reports_selection_changes()
    {
        using var context = new BunitContext();
        var options = new[]
        {
            new SuiDropdownOption("feeder", "Document feeder"),
            new SuiDropdownOption("flatbed", "Flatbed"),
        };
        string? selectedValue = null;
        var rendered = context.Render<SuiDropdown>(parameters =>
            parameters
                .Add(component => component.Id, "source")
                .Add(component => component.Options, options)
                .Add(component => component.Value, "feeder")
                .Add(
                    component => component.ValueChanged,
                    value =>
                    {
                        selectedValue = value;
                        return Task.CompletedTask;
                    }
                )
                .Add(
                    component => component.AdditionalAttributes,
                    new Dictionary<string, object> { ["aria-describedby"] = "source-help" }
                )
        );

        var dropdown = rendered.Find("select#source");
        Assert.Equal("source-help", dropdown.GetAttribute("aria-describedby"));
        Assert.Equal("feeder", ((AngleSharp.Html.Dom.IHtmlSelectElement)dropdown).Value);
        Assert.Equal("Flatbed", dropdown.QuerySelector("option[value='flatbed']")!.TextContent);

        dropdown.Change("flatbed");

        Assert.Equal("flatbed", selectedValue);
    }
}
