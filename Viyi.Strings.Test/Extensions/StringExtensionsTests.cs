using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
