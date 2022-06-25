using Viyi.Strings.Codec.Base32;
using static Viyi.Strings.Codec.Base32.ReverseCharset;

namespace Viyi.Strings.Test.Codec.Base32;

[TestClass]
public class Base32ExtensionsTests {
    readonly Random random = new();
    readonly int[] cases = Enumerable.Range(0, 10)
        .Union(Enumerable.Range(1020, 10))
        .ToArray();

    [TestMethod]
    public void TestReverseChars() {
        var map = Enumerable.Range('a', 26).Select((n, i) => new { code = n, value = i })
            .Union(Enumerable.Range('A', 26).Select((n, i) => new { code = n, value = i }))
            .Union(Enumerable.Range('2', 6).Select((n, i) => new { code = n, value = i + 26 }))
            .ToDictionary(it => it.code, it => it.value);
        printTable();

        Codes.Select((v, i) => {
            Assert.AreEqual(map.TryGetValue(i, out int value) ? v : -1, v);
            return true;
        });

        map.ForEach(entry => Assert.AreEqual(entry.Value, Codes[entry.Key]));

        void printTable() {
            print(0, 16);
            print(16, 32);
            print(32, 48);
            print(48, 58);                  // 数字
            print(58, 65);
            print(65, 65 + 7);              // 大写字母 4 行
            print(65 + 7, 65 + 14);
            print(65 + 14, 65 + 14 + 6);
            print(65 + 14 + 6, 65 + 26);
            print(91, 97);
            print(97, 97 + 7);              // 小写字母 4 行
            print(97 + 7, 97 + 14);
            print(97 + 14, 97 + 14 + 6);
            print(97 + 14 + 6, 97 + 26);
        }

        // 输出
        void print(int start, int end) {
            var line = string.Join(
                ", ",
                Enumerable.Range(start, end - start)
                    .Select(key => map.TryGetValue(key, out var value) ? value : -1)
            );
            Trace.WriteLine($"{line},");
        }
    }

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

            byte[] data = base32.DecodeBase32();
            Assert.AreEqual(length * charBits / byteBits, data.Length);
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
            Assert.IsTrue(base32.EndsWith(new String('=', paddingLength)));
            Assert.IsTrue(base32[^(paddingLength + 1)] != '=');

            CollectionAssert.AreEqual(data, base32.DecodeBase32());
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
            string base32 = data.EncodeBase32(true);

            if (size == 0) {
                Assert.AreEqual(0, base32.Length);
                return;
            }

            CollectionAssert.AreEqual(data, base32.DecodeBase32());

            int codeLength = (size * byteBits + groupBits - 1) / groupBits * groupBits / charBits;
            Assert.AreEqual((codeLength + 63) / 64, base32.Split("\n").Length);
        }
    }
}
