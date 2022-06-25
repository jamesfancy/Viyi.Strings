using System.Runtime.InteropServices;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Test.Codec.Io;

[TestClass]
public class CodecWrappingWriterTests {
    readonly Random random = new();
    readonly string s10;
    readonly string s1024;

    public CodecWrappingWriterTests() {
        s10 = random.RandomString(10);
        s1024 = random.RandomString(1024);
    }

    [TestMethod]
    public void TestOneCharWrapping() {
        InternalProcess(1);
    }

    [TestMethod]
    public void TestInvalidWrapping() {
        Assert.ThrowsException<ArgumentException>(() => InternalProcess(0));
    }

    [TestMethod]
    public void TestLineEnding() {
        test(LineEndings.Lf, s => assert(s, "\n"));
        test(LineEndings.Crlf, s => assert(s, "\r\n"));
        test(LineEndings.Cr, s => assert(s, "\r"));

        // 非预定义的 Line Ending 会引发 IndexOutOfRangeException
        new[] {
            (LineEndings)(-1),
            (LineEndings)4
        }.ForEach(
            lineEnding => Assert.ThrowsException<IndexOutOfRangeException>(
                () => test(lineEnding, _ => { })
            )
        );

        Assert.ThrowsException<IndexOutOfRangeException>(
            () => test((LineEndings)10, s => assert(s, ""))
        );

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            test(LineEndings.ByEnvironment, s => assert(s, "\r\n"));
        }
        else {
            test(LineEndings.ByEnvironment, s => assert(s, "\n"));
        }

        void test(LineEndings lineEnding, Action<string> assert) {
            using var sWriter = new StringWriter();
            CodecWrappingWriter writer = new(
                sWriter,
                CodecOptions.Create()
                    .SetLineWidth(64)
                    .SetLineEnding(lineEnding)
                    .Build()
            );
            writer.Write(s1024);
            var result = sWriter.ToString();
            assert(result);
        }

        void assert(string s, string spliter) {
            var segments = s.Split(spliter);
            Assert.AreEqual(1024 / 64, segments.Length);
            Assert.IsTrue(segments.All(line => line.Length == 64));
        }
    }

    void InternalProcess(int wrapWidth, Action<string>? check = null) {
        using var sWriter = new StringWriter();
        CodecWrappingWriter writer = new(
            sWriter,
            CodecOptions.Create().SetLineWidth(wrapWidth).Build()
        );
        writer.Write(s10);

        var result = sWriter.ToString();
        (check ?? CheckLength)(result);
    }

    void CheckLength(string result) {
        Assert.AreEqual(s10.Length + s10.Length - 1, result.Length);
    }
}
