# ScanUi

ScanUi is a .NET 10 desktop document workspace built with Blazor and Photino. The current `/` screen is a presentation demo: it shows a page list, document preview, scanner settings, and export controls using in-memory sample pages. It does not connect to scanner hardware.

## Requirements

- .NET 10 SDK
- Node.js 20 or later
- npm

## Build and run

From the repository root, install the Tailwind CSS dependencies, then build or run the app:

```sh
npm ci --prefix ScanUi.App
dotnet build ScanUi.slnx
dotnet run --project ScanUi.App/ScanUi.App.csproj
```

The app build generates its Tailwind CSS automatically. No npm development server is configured.

## Tests

Run the unit test projects from the repository root:

```sh
dotnet test ScanUi.slnx
```

The tests use xUnit v3 and bUnit. They cover the workspace UI and shared app components; they do not require a scanner or external service.

## Repository layout

- `ScanUi.App/` — Photino host, Blazor workspace page, page-specific components, Tailwind styles, and static assets.
- `ScanUi.App.Components/` — components shared by the app, including the icon component and SVG sprite.
- `ScanUi.App.Test.Unit/` — workspace unit tests.
- `ScanUi.App.Components.Test.Unit/` — shared-component unit tests.
- `ScanUi.slnx` — solution containing the app, shared component library, and their unit test projects.

See [`contributing.md`](docs/contributing.md) for development workflow and [`CODING_STANDARDS.md`](docs/CODING_STANDARDS.md) for project, component, and testing conventions.

## License

This project is licensed under the MIT License. See [`LICENSE`](LICENSE).
