namespace Viyi.Strings.HumanReadable;

public static class Extensions {
    public static string ToReadableSize(this ulong size, Action<Options>? config = null) =>
        Fomatters.Create(config).Format(size);

    public static string ToReadableSize(this uint size, Action<Options>? config = null) =>
        Fomatters.Create(config).Format(size);

    public static string ToReadableSize(this long size, Action<Options>? config = null) =>
        Fomatters.Create(config).Format((ulong) size);

    public static string ToReadableSize(this int size, Action<Options>? config = null) =>
        Fomatters.Create(config).Format((ulong) size);
}
