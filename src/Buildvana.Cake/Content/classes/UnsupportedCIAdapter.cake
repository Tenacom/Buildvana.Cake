// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/*
 * Summary : Continuous Integration adapter for unsupported platforms
 */
sealed class UnsupportedCIAdapter : CIAdapter
{
    private readonly CIPlatform _platform;

    internal UnsupportedCIAdapter(ICakeContext context, BuildData buildData, CIPlatform platform)
        : base(context, buildData)
    {
        _platform = platform;
    }
}
