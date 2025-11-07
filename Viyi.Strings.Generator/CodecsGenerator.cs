using Microsoft.CodeAnalysis;

namespace Viyi.Strings.Generator;

[Generator]
internal class CodecsGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var primitiveTypesProvider = context.CompilationProvider
           .Select((compilation, token) => new CodecEntry[] {
               new("Base32Hex", "Base 32 Hex", "Base32"),
               new("Base32", "Base 32"),
               new("Base16", "Base 16"),
           });

        context.RegisterSourceOutput(primitiveTypesProvider, GenerateForPrimitiveTypes);
    }

    private void GenerateForPrimitiveTypes(SourceProductionContext context, CodecEntry[] codecEntries) {
        foreach (var entry in codecEntries) {
            try {
                var sourceCode = BuildContent(entry);
                var fileName = $"{entry.Name}Extensions.gen.cs";
                context.AddSource(fileName, sourceCode);
            }
            catch (Exception ex) {
                // 处理生成过程中的错误
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "SG0001",
                        "Code generation failed",
                        $"Failed to generate code for type {entry}: {ex.Message}",
                        "CodeGeneration",
                        DiagnosticSeverity.Error,
                        true),
                    Location.None));
            }
        }
    }

    private static string BuildContent(CodecEntry it) {
        return $$"""
            namespace Viyi.Strings.Codec.{{it.Namespace}};

            using System;
            using Viyi.Strings.Codec.Extensions;
            using Viyi.Strings.Codec.Options;

            public static class {{it.Name}}Extensions {
                const int Default{{it.Name}}LineWidth = {{it.Width}};

                public static byte[] Decode{{it.Name}}(this string {{it.Name}}) {
                    return new {{it.Name}}Codec().Decode({{it.Name}});
                }

                public static string Encode{{it.Name}}(this byte[] bytes, CodecOptions? options = null) {
                    return bytes.IsEmpty()
                        ? string.Empty
                        : new {{it.Name}}Codec().Encode(bytes, options);
                }

                public static string Encode{{it.Name}}(
                    this byte[] bytes,
                    Action<CodecOptions.Builder> building
                ) {
                    var optionsBuilder = CodecOptions.Create();
                    building?.Invoke(optionsBuilder);
                    return Encode{{it.Name}}(bytes, optionsBuilder.Build());
                }

                /// <summary>
                /// 对二进制数据进行 {{it.Caption}} 编码，可以指定字母大小写，以及折行位置。
                /// </summary>
                /// <param name=""bytes""></param>
                /// <param name=""upperCase"">是否以大写字母输出。默认为 false，即小写字母输出。</param>
                /// <param name=""lineWidth"">默认的换行符是 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。</param>
                /// <returns></returns>
                public static string Encode{{it.Name}}(this byte[] bytes, bool upperCase, int lineWidth) {
                    return Encode{{it.Name}}(
                        bytes,
                        CodecOptions.Create()
                            .UseUpperCase(upperCase)
                            .SetLineWidth(lineWidth)
                            .Build()
                    );
                }

                /// <summary>
                /// 对二进制数据进行 {{it.Caption}} 编码，可以指定字母大小写，以及是否折行。
                /// </summary>
                /// <param name=""bytes""></param>
                /// <param name=""upperCase"">是否以大写字母输出。默认为 false，即小写字母。</param>
                /// <param name=""lineBreak"">
                /// 是否以默认宽度（{{it.Width}} 字符）换行，默认为 false。
                /// 默认换行符为 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。
                /// </param>
                /// <returns></returns>
                public static string Encode{{it.Name}}(this byte[] bytes, bool upperCase, bool lineBreak = false) {
                    return Encode{{it.Name}}(
                        bytes,
                        upperCase,
                        lineBreak ? Default{{it.Name}}LineWidth : CodecOptions.NoLineWidth);
                }

                public static string Encode{{it.Name}}(this byte[] bytes, int lineWidth) =>
                    Encode{{it.Name}}(bytes, false, lineWidth);
            }
            """;
    }
}
