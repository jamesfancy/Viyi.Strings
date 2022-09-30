using System.Runtime.CompilerServices;

namespace Viyi.Strings.Extensions;

public static class FormattableStringExtensions {
    /// <summary>
    /// 预先对 FormattableString 进行一次不完全的格式化
    /// </summary>
    /// <param name="formattable"></param>
    /// <param name="choosePreArgs">
    /// 用来选择需要预格式化的函数：
    /// 签名 `(object? value, int index) => bool`。
    /// value 是参数值，index 是参数序号
    /// </param>
    /// <returns></returns>
    public static FormattableString PreFormat(
        this FormattableString formattable,
        Func<object?, int, bool> choosePreArgs
    ) {
        var arguments = formattable.GetArguments();
        var restArguments = Enumerable.Empty<object?>();
        int restIndex = 0;
        var preArgs = arguments
            .Select((value, index) => {
                if (choosePreArgs(value, index)) { return value; }
                else {
                    restArguments = restArguments.Append(value);
                    return $"{{{restIndex++}}}";
                }
            })
            .ToArray();
        return FormattableStringFactory.Create(
            string.Format(formattable.Format, preArgs),
            restArguments.ToArray()
        );
    }

    /// <summary>
    /// 预先对 FormattableString 进行一次不完全的格式化
    /// </summary>
    /// <param name="formattable"></param>
    /// <param name="choosePreArgs">
    /// 用来选择需要预格式化的函数：
    /// 签名 `(object? value, int index, int count) => bool`。
    /// value 是参数值，index 是参数序号，count 是参数总数
    /// </param>
    /// <returns></returns>
    public static FormattableString PreFormat(
        this FormattableString formattable,
        Func<object?, int, int, bool> choosePreArgs
    ) {
        return PreFormat(
            formattable,
            (value, i) => choosePreArgs(value, i, formattable.ArgumentCount)
        );
    }

    /// <summary>
    /// 预先对 FormattableString 进行一次不完全的格式化
    /// </summary>
    /// <param name="formattable"></param>
    /// <param name="preArgIndexes">用于预格式化的参数序号</param>
    /// <returns></returns>
    public static FormattableString PreFormat(
        this FormattableString formattable,
        params int[] preArgIndexes
    ) {
        return PreFormat(
            formattable,
            (_, i) => preArgIndexes.Contains(i)
        );
    }
}
