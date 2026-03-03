using Microsoft.CodeAnalysis;

namespace Viyi.Strings.Generator;

[Generator]
internal class CodecsGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var primitiveTypesProvider = context.CompilationProvider
           .Select((_, _) => new CodecEntry[] {
               new("Base32Hex", "Base 32 Hex", "Base32"),
               new("Base32", "Base 32"),
               new("Base16", "Base 16"),
           });

        context.RegisterSourceOutput(primitiveTypesProvider, GenerateForPrimitiveTypes);
    }

    private void GenerateForPrimitiveTypes(SourceProductionContext context, CodecEntry[] codecEntries) {
        foreach (var entry in codecEntries) {
            try {
                var sourceCode = entry.ToSource();
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
}
