// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

using System.Collections.Generic;
using System.Text.Json;

using SysFile = System.IO.File;

DocFx _docfx = null!;

Task("_docfx_init")
    .Description("(INTERNAL USE ONLY) Initialize DocFx support in script")
    .Does<BuildData>((context, data) => {
        _docfx = new DocFx(context, data, "docs");
        var globalMetadata= new
        {
            RepoOwner = data.RepositoryOwner,
            RepoName = data.RepositoryName,
            RepoUrl = $"{data.RepositoryHostUrl}/{data.RepositoryOwner}/{data.RepositoryName}",
            RepoVersion = data.VersionStr,
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var jsonPath = new DirectoryPath("docs").CombineWithFilePath("globalMetadata.json");
        using var stream = SysFile.Create(jsonPath.FullPath);
        JsonSerializer.Serialize(stream, globalMetadata, options);
    });

Task("_docfx_build")
    .Description("(INTERNAL USE ONLY) Build solution to get correct metadata for DocFx")
    .Does<BuildData>((context, data) => context.BuildSolution(data, true));

Task("DocFxMetadata")
    .Description("Generate documentation metadata from sources")
    .IsDependentOn("_docfx_init")
    .IsDependentOn("_docfx_build")
    .Does<BuildData>(_ => _docfx.Metadata());

Task("DocFxBuild")
    .Description("Build documentation from metadata")
    .IsDependentOn("_docfx_init")
    .Does<BuildData>(_ => _docfx.Build());

Task("DocFxServe")
    .Description("Host documentation web site (only on local machine)")
    .WithCriteria<BuildData>(data => data.CIPlatform is CIPlatform.None)
    .IsDependentOn("_docfx_init")
    .Does<BuildData>(_ => _docfx.Serve());

Task("DocFx")
    .Description("Generate (on local machine, also host) documentation from sources")
    .IsDependentOn("_docfx_init")
    .IsDependentOn("DocFxMetadata")
    .IsDependentOn("DocFxBuild")
    .IsDependentOn("DocFxServe");
