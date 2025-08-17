// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Provides shortcut methods to use Git.
/// </summary>
sealed class GitService
{
    private readonly ICakeContext _context;
    private readonly IRepository _repository;

    public GitService(ICakeContext context, OptionsService options)
    {
        Guard.IsNotNull(context);
        Guard.IsNotNull(options);
        _context = context;
        var workingDirectory = context.Environment.WorkingDirectory.FullPath;
        _context.Ensure(LibGit2Sharp.Repository.IsValid(workingDirectory), $"There is no Git repository at {workingDirectory}");
        _repository = new LibGit2Sharp.Repository(workingDirectory);
        _context.Ensure(TryGetOriginUrl(out var originUrl), "No origin URL found in the Git repository.");
        OriginUrl = originUrl;
        var headName = _repository.Head.CanonicalName;
        CurrentBranch = headName.StartsWith("refs/heads/", StringComparison.Ordinal) ? _repository.Head.FriendlyName : string.Empty;
        var mainBranch = options.GetOption<string>("mainBranch", string.Empty);
        var mainBranchFound = false;
        var mainFound = false;
        var masterFound = false;
        foreach (var branch in _repository.Branches.Select(static x => x.FriendlyName))
        {
            if (branch == mainBranch)
            {
                mainBranchFound = true;
                break;
            }
            else if (branch == "main")
            {
                mainFound = true;
            }
            else if (branch == "master")
            {
                masterFound = true;
            }
        }

        if (mainBranchFound)
        {
            MainBranch = mainBranch;
        }
        else if (mainFound)
        {
            MainBranch = "main";
        }
        else if (masterFound)
        {
            MainBranch = "master";
        }
        else if (string.IsNullOrEmpty(mainBranch))
        {
            _context.Fail("Could not find a 'main' or 'master' branch in the repository.");
        }
        else
        {
            _context.Fail($"Could not find the configured main branch ('{mainBranch}') in the repository.");
        }
    }

    /// <summary>
    /// Gets the fetch URL of the origin remote, i.e. either the remote whose name is "origin", or the only remote if there is only one, even if its name is not "origin".
    /// </summary>
    public string OriginUrl { get; }

    /// <summary>
    /// Gets the name of the main Git branch.
    /// </summary>
    /// <value>The name of the main branch.</value>
    public string MainBranch { get; }

    /// <summary>
    /// Gets the name of the current Git branch.
    /// </summary>
    /// <value>If HEAD is on a branch, the name of the branch; otherwise, the empty string.</value>
    public string CurrentBranch { get; }

    /// <summary>
    /// Tells whether a tag exists in the local Git repository.
    /// </summary>
    /// <param name="tag">The tag to check for.</param>
    /// <returns>If a tag whose name is equal to <paramref name="tag"/> exists in the repository, <see langword="true"/>;
    /// otherwise, <see langword="false"/>.</returns>
    public bool TagExists(string tag)
    {
        Guard.IsNotNullOrEmpty(tag);
        return _repository.Tags.Any(x => string.Equals(x.FriendlyName, tag, StringComparison.Ordinal));
    }

    /// <summary>
    /// Gets the latest version and the latest stable version in commit history.
    /// </summary>
    /// <returns>A tuple of the latest version and the latest stable version.</returns>
    /// <remarks>
    /// <para>If no version tag is found in commit history, this method returns a tuple of two <see langword="null"/>s.</para>
    /// <para>If no stable version tag is found in commit history, this method returns a tuple of the latest version and <see langword="null"/>.</para>
    /// </remarks>
    public (SemanticVersion? Latest, SemanticVersion? LatestStable) GetLatestVersions()
    {
        var versions = _repository.Tags
            .Select(x => SemanticVersion.TryParse(x.FriendlyName, out var version) ? (Sha: x.Target.Sha, Version: version) : (Sha: null!, Version: null!))
            .Where(x => x.Sha is not null)
            .ToDictionary();

        SemanticVersion? latest = null;
        SemanticVersion? latestStable = null;
        foreach (var commit in _repository.Head.Commits)
        {
            if (versions.TryGetValue(commit.Sha, out var version))
            {
                if (latest == null)
                {
                    latest = version;
                }

                if (!version.IsPrerelease)
                {
                    latestStable = version;
                    break;
                }
            }
        }

        return (latest, latestStable);
    }

    /// <summary>
    /// Adds one or more files to the Git index.
    /// </summary>
    /// <param name="paths">The paths of the files to add.</param>
    public void Stage(params FilePath[] paths)
    {
        Guard.IsNotNull(paths);
        if (paths.Length == 0)
        {
            return;
        }

        var pathsInRepo = paths.Select(path => {
            Guard.IsTrue(path is not null, nameof(paths), "One or more paths are null.");
            var pathInRepo = _context.Environment.WorkingDirectory.GetRelativePath(path);
            if (!pathInRepo.IsRelative || pathInRepo.Segments[0] == "..")
            {
                _context.Fail($"Git: cannot stage '{path}' because it is not in the repository.");
            }

            return pathInRepo.ToString();
        }).ToArray();

        _context.Verbose($"Git: staging {pathsInRepo.Length} file(s)...");
        Commands.Stage(_repository, pathsInRepo, new StageOptions() { IncludeIgnored = false, ExplicitPathsOptions = new() { ShouldFailOnUnmatchedPath = true }});
    }

    /// <summary>
    /// Commits staged changes, or amends last commit.
    /// </summary>
    /// <param name="message">The commit message.</param>
    /// <param name="amend">If <see langword="true"/>, amends last commit instead of creating a new commit.</param>
    public void Commit(string message, bool amend = false)
    {
        var signature = _repository.Config.BuildSignature(DateTimeOffset.Now);
        _context.Ensure(signature is not null, "Git: cannot obtain author identity from configuration.");
        var options = new CommitOptions() { AmendPreviousCommit = amend };
        _ = _repository.Commit(message, signature, signature, options);
    }

    /// <summary>
    /// Undoes the most recent commit.
    /// </summary>
    /// <remarks>
    /// <para>This method's purpose is to undo a commit that was just generated by code and is not a merge commit.</para>
    /// <para>If the current <c>HEAD</c> has multiple parents, the behavior of this method is undefined.</para>
    /// <para>If the repository has no commits, or the current <c>HEAD</c> has no parents, this method will fail.</para>
    /// </remarks>
    public void UndoLastCommit()
    {
        _context.Information("Git: undoing last commit...");
        var previousCommit = _repository.Head.Tip.Parents.FirstOrDefault();
        _context.Ensure(previousCommit is not null, "Git: cannot reset, there is no commit to go back to.");
        _repository.Reset(ResetMode.Hard, previousCommit);
    }

    /// <summary>
    /// Pushes changes made to HEAD to the tracked remote. Fails if HEAD is not tracking any remote.
    /// </summary>
    public void Push(bool force = false)
    {
        var head = _repository.Head;
        var remote = head.RemoteName;
        _context.Ensure(!string.IsNullOrEmpty(remote), "Git: cannot push, HEAD is not tracking any remote.");
        if (force)
        {
            // https://stackoverflow.com/a/47295101/5753412
            // https://github.com/libgit2/libgit2sharp/blob/5085a0c6173cdb2a3fde205330b327a8eb0a26c4/LibGit2Sharp.Tests/PushFixture.cs#L183-L187
            // https://github.com/libgit2/libgit2sharp/issues/104#issuecomment-1553347893
            _context.Information($"Git: force pushing changes to '{remote}'...");
            string pushRefSpec = string.Format("+{0}:{0}", _repository.Head.CanonicalName);
            _repository.Network.Push(_repository.Network.Remotes[remote], pushRefSpec);
        }
        else
        {
            _context.Information($"Git: pushing changes to '{remote}'...");
            _repository.Network.Push(head);
        }
    }

    private bool TryGetOriginUrl([MaybeNullWhen(false)] out string url)
    {
        url = null!;
        string? originUrl = null;
        string? onlyRemoteUrl = null;
        bool isFirst = true;
        foreach (var remote in _repository.Network.Remotes)
        {
            using (remote)
            {
                if (remote.Name == "origin")
                {
                    originUrl = remote.Url;
                    break;
                }

                if (isFirst)
                {
                    onlyRemoteUrl = remote.Url;
                    isFirst = false;
                }
                else
                {
                    onlyRemoteUrl = null;
                }
            }
        }

        // URL of "origin" if present; otherwise, URL of the _only_ remote.
        url = originUrl ?? onlyRemoteUrl;
        if (url is null)
        {
            return false;
        }

        // Remove trailing slashes and optional ".git" suffix
        url = url.TrimEnd('/');
        if (url.EndsWith(".git", StringComparison.Ordinal))
        {
            url = url[..(url.Length - 4)];
        }

        return true;
    }
}
