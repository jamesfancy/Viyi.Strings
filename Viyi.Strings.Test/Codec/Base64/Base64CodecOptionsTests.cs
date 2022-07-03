using Viyi.Strings.Codec.Base64;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Test.Codec.Base64;

[TestClass]
public class Base64CodecOptionsTests {
    [TestMethod]
    public void TestBase64CodecOptionsBasic() {
        Base64CodecOptions options = Base64CodecOptions.Create()
            .Also(builder => builder
                .UseLowerCase()
                .SetLineEnding(LineEndings.Cr)
            )
            .UseScheme(Schemes.Base64Url)
            .Build();

        Assert.IsFalse(options.UpperCase);
        Assert.AreEqual(LineEndings.Cr, options.LineEnding);
        Assert.AreEqual(Schemes.Base64Url, options.Scheme);
        Assert.AreEqual(options.Scheme, options.EncodingScheme);
    }

    [TestMethod]
    public void TestCreatingByCodecOptions() {
        CodecOptions baseOptions = CodecOptions.Create()
            .UseUpperCase()
            .SetLineWidth(77)
            .Build();
        Base64CodecOptions options = Base64CodecOptions.Create(baseOptions).Build();

        Assert.IsTrue(options.UpperCase);
        Assert.AreEqual(77, options.LineWidth);

        var another = Base64CodecOptions.Create(options).Build();
        Assert.AreNotSame(options, another);
        Assert.IsTrue(another.UpperCase);
        Assert.AreEqual(77, another.LineWidth);
    }
}
