// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// <para>Fails the build with the specified message.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="message">A message explaining the reason for failing the build.</param>
[DoesNotReturn]
static void Fail(this ICakeContext @this, string message)
{
    @this.Error(message);
    throw new CakeException(message);
}

/// <summary>
/// <para>Fails the build with the specified message.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <typeparam name="T">The expected return type.</typeparam>
/// <param name="this">The Cake context.</param>
/// <param name="message">A message explaining the reason for failing the build.</param>
/// <returns>This method never returns.</returns>
[DoesNotReturn]
static T Fail<T>(this ICakeContext @this, string message)
{
    @this.Error(message);
    throw new CakeException(message);
}

/// <summary>
/// <para>Fails the build with the specified exit code and message.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="exitCode">The Cake exit code.</param>
/// <param name="message">A message explaining the reason for failing the build.</param>
[DoesNotReturn]
static void Fail(this ICakeContext @this, int exitCode, string message)
{
    @this.Error(message);
    throw new CakeException(exitCode, message);
}

/// <summary>
/// <para>Fails the build with the specified exit code and message.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <typeparam name="T">The expected return type.</typeparam>
/// <param name="this">The Cake context.</param>
/// <param name="exitCode">The Cake exit code.</param>
/// <param name="message">A message explaining the reason for failing the build.</param>
/// <returns>This method never returns.</returns>
[DoesNotReturn]
static T Fail<T>(this ICakeContext @this, int exitCode, string message)
{
    @this.Error(message);
    throw new CakeException(exitCode, message);
}

/// <summary>
/// Fails the build with the specified message if a condition is not verified.
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="condition">The condition to verify.</param>
/// <param name="message">A message explaining the reason for failing the build.</param>
static void Ensure(this ICakeContext @this, [DoesNotReturnIf(false)] bool condition, string message)
{
    if (!condition)
    {
        @this.Error(message);
        throw new CakeException(message);
    }
}

/// <summary>
/// Fails the build with the specified exit code and message if a condition is not verified.
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="condition">The condition to verify.</param>
/// <param name="exitCode">The Cake exit code.</param>
/// <param name="message">A message explaining the reason for failing the build.</param>
static void Ensure(this ICakeContext @this, [DoesNotReturnIf(false)] bool condition, int exitCode, string message)
{
    if (!condition)
    {
        @this.Error(message);
        throw new CakeException(exitCode, message);
    }
}

/// <summary>
/// <para>Fails the build because an unsupported method has been called.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="methodName">The name of the unsupported method. This parameter defaults to the name of the calling method.</param>
[DoesNotReturn]
static void FailOnUnsupportedMethod(this ICakeContext @this, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    => @this.Fail($"Unsupported method {methodName} in {sourceFilePath} ({sourceLineNumber})");

/// <summary>
/// <para>Fails the build because an unsupported method has been called.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <typeparam name="T">The expected return type.</typeparam>
/// <param name="this">The Cake context.</param>
/// <param name="methodName">The name of the unsupported method. This parameter defaults to the name of the calling method.</param>
/// <returns>This method never returns.</returns>
[DoesNotReturn]
static T FailOnUnsupportedMethod<T>(this ICakeContext @this, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    => @this.Fail<T>($"Unsupported method {methodName} in {sourceFilePath} ({sourceLineNumber})");

/// <summary>
/// <para>Fails the build because an unsupported property setter has been called.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <param name="this">The Cake context.</param>
/// <param name="methodName">The name of the unsupported property. This parameter defaults to the name of the calling property (or method).</param>
/// <returns>This method never returns.</returns>
[DoesNotReturn]
static void FailOnUnsupportedProperty(this ICakeContext @this, [CallerMemberName] string propertyName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    => @this.Fail($"Unsupported property {propertyName} in {sourceFilePath} ({sourceLineNumber})");

/// <summary>
/// <para>Fails the build because an unsupported property getter has been called.</para>
/// <para>This method does not return.</para>
/// </summary>
/// <typeparam name="T">The expected return type.</typeparam>
/// <param name="this">The Cake context.</param>
/// <param name="methodName">The name of the unsupported property. This parameter defaults to the name of the calling property (or method).</param>
/// <returns>This method never returns.</returns>
[DoesNotReturn]
static T FailOnUnsupportedProperty<T>(this ICakeContext @this, [CallerMemberName] string propertyName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
    => @this.Fail<T>($"Unsupported property {propertyName} in {sourceFilePath} ({sourceLineNumber})");
