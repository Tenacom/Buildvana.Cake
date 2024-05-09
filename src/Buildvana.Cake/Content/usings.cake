// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

// ===========================================================================
// @rdeago 2024-04-26
// Using statements in Cake scripts are all global.
// Referenced namespaces are collected from all loaded .cake files;
// using statements are all put together (with namespaces sorted
// alphabetically, *not* with System namespaces first)
// at the beginning of the generated single script that is passed to Roslyn.
// Using aliases and "using static" statements are treated similarly.
// Instead of disseminating using statements among source files,
// it's better to group them here, thus acknowledging the fact that they have
// global scope.
// ===========================================================================

using CommunityToolkit.Diagnostics;
using LibGit2Sharp;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Versioning;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using SysDirectory = System.IO.Directory;
using SysFile = System.IO.File;
using SysPath = System.IO.Path;
using SysTask = System.Threading.Tasks.Task;
