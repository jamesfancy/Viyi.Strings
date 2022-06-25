using System.Text;
using Viyi.Strings.Codec.Base64;
using Viyi.Strings.Codec.Hex;

namespace Viyi.Strings.Codec.Extensions.Tests;

[TestClass()]
public class ExtensionsTests {
    readonly Random random = new();
    const string utf8Source = "这段中文是用来测试的 (Hello World)";

    [TestMethod()]
    public void DecodeUtf8Test() {
        byte[] data = Encoding.UTF8.GetBytes(utf8Source);
        CollectionAssert.AreEqual(data, utf8Source.DecodeUtf8());
        CollectionAssert.AreEqual(data, utf8Source.Decode("utf-8"));
    }

    [TestMethod()]
    public void DecodeTest() {
        string base64 = random.RandomBase64(64);
        string hex = random.RandomBase64(64);

        CollectionAssert.AreEqual(base64.DecodeBase64(), base64.Decode("base64"));
        CollectionAssert.AreEqual(hex.DecodeHex(), hex.Decode("hex"));
    }

    [TestMethod()]
    public void EncodeTest() {
        byte[] data = random.RandomBytes(64);
        Assert.AreEqual(data.EncodeHex(), data.Encode("hex"));
        Assert.AreEqual(data.EncodeBase64(), data.Encode("base64"));
    }

    [TestMethod()]
    public void EncodeUtf8Test() {
        byte[] data = Encoding.UTF8.GetBytes(utf8Source);

        string s1 = Encoding.UTF8.GetString(data);
        string s2 = data.EncodeUtf8();
        string s3 = data.Encode("utf-8");

        Assert.AreEqual(s1, s2);
        Assert.AreEqual(s1, s3);
    }
}
