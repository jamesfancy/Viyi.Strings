namespace Viyi.Strings.Generator;

internal record CodecEntry {
    public CodecEntry(string name, string? caption, string? ns = null, int width = 64) {
        Name = name;
        Caption = caption;
        Namespace = ns ?? name;
        Width = width;
    }

    public string Name { get; private set; }
    public string Namespace { get; private set; }
    public string? Caption { get; private set; }
    public int Width { get; private set; }

    public string ToSource() {
        return $$"""
            namespace Viyi.Strings.Codec.{{Namespace}};

            using System;
            using System.Runtime.CompilerServices;
            using Viyi.Strings.Codec.Extensions;
            using Viyi.Strings.Codec.Options;

            public static class {{Name}}Extensions {
                private const int Default{{Name}}LineWidth = {{Width}};

                extension(string source) {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    public byte[] Decode{{Name}}() => new {{Name}}Codec().Decode(source);
                }

                extension(byte[] bytes) {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    public string Encode{{Name}}(int lineWidth) => Encode{{Name}}(bytes, false, lineWidth);

                    public string Encode{{Name}}(CodecOptions? options = null) {
                        return bytes.IsEmpty()
                            ? string.Empty
                            : new {{Name}}Codec().Encode(bytes, options);
                    }

                    public string Encode{{Name}}(Action<CodecOptions.Builder> building) {
                        var optionsBuilder = CodecOptions.Create();
                        building?.Invoke(optionsBuilder);
                        return Encode{{Name}}(bytes, optionsBuilder.Build());
                    }

                    /// <summary>
                    /// 对二进制数据进行 {{Caption}} 编码，可以指定字母大小写，以及折行位置。
                    /// </summary>
                    /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母输出。</param>
                    /// <param name="lineWidth">默认的换行符是 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。</param>
                    public string Encode{{Name}}(bool upperCase, int lineWidth) {
                        return Encode{{Name}}(
                            bytes,
                            CodecOptions.Create().UseUpperCase(upperCase).SetLineWidth(lineWidth).Build()
                        );
                    }

                    /// <summary>
                    /// 对二进制数据进行 {{Caption}} 编码，可以指定字母大小写，以及是否折行。
                    /// </summary>
                    /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母。</param>
                    /// <param name="lineBreak">
                    /// 是否以默认宽度（{{Width}} 字符）换行，默认为 false。
                    /// 默认换行符为 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。
                    /// </param>
                    public string Encode{{Name}}(bool upperCase, bool lineBreak = false) {
                        return Encode{{Name}}(bytes, upperCase, lineBreak ? Default{{Name}}LineWidth : CodecOptions.NoLineWidth);
                    }
                }
            }
            """;
    }
}
