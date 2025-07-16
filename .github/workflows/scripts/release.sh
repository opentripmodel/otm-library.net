#!/usr/bin/env bash
# scripts/release.sh
# Usage: ./scripts/release.sh <csproj_path> <version>
#
# Required environment variables:
#   NUGET_APIKEY - NuGet API key
#
# Optional environment variables:
#   NUGET_OUTPUT - Output directory for packages (default: ./nupkg)
#   NUGET_SOURCE_URL - NuGet source URL (default: https://api.nuget.org/v3/index.json)

set -euo pipefail

# Arguments
CSPROJ="${1:?Need <csproj_path> argument}"
VERSION="${2:?Need <version> argument}"

echo "actual project value: $CSPROJ"

# Environment variables
API_KEY="${NUGET_APIKEY:?❌  Environment variable NUGET_APIKEY must be set}"
NUGET_OUTPUT="${NUGET_OUTPUT:-./nupkg}"
SOURCE_URL="${NUGET_SOURCE_URL:-https://api.nuget.org/v3/index.json}"

# Ensure output directory exists
mkdir -p "$NUGET_OUTPUT"

echo "🔨  Building project (v$VERSION)…"
dotnet build "$CSPROJ" -c Release --nologo

echo "📦  Packing project…"
dotnet pack "$CSPROJ" \
  -c Release \
  -o "$NUGET_OUTPUT" \
  --no-build \
  /p:Version="$VERSION"


echo "🚀  Publishing NuGet package…"
dotnet nuget push "$NUGET_OUTPUT"/*.nupkg \
  --api-key "$API_KEY" \
  --source "$SOURCE_URL"

# Cleanup
rm -f "$NUGET_OUTPUT"/*.nupkg


#for pkg in "${PKGS[@]}"; do
#  echo "    → $(basename "$pkg")"
#  dotnet nuget push "$pkg" \
#    --api-key "$API_KEY" \
#    --source "$SOURCE_URL"
#
#  # Cleanup output dir for (optional) next run
#  rm -f -- "$pkg"
#done

echo "✅  Release v$VERSION completed successfully!"
