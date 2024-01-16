using Viyi.Strings.Extensions;

namespace Viyi.Strings.Test;

[TestClass]
public class NumsRangeTests {
    [TestMethod]
    public void ParseRangeTest() {
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 10, 11, 12 }, NumsRange.Parse("#1~#4,10-12"));
    }

    [TestMethod]
    public void MakeRangeTest() {
        Assert.AreEqual(
            "#1~#4,#10~#12",
            new[] { 1, 2, 3, 4, 10, 11, 12 }.ToRangeString("#")
        );

        Assert.AreEqual(
            "3,10-12,21-25",
            new[] { 3, 10, 11, 12, 21, 22, 23, 24, 25 }.ToRangeString("", "-")
        );

        Assert.AreEqual(
            "$03,$10~$12,$21~$25",
            new[] { 3, 10, 11, 12, 21, 22, 23, 24, 25 }
                .ToRangeString(n => $"${n:00}")
        );

        Assert.AreEqual(
            "$03,#10-12,#21-25",
            new[] { 3, 10, 11, 12, 21, 22, 23, 24, 25 }.ToRangeString(
                n => $"${n:00}",
                (a, b) => $"#{a}-{b}"
            )
        );
    }
}
