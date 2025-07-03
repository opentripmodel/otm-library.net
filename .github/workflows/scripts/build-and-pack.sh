#!/usr/bin/env bash
# Usage: build-and-pack.sh <scope> <project> <version> <outputDir>
#   scope   = tag | all
#   project = Model | Profile | Serializer   (only when scope=tag)
#   version = X.Y.Z                         (only when scope=tag)
#   outputDir = path for .nupkg files

pack_single() {
  local project="$1"
  local version="$2"
  local csproj

  case "$project" in
    Model)      csproj="src/Otm.Model/Otm.Model.csproj" ;;
    Profile)    csproj="src/Otm.Profile/Otm.Profile.csproj" ;;
    Serializer) csproj="src/Otm.Serializer/Otm.Serializer.csproj" ;;
    *) echo "❌ Unknown project: $project"; exit 1 ;;
  esac

  echo "🏷  Packing $csproj @ $version"
  dotnet pack "$csproj" -c Release -o "$OUT_DIR" --no-build /p:Version="$version"
}


pack_all() {
  local projects=("Model" "Profile" "Serializer")
  for project in "${projects[@]}"; do
    local csproj="src/Otm.$project/Otm.$project.csproj"
    echo "🔍 Determining version for $project"
    local ver
    ver=$(get_version "$project")
    echo "📦 Packing $csproj @ $ver"
    dotnet pack "$csproj" -c Release -o "$OUT_DIR" --no-build /p:Version="$ver"
  done
}


set -euo pipefail

# Pack variables
SCOPE="$1"
PROJECT="${2:-}"
VERSION="${3:-}"
OUT_DIR="${4:-nuget}"



echo "🔧 build-and-pack: scope=$SCOPE project=${PROJECT:-<all>} version=${VERSION:-<snapshot>}"


# Build solution
echo "🛠  Restoring & building solution"
dotnet build Otm.All.sln -c Release

mkdir -p "$OUT_DIR"



# Scoped packing
if [[ "$SCOPE" == "tag" ]]; then
    echo "🏷  Packing single project: $PROJECT v$VERSION"
    pack_single "$PROJECT" "$VERSION"
else
    echo "📦 Packing all projects (snapshot versions)"
    pack_all
fi

echo "✅ Done! Packages in: $OUT_DIR"

