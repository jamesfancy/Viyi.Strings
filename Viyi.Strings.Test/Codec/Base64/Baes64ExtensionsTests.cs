using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using Viyi.Strings.Codec;
using Viyi.Strings.Codec.Base64;
using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Test.Toolkit;

namespace Viyi.Strings.Test.Codec.Base64 {
    [TestClass]
    public class Baes64ExtensionsTests {
        readonly Random random = new();

        readonly int[] cases = new[] {
            0, 1, 2, 3, 4,
            1023, 1024, 1025, 1026
        };

        [TestMethod]
        public void TestBase64Decode() {
            int[] restCounts = new[] { 0, 0, 1, 2 };

            cases.ForEach(n => {
                if (n % 4 == 1) {
                    Assert.ThrowsException<CodecException>(() => test(n));
                }
                else {
                    test(n);
                }
            });

            void test(int length) {
                Trace.WriteLine($"[TestBase64Decode] with length {length}");
                var base64 = random.RandomBase64(length);

                byte[] data = base64.DecodeBase64();
                Assert.AreEqual(length / 4 * 3 + restCounts[length % 4], data.Length);
            }
        }

        [TestMethod]
        public void TestBase64Encode() {
            cases.ForEach(n => test(n));

            void test(int length) {
                byte[] data = random.RandomBytes(length);
                string base64 = data.EncodeBase64();
                var rest = length % 3;
                Assert.AreEqual((length / 3 + (rest == 0 ? 0 : 1)) * 4, base64.Length);

                switch (rest) {
                    case 1:
                        Assert.IsTrue(base64.EndsWith("==") && base64[^3] != '=');
                        break;
                    case 2:
                        Assert.IsTrue(base64.EndsWith("=") && base64[^2] != '=');
                        break;
                    default:
                        Assert.IsTrue(base64.Length == 0 || base64[^1] != '=');
                        break;
                }

                CollectionAssert.AreEqual(data, base64.DecodeBase64());
            }
        }

        [TestMethod]
        public void TestLineBreak() {
            cases.ForEach(n => test(n));

            void test(int length) {
                byte[] data = random.RandomBytes(length);
                string base64 = data.EncodeBase64(true);

                if (length == 0) {
                    Assert.AreEqual(0, base64.Length);
                    return;
                }

                int base64Length = (length / 3 + (length % 3 == 0 ? 0 : 1)) * 4;
                int lines = base64Length / 76 + (base64Length % 76 == 0 ? 0 : 1);

                int expectLength = base64Length + lines - 1;
                Assert.AreEqual(expectLength, base64.Length);

                var sections = base64.Split("\n");
                if (sections.Length > 1) {
                    for (int i = 0; i < sections.Length - 1; i++) {
                        Assert.AreEqual(76, sections[i].Length);
                    }
                    Assert.IsTrue(sections[^1].Length <= 76);
                }
            }
        }

        [TestMethod]
        public void TestSpecialCases() {
            new[]
            {
                (new byte[] { 0, 0 ,0, 0 }, "AAAAAA=="),
                ("administrator".DecodeUtf8(), "YWRtaW5pc3RyYXRvcg=="),
                (
                    Enumerable.Range(0, 256).Select(n => (byte) n).ToArray(),
                    "AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIjJCUmJygpKissLS4vMDEyMzQ1Njc4OTo7PD0+P0BBQkNERUZHSElKS0xNTk9QUVJTVFVWV1hZWltcXV5fYGFiY2RlZmdoaWprbG1ub3BxcnN0dXZ3eHl6e3x9fn+AgYKDhIWGh4iJiouMjY6PkJGSk5SVlpeYmZqbnJ2en6ChoqOkpaanqKmqq6ytrq+wsbKztLW2t7i5uru8vb6/wMHCw8TFxsfIycrLzM3Oz9DR0tPU1dbX2Nna29zd3t/g4eLj5OXm5+jp6uvs7e7v8PHy8/T19vf4+fr7/P3+/w=="
                )
            }.ForEach(tuple => {
                var (bytes, base64) = tuple;
                Assert.AreEqual(base64, bytes.EncodeBase64());
            });
        }
    }
}
