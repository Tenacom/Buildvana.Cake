// Direct dependencies
#addin nuget:?package=Cake.Http&version=5.0.0
#addin nuget:?package=CommunityToolkit.Diagnostics&version=8.4.0&loaddependencies=true
#addin nuget:?package=Docfx.App&version=2.78.3&loaddependencies=true
#addin nuget:?package=LibGit2Sharp&version=0.31.0&loaddependencies=true
#addin nuget:?package=Microsoft.Extensions.DependencyInjection&version=9.0.8&loaddependencies=true
#addin nuget:?package=Microsoft.Extensions.DependencyInjection.Abstractions&version=9.0.8&loaddependencies=true
#addin nuget:?package=Octokit&version=14.0.0&loaddependencies=true

// Do not use #addin for assemblies distributed within Cake.Tool
#reference Microsoft.CodeAnalysis
#reference NuGet.Versioning
