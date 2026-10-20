using Bunit;
using ScanUi.App.Components.Forms;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiFormsTests
{
    [Fact]
    public void Associates_the_label_and_help_text_with_the_field()
    {
        using var context = new BunitContext();
        var rendered = context.Render<SuiForms>(parameters =>
            parameters
                .Add(component => component.Label, "Scan source")
                .Add(component => component.For, "source")
                .Add(component => component.HelpText, "Choose how pages are fed.")
                .Add(component => component.CssClass, "settings-field")
                .Add(
                    component => component.ChildContent,
                    content => content.AddMarkupContent(0, "<select id=\"source\"></select>")
                )
        );

        Assert.Equal("Scan source", rendered.Find("label[for='source']").TextContent);
        Assert.Equal("Choose how pages are fed.", rendered.Find("#source-help").TextContent);
        Assert.NotNull(rendered.Find(".sui-forms.settings-field select#source"));
    }
}
