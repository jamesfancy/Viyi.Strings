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
        /// <param name="holdUpException">true，不抛异常；false 可能抛异常</param>
        /// <returns></returns>
        public static ICaseConverter? Get(string casing, bool holdUpException) {
            return holdUpException ? Converters[casing] : Get(casing);
        }

        /// <summary>
        /// 按名称获取 Converter，如果名称未注册会抛出异常。
        /// 如果希望用返回 null 来代替抛出异常，请使用 Get(casing, true)
        /// </summary>
        /// <param name="casing"></param>
        /// <exception cref="System.NotSupportedException">casing 指定的名称未注册时</exception>
        /// <returns></returns>
        public static ICaseConverter Get(string casing) => Converters.Get(casing);

        public static string SpecifiedCase(string value, string caseing) =>
            Converters.Get(caseing).Convert(value);

        /// <summary>
        /// 始终获取预定义的 Pascal Case Converter。
        /// 即使用 "pascal" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
        public static string PascalCase(string name) => Predefined.Pascal.Convert(name);

        /// <summary>
        /// 始终获取预定义的 Camel Case Converter。
        /// 即使用 "camel" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
        public static string CamelCase(string name) => Predefined.Camel.Convert(name);

        /// <summary>
        /// 始终获取预定义的 Snake Case Converter。
        /// 即使用 "snake" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
        public static string SnakeCase(string name) => Predefined.Snake.Convert(name);

        /// <summary>
        /// 始终获取预定义的 Kebab Case Converter。
        /// 即使用 "kebab" 覆盖注册了新的 Converter 也不会影响结果。
        /// 若要使用覆盖注册的 Converter 请使用 Get 或 SpecifiedCase 方法获。
        /// </summary>
        public static string KebabCase(string name) => Predefined.Kebab.Convert(name);
    }
}
