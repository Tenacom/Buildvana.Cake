// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/*
 * Summary : Continuous Integration adapter for local builds
 */
sealed class LocalCIAdapter : CIAdapter
{
    internal LocalCIAdapter(ICakeContext context, BuildData buildData)
        : base(context, buildData)
    {
    }
}
