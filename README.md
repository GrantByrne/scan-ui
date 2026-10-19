# ScanUi

ScanUi is a .NET 10 desktop document workspace built with Blazor and Photino. The current `/` screen is a presentation demo: it shows a page list, document preview, scanner settings, and export controls using in-memory sample pages. It does not connect to scanner hardware.

## Requirements

- .NET 10 SDK
- Node.js 20 or later
- npm

## Build and run

From the repository root, install the Tailwind CSS dependencies, then build or run the app:

```sh
npm ci --prefix src/ScanUi.App
dotnet build src/ScanUi.slnx
dotnet run --project src/ScanUi.App/ScanUi.App.csproj
```

The app build generates its Tailwind CSS automatically. No npm development server is configured.

## Tests

Run the unit test projects from the repository root:

```sh
dotnet test src/ScanUi.slnx
```

The tests use xUnit v3 and bUnit. They cover the workspace UI and shared app components; they do not require a scanner or external service.

## VS Code

Install the recommended C# and C# Dev Kit extensions, then run the `npm: install ScanUi.App dependencies` task once after checkout. VS Code tasks provide solution build, test, and app-run commands; `Ctrl+Shift+B` runs the default solution build. Select `Debug ScanUi.App` in Run and Debug and press F5 to launch the app under the debugger. C# Dev Kit also provides test discovery and debugging in the Testing view.

## Repository layout

- `src/ScanUi.App/` — Photino host, Blazor workspace page, page-specific components, Tailwind styles, and static assets.
- `src/ScanUi.App.Components/` — shared app components, including `SuiButton`, the icon component, and SVG sprite.
- `src/ScanUi.App.Test.Unit/` — workspace unit tests.
- `src/ScanUi.App.Components.Test.Unit/` — shared component unit tests.
- `src/ScanUi.slnx` — solution containing the app, shared component library, and two unit-test projects.

See [`contributing.md`](docs/contributing.md) for development workflow and [`CODING_STANDARDS.md`](docs/CODING_STANDARDS.md) for project, component, and testing conventions.

## License

This project is licensed under the MIT License. See [`LICENSE`](LICENSE).
