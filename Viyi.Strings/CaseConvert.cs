using System;
using System.Diagnostics.CodeAnalysis;
using Viyi.Strings.CaseConverters;
using static Viyi.Strings.CaseConverters.ConverterCollection;

namespace Viyi.Strings {
    public static class CaseConvert {
        static readonly ConverterCollection Converters = new();

        /// <summary>
        /// 按名称获取 Converter，若要获取的 Converter 名称未注册，
        /// 根据 casing 的值来决定是抛出异常还是返回 null。
        /// </summary>
        /// <param name="casing"></param>
        /// <param name="holdException">true，不抛异常；false 可能抛异常</param>
        /// <returns></returns>
        public static ICaseConverter? Get(string casing, bool holdException) {
            return holdException ? Converters[casing] : Get(casing);
        }

        /// <summary>
        /// 按名称获取 Converter，如果名称未注册会抛出异常。
        /// 如果希望用返回 null 来代替抛出异常，请使用 Get(casing, true)
        /// </summary>
        /// <param name="casing"></param>
        /// <exception cref="System.NotSupportedException">casing 指定的名称未注册时</exception>
        /// <returns></returns>
        public static ICaseConverter Get(string casing) => Converters.Get(casing);

        /// <summary>
        /// 注册一个 ICaseConverter，若 `force` 为 `ture`，可强制覆盖已经注册的同名 converter。
        /// </summary>
        /// <param name="casing"></param>
        /// <param name="converter"></param>
        /// <param name="force">true 强制覆盖注册；false 且 casing 已经存在会引发异常</param>
        /// <exception cref="ArgumentNullException">casing 或 converter 无效时发生</exception>
        /// <exception cref="InvalidOperationException">casing 已经存在且 force 不为 true 时发生</exception>
        /// <remark>
        /// 注意：强制注册名为 "pascal", "camel", "kebab", "snake" 的转换器并不能改变由 CaseConvert
        /// 属性提供的预置同名转换器，但可以使用 Get(string casing) 取得。
        /// CaseConvert 提供的对应的扩展方法也是使用的预置转换器，而不是注册的同名转换器。
        /// 但可以扩展方法 CaseTo(string casing) 会使用注册的同名转换器。
        /// </remark>
        public static void Register(string casing, ICaseConverter converter, bool force = false) {
            Converters.Register(casing, converter, force);
        }

        /// <summary>
        /// 注册一个 ICaseConverter，注册失败时由 holdUpExceptoin 决定是否引发异常。以下三种情况会引发注册失败
        /// 1. casing 是无效字符串（null, "" 或只包含空白字符）
        /// 2. converter 为 null
        /// 3. 已存在名为 casing（不区分大小写）的注册，且 force 不为 true。
        /// </summary>
        /// <param name="casing"></param>
        /// <param name="converter"></param>
        /// <param name="force"></param>
        /// <param name="hodeException"></param>
        /// <remark>
        /// 注意：强制注册名为 "pascal", "camel", "kebab", "snake" 的转换器并不能改变由 CaseConvert
        /// 属性提供的预置同名转换器，但可以使用 Get(string casing) 取得。
        /// CaseConvert 提供的对应的扩展方法也是使用的预置转换器，而不是注册的同名转换器。
        /// 但可以扩展方法 CaseTo(string casing) 会使用注册的同名转换器。
        /// </remark>
        public static bool Register(string casing, ICaseConverter converter,
            bool force, bool hodeException) {
            if (force || !hodeException) {
                Register(casing, converter, force);
                return true;
            }

            try {
                Register(casing, converter, force);
                return true;
            }
            catch {
                return false;
            }
        }

        public static void Register(string casing, Func<string?, string?> convert,
            bool force = false) =>
            Register(casing, new LambdaCaseConverter(convert), force);

        public static bool Register(string casing, Func<string?, string?> convert,
            bool force, bool holdUpExceptoin) =>
            Register(casing, new LambdaCaseConverter(convert), force, holdUpExceptoin);

        public static ICaseConverter Pascal => Predefined.Pascal;
        public static ICaseConverter Camel => Predefined.Camel;
        public static ICaseConverter Kebab => Predefined.Kebab;
        public static ICaseConverter Snake => Predefined.Snake;

#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        [Obsolete("在 v1.1 删除。使用 CaseTo(this string? value, string casing) 代替")]
        public static string? SpecifiedCase(string? value, string casing) =>
            Converters.Get(casing).Convert(value);

#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public static string? CaseTo(this string? value, string casing) =>
            Converters.Get(casing).Convert(value);

        /// <summary>
        /// 始终获取预定义的 Pascal Case Converter。
        /// 即使用 "pascal" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public static string? PascalCase(this string? value) => Pascal.Convert(value);

        /// <summary>
        /// 始终获取预定义的 Camel Case Converter。
        /// 即使用 "camel" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public static string? CamelCase(this string? value) => Camel.Convert(value);

        /// <summary>
        /// 始终获取预定义的 Snake Case Converter。
        /// 即使用 "snake" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public static string? SnakeCase(this string? value) => Snake.Convert(value);

        /// <summary>
        /// 始终获取预定义的 Kebab Case Converter。
        /// 即使用 "kebab" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public static string? KebabCase(this string? value) => Kebab.Convert(value);
    }
}
