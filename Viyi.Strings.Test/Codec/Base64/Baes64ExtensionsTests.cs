using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using Viyi.Strings.Codec;
using Viyi.Strings.Codec.Base64;
using Viyi.Strings.Test.Toolkit;

namespace Viyi.Strings.Test.Codec.Base64
{
    [TestClass]
    public class Baes64ExtensionsTests
    {
        readonly Random random = new();

        readonly int[] cases = new[]
            {
                0, 1, 2, 3, 4,
                1023, 1024, 1025, 1026
            };

        [TestMethod]
        public void TestBase64Decode()
        {
            int[] counts = new[] { 0, 0, 1, 2 };

            cases.ForEach(n =>
            {
                if (n % 4 == 1)
                {
                    Assert.ThrowsException<CodecException>(() => test(n));
                }
                else
                {
                    test(n);
                }
            });

            void test(int length)
            {
                Trace.WriteLine($"[TestBase64Decode] with length {length}");
                var base64 = random.RandomBase64(length);

                byte[] data = base64.DecodeBase64();
                Assert.AreEqual(length / 4 * 3 + counts[length % 4], data.Length);
            }
        }

        [TestMethod]
        public void TestBase64Encode()
        {
            cases.ForEach(n => test(n));

            void test(int length)
            {
                byte[] data = random.RandomBytes(length);
                string base64 = data.EncodeBase64();
                var rest = length % 3;
                Assert.AreEqual((length / 3 + (rest == 0 ? 0 : 1)) * 4, base64.Length);

                switch (rest)
                {
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
            }
        }

        [TestMethod]
        public void TestLineBreak()
        {
            cases.ForEach(n => test(n));

            void test(int length)
            {
                byte[] data = random.RandomBytes(length);
                string base64 = data.EncodeBase64(true);

                if (length == 0)
                {
                    Assert.AreEqual(0, base64.Length);
                    return;
                }

                int base64Length = (length / 3 + (length % 3 == 0 ? 0 : 1)) * 4;
                int lines = base64Length / 76 + (base64Length % 76 == 0 ? 0 : 1);
                int expectLength = base64Length + lines - (base64[^1] == '\n' ? 0 : 1);
                Assert.AreEqual(expectLength, base64.Length);

                var sections = base64.Split("\n");
                if (sections.Length > 1)
                {
                    for (int i = 0; i < sections.Length - 1; i++)
                    {
                        Assert.AreEqual(76, sections[i].Length);
                    }
                    Assert.IsTrue(sections[^1].Length <= 76);
                }
            }
        }
    }
}
