using Viyi.Strings.Extensions;

namespace Viyi.Strings.Test.Extensions;

[TestClass]
public class FormattableExtensionsTests {
    [TestMethod]
    public void TestPreFormat1() {
        const string varName = "hello";
        const int a = 3;
        const int b = 5;
        FormattableString formattable = $"{varName} = {a} + {b}";
        var preFormatted = formattable.PreFormat(0);
        Assert.AreEqual("hello = {0} + {1}", preFormatted.Format);
        Assert.AreEqual("hello = 3 + 5", preFormatted.ToString());
    }

    [TestMethod]
    public void TestPreFormat2() {
        const string varName = "hello";
        const int a = 3;
        const int b = 5;
        FormattableString formattable = $"{varName} = {a} + {b}";
        var preFormatted = formattable.PreFormat((v, _) => v is int);
        Assert.AreEqual("{0} = 3 + 5", preFormatted.Format);
        Assert.AreEqual("hello = 3 + 5", preFormatted.ToString());
    }

    [TestMethod]
    public void TestPreFormat3() {
        const string varName = "hello";
        const int a = 3;
        const int b = 5;
        FormattableString formattable = $"{varName} = {a} + {b}";
        var preFormatted = formattable.PreFormat((_, i, count) => i == count - 1);
        Assert.AreEqual("{0} = {1} + 5", preFormatted.Format);
        Assert.AreEqual("hello = 3 + 5", preFormatted.ToString());
    }
}
