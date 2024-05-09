// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Provides information about commonly-used paths in the repository.
/// </summary>
sealed class PathsService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathsService"/> class.
    /// </summary>
    public PathsService(ICakeContext context)
    {
        AllArtifacts = new DirectoryPath("artifacts");
        Temp = new DirectoryPath("temp");
        Docs = new DirectoryPath("docs");
    }

    /// <summary>
    /// Gets the path of the directory where build artifacts for all configurations are stored.
    /// </summary>
    public DirectoryPath AllArtifacts { get; }

    /// <summary>
    /// Gets the path of the directory where test results and coverage reports are stored.
    /// </summary>
    public DirectoryPath Temp { get; }

    /// <summary>
    /// Gets the path of the directory where documentation is stored.
    /// </summary>
    public DirectoryPath Docs { get; }
}
