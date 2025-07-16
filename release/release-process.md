
# Release Process Documentation

## Overview

This document describes the release process for the OTM Library .NET monorepo. 

The process is inspired by Maven's release workflow and consists of three main steps: **create release branch**, **prepare**, and **perform**.

## 🔧 Prerequisites

- [dotnet-script](https://github.com/filipw/dotnet-script) installed globally

```shell
dotnet tool install -g dotnet-script
```

## 🔧 Release Branch Strategy
All releases must be done from dedicated release branches following the naming pattern:

release-{ProjectName}@{Version}

Examples:

`release-Profile@1.2.3.4`

`release-Model@1.2.3.5`

`release-Serializer@1.2.3.6`

## 🔧 Release Commands

### 1. Create Release Branch

Create a release branch following the naming pattern:

```shell
git checkout -b release-Profile@1.2.3.4
```

### 2. Prepare Release

Run the release preparation script for each project you want to release:

```shell
dotnet script release-prepare.csx --project ./src/Otm.Profile/Otm.Profile.csproj
dotnet script release-prepare.csx --project ./src/Otm.Model/Otm.Model.csproj
dotnet script release-prepare.csx --project ./src/Otm.Serializer/Otm.Serializer.csproj
```

**What this does:**
- Reads current version from `.csproj` file (e.g., `1.2.3.4-SNAPSHOT`)
- **Commit 1**: Removes `-SNAPSHOT` suffix and commits the release version
- **Creates Tag**: Creates annotated git tag (e.g., `Profile@1.2.3.4`)
- **Commit 2**: Bumps to next development version (e.g., `1.2.3.5-SNAPSHOT`)

### 3. Push Changes

Push the commits and tags to remote repository:

```shell
git push --follow-tags
```

**What this does:**
- Pushes both release commits to remote
- Pushes annotated tags to remote

### 4. Create Pull Request

Create a Pull Request to merge the release branch into main.

**What this does:**
- 🤖 Triggers automated CI pipeline
- 📦 Builds, packs, and publishes to NuGet Gallery

## 🔧 Example Workflow

```shell
# 1. Prepare releases for multiple projects
dotnet script release-prepare.csx --project ./src/Otm.Profile/Otm.Profile.csproj
dotnet script release-prepare.csx --project ./src/Otm.Model/Otm.Model.csproj

# 2. Push everything
git push --follow-tags
```

## 🔧 What Happens in CI

When Pull Request from release branch is created, the CI pipeline automatically:
1. Detects new tags
2. Builds the tagged version
3. Packs NuGet packages
4. Publishes to NuGet Gallery

## 🏷️ Tag Format

Tags follow the pattern: `{ProjectName}@{Version}`

Examples:
- `Profile@1.2.3.4`
- `Model@1.2.3.5`
- `Serializer@1.2.3.6`
