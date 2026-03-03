using System.Diagnostics.CodeAnalysis;
using Viyi.Strings.Extensions;

namespace Viyi.Strings.Test.Extensions;

[TestClass]
public class StringStripTest {
    [NotNull]
    public TestContext? TestContext { get; set; }

    [TestMethod]
    public void StripTestBasic() {
        const string source = """
              public static Func<string?, bool?> CreatePredicator(
                  string? truthyString,
                  string? falsyString
              ) => str => str.ToBoolean(truthyString, falsyString);
            """;
        const string expect = """
            public static Func<string?, bool?> CreatePredicator(
                string? truthyString,
                string? falsyString
            ) => str => str.ToBoolean(truthyString, falsyString);
            """;

        Assert.AreEqual(expect, source.StripIndent());
    }

    [TestMethod]
    public void StripTestIndentChars() {
        string source = string.Join("\n", [
            "\tpublic static Func<string?, bool?> CreatePredicator(",
            "  \t    string? truthyString,",
            "\t    string? falsyString",
            " \t) => str => str.ToBoolean(truthyString, falsyString);"
        ]);

        // test by whitespace width, and convert indent to spaces
        TestContext.WriteLine(source.StripIndent(true, 4));
        Assert.AreEqual("""
            public static Func<string?, bool?> CreatePredicator(
                string? truthyString,
                string? falsyString
            ) => str => str.ToBoolean(truthyString, falsyString);
            """, source.StripIndent(true, 4));

        // test by space char count
        string expect = string.Join("\n", [
            "public static Func<string?, bool?> CreatePredicator(",
            " \t    string? truthyString,",
            "    string? falsyString",
            "\t) => str => str.ToBoolean(truthyString, falsyString);"
        ]);

        TestContext.WriteLine(source.StripIndent());
        Assert.AreEqual(expect, source.StripIndent());
    }

    [TestMethod]
    public void StripTestEmptyLines() {
        const string source = """
              public static Func<string?, bool?> CreatePredicator(

                  string? truthyString,
                  string? falsyString

              ) => str => str.ToBoolean(truthyString, falsyString);
            """;
        const string expect = """
            public static Func<string?, bool?> CreatePredicator(

                string? truthyString,
                string? falsyString

            ) => str => str.ToBoolean(truthyString, falsyString);
            """;

        Assert.AreEqual(expect, source.StripIndent());
    }

    [TestMethod]
    public void StripTestNewLineChars() {
        string source = string.Concat([
            "  public static Func<string?, bool?> CreatePredicator(\n",
            "    string? truthyString,\r\n",
            "    string? falsyString\n",
            "  ) => str => str.ToBoolean(truthyString, falsyString);\r\n"
        ]);

        string expect = string.Concat([
            "public static Func<string?, bool?> CreatePredicator(\n",
            "  string? truthyString,\r\n",
            "  string? falsyString\n",
            ") => str => str.ToBoolean(truthyString, falsyString);\r\n"
        ]);

        Assert.AreEqual(expect, source.StripIndent());
        Assert.AreEqual(expect, source.StripIndent(true));
    }
}
