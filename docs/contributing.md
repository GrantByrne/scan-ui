# Contributing to ScanUi

Thanks for contributing. Keep changes focused, follow the repository conventions below, and include verification appropriate to the change.

## Development setup

From the repository root, install the frontend dependencies:

```sh
npm ci --prefix src/ScanUi.App
```

Use the .NET 10 SDK and Node.js 20 or later. The app's non-design-time build runs the Tailwind CSS build automatically; no separate npm development server is configured.

## Make a change

- Keep business rules in the core layer and presentation in UI projects. External integrations belong in separate adapter projects; do not add device or integration code to the UI.
- Keep a page-only component beside its page in a `Components/` directory. Put app-wide components in the owning project's shared component directory; reserve generic cross-project components for the separate `ScanUi.Components` project when available.
- Put Blazor markup in `.razor` files and C# state, lifecycle, and event logic in co-located partial `.razor.cs` files. Use constructor injection rather than `@inject` or property injection.
- Follow [`CODING_STANDARDS.md`](CODING_STANDARDS.md) for file and method size limits, project boundaries, component design, accessibility, and test conventions.
- Do not hand-edit generated output or dependency lock files to satisfy source-code rules.

## Verify your change

Run the solution build and tests from the repository root:

```sh
dotnet build src/ScanUi.slnx
dotnet test src/ScanUi.slnx
```

For UI changes, also launch the desktop app and exercise the changed path:

```sh
dotnet run --project src/ScanUi.App/ScanUi.App.csproj
```

The current workspace is a demo backed by in-memory sample pages; it does not connect to scanner hardware. Do not rely on physical-device testing for the existing workspace behavior.

Tests use xUnit v3. Use bUnit for Blazor component tests and FakeItEasy for test doubles. Keep unit tests isolated from external services and devices, and follow Arrange / Act / Assert without section comments. Cover observable behavior rather than implementation details.

## Pull requests

- Explain the behavior changed and the reason for the change.
- Report the build, tests, and any manual UI checks you ran; include relevant limitations.
- For visible UI changes, include a screenshot when it helps reviewers assess the result.
