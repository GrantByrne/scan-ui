using Photino.Blazor;

namespace ScanUi.App;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        var builder = PhotinoBlazorApp.CreateBuilder(args);
        builder.RootComponents.Add<App>("#app");
        builder.ConfigureMainWindow(window =>
        {
            window
                .SetTitle("Folio — Document Studio")
                .SetSize(1200, 800);
        });

        await using var app = builder.Build();
        return app.Run();
    }
}
