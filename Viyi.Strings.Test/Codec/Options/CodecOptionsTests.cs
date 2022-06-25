using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Test.Codec.Options {
    [TestClass]
    public class CodecOptionsTests {
        [TestMethod]
        public void TestCloneDefault() {
            areNotEqual(
                CodecOptions.Default,
                CodecOptions.Create()
                    .SetLineWidth(99)
                    .SetLineEnding(LineEndings.Cr)
                    .UseUpperCase()
                    .Build()
            );

            areNotEqual(
                CodecOptions.Default,
                CodecOptions.Create(CodecOptions.Default)
                    .SetLineWidth(99)
                    .SetLineEnding(LineEndings.Cr)
                    .UseUpperCase()
                    .Build()
            );

            static void areNotEqual(CodecOptions source, CodecOptions target) {
                Assert.AreNotEqual(source, target);
            }
        }

        [TestMethod]
        public void TestCreateMore() {
            var bulder = CodecOptions.Create()
                .SetLineWidth(99)
                .SetLineEnding(LineEndings.Cr)
                .UseUpperCase();

            var opts1 = bulder.Build();
            var opts2 = bulder.Build();

            Assert.AreNotSame(opts1, opts2);
            Assert.AreEqual(opts1, opts2);
        }

        [TestMethod]
        public void TestNonNegativeLineWidth() {
            var opts = CodecOptions.Create().SetLineWidth(-1).Build();
            Assert.AreEqual(0, opts.LineWidth);
        }

        [TestMethod]
        public void TestDefaultCreator() {
            Assert.IsNull(CodecOptions.DefaultCreator);

            CodecOptions.DefaultCreator = () => CodecOptions.Create().Build();
            var opt1 = CodecOptions.DefaultCreator();
            var opt2 = CodecOptions.DefaultCreator();
            Assert.IsNotNull(opt1);
            Assert.IsNotNull(opt2);
            Assert.AreEqual(opt1, opt2);
            Assert.AreNotSame(opt1, opt2);

            // 恢复为 null
            CodecOptions.DefaultCreator = null;
        }

        [TestMethod]
        public void TestOptionsBuilder() {
            Assert.AreEqual(CodecOptions.Default, CodecOptions.CreatePure().Build());

            var opt1 = CodecOptions.Create().Build();
            Assert.AreEqual(CodecOptions.Default, opt1);
            Assert.AreNotSame(CodecOptions.Default, opt1);

            var prototype = CodecOptions.Create()
                .SetLineEnding(LineEndings.Cr)
                .SetLineWidth(77)
                .UseUpperCase()
                .Build();
            Assert.AreNotEqual(CodecOptions.Default, prototype);
            Assert.AreEqual(prototype, CodecOptions.Create(prototype).Build());
            Assert.AreNotSame(prototype, CodecOptions.Create(prototype).Build());

            CodecOptions.DefaultCreator = () => CodecOptions.Create(prototype).Build();
            Assert.AreEqual(CodecOptions.Default, CodecOptions.CreatePure().Build());
            Assert.AreNotEqual(CodecOptions.Default, CodecOptions.Create().Build());
            Assert.AreNotEqual(prototype, CodecOptions.CreatePure().Build());
            Assert.AreEqual(prototype, CodecOptions.Create().Build());
            Assert.AreNotSame(prototype, CodecOptions.Create().Build());
        }
    }
}
