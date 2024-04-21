// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

Task("Default")
    .Description("Default task - Do nothing (but log build configuration data)")
    .Does(context => {
        context.Information("The default task does nothing. This is intentional.");
        context.Information("Use `dotnet cake --description` to see the list of available tasks.");
    });
