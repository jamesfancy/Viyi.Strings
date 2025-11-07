using Viyi.Strings.Build.Builder;

var builders = new IBuilder[] {
    new CaseInsensitiveCodecsBuilder(),
};

foreach (var builder in builders) {
    await builder.BuildAsync();
}
