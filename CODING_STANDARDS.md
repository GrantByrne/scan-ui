# ScanUi Coding Standards

These standards apply to all hand-maintained projects in the ScanUi solution. **MUST**, **MUST NOT**, **SHOULD**, and **MAY** have their RFC 2119 meanings.

## Project boundaries

- All business rules and use-case logic MUST live in `ScanUi.Core`. UI projects and adapters MUST NOT duplicate those rules.
- `ScanUi.Core` MUST remain independent of Blazor and external-system implementations. Keep presentation in the UI and integration details in adapters.
- Each external integration (for example, an API, console application, or device driver) MUST be isolated in its own `ScanUi.Adapters.{Module}` project and have its own tests. Adapter code owns communication and translation at that boundary; it does not own business rules.
- Project references MUST follow these boundaries: consumers depend on `ScanUi.Core` contracts, and `ScanUi.Core` does not depend on UI or adapter projects.

## File and method size

- A hand-maintained file in the repository MUST NOT exceed 500 lines. Split oversized files into cohesive files or responsibilities; do not evade the limit with formatting changes, partial files, or unrelated fragments. Generated, vendored, and dependency-lock files are excluded and MUST NOT be edited by hand to meet this limit.
- A method MUST NOT exceed 78 physical lines, counted from its declaration through its closing brace, including blank and comment lines. This also applies to constructors, lifecycle handlers, and local functions.
- When a method approaches the limit, extract cohesive operations into clearly named methods or types. Use an established design pattern only when it fits the problem; do not split code mechanically or add abstraction without a responsibility to represent.

## Tests

- Business logic in `ScanUi.Core` MUST be covered by unit tests.
- Test project names MUST use one of these forms: `{ProjectName}.Test.Unit` or `{ProjectName}.Tests.Integration`. For example: `ScanUi.Core.Test.Unit` and `ScanUi.Adapters.Scanner.Tests.Integration`.
- Tests MUST use xUnit v3. Test doubles (fakes and mocks) MUST use FakeItEasy. Blazor component tests MUST use Bunit.
- Tests SHOULD be succinct and follow Arrange, Act, Assert: establish inputs and dependencies, perform the behavior once, then assert its observable result. Do NOT add `// Arrange`, `// Act`, or `// Assert` comments.
- Unit tests MUST exercise business behavior without relying on external services or devices. Test adapter boundary behavior in the adapter's test project.

## Blazor pages and component code

- Every Blazor page and component MUST keep its C# functionality in a separate `.razor.cs` code-behind file using a partial class. Keep C# methods, state, lifecycle handlers, and event logic out of the `.razor` file.
- Blazor pages and components MUST use constructor injection for dependencies; do not inject dependencies with `@inject` or property injection.

## Blazor component placement

- A component used by only one page MUST live in a `Components/` folder alongside that page.
- A component shared by multiple pages but specific to its owning project MUST live in that project's shared `Components/` folder.
- A highly generic component intended for reuse across features or projects (for example, a button, data grid, label, or form control) MUST live in the separate `ScanUi.Components` project.
- Keep page-specific behavior with the page or feature. Shared components MUST provide presentation and interaction primitives, not duplicate business rules from `ScanUi.Core`.

## Shared component design

Use [LumexUI](https://lumexui.org/) as a reference for polished, customizable, responsive Blazor components. Build components as a consistent library rather than one-off page markup:

- Prefer small, composable components with clear, typed parameters and sensible defaults.
- Expose customization through explicit parameters and composition (such as content fragments) instead of requiring consumers to copy component markup.
- Keep variants and sizes consistent across related controls; use the project's shared styling conventions rather than per-page visual rules.
- Preserve accessible semantics, labels, keyboard behavior, and visible focus states. Responsive layout MUST work at narrow and wide viewport sizes.
- Keep components focused on UI behavior. Put domain decisions and business rules in `ScanUi.Core`.
