// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

/// <summary>
/// Filters a sequence of nullable values, taking only those that are not null.
/// </summary>
/// <typeparam name="T">The type of the elements of <paramref name="this"/>.</typeparam>
/// <param name="this">The sequence on which this method is called.</param>
/// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that are not <see langword="null"/>.</returns>
static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> @this)
    where T : class
{
    return @this.Where(IsNotNull) as IEnumerable<T>;

    static bool IsNotNull(T? x) => x is not null;
}

/// <summary>
/// Filters a sequence of nullable values, taking only those that are not null.
/// </summary>
/// <typeparam name="T">The type of the elements of <paramref name="this"/>.</typeparam>
/// <param name="this">The sequence on which this method is called.</param>
/// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that are not <see langword="null"/>.</returns>
static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> @this)
    where T : struct
{
    return @this.Where(IsNotNull).Select(GetValue);

    static bool IsNotNull(T? x) => x.HasValue;

    static T GetValue(T? x) => x!.Value;
}

/// <summary>
/// Filters a sequence of nullable <see langword="string"/>s, taking only those that are neither <see langword="null"/> nor the empty string.
/// </summary>
/// <param name="this">The sequence on which this method is called.</param>
/// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that are neither <see langword="null"/> nor the empty string.</returns>
static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string?> @this)
{
    return @this.Where(IsNotNullOrEmpty) as IEnumerable<string>;

    static bool IsNotNullOrEmpty(string? x) => !string.IsNullOrEmpty(x);
}
