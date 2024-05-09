// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// <para>Implements a dummy Continuous Integration adapter for unknown system / local build.</para>
/// <para>All property and methods of this class will fail the build when called,
/// except for <<see cref="IsCloudBuild"/>, which will always return <see langword="false"/>.</para>
/// </summary>
sealed class UnknownServerAdapter : ServerAdapter
{
    private readonly ICakeContext _context;

    internal UnknownServerAdapter(IServiceProvider services)
    {
        _context = services.GetRequiredService<ICakeContext>();
    }

    /// <inheritdoc/>
    public override string Name => "(unknown / local build)";

    /// <inheritdoc/>
    /// <summary>
    /// This property is not supported on this adapter and will always throw.
    /// </summary>
    public override string HostName => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    /// <summary>
    /// This property is not supported on this adapter and will always throw.
    /// </summary>
    public override string RepositoryOwner => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    /// <summary>
    /// This property is not supported on this adapter and will always throw.
    /// </summary>
    public override string RepositoryName => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    /// <summary>
    /// This property is not supported on this adapter and will always throw.
    /// </summary>
    public override string RepositoryUrl => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    /// <value>Always <see langword="false"/>.</value>
    public override bool IsCloudBuild => false;

    /// <inheritdoc/>
    /// <summary>
    /// This method is not supported on this adapter and will always throw.
    /// </summary>
    public override Task<bool> IsPrivateRepositoryAsync() => _context.FailOnUnsupportedMethod<Task<bool>>();

    /// <inheritdoc/>
    /// <summary>
    /// This method is not supported on this adapter and will always throw.
    /// </summary>
    public override string GetReleaseUrl(string version) => _context.FailOnUnsupportedMethod<string>();

    /// <inheritdoc/>
    /// <summary>
    /// This method is not supported on this adapter and will always throw.
    /// </summary>
    public override string GetFileUrl(FilePath path, string commitish) => _context.FailOnUnsupportedMethod<string>();

    /// <inheritdoc/>
    /// <summary>
    /// This method is not supported on this adapter and will always throw.
    /// </summary>
    public override Task<ServerRelease> CreateReleaseAsync() => _context.FailOnUnsupportedMethod<Task<ServerRelease>>();
}
