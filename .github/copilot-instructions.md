# Sutil Playground - SAFE Stack + Sutil Development Guide

## Architecture Overview

This is a SAFE Stack application using **Sutil** (not Elmish) for the F# frontend, with a clear separation:
- **Server**: Saturn/Giraffe with Fable.Remoting for type-safe APIs (`src/Server/`)
- **Client**: Sutil-based SPA with component system (`src/Client/`)
- **Shared**: Domain types and API contracts shared between client/server (`src/Shared/`)

## Key Technologies & Patterns

### Frontend (Sutil-specific)
- **Component Pattern**: Use attribute-based builders, not Elmish MVU
  ```fsharp
  Button.create [ Button.text "Click"; Button.onClick handler ]
  ```
- **Navigation**: Uses `Sutil.Router` with pattern matching routes in `Route.fs`
- **State Management**: `Store.makeElmish` for local component state
- **Testing**: Web Test Runner with browser DOM testing via `Expect.Dom`

### Project Structure
- **Features**: Organized by domain (`Features/Users/`, `Features/Sandbox/`)
- **Components**: Reusable UI components with typed builders (`Components/Button.fs`)
- **Route Hierarchy**: Main routes delegate to feature-specific routes using active patterns

## Development Workflows

### Build & Run
```bash
dotnet tool restore          # First time only
dotnet run                   # Starts both server (5000) and client (8080)
dotnet run -- RunTests       # Run tests in watch mode (client on 8081)
dotnet run -- Bundle         # Production build
```

### Testing Infrastructure
- **Client Tests**: Use Web Test Runner with DOM testing, not Jest/Node
- **Test Pattern**: Create `render` helper for DOM container mounting
  ```fsharp
  let render component' =
      let container = Container.New()
      Sutil.Program.mount (container.El, component') |> ignore
      container
  ```
- **DOM Assertions**: Use `Expect.Dom` for element queries and assertions
- **Test Commands**: 
  ```bash
  # In tests/Client directory
  dotnet fable --define HEADLESS --run web-test-runner "*\*Test.fs.js" --node-resolve
  ```

### Component Development Pattern

Follow the `Button.fs` architectural pattern for all components:

1. **Attribute Union Type**: Define component properties as discriminated union
   ```fsharp
   type Attribute =
       | Variant of Classes
       | Size of Classes  
       | Text of string
       | OnClick of (unit -> unit)
   ```

2. **Property Record**: Internal state representation
   ```fsharp
   type Property = {
       variant: Classes
       size: Classes
       text: string
       onClick: unit -> unit
   }
   ```

3. **Engine Classes**: Typed builders with method chaining
   ```fsharp
   type VariantEngine() =
       member _.default' = Variant [ "bg-primary"; "text-primary-foreground" ]
       member _.destructive = Variant [ "bg-destructive"; "text-destructive-foreground" ]
   ```

4. **Main Engine**: Combines all aspects with create method
   ```fsharp
   type ButtonEngine() =
       member this.variant = VariantEngine()
       member this.size = SizeEngine()
       member this.create attrs = (* component creation logic *)
   ```

## Critical Conventions

### Route Handling Architecture

**Active Pattern System**: Each feature defines route parsing with active patterns
```fsharp
// In Features/Users/Route.fs
let (|IsUser|_|) (segments: UrlSegments) =
    match segments with
    | "users" :: s -> Some(ofUrl s)
    | _ -> None
```

**Route Composition**: Main router delegates to feature routers
```fsharp
// In Route.fs
let ofUrl (segments: UrlSegments) =
    match segments with
    | Users.Route.IsUser route -> UserRoute route
    | Sandbox.Route.IsSandbox route -> SandboxRoute route
```

**Navigation Interface**: Use `INavigator` for testable navigation
```fsharp
let navigator: INavigator = Navigator (Navigate >> dispatch)
navigator.navigate "/users/123"
```

### API Communication (Fable.Remoting)

**Shared Contract**: Define APIs in `Shared.fs` as interfaces
```fsharp
type ITodosApi = {
    getTodos: unit -> Async<Todo list>
    addTodo: Todo -> Async<Todo>
}
```

**Server Implementation**: Implement interface with actual logic
```fsharp
let todosApi: ITodosApi = {
    getTodos = fun () -> async { return Storage.todos |> List.ofSeq }
    addTodo = fun todo -> async { (* implementation *) }
}
```

**Client Usage**: Automatic proxy generation via Fable.Remoting
```fsharp
// Client gets type-safe API calls automatically
```

### State Management Patterns

**Sutil vs Elmish**: Use `Store.makeElmish` for component-local state
```fsharp
let model, dispatch = () |> Store.makeElmish init update ignore
```

**Subscription Management**: Always clean up subscriptions
```fsharp
Html.div [
    disposeOnUnmount [ model ]
    unsubscribeOnUnmount [ navigationSubscription ]
]
```

### CSS & Styling Strategy

**Tailwind Integration**: Uses `Zanaptak.TypedCssClasses` for type safety
- Base classes defined in component engines
- Variant classes for different styles
- Size classes for different dimensions

**Class Composition Pattern**:
```fsharp
Attr.classes (names = this.baseClasses @ prop.variant @ prop.size)
```

## Build System Deep Dive

### FAKE Build Script (`Build.fs`)
- **Parallel Execution**: Uses custom `Proc.Parallel.run` for concurrent tasks
- **Colored Output**: Console output with color coding per process
- **Target Dependencies**: Clean → Install → Build/Run/Test chains

### Vite Configuration
- **Dual Servers**: Development (8080) and test (8081) servers
- **API Proxy**: Routes `/api/*` to server on port 5000
- **Build Output**: Produces static files in `deploy/public`

### Package Management
- **Paket**: .NET dependencies in `paket.dependencies`
- **NPM**: JS dependencies for Vite, testing tools
- **Tool Restoration**: `dotnet tool restore` for F# tools

## Development Environment Setup

### Prerequisites
- .NET SDK 8.0+
- Node.js 18+
- NPM 9+

### First-time Setup
```bash
dotnet tool restore    # Install F# tools
npm install           # Install JS dependencies
```

### Common Troubleshooting
- **Port Conflicts**: Server (5000), Client (8080), Tests (8081)
- **Fable Compilation**: Delete `*.fs.js` files if stale
- **Tool Issues**: Re-run `dotnet tool restore`

## File Organization Patterns

### Feature Structure
Each feature follows this pattern:
```
Features/[FeatureName]/
├── Route.fs          # URL parsing and generation
├── Layout.fs         # Feature-level layout
├── [Page]Page.fs     # Individual pages
└── Components/       # Feature-specific components
```

### Component Structure
```
Components/
├── Button.fs         # Full component with engines
├── Button1.fs        # Alternative implementation
└── [Component].fs    # Follow Button.fs pattern
```

### Test Structure
```
tests/Client/
├── Components/
│   └── Button.Test.fs    # Mirror source structure
└── Features/
    └── [Feature]/
        └── [Component].Test.fs
```

This architecture emphasizes type safety, composability, and clear separation of concerns while leveraging F#'s unique strengths.
