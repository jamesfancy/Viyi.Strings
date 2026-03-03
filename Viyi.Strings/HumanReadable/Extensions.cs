using System.Runtime.CompilerServices;

namespace Viyi.Strings.HumanReadable;

public static class Extensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToReadableSize(this ulong size, Action<Options>? config = null) =>
        Formatters.Create(config).Format(size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToReadableSize(this uint size, Action<Options>? config = null) =>
        Formatters.Create(config).Format(size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToReadableSize(this long size, Action<Options>? config = null) =>
        Formatters.Create(config).Format((ulong) size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToReadableSize(this int size, Action<Options>? config = null) =>
        Formatters.Create(config).Format((ulong) size);
}
