using System.Diagnostics.CodeAnalysis;
using Viyi.Strings.CaseConverters;
using CaseToolkit = Viyi.Strings.CaseConverters.Toolkit;

namespace Viyi.Strings.Test;

[TestClass]
public class CaseConvertTests {
    readonly static string?[][] cases = new[] {
        // original, pascal, camel, kebab, snake
        new string?[] { null, null, null, null, null },
        new[] { "", "", "", "", "" },
        new[] { "single", "Single", "single", "single", "single" },
        new[] { "two words", "TwoWords", "twoWords", "two-words", "two_words" },
        new[] {
            "the quick brown fox jumps over the lazy dog",
            "TheQuickBrownFoxJumpsOverTheLazyDog",
            "theQuickBrownFoxJumpsOverTheLazyDog",
            "the-quick-brown-fox-jumps-over-the-lazy-dog",
            "the_quick_brown_fox_jumps_over_the_lazy_dog",
        },
        new[] { "HTML", "Html", "html", "html", "html" },
        new[] {
            "CONTENT_CASE_STRING",
            "ContentCaseString", "contentCaseString",
            "content-case-string", "content_case_string",
        },
        new[] {
            "HTTPClient", "HttpClient", "httpClient", "http-client", "http_client"
        },
    };

    string[] x = new[] { "asdf", "dasfasdf" };

    [TestMethod]
    public void SingleCaseTest() {
        InternalSingleCaseTest(CaseConvert.Pascal, CaseConvert.PascalCase, 1);
        InternalSingleCaseTest(CaseConvert.Camel, CaseConvert.CamelCase, 2);
        InternalSingleCaseTest(CaseConvert.Kebab, CaseConvert.KebabCase, 3);
        InternalSingleCaseTest(CaseConvert.Snake, CaseConvert.SnakeCase, 4);
    }

    [TestMethod]
    public void ConvertToEachOtherTest() {
        var idx = new int[] { 1, 2, 3, 4 };
        var cvts = new Func<string?, string?>[] {
            CaseConvert.PascalCase,
            CaseConvert.CamelCase,
            CaseConvert.KebabCase,
            CaseConvert.SnakeCase
        };

        for (int i = 0; i < 4; i++) {
            cases.ForEach(group => {
                Assert.AreEqual(group[idx[1]], cvts[1](group[idx[0]]));
                Assert.AreEqual(group[idx[2]], cvts[2](group[idx[0]]));
                Assert.AreEqual(group[idx[3]], cvts[3](group[idx[0]]));
            });
            shift();
        }

        void shift() {
            var n = idx[0];
            var c = cvts[0];

            for (int i = 0; i < 3; i++) {
                idx[i] = idx[i + 1];
                cvts[i] = cvts[i + 1];
            }

            idx[3] = n;
            cvts[3] = c;
        }
    }

    void InternalSingleCaseTest(
        ICaseConverter converter,
        Func<string?, string?> convert,
        int expectIndex) {
        cases.ForEach(group => {
            var origin = group[0];
            var expect = group[expectIndex];
            Assert.AreEqual(expect, converter.Convert(origin));
            Assert.AreEqual(expect, convert(origin));
        });
    }

    [TestMethod]
    public void RegisterTest() {
        Assert.ThrowsException<InvalidOperationException>(() => {
            CaseConvert.Register("camel", s => s?.ToLower());
        });
        Assert.IsFalse(CaseConvert.Register("camel", s => s?.ToLower(), false, true));
        CaseConvert.Register("camel", s => s?.ToLower(), true);
        const string? theCase = "HTTPClient";
        Assert.AreEqual("httpClient", theCase.CamelCase());
        Assert.AreEqual("httpclient", theCase.CaseTo("camel"));

        Assert.ThrowsException<NotSupportedException>(() => theCase.CaseTo("unknown"));

        CaseConvert.Register("sentence", new MyCaseConverter());
        Assert.AreEqual("hello world", "HelloWorld".CaseTo("sentence"));
    }

    class MyCaseConverter : ICaseConverter {
        [return: NotNullIfNotNull(nameof(value))]
        public string? Convert(string? value) {
            return CaseToolkit.ToKebabCase(value)?.Replace("-", " ");
        }
    }
}
