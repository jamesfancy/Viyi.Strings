using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Test.Codec.Options;

[TestClass]
public class CodecOptionsTests {
    [TestMethod]
    public void TestCloneDefault() {
        areNotEqual(
            CodecOptions.Default,
            CodecOptions.Create()
                .SetLineWidth(99)
                .SetLineEnding(LineEndings.Cr)
                .UseUpperCase()
                .Build()
        );

        areNotEqual(
            CodecOptions.Default,
            CodecOptions.Create(CodecOptions.Default)
                .SetLineWidth(99)
                .SetLineEnding(LineEndings.Cr)
                .UseUpperCase()
                .Build()
        );

        static void areNotEqual(CodecOptions source, CodecOptions target) {
            Assert.AreNotEqual(source, target);
        }
    }

    [TestMethod]
    public void TestCreateMore() {
        var builder = CodecOptions.Create()
            .SetLineWidth(99)
            .SetLineEnding(LineEndings.Cr)
            .UseUpperCase();

        var opts1 = builder.Build();
        var opts2 = builder.Build();

        Assert.AreNotSame(opts1, opts2);
        Assert.AreEqual(opts1, opts2);
    }

    [TestMethod]
    public void TestNonNegativeLineWidth() {
        var opts = CodecOptions.Create().SetLineWidth(-1).Build();
        Assert.AreEqual(0, opts.LineWidth);
    }
}
