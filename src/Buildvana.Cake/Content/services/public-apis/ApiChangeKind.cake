// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Represents the kind of changes public APIs have undergone between an older and a newer version.
/// </summary>
/// <remarks>
/// <para>The values of this enum are sorted in ascending order of importance, so that they may be compared.</para>
/// <remarks>
enum ApiChangeKind
{
    /// <summary>
    /// Public APIs have not changed between two versions.
    /// </summary>
    None,

    /// <summary>
    /// A newer version has only added public APIs with respect to an older version.
    /// </summary>
    Additive,

    /// <summary>
    /// A newer version's public APIs have undergone breaking changes since an older version was published.
    /// </summary>
    Breaking,
}
