// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Executes an external command, capturing standard output and failing if the exit code is not zero.
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="command">The name of the command to execute.</param>
/// <param name="arguments">The arguments to pass to <paramref name="command"/>.</param>
/// <returns>The captured output of the command.</returns>
/// <exception cref="CakeException"/>The command exited with a non-zero exit code.</exception>
static IEnumerable<string> Exec(this ICakeContext @this, string command, ProcessArgumentBuilder arguments)
{
    var exitCode = @this.Exec(command, arguments, out var output);
    @this.Ensure(exitCode == 0, $"'{command} {arguments.RenderSafe()}' exited with code {exitCode}.");
    return output;
}

/// <summary>
/// Executes an external command, capturing standard output.
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="command">The name of the command to execute.</param>
/// <param name="arguments">The arguments to pass to <paramref name="command"/>.</param>
/// <param name="output">When this method returns, the captured output of the command. This parameter is passed uninitialized.</param>
/// <returns>The exit code of the command.</returns>
static int Exec(this ICakeContext @this, string command, ProcessArgumentBuilder arguments, out IEnumerable<string> output)
    => @this.StartProcess(
        command,
        new ProcessSettings { Arguments = arguments, RedirectStandardOutput = true },
        out output);
