# Sutil Playground

A full-stack web application built with the [SAFE Stack](https://safe-stack.github.io/) using **Sutil** for the F# frontend. This project demonstrates modern F# web development with type-safe client-server communication, component-based UI architecture, and comprehensive testing.

## Architecture Overview

### Technology Stack
- **Server**: Saturn/Giraffe with F# and ASP.NET Core
- **Client**: Sutil (F# React-like library) with Vite for bundling
- **API**: Fable.Remoting for type-safe client-server communication  
- **Styling**: Tailwind CSS with typed CSS classes
- **Testing**: Web Test Runner for DOM-based component testing
- **Build**: FAKE (F# Make) with parallel execution

### Project Structure
```
src/
├── Server/           # Saturn/Giraffe API server
├── Client/           # Sutil SPA frontend
│   ├── Components/   # Reusable UI components
│   ├── Features/     # Domain-organized features
│   └── Route.fs      # Main routing logic
├── Shared/           # Domain types and API contracts
└── tests/            # Comprehensive test suites
```

### Key Architectural Decisions

**Sutil over Elmish**: Uses Sutil's component-based approach instead of traditional Elmish MVU pattern, enabling more React-like development with F# type safety.

**Feature-Based Organization**: Code organized by business domains (Users, Sandbox, etc.) rather than technical layers, promoting maintainability and team ownership.

**Type-Safe APIs**: Fable.Remoting generates client proxies automatically from server interface definitions, eliminating API contract drift.

**Component Builders**: Custom attribute-based component builders provide fluent APIs while maintaining strong typing:
```fsharp
Button.create [ 
    Button.variant.primary
    Button.size.large  
    Button.text "Click me"
    Button.onClick handler
]
```

## Getting Started

### Prerequisites
- [.NET SDK 8.0+](https://www.microsoft.com/net/download) 
- [Node.js 18+](https://nodejs.org/en/download/)
- [NPM 9+](https://www.npmjs.com/package/npm)

### First-Time Setup
**Important**: Run this command before your first build:
```bash
dotnet tool restore
```
This installs F# development tools (Fable, Fantomas, etc.) defined in the project.

### Development Commands

**Start Development Servers** (recommended)
```bash
dotnet run
```
- Starts server on `http://localhost:5000` (API)
- Starts client on `http://localhost:8080` (UI)
- Both run in watch mode with hot reload

**Run Tests in Watch Mode**
```bash
dotnet run -- RunTests
```
- Client tests available at `http://localhost:8081`
- Server tests run in console watch mode
- Can run in parallel with development servers

**Alternative Test Commands** (from tests/Client directory)
```bash
npm run test        # Run tests once
npm run test:watch  # Run tests in watch mode
```

**Production Build**
```bash
dotnet run -- Bundle
```
- Optimized build output in `deploy/` directory
- Ready for deployment

**Code Formatting**
```bash
dotnet run -- Format
```
- Formats all F# code using Fantomas

### Development Workflow

1. **Make Changes**: Edit files in `src/` directories
2. **Watch Mode**: Changes automatically rebuild and reload
3. **Test**: Run tests to verify functionality  
4. **Format**: Use formatting command before committing

### Port Configuration
- **API Server**: 5000 (configurable via `SERVER_PROXY_PORT`)
- **Client Dev**: 8080 
- **Client Tests**: 8081

### Common Issues
- **Port conflicts**: Change ports in `vite.config.mts` if needed
- **Stale builds**: Delete `*.fs.js` files if Fable compilation seems stuck
- **Tool issues**: Re-run `dotnet tool restore`

## Development Guide

### Component Development
Components follow a consistent builder pattern. See `src/Client/Components/Button.fs` for the complete example:

```fsharp
// Define component attributes as union types
type Attribute = 
    | Variant of Classes
    | Size of Classes  
    | Text of string
    | OnClick of (unit -> unit)

// Create engine classes for fluent API
type ButtonEngine() =
    member this.variant = VariantEngine()
    member this.size = SizeEngine() 
    member this.text t = Text t
    member this.onClick e = OnClick e
    member this.create attrs = (* render logic *)
```

### Routing System
Routes use F# active patterns for type-safe URL handling:

```fsharp
// Feature-specific route parsing
let (|IsUser|_|) (segments: UrlSegments) =
    match segments with
    | "users" :: s -> Some(ofUrl s)
    | _ -> None

// Main router delegates to features  
let ofUrl (segments: UrlSegments) =
    match segments with
    | Users.Route.IsUser route -> UserRoute route
    | Sandbox.Route.IsSandbox route -> SandboxRoute route
```

### API Development
Define APIs as interfaces in `src/Shared/Shared.fs`:

```fsharp
type ITodosApi = {
    getTodos: unit -> Async<Todo list>
    addTodo: Todo -> Async<Todo>
}
```

Server implements the interface, client gets automatic type-safe proxy via Fable.Remoting.

### Testing
Client tests use Web Test Runner with DOM testing and enhanced component testing infrastructure:

```fsharp
let render component' =
    let container = Container.New()
    Sutil.Program.mount (container.El, component') |> ignore
    container

// Test DOM interactions
use container = Button.create [ Button.text "Click me" ] |> render
let button = container.El.getButton "Click me"
button |> Expect.innerText "Click me"
```

**Test Infrastructure**: 
- Dedicated `tests/Client/` directory with its own `package.json`
- Component-specific test files in `tests/Client/Components/`
- Web Test Runner with DOM accessibility testing support
- Expect.Dom library for comprehensive UI assertions

## Technology Deep Dive

### SAFE Stack + Sutil
- **S**QL Server / Storage - Data persistence
- **A**zure - Cloud hosting  
- **F**able - F# to JavaScript compilation
- **E**lmish/Sutil - Frontend architecture

This project uses **Sutil** instead of traditional Elmish, providing a more component-oriented approach while maintaining F# type safety.

### Build System
Custom FAKE build script (`Build.fs`) provides:
- Parallel task execution with colored console output
- Integrated Vite bundling for client assets
- Automated test running with watch mode
- Production bundling and Azure deployment

**Recent Enhancements**:
- Enhanced test infrastructure with dedicated npm scripts
- Improved component testing with DOM accessibility support
- Environment-specific configuration management

### Dependencies
- **Fable.Remoting**: Type-safe client-server communication
- **Sutil**: F# UI library with React-like components  
- **Saturn**: F# web framework built on ASP.NET Core
- **Zanaptak.TypedCssClasses**: Type-safe Tailwind CSS integration
- **Web Test Runner**: Modern web testing with DOM support
- **Aria-Query & DOM Accessibility API**: Enhanced accessibility testing

## Resources

### Documentation
- [SAFE Stack](https://safe-stack.github.io/docs/) - Full stack documentation
- [Sutil](https://sutil.dev/) - UI library documentation
- [Saturn](https://saturnframework.org/) - Server framework
- [Fable](https://fable.io/docs/) - F# to JavaScript compiler

### Project-Specific
- `.github/copilot-instructions.md` - Detailed development patterns and conventions for AI coding agents
- `Build.fs` - Build script with all available targets
- `src/Client/Components/Button.fs` - Reference component implementation
- `tests/Client/Components/Button.Test.fs` - Component testing patterns
- `tests/Client/package.json` - Client-side testing infrastructure

## SAFE Stack Documentation

If you want to know more about the full Azure Stack and all of it's components (including Azure) visit the official [SAFE documentation](https://safe-stack.github.io/docs/).

You will find more documentation about the used F# components at the following places:

* [Saturn](https://saturnframework.org/)
* [Fable](https://fable.io/docs/)
* [Elmish](https://elmish.github.io/elmish/)
