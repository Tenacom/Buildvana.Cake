// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

using System;
using System.Text;

using SysDirectory = System.IO.Directory;
using SysFile = System.IO.File;
using SysPath = System.IO.Path;

Task("CleanAll")
    .Description("Delete all output directories, VS data, R# caches")
    .Does<BuildData>((context, data) => context.CleanAll(data));

Task("LocalCleanAll")
    .Description("Like CleanAll, but only runs on a local machine")
    .WithCriteria<BuildData>(data => data.CIPlatform is CIPlatform.None)
    .Does<BuildData>((context, data) => context.CleanAll(data));

Task("Restore")
    .Description("Restore dependencies")
    .IsDependentOn("LocalCleanAll")
    .Does<BuildData>((context, data) => context.RestoreSolution(data));

Task("Build")
    .Description("Build all projects")
    .IsDependentOn("Restore")
    .Does<BuildData>((context, data) => context.BuildSolution(data, false));

Task("Test")
    .Description("Build all projects and run tests")
    .IsDependentOn("Build")
    .Does<BuildData>((context, data) => context.TestSolution(data, false, false, true));

Task("Pack")
    .Description("Build all projects, run tests, and prepare build artifacts")
    .IsDependentOn("Test")
    .Does<BuildData>((context, data) => context.PackSolution(data, false, false));
