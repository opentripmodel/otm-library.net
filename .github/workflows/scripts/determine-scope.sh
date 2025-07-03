#!/usr/bin/env bash
# Usage: ./determine-scope.sh "$GITHUB_REF"

echo "🔍 determine-scope: ref='$1'"

write () {
  if [[ -n "$GITHUB_OUTPUT" ]]; then
    echo "$1" >> "$GITHUB_OUTPUT"
  else
    echo "$1"
  fi
}

if [[ "$1" =~ refs/tags/([^/]+)/v([0-9]+\.[0-9]+\.[0-9]+)$ ]]; then
  write "scope=tag"
  write "project=${BASH_REMATCH[1]}"
  write "version=${BASH_REMATCH[2]}"
else
  write "scope=all"
fi

