namespace Viyi.Strings.HumanReadable;

public static class Formatters {
    private static readonly IFormatter defaultInstance = new DefaultFormatter(new());

    public static IFormatter Create(Action<Options>? config = null) {
        if (config == null) {
            return defaultInstance;
        }

        Options options = new();
        config(options);
        return Create(options);
    }

    public static IFormatter Create(Options options) => new DefaultFormatter(options);
}
