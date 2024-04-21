// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/*
 * Summary : Continuous Integration adapter for GitHub
 */
sealed class GitHubCIAdapter : CIAdapter
{
    internal GitHubCIAdapter(ICakeContext context, BuildData buildData)
        : base(context, buildData)
    {
    }
}
