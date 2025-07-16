#!/usr/bin/env dotnet-script

using System.Diagnostics;
using System.Text.RegularExpressions;


var projectPath = Args[1]; // --project ./path/to/file.csproj


var projectFileContent = File.ReadAllText(projectPath);

var newContent = projectFileContent.Replace("-SNAPSHOT", "");
File.WriteAllText(projectPath, newContent);

var match = Regex.Match(newContent, @"<ReleaseVersion>(.*?)</ReleaseVersion>");

if (match.Success)
{
    var version = match.Groups[1].Value;
    var projectName = Path.GetFileNameWithoutExtension(projectPath);

    // Commit 1: Prepare for release tag
    Process.Start("git", $"add {projectPath}").WaitForExit();
    Process.Start("git", $"commit -m \"Release {projectName} version {version}\"").WaitForExit();

    // Release Tag
    var shortName = projectName.Replace("Otm.", "");

    var tagName = $"{shortName}@{version}";

    Process.Start("git", $"tag -a {tagName} -m \"Release {projectName} {version}\"").WaitForExit();

    Console.WriteLine($"✅ Created tag: {tagName}");


    // Bump version

    var parts = version.Split('.');
    var lastPart = int.Parse(parts[parts.Length - 1]);
    parts[parts.Length - 1] = (lastPart + 1).ToString();

    var newVersion = string.Join(".", parts) + "-SNAPSHOT";

    var newContentPlusSnapshot = newContent.Replace(version, newVersion);


    // Commit 2: Snapshot commit
    File.WriteAllText(projectPath, newContentPlusSnapshot);
    Process.Start("git", $"add {projectPath}").WaitForExit();
    Process.Start("git", $"commit -m \"Prepare {projectName} for next development iteration {newVersion}\"").WaitForExit();

    Console.WriteLine($"✅ Next development version: {newVersion}");

}
else
{
    Console.WriteLine("Version not found");
}

