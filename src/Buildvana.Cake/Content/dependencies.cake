// Direct dependencies
#addin nuget:?package=Cake.Http&version=4.0.0
#addin nuget:?package=CommunityToolkit.Diagnostics&version=8.2.2&loaddependencies=true
#addin nuget:?package=Docfx.App&version=2.76.0&loaddependencies=true
#addin nuget:?package=LibGit2Sharp&version=0.30.0&loaddependencies=true
#addin nuget:?package=Microsoft.Extensions.DependencyInjection&version=8.0.0&loaddependencies=true
#addin nuget:?package=Microsoft.Extensions.DependencyInjection.Abstractions&version=8.0.1&loaddependencies=true
#addin nuget:?package=Octokit&version=11.0.0&loaddependencies=true

// Do not use #addin for assemblies distributed within Cake.Tool
#reference Microsoft.CodeAnalysis
#reference NuGet.Versioning
