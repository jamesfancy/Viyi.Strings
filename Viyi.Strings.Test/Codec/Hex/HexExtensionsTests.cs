using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using Viyi.Strings.Codec.Hex;
using Viyi.Strings.Test.Toolkit;

namespace Viyi.Strings.Test.Codec.Hex {
    [TestClass]
    public class HexExtensionsTests {
        readonly Random random = new();

        readonly int[] cases = new[] {
            0, 1, 2, 3, 4,
            1023, 1024, 1025, 1026
        };

        [TestMethod]
        public void TestHexDecode() {
            cases.ForEach(n => test(n));

            void test(int length) {
                Trace.WriteLine($"[TestHexDecode] with length {length}");
                var hex = random.RandomHex(length);

                byte[] data = hex.DecodeHex();
                Assert.AreEqual(length / 2, data.Length);
            }
        }

        [TestMethod]
        public void TestHexEncode() {
            cases.ForEach(n => test(n));

            void test(int length) {
                Trace.WriteLine($"[TestHexEncode] with length {length}");
                var data = random.RandomBytes(length);
                var hex = data.EncodeHex();
                Assert.AreEqual(length * 2, hex.Length);
                CollectionAssert.AreEqual(data, hex.DecodeHex());
            }
        }
    }
}
