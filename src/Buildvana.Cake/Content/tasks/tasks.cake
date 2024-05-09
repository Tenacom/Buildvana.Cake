// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

Task("Prepare")
    .Description("Prepare workspace: delete all output directories, VS data, R# caches")
    .Does(() =>
    {
        var context = GetService<ICakeContext>();
        var dotnet = GetService<DotNetService>();
        context.DeleteDirectoryIfExists(".vs");
        context.DeleteDirectoryIfExists("_ReSharper.Caches");
        context.DeleteDirectoryIfExists("artifacts");
        context.DeleteDirectoryIfExists("temp");
        foreach (var project in dotnet.Solution.Projects)
        {
            var projectDirectory = project.Path.GetDirectory();
            context.DeleteDirectoryIfExists(projectDirectory.Combine("bin"));
            context.DeleteDirectoryIfExists(projectDirectory.Combine("obj"));
            context.DeleteDirectoryIfExists(projectDirectory.Combine("TestResults"));
        }
    });

Task("Restore")
    .Description("Restore dependencies")
    .IsDependentOn("Prepare")
    .Does(() => GetService<DotNetService>().RestoreSolution());

Task("Build")
    .Description("Build all projects")
    .IsDependentOn("Restore")
    .Does(() => GetService<DotNetService>().BuildSolution(false));

Task("Test")
    .Description("Build all projects and run tests")
    .IsDependentOn("Build")
    .Does(() => GetService<DotNetService>().TestSolution(false, false, true));

Task("Pack")
    .Description("Build all projects, run tests, and prepare build artifacts")
    .IsDependentOn("Test")
    .Does(() => GetService<DotNetService>().PackSolution(false, false));
