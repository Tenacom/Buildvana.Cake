// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/*
 * Summary : Base class for Continuous Integration adapters
 */
abstract class CIAdapter
{
    protected CIAdapter(ICakeContext context, BuildData buildData)
    {
        Context = context;
        BuildData = buildData;
    }

    protected ICakeContext Context { get; }
    
    protected BuildData BuildData { get; }

    public static CIAdapter Create(ICakeContext context, BuildData buildData, CIPlatform platform)
        => platform switch {
            CIPlatform.None => new LocalCIAdapter(context, buildData),
            CIPlatform.GitHub => new GitHubCIAdapter(context, buildData),
            CIPlatform.GitLab => new GitLabCIAdapter(context, buildData),
            _ => new UnsupportedCIAdapter(context, buildData, platform),
        };
}
