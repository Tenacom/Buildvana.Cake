// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Delete a directory, including its contents, if it exists.
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="directory">The directory to delete.</param>
static void DeleteDirectoryIfExists(this ICakeContext @this, DirectoryPath directory)
{
    if (!@this.DirectoryExists(directory))
    {
        @this.Verbose($"Skipping non-existent directory: {directory}");
        return;
    }

    @this.Information($"Deleting directory: {directory}");
    @this.DeleteDirectory(directory, new() { Force = false, Recursive = true });
}
