using Viyi.Strings.Codec.Base32;

namespace Viyi.Strings.Test.Codec.Base32;

[TestClass]
public class Base32ExtensionsTests {
    readonly Random random = new();
    readonly int[] cases = Enumerable.Range(0, 10)
        .Union(Enumerable.Range(1020, 10))
        .ToArray();

    [TestMethod]
    public void TestBase32Decode() {
        // Base32 编码以 8 个字符为一组，表示 5 个字节。
        // Base32 编码的 padding 只可能是 6、4、3、1 个等号，
        // 去除 Padding 后的 Base32 编码长度除以 8 的余数对应的字节数如下表，
        // 其中 -1 表示不可能出现的余数
        var restCounts = new int[] { 0, -1, 1, -3, 2, 3, -1, 4 };

        const int groupSize = 8;    // Base32 是按 8 个字符一组来解码的
        const int charBits = 5;     // 一个 Base32 字符含 5 bits 信息
        const int byteBits = 8;     // 一个字节是 8 bits

        cases.ForEach(length => {
            var restCount = restCounts[length % groupSize];
            if (restCount < 0) {
                Assert.ThrowsException<CodecException>(() => test(length));
            }
            else {
                test(length);
            }
        });

        void test(int length) {
            Trace.WriteLine($"[TestBase32Decode] with length {length}");
            var base32 = random.RandomBase32(length);
            var base32Hex = random.RandomBase32Hex(length);

            byte[] base32Data = base32.DecodeBase32();
            Assert.AreEqual(length * charBits / byteBits, base32Data.Length);
            byte[] base32HexData = base32Hex.DecodeBase32Hex();
            Assert.AreEqual(length * charBits / byteBits, base32HexData.Length);
        }
    }

    [TestMethod]
    public void TestBase32Encode() {
        cases.ForEach(size => test(size));

        const int groupBits = 40;   // 一组编码 8 个字符，表示 40 位数据
        const int charBits = 5;
        const int byteBits = 8;

        void test(int size) {
            // 随机产生 size 个 byte 的数据
            byte[] data = random.RandomBytes(size);
            string base32 = data.EncodeBase32();
            string base32Hex = data.EncodeBase32Hex();
            Assert.AreEqual(base32.Length, base32Hex.Length);

            if (size == 0) {
                Assert.AreEqual(0, base32.Length);
                return;
            }

            // 验证总长度
            Assert.AreEqual(
                // 把最后一组补足 40 位之后的总体长度，用来计算应该生成的编码数
                (size * byteBits + groupBits - 1) / groupBits * groupBits / charBits,
                base32.Length
            );

            var validCodeLength = (data.Length * byteBits + charBits - 1) / charBits;
            var paddingLength = base32.Length - validCodeLength;
            Assert.IsTrue(base32.EndsWith(new string('=', paddingLength)));
            Assert.IsTrue(base32Hex.EndsWith(new string('=', paddingLength)));
#if NET48
            Assert.IsTrue(base32[base32.Length - paddingLength - 1] != '=');
            Assert.IsTrue(base32Hex[base32.Length - paddingLength - 1] != '=');
#else
            Assert.IsTrue(base32[^(paddingLength + 1)] != '=');
            Assert.IsTrue(base32Hex[^(paddingLength + 1)] != '=');
#endif

            CollectionAssert.AreEqual(data, base32.DecodeBase32());
            CollectionAssert.AreEqual(data, base32Hex.DecodeBase32Hex());
        }
    }

    [TestMethod]
    public void TestLineBreak() {
        const int groupBits = 40;   // 一组编码 8 个字符，表示 40 位数据
        const int charBits = 5;
        const int byteBits = 8;

        cases.ForEach(size => test(size));

        void test(int size) {
            // 随机产生 size 个字节的数据用于测试
            byte[] data = random.RandomBytes(size);
            string base32 = data.EncodeBase32(true, true);
            string base32Hex = data.EncodeBase32(true, true);
            Assert.AreEqual(base32.Length, base32Hex.Length);

            if (size == 0) {
                Assert.AreEqual(0, base32.Length);
                Assert.AreEqual(0, base32Hex.Length);
                return;
            }

            CollectionAssert.AreEqual(data, base32.DecodeBase32());
            CollectionAssert.AreEqual(data, base32Hex.DecodeBase32());

            int codeLength = (size * byteBits + groupBits - 1) / groupBits * groupBits / charBits;
            Assert.AreEqual((codeLength + 63) / 64, base32.Split('\n').Length);
        }
    }
}
