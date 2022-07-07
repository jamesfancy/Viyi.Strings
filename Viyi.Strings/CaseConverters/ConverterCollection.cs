using System;
using System.Collections.Generic;

namespace Viyi.Strings.CaseConverters;

class ConverterCollection {
    public static class PredefinedNames {
        public const string Pascal = "pascal";
        public const string Camel = "camel";
        public const string Snake = "snake";
        public const string Kebab = "kebab";
    }

    public static class Predefined {
        public static readonly ICaseConverter Pascal = new PascalCaseConverter();
        public static readonly ICaseConverter Camel = new CamelCaseConverter();
        public static readonly ICaseConverter Snake = new SnakeCaseConverter();
        public static readonly ICaseConverter Kebab = new KebabCaseConverter();
    }

    private readonly Dictionary<string, ICaseConverter>
        data = new(StringComparer.OrdinalIgnoreCase)
        {
            { PredefinedNames.Pascal, Predefined.Pascal },
            { PredefinedNames.Camel, Predefined.Camel },
            { PredefinedNames.Snake, Predefined.Snake },
            { PredefinedNames.Kebab, Predefined.Kebab }
        };

    /// <summary></summary>
    /// <param name="casing"></param>
    /// <param name="converter"></param>
    /// <param name="force">如果已经存在某个名称的 Converter，则强制覆盖之。</param>
    /// <returns></returns>
    public ConverterCollection Register(string casing,
        ICaseConverter converter, bool force = false) {
        if (string.IsNullOrWhiteSpace(casing)) {
            throw new ArgumentNullException(nameof(casing));
        }

        if (converter == null) {
            throw new ArgumentNullException(nameof(converter));
        }

        if (!force && data.ContainsKey(casing)) {
            throw new InvalidOperationException(
                $"case converter named '{casing}' has already existed.");
        }

        data[casing] = converter;
        return this;
    }

    /// <summary>
    /// 按指定名称（不区分大小写）获取 ICaseConverter，如果未找到，
    /// 抛出 NotSupportedException。
    /// </summary>
    /// <param name="casing">转换器名称</param>
    /// <exception cref="NotSupportedException">未找到指定名称的 Converter </exception>
    /// <returns></returns>
    public ICaseConverter Get(string casing) {
        if (data.TryGetValue(casing, out ICaseConverter? converter)) {
            return converter;
        }

        throw new NotSupportedException($"not support case converter named '{casing}'");
    }

    public ICaseConverter? this[string casing] {
        get => data.TryGetValue(casing, out ICaseConverter? converter) ? converter : null;
        set { Register(casing, value!, false); }
    }
}
