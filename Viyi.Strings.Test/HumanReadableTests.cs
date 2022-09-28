using Viyi.Strings.HumanReadable;

namespace Viyi.Strings.Test;

[TestClass]
public class HumanReadableTests {
    [TestMethod]
    public void ReadableSizeTest() {
        ulong v = 1ul;
        // 最后一项溢出
        var expects = new[] { "1B", "1KB", "1MB", "1GB", "1TB", "1PB", "1EB", "0B" };
        for (int i = 0; i < expects.Length; i++) {
            Assert.AreEqual(expects[i], v.ToReadableSize());
            v = unchecked(v * 1024);
        }
    }

    [TestMethod]
    public void SpecialSizeTest() {
        Assert.AreEqual("1.23KB", 1260.ToReadableSize());
        Assert.AreEqual("999KB", 1022976.ToReadableSize());
        Assert.AreEqual("999.9KB", 1023898.ToReadableSize());
    }

    [TestMethod]
    public void OptionsTest() {
        Assert.AreEqual("229.07KB", 234567.ToReadableSize());
        Assert.AreEqual("234.57KB", 234567.ToReadableSize(opt => opt.Step = Options.Steps.By1000));
        Assert.AreEqual("0.224MB", 234567.ToReadableSize(opt => opt.Decimal = 3));

        var formatter = Fomatters.Create(opt => {
            opt.MaxLength = 4;
            opt.Decimal = 2;
        });
        Assert.AreEqual("0.22MB", formatter.Format(234567));
        Assert.AreEqual("1.03KB", formatter.Format(1050));

        Assert.AreEqual("229.07 Kb", 234567.ToReadableSize(opt => opt.SetBasicUnit("b").SeparateBySpace()));
    }
}
