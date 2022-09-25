using Viyi.Strings.Codec.Base64;
using Viyi.Strings.Codec.Extensions;

namespace Viyi.Strings.Test.Codec.Base64;

[TestClass]
public class Base64MoreExtensionsTests {
    [TestMethod]
    public void TestBase64Decode() {
        const string base64 = "ab+c/def";
        const string base64Url = "ab-c_def";
        const string base64Mix = "ab-c/def";

        CollectionAssert.AreEqual(base64.DecodeBase64(), base64Url.DecodeBase64Url());
        CollectionAssert.AreEqual(base64Url.DecodeBase64(), base64.DecodeBase64Url());
        CollectionAssert.AreEqual(base64.DecodeBase64Compatible(), base64Url.DecodeBase64Compatible());
        CollectionAssert.AreEqual(base64.DecodeBase64(), base64Url.DecodeBase64Compatible());
        CollectionAssert.AreEqual(base64.DecodeBase64(), base64Mix.DecodeBase64Compatible());

        CollectionAssert.AreNotEqual(base64.DecodeBase64Compatible(), base64.DecodeBase64Url());
        CollectionAssert.AreNotEqual(base64Url.DecodeBase64Compatible(), base64Url.DecodeBase64());
    }

    [TestMethod]
    public void TestBase64UrlEncode() {
        const string case1 = "ab+c_def";
        var bytes = case1.DecodeBase64Compatible();

        Assert.AreEqual("ab+c/def", bytes.EncodeBase64());
        Assert.AreEqual("ab-c_def", bytes.EncodeBase64Url());
    }

    [TestMethod]
    public void TestSpecialCases() {
        new[]
        {
            (new byte[] { 0, 0 ,0, 0 }, "AAAAAA=="),
            ("administrator".DecodeUtf8(), "YWRtaW5pc3RyYXRvcg=="),
            (
                Enumerable.Range(0, 256).Select(n => (byte) n).ToArray(),
                "AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIjJCUmJygpKissLS4vMDEyMzQ1Njc4OTo7PD0-P0BBQkNERUZHSElKS0xNTk9QUVJTVFVWV1hZWltcXV5fYGFiY2RlZmdoaWprbG1ub3BxcnN0dXZ3eHl6e3x9fn-AgYKDhIWGh4iJiouMjY6PkJGSk5SVlpeYmZqbnJ2en6ChoqOkpaanqKmqq6ytrq-wsbKztLW2t7i5uru8vb6_wMHCw8TFxsfIycrLzM3Oz9DR0tPU1dbX2Nna29zd3t_g4eLj5OXm5-jp6uvs7e7v8PHy8_T19vf4-fr7_P3-_w=="
            )
        }.ForEach(tuple => {
            var (bytes, base64) = tuple;
            Assert.AreEqual(base64, bytes.EncodeBase64Url());
        });
    }
}
