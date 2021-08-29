using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viyi.Strings.Test.Toolkit;

namespace Viyi.Strings.Extensions.Tests {
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
            Assert.IsFalse(NullString.ToBoolean((string?)null, null));
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
    }
}
