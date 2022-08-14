using Viyi.Strings.Codec.Base32;

namespace Viyi.Strings.Test.Codec.Base32;

[TestClass]
public class Base32ReverseCharsTest {
    [TestMethod]
    public void TestReverseChars() {
        var map = Enumerable.Range('a', 26).Select((n, i) => new { code = n, value = i })
            .Union(Enumerable.Range('A', 26).Select((n, i) => new { code = n, value = i }))
            .Union(Enumerable.Range('2', 6).Select((n, i) => new { code = n, value = i + 26 }))
            .ToDictionary(it => it.code, it => it.value);
        PrintTable(map);

        var codes = Base32ReverseCharset.Codes;
        codes.Select((v, i) => {
            Assert.AreEqual(map.TryGetValue(i, out int value) ? v : -1, v);
            return true;
        });

        map.ForEach(entry => Assert.AreEqual(entry.Value, codes[entry.Key]));
    }

    [TestMethod]
    public void TestHexReverseChars() {
        var map = Enumerable.Range('0', 10).Select((n, i) => new { code = n, value = i })
            .Union(Enumerable.Range('A', 22).Select((n, i) => new { code = n, value = i + 10 }))
            .Union(Enumerable.Range('a', 22).Select((n, i) => new { code = n, value = i + 10 }))
            .ToDictionary(it => it.code, it => it.value);
        PrintTable(map);

        var codes = Base32HexReverseCharset.Codes;

        codes.Select((v, i) => {
            Assert.AreEqual(map.TryGetValue(i, out int value) ? v : -1, v);
            return true;
        });

        map.ForEach(entry => Assert.AreEqual(entry.Value, codes[entry.Key]));
    }

    private static void PrintTable(Dictionary<int, int>? map) {
        Print(0, 16, map);
        Print(16, 32, map);
        Print(32, 48, map);
        Print(48, 58, map);                  // 数字
        Print(58, 65, map);
        Print(65, 65 + 7, map);              // 大写字母 4 行
        Print(65 + 7, 65 + 14, map);
        Print(65 + 14, 65 + 14 + 6, map);
        Print(65 + 14 + 6, 65 + 26, map);
        Print(91, 97, map);
        Print(97, 97 + 7, map);              // 小写字母 4 行
        Print(97 + 7, 97 + 14, map);
        Print(97 + 14, 97 + 14 + 6, map);
        Print(97 + 14 + 6, 97 + 26, map);
    }

    private static void Print(int start, int end, Dictionary<int, int>? map) {
        var line = string.Join(
            ", ",
            Enumerable.Range(start, end - start)
                .Select(key => map.TryGetValue(key, out var value) ? value : -1)
        );
        Trace.WriteLine($"{line},");
    }
}
