// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Represents the protocol used by a Git URL.
/// </summary>
enum GitProtocol
{
    /// <summary>
    /// HTTP protocol.
    /// </summary>
    Http,

    /// <summary>
    /// HTTPS protocol.
    /// </summary>
    Https,

    /// <summary>
    /// SSH protocol.
    /// </summary>
    Ssh,

    /// <summary>
    /// Git protocol.
    /// </summary>
    Git,
}
