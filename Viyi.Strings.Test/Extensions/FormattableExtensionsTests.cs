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
        // TODO 可能是 Roslyn 的 BUG (https://github.com/dotnet/roslyn/issues/80024)
        //      这个 BUG 修复之后解开此 disable CS8620
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        var preFormatted = formattable.PreFormat(0);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
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
