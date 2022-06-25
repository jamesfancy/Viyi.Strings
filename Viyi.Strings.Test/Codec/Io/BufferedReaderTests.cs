using System.Text;
using Viyi.Strings.Codec.Io;

namespace Viyi.Strings.Test.Codec.Io;

[TestClass]
public class BufferedReaderTests {
    readonly Random random = new();

    readonly int[] cases = new[] {
        0, 1,
        63, 64, 65,
        4095, 4096, 4097,
    };

    [TestMethod]
    public void TestReadJustBufferSize() {
        cases.ForEach(n => test(n));

        void test(int length) {
            Trace.WriteLine($"[TestReadJustBufferSize] with length {length}");
            var buffer = new char[64];

            var expect = random.RandomString(length);
            using var sReader = new StringReader(expect);
            var textReader = new CodecTextReader(sReader);
            var reader = new BufferedReader(textReader, length);

            int count;
            var builder = new StringBuilder();
            while ((count = reader.Read(buffer)) > 0) {
                builder.Append(buffer, 0, count);
            }

            Assert.AreEqual(expect, builder.ToString());
        }
    }

    [TestMethod]
    public void TestSmallBufferSize() {
        cases.ForEach(length => {
            var expect = random.RandomString(length);
            using var sReader = new StringReader(expect);
            var reader = new BufferedReader(sReader, 64);

            int count;
            var buffer = new char[1024];
            var builder = new StringBuilder();
            while ((count = reader.Read(buffer)) > 0) {
                builder.Append(buffer, 0, count);
            }

            Assert.AreEqual(expect, builder.ToString());
        });
    }

    [TestMethod]
    public void TestLargeBufferSize() {
        cases.ForEach(length => {
            var expect = random.RandomString(length);
            using var sReader = new StringReader(expect);
            var reader = new BufferedReader(sReader);

            int count;
            var buffer = new char[24];
            var builder = new StringBuilder();
            while ((count = reader.Read(buffer)) > 0) {
                builder.Append(buffer, 0, count);
            }

            Assert.AreEqual(expect, builder.ToString());
        });
    }
}
