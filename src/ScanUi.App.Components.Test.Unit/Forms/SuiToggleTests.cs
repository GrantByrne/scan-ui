using Bunit;
using ScanUi.App.Components.Forms;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiToggleTests
{
    [Fact]
    public void Announces_its_value_and_reports_the_inverted_value_on_click()
    {
        using var context = new BunitContext();
        bool? changedValue = null;
        var rendered = context.Render<SuiToggle>(parameters =>
            parameters
                .Add(component => component.Label, "Remove blank pages")
                .Add(component => component.Value, false)
                .Add(
                    component => component.ValueChanged,
                    value =>
                    {
                        changedValue = value;
                        return Task.CompletedTask;
                    }
                )
        );

        var toggle = rendered.Find("button.sui-toggle");
        Assert.Equal("Remove blank pages", toggle.GetAttribute("aria-label"));
        Assert.Equal("false", toggle.GetAttribute("aria-pressed"));

        toggle.Click();

        Assert.True(changedValue);
    }
}
