using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Test.Codec.Options;

[TestClass]
public class CodecOptionsWithDefaultTests {
    [TestMethod]
    public void TestSetDefaultCreator() {
        CodecOptions.SetDefaultCreator(_ => { });
        var opt1 = CodecOptions.CreateDefault();
        var opt2 = CodecOptions.CreateDefault();
        Assert.IsNotNull(opt1);
        Assert.IsNotNull(opt2);
        Assert.AreEqual(opt1, opt2);
        Assert.AreNotSame(opt1, opt2);

        // 恢复为 null
        CodecOptions.SetDefaultCreator(null);
    }

    [TestMethod]
    public void TestOptionsBuilder() {
        Assert.AreEqual(CodecOptions.Default, CodecOptions.CreatePure().Build());

        var opt1 = CodecOptions.Create().Build();
        Assert.AreEqual(CodecOptions.Default, opt1);
        Assert.AreNotSame(CodecOptions.Default, opt1);

        var prototype = CodecOptions.Create()
            .SetLineEnding(LineEndings.Cr)
            .SetLineWidth(77)
            .UseUpperCase()
            .Build();
        Assert.AreNotEqual(CodecOptions.Default, prototype);
        Assert.AreEqual(prototype, CodecOptions.Create(prototype).Build());
        Assert.AreNotSame(prototype, CodecOptions.Create(prototype).Build());

        CodecOptions.SetDefaultCreator(prototype);
        Assert.AreEqual(CodecOptions.Default, CodecOptions.CreatePure().Build());
        Assert.AreNotEqual(CodecOptions.Default, CodecOptions.Create().Build());
        Assert.AreNotEqual(prototype, CodecOptions.CreatePure().Build());
        Assert.AreEqual(prototype, CodecOptions.Create().Build());
        Assert.AreNotSame(prototype, CodecOptions.Create().Build());
        CodecOptions.SetDefaultCreator(null);
    }

    [TestMethod]
    public void TestDefaultCreator() {
        Trace.WriteLine($"DefaultCreator is null: {CodecOptions.DefaultCreator == null}");
        Assert.IsNull(CodecOptions.DefaultCreator);

        CodecOptions.SetDefaultCreator(CodecOptions.Default);
        var opt1 = CodecOptions.CreateDefault();
        var opt2 = CodecOptions.CreateDefault();
        Assert.IsNotNull(opt1);
        Assert.IsNotNull(opt2);
        Assert.AreEqual(opt1, opt2);
        Assert.AreNotSame(opt1, opt2);

        CodecOptions.SetDefaultCreator(builder => builder.SetLineWidth(123));
        var opt3 = CodecOptions.CreateDefault();
        Assert.AreEqual(123, opt3.LineWidth);
        var opt4 = CodecOptions.Create().Build();
        Assert.AreEqual(opt3, opt4);

        // 恢复为 null
        CodecOptions.SetDefaultCreator(null);
    }
}
