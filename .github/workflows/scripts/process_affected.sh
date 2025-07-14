#!/bin/bash

# Usage: ./process_affected.sh affected.txt

while read -r csproj; do
  # Get directory of csproj file
  proj_dir=$(dirname "$csproj")
  proj_file=$(basename "$csproj")

  # Go to the project directory
  cd "$proj_dir" || exit 1


  # Example: print working directory and csproj name
  echo
  echo "🚀 Processing $proj_file"
  pwd

  # Your per-project commands go here:
  # dotnet build "$proj_file" -c Release
  # dotnet pack "$proj_file" -c Release -o "$NUGET_OUTPUT"

  ###

  # 1. Create minimal package.json with semantic release dependencies
  cat > package.json <<EOF
{
  "devDependencies": {
    "semantic-release": "24.2.6",
    "semantic-release-monorepo": "8.0.2",
    "@semantic-release/exec": "^7.1.0"
  }
}
EOF

  # 2. Install deps
  npm install --no-audit --no-fund --ignore-scripts


  # 3. Run semantic-release
  CSPROJ="$proj_file" npx semantic-release --no-ci --dry-run

  ###

  # Return to the previous directory
  cd - > /dev/null

done < "$1"
