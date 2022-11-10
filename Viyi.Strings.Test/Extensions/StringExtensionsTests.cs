namespace Viyi.Strings.Extensions.Tests;

[TestClass()]
public class StringExtensionsTests {
    const string Spaces = "         ";
    const string SpacesWithTabs = "   \t  \t   ";
    const string SpacesWithLineBreaks = "\r\n";

    [TestMethod()]
    public void EmptyAsTest() {
        const string? nullString = null;
        const string specifiedValue = "specified value";
        Assert.AreEqual(specifiedValue, nullString.EmptyAs(specifiedValue));
        Assert.IsNull(nullString?.EmptyAs(specifiedValue));
        Assert.AreEqual(specifiedValue, "".EmptyAs(specifiedValue));
        Assert.AreEqual(Spaces, Spaces.EmptyAs(specifiedValue));
    }

    [TestMethod()]
    public void SpacesAsTest() {
        const string? nullString = null;
        const string specifiedValue = "specified value";
        Assert.AreEqual(specifiedValue, nullString.SpacesAs(specifiedValue));
        Assert.IsNull(nullString?.SpacesAs(specifiedValue));
        Assert.AreEqual(specifiedValue, "".SpacesAs(specifiedValue));
        Assert.AreEqual(specifiedValue, Spaces.SpacesAs(specifiedValue));
        Assert.AreEqual(specifiedValue, SpacesWithTabs.SpacesAs(specifiedValue));
        Assert.AreEqual(specifiedValue, SpacesWithLineBreaks.SpacesAs(specifiedValue));
    }

    [TestMethod()]
    public void RepeatTest() {
        Assert.AreEqual("aaaaaa", "a".Repeat(6));
        Assert.AreEqual("aaaaaa", 'a'.Repeat(6));
        Assert.AreEqual("HelloHelloHello", "Hello".Repeat(3));
        Assert.AreEqual("", (null as string).Repeat(6));
        Assert.IsNull((null as string)?.Repeat(3));
    }

    const string? NullString = null;
    readonly string[] TrueStrings = new[] { "true", "on", "yes" };
    readonly string[] FalseStrings = new[] { "false", "off", "no" };
    readonly string?[] StringsContainsNull = new[] { "abc", "xyz", "hello world", null };

    [TestMethod()]
    public void ToBooleanTest() {
        // 非严格模式
        new[] { null, "False", "" }.ForEach(str => {
            Assert.IsFalse(str.ToBoolean());
            Assert.IsFalse(str.ToBoolean(false));
        });
        new[] { "blabal", "    ", "tRue" }.ForEach(str => {
            Assert.IsTrue(str.ToBoolean());
            Assert.IsTrue(str.ToBoolean(false));
        });

        // 严格模式
        Assert.IsTrue("truE".ToBoolean(true));
        Assert.IsFalse("false".ToBoolean(true));
        Assert.IsNull("".ToBoolean(true));

        // 真假字符串
        Assert.IsTrue("abce".ToBoolean("abce", "efgh"));
        Assert.IsFalse("efgh".ToBoolean("abce", "efgh"));
        Assert.IsTrue(NullString.ToBoolean(null, ""));
        Assert.IsFalse(NullString.ToBoolean((string?) null, null));
        Assert.IsTrue("xyz".ToBoolean("xyz", "xyz"));

        // 真假字符串数组
        TrueStrings.ForEach(str => {
            Assert.IsTrue(str.ToBoolean(TrueStrings, FalseStrings));
        });

        FalseStrings.ForEach(str => {
            Assert.IsFalse(str.ToBoolean(TrueStrings, FalseStrings));
        });

        Assert.IsTrue("xyz".ToBoolean(StringsContainsNull, StringsContainsNull));
        Assert.IsFalse(NullString.ToBoolean(StringsContainsNull, StringsContainsNull));

        // 真串或假串
        StringsContainsNull.ForEach(str => {
            Assert.IsTrue(str.ToBoolean(true, StringsContainsNull));
            Assert.IsFalse(str.ToBoolean(true, TrueStrings));
        });

        StringsContainsNull.ForEach(str => {
            Assert.IsFalse(str.ToBoolean(false, StringsContainsNull));
            Assert.IsTrue(str.ToBoolean(false, TrueStrings));
        });
    }

    [TestMethod()]
    public void ToBooleanPredicatorTest() {
        var stringsPredicator = Booleans.CreatePredicator(TrueStrings, FalseStrings);
        TrueStrings.ForEach(str => Assert.IsTrue(str.ToBoolean(stringsPredicator)));
        FalseStrings.ForEach(str => Assert.IsFalse(str.ToBoolean(stringsPredicator)));
        StringsContainsNull.ForEach(str => Assert.IsNull(str.ToBoolean(stringsPredicator)));

        var stringPredicator = Booleans.CreatePredicator("abc", "xyz");
        Assert.IsTrue("abc".ToBoolean(stringPredicator));
        Assert.IsFalse("xyz".ToBoolean(stringPredicator));
        Assert.IsNull("true".ToBoolean(stringPredicator));

        var valueStringsPredicator = Booleans.CreatePredicator(false, StringsContainsNull);
        StringsContainsNull.ForEach(str => Assert.IsFalse(str.ToBoolean(valueStringsPredicator)));
        TrueStrings.ForEach(str => Assert.IsTrue(str.ToBoolean(valueStringsPredicator)));
    }

    [TestMethod()]
    public void IsEmptyAndIsSpacesTest() {
        // orders
        // case string,
        var cases = new (
            string? value,
            bool isEmpty, bool isStrictEmpty,
            bool isSpaces, bool isStrictSpaces
        )[] {
            (null, true, false, true, false),
            ("", true, true, true, false),
            ("    ", false, false, true, true),
            ("  a ", false, false, false, false)
        };

        cases.ForEach(c => {
            Assert.AreEqual(c.isEmpty, c.value.IsEmpty());
            Assert.AreEqual(!c.isEmpty, c.value.IsNotEmpty());

            Assert.AreEqual(c.isEmpty, c.value.IsEmpty(false));
            Assert.AreEqual(!c.isEmpty, c.value.IsNotEmpty(false));

            Assert.AreEqual(c.isStrictEmpty, c.value.IsEmpty(true));
            Assert.AreEqual(!c.isStrictEmpty, c.value.IsNotEmpty(true));

            Assert.AreEqual(c.isSpaces, c.value.IsSpaces());
            Assert.AreEqual(!c.isSpaces, c.value.IsNotSpaces());

            Assert.AreEqual(c.isSpaces, c.value.IsSpaces(false));
            Assert.AreEqual(!c.isSpaces, c.value.IsNotSpaces(false));

            Assert.AreEqual(c.isStrictSpaces, c.value.IsSpaces(true));
            Assert.AreEqual(!c.isStrictSpaces, c.value.IsNotSpaces(true));
        });
    }

    [TestMethod()]
    public void ToStringTest() {
        Assert.AreEqual("0", 0.ToString(3));
        Assert.AreEqual("a83", 2691u.ToString(16));
        Assert.AreEqual("-a83", (-2691).ToString(16));
        Assert.AreEqual(new string('1', 32), uint.MaxValue.ToString(2));
        Assert.AreEqual(new string('1', 64), ulong.MaxValue.ToString(2));
        Assert.AreEqual($"-1{new string('0', 31)}", int.MinValue.ToString(2));
        Assert.AreEqual("7fffffff", int.MaxValue.ToString(16));
    }

    [TestMethod]
    public void ToIntTypesTest() {
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.ThrowsException<NullReferenceException>(() => (null as string).ToInt32(8));
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.AreEqual(0, "".ToInt32(8));
        Assert.AreEqual(0, "0".ToInt32(7));
        Assert.AreEqual(2691u, "a83".ToUInt32(16));
        Assert.AreEqual(-2691, "-a83".ToInt32(16));
        Assert.AreEqual(uint.MaxValue, new string('F', 8).ToUInt32(16));
        Assert.AreEqual(int.MinValue, "-80000000".ToInt32(16));

        Assert.AreEqual(0, "".ToInt32(true));
        Assert.AreEqual(-14, "-14".ToInt32(true));
        Assert.AreEqual(14, "14".ToInt32(true));
        Assert.AreEqual(123456789ul, "123456789ul".ToUInt64(true));
        Assert.AreEqual(-0x14, "-0x14".ToInt32(true));
        Assert.AreEqual(0x14u, "0X14".ToUInt32(true));
        Assert.AreEqual(0xff03ul, "0xff03".ToUInt64(true));
        Assert.AreEqual(0b101, "0B101".ToInt32(true));
        Assert.AreEqual(77, "077".ToInt32(true));
    }

    [TestMethod]
    public void ToTimeSpanTest() {
        Assert.AreEqual(TimeSpan.FromMinutes(24), "24m".ToTimeSpan());
        Assert.AreEqual(TimeSpan.FromMinutes(2324), "2324".ToTimeSpan());
        Assert.AreEqual(TimeSpan.FromMinutes(124), "124 minutes".ToTimeSpan());
        Assert.AreEqual(TimeSpan.FromMilliseconds(2022), "2022ms".ToTimeSpan());
        Assert.AreEqual(TimeSpan.FromSeconds(2022), "2022".ToTimeSpan("s"));
        Assert.AreEqual(TimeSpan.FromDays(14), "2w".ToTimeSpan("s"));
    }

    [TestMethod]
    public void SliceTest() {
        const string s = "hello world";
        Assert.AreEqual("hello world", s.Slice(0, s.Length));
        Assert.AreEqual("hello", s.Slice(0, -6));
        Assert.AreEqual("world", s.Slice(-5, s.Length));
        Assert.AreEqual("", s.Slice(s.Length, s.Length + 3));
        Assert.AreEqual("", s.Slice(5, 4));
        Assert.AreEqual("", s.Slice(5, 5));

        Assert.AreEqual("hello", s.SliceUntil(' '));

        Assert.AreEqual("o w", s.Clice(s => s.IndexOf('o'), (s, i) => s.IndexOf('o', i + 1)));
        Assert.AreEqual("llo wo", s.Clice(s => s.IndexOf('l'), s => s.IndexOf('r')));
        Assert.AreEqual("hello", s.Clice(s => s.IndexOf('x'), (s, i) => s.IndexOf(' ', i + 1)));
        Assert.AreEqual(" world", s.Clice(s => s.IndexOf(' ')));
        Assert.AreEqual("hello", s.CliceUntil(s => s.IndexOf(' ')));
    }
}
