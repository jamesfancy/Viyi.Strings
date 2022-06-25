using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using Viyi.Strings.Codec.Hex;
using Viyi.Strings.Test.Toolkit;

namespace Viyi.Strings.Test.Codec.Base16 {
    [TestClass]
    public class Base16ExtensionsTests {
        readonly Random random = new();

        readonly int[] cases = new[] {
            0, 1, 2, 3, 4,
            1023, 1024, 1025, 1026
        };

        [TestMethod]
        public void TestBase16Decode() {
            cases.ForEach(n => test(n));

            void test(int length) {
                Trace.WriteLine($"[TestBase16Decode] with length {length}");
                var hex = random.RandomHex(length);

                byte[] data = hex.DecodeHex();
                Assert.AreEqual(length / 2, data.Length);
            }
        }

        [TestMethod]
        public void TestBase16Encode() {
            cases.ForEach(n => test(n));

            void test(int length) {
                Trace.WriteLine($"[TestBase16Encode] with length {length}");
                var data = random.RandomBytes(length);
                var hex = data.EncodeHex();
                Assert.AreEqual(length * 2, hex.Length);
                CollectionAssert.AreEqual(data, hex.DecodeHex());
            }
        }

        [TestMethod]
        public void TestAllBytes() {
            byte[] all = Enumerable.Range(0, 256)
                .Select((_, i) => (byte) i)
                .ToArray();

            var expect = string.Concat(
                Enumerable.Range(0, 256).Select((_, i) => i.ToString("x2"))
            );

            string hex = all.EncodeHex();
            Assert.AreEqual(expect, hex);
            CollectionAssert.AreEqual(all, hex.DecodeHex());
        }
    }
}
