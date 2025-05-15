## Example of file structure that can be used

```
/
├── src/
│   ├── YourProjectName/
│   │   ├── YourProjectName.csproj
│   │   ├── Program.cs (if applicable, e.g., for apps/libraries with a main entry point)
│   │   ├── Properties/
│   │   │   └── AssemblyInfo.cs (if not using SDK-style csproj for metadata)
│   │   ├── Dependencies/
│   │   └── Core logic files (e.g., Classes, Interfaces, etc.)
│   └── (optional additional projects in the solution)
│
├── tests/
│   ├── YourProjectName.Tests/
│   │   ├── YourProjectName.Tests.csproj
│   │   ├── UnitTests/
│   │   ├── IntegrationTests/
│   │   └── Test logic files
│   └── (optional additional test projects)
│
├── build/
│   ├── NuGet.Config (optional, for custom feeds or overrides)
│   ├── Directory.Build.props (optional, centralizes build properties)
│   └── Directory.Build.targets (optional, for custom build tasks)
│
├── .gitignore
├── LICENSE (required if open-source)
├── README.md (highly recommended for public packages)
├── YourSolutionName.sln
└── nuget.config (optional, for specifying NuGet feeds)
```
