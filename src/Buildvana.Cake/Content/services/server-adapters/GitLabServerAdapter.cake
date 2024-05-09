// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/*
 * Summary : Continuous Integration adapter for GitLab
 */
sealed class GitLabServerAdapter : ServerAdapter
{
    private readonly ICakeContext _context;

    internal GitLabServerAdapter(IServiceProvider services)
    {
        _context = services.GetRequiredService<ICakeContext>();
    }

    /// <inheritdoc/>
    public override string Name => "GitLab CI";

    /// <inheritdoc/>
    public override string HostName => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    public override string RepositoryOwner => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    public override string RepositoryName => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    public override string RepositoryUrl => _context.FailOnUnsupportedProperty<string>();

    /// <inheritdoc/>
    /// <value>Always <see langword="true"/>.</value>
    public override bool IsCloudBuild => true;

    /// <summary>
    /// Creates and returns an instance of <see cref="GitLabServerAdapter"/> if the build is running in a GitLab CI runner.
    /// </summary>
    /// <param name="services">The service provider.</param>
    /// <returns>If the build is running in GitLab CI, a newly-created <see cref="GitLabServerAdapter"/>;
    /// otherwise, <see langword="null"/>.</returns>
    public static ServerAdapter? CreateIfApplicable(IServiceProvider services)
    {
        Guard.IsNotNull(services);

        var context = services.GetRequiredService<ICakeContext>();

        return context.HasEnvironmentVariable("GITLAB_CI")
            ? new GitLabServerAdapter(services)
            : null;
    }

    /// <inheritdoc/>
    public override Task<bool> IsPrivateRepositoryAsync() => _context.FailOnUnsupportedMethod<Task<bool>>();

    /// <inheritdoc/>
    public override string GetReleaseUrl(string version) => _context.FailOnUnsupportedMethod<string>();

    /// <inheritdoc/>
    public override string GetFileUrl(FilePath path, string commitish) => _context.FailOnUnsupportedMethod<string>();

    /// <inheritdoc/>
    public override Task<ServerRelease> CreateReleaseAsync() => _context.FailOnUnsupportedMethod<Task<ServerRelease>>();
}
