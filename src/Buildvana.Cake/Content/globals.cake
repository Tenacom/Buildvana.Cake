// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Entry point, called from <c>build.cake</c>.
/// <summary>
void Run(Dictionary<string, string>? options = null)
{
    Global.SetInitialOptions(options);
    RunTarget(Argument("target", "Default"));
}

Setup(Global.Setup);
Teardown(Global.Teardown);

/// <summary>
/// Gets a service from the global service locator,
/// failing if the requested service cannot be provided.
/// </summary>
/// <typeparam name="TService">The type of the requested service.</typeparam>
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static TService GetService<TService>() where TService : notnull => Global.GetService<TService>();

static class Global
{
    private static IServiceProvider _services = null!;
    private static Dictionary<string, string>? _initialOptions;

    public static void Setup(ICakeContext context)
    {
        Guard.IsNotNull(context);
        _services = new ServiceCollection()
            .AddSingleton<ICakeContext>(context)
            .AddSingleton<PathsService>()
            .AddSingleton<OptionsService>()
            .AddSingleton<GitService>()
            .AddSingleton<PublicApiFilesService>()
            .AddSingleton<VersionService>()
            .AddSingleton<DotNetService>()
            .AddSingleton<DocFXService>()
            .AddSingleton<ChangelogService>()
            .AddSingleton(x => ServerAdapter.Create(x))
            .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true });

        if (_initialOptions is not null)
        {
            var options = GetService<OptionsService>();
            foreach (var pair in _initialOptions)
            {
                options.SetOption(pair.Key, pair.Value);
            }

            // We don't need initialOptions any longer.
            _initialOptions = null;
        }

        var version = GetService<VersionService>();
        context.Information($"Current version       : {version.Current} ({(version.IsPublicRelease ? "public release" : "private build")} / {(version.IsPrerelease ? "prerelease" : "stable")})");
        context.Information($"Latest version        : {version.Latest}");
        context.Information($"Latest stable version : {version.LatestStable}");
   }

   public static void Teardown(ITeardownContext _)
   {
        // Bail out if Setup didn't complete.
        if (_services is null)
        {
            return;
        }

        var context = GetService<ICakeContext>();
        var server = GetService<ServerAdapter>();

        // For some reason, DotNetBuildServerShutdown hangs in a GitHub Actions runner;
        // it is still useful on a local machine though.
        // TODO: Test whether it works in e.g. GitLab CI. Low priority, since a CI runner will be shut down immediately anyway.
        if (!server.IsCloudBuild)
        {
            context.DotNetBuildServerShutdown(new DotNetBuildServerShutdownSettings
            {
                Razor = true,
                VBCSCompiler = true,
            });
        }

        if (_services is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public static void SetInitialOptions(Dictionary<string, string>? initialOptions)
        => _initialOptions = initialOptions;

    public static TService GetService<TService>() where TService : notnull => _services.GetRequiredService<TService>();
}
