using Bunit;
using ScanUi.App.Components;
using Xunit;

namespace ScanUi.App.Components.Test.Unit;

public class SuiFolioIconTests
{
    [Theory]
    [InlineData(FolioIconName.Scan, "ic-scan")]
    [InlineData(FolioIconName.Grid, "ic-grid")]
    [InlineData(FolioIconName.File, "ic-file")]
    [InlineData(FolioIconName.Clock, "ic-clock")]
    [InlineData(FolioIconName.Settings, "ic-settings")]
    [InlineData(FolioIconName.Plus, "ic-plus")]
    [InlineData(FolioIconName.Chevron, "ic-chevron")]
    [InlineData(FolioIconName.ChevronRight, "ic-chevron-right")]
    [InlineData(FolioIconName.Rotate, "ic-rotate")]
    [InlineData(FolioIconName.Spark, "ic-spark")]
    [InlineData(FolioIconName.Crop, "ic-crop")]
    [InlineData(FolioIconName.Text, "ic-text")]
    [InlineData(FolioIconName.Trash, "ic-trash")]
    [InlineData(FolioIconName.ArrowUp, "ic-arrow-up")]
    [InlineData(FolioIconName.Download, "ic-download")]
    [InlineData(FolioIconName.Import, "ic-import")]
    [InlineData(FolioIconName.Minus, "ic-minus")]
    [InlineData(FolioIconName.Max, "ic-max")]
    [InlineData(FolioIconName.Check, "ic-check")]
    [InlineData(FolioIconName.Info, "ic-info")]
    public void Renders_the_symbol_with_composed_class_and_decorative_accessibility(
        FolioIconName name,
        string symbolId)
    {
        using var context = new BunitContext();
        var rendered = context.Render<SuiFolioIcon>(parameters => parameters
            .Add(icon => icon.Name, name)
            .Add(icon => icon.CssClass, "small-icon"));

        rendered.MarkupMatches(
            $"<svg class=\"icon small-icon\" aria-hidden=\"true\"><use href=\"#{symbolId}\"></use></svg>");
    }

    [Fact]
    public void Sprite_contains_each_referenced_symbol_once()
    {
        using var context = new BunitContext();
        var rendered = context.Render<SuiFolioIconSprite>();
        var symbolIds = rendered.FindAll("symbol").Select(symbol => symbol.GetAttribute("id"));

        Assert.Equal(
            new[]
            {
                "ic-scan", "ic-grid", "ic-file", "ic-clock", "ic-settings", "ic-plus",
                "ic-chevron", "ic-chevron-right", "ic-rotate", "ic-spark", "ic-crop",
                "ic-text", "ic-trash", "ic-arrow-up", "ic-download", "ic-import",
                "ic-minus", "ic-max", "ic-check", "ic-info"
            },
            symbolIds);
    }
}
