using System;
using System.Linq;

namespace Viyi.Strings.Extensions {
    public static partial class StringExtensions {
        private const StringComparison BoolComparison = StringComparison.OrdinalIgnoreCase;

        /// <summary>
        /// "false" (忽略大小写)、""（空字符串）和 null 被识别为 false，
        /// 其他情况识别为 true。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? str) {
            return !str.EmptyAs("false").Equals("false", BoolComparison);
        }

        /// <summary>
        /// 严格模式下，只对 "true" 和 "false" 进行解析（不区分大小写），其他情况返回 null。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strict">指示是否严格模式</param>
        /// <returns></returns>
        public static bool? ToBoolean(this string? str, bool strict) {
            if (!strict) { return ToBoolean(str); }
            return bool.TryParse(str, out var value) ? value : null;
        }

        /// <summary>
        /// 根据指定的真/假字符串（可 null）判断 true/false。
        /// 如果 trueString 和 falseString 都是 null，优先判定为 false；
        /// 其他情况下如果 trueString == falseString，优先判定为 true。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="trueString"></param>
        /// <param name="falseString"></param>
        /// <returns></returns>
        public static bool? ToBoolean(this string? str, string? trueString, string? falseString) {
            if (str == null) {
                if (falseString == null) { return false; }
                if (trueString == null) { return true; }
                return null;
            }

            if (str.Equals(trueString, BoolComparison)) { return true; }
            if (str.Equals(falseString, BoolComparison)) { return false; }
            return null;
        }

        /// <summary>
        /// 如果 str 在给定的 valueStrings 中（包含 null 元素），则返回 value；
        /// 否则返回 !value。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="valueStrings"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? str, bool value, params string?[] valueStrings) {
            if (str == null) {
                return !(value ^ (valueStrings?.Contains(null) ?? false));
            }

            return !(value ^ (valueStrings?.Any(s => str.Equals(s, BoolComparison)) ?? false));
        }

        /// <summary>
        /// 根据给定的真值数组和假值数组对当前字符串进行判定（不区分大小写）。
        /// 如果当前字符串在某个数组中，则返回对应的 bool 值。
        /// 如果当前字符串在两个数组中都存在，其值为 null 时优先判断 false 值，
        /// 其他情况优先判断n true 值。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="trueStrings"></param>
        /// <param name="falseStrings"></param>
        /// <returns></returns>
        public static bool? ToBoolean(this string? str,
            string?[] trueStrings, string?[] falseStrings) {
            // str 为 null 时优先判定 false 值
            if (str == null && falseStrings.Contains(null)) { return false; }

            // 其他情况优先判断 true 值
            if (ToBoolean(str, true, trueStrings)) { return true; }
            if (ToBoolean(str, true, falseStrings)) { return false; }
            return null;
        }

        /// <summary>
        /// 根据指定的 predicator 判定当前字符串为 true/false/null。
        /// predicator 可以由 Viyi.Strings.Booleans.CreatePredicator(...) 创建。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="predicator"></param>
        /// <returns></returns>
        public static bool? ToBoolean(this string? str,
            Func<string?, bool?> predicator) => predicator(str);

        /// <summary>
        /// 根据指定的 predicator 判定当前字符串为 true/false。
        /// predicator 可以由 Viyi.Strings.Booleans.CreatePredicator(...) 创建。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="predicator"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? str,
            Func<string?, bool> predicator) => predicator(str);
    }
}
