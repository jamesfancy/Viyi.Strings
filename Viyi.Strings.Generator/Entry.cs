namespace Viyi.Strings.Generator;

internal record Entry {
    public Entry(string name, string? caption, string? ns = null, int width = 64) {
        Name = name;
        Caption = caption;
        Namespace = ns ?? name;
        Width = width;
    }

    public string Name { get; private set; }
    public string Namespace { get; private set; }
    public string? Caption { get; private set; }
    public int Width { get; private set; }
};
