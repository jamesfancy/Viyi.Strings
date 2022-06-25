using System;

namespace Viyi.Strings.Codec.Options;

public enum LineEndings {
    ByEnvironment,
    Lf,
    Crlf,
    Cr,
}

public sealed partial class CodecOptions : IEquatable<CodecOptions> {
    public const int NoLineWidth = 0;

    /// <summary>
    /// 默认配置创建程序（已不推荐）。
    /// </summary>
    /// <remarks>
    /// 由于 DefaultCreator 的创建结果有可能会用于 Builder 加工，
    /// 所以对 DefaultCreator 的每次调用都应该产生新的 CodecOptions
    /// <strong>警告：由于 Codec.Create() 会调用 DefaultCreator，
    /// 如果用户在 DefaultCreator 中调用了 Codec.Create() 会造成无限递归调用</strong>
    /// </remarks>
    public static Func<CodecOptions>? DefaultCreator {
        get => defaultCreator;
        [Obsolete("DefaultCreator 可能造成递无限归调用。应该使用 SetDefaultCreator 代替默认全局配置。DefaultCreator 的设置接口将在 2022-9-1 后的 v1.x 版本中删除。")]
        set => defaultCreator = value;
    }

    /// <summary>
    /// 设置一个全局的 Build 函数，用来初始化配置。
    /// </summary>
    /// <remarks>
    /// 除非使用 CodecOptions.CreatePure() 开始创建配置，
    /// 否则配置都是重新创建并从由本方法设置的 building 开始进行配置。
    /// </remarks>
    /// <param name="buidling"></param>
    /// <param name="prototype">指定配置所基于的原型配置对象</param>
    public static void SetDefaultCreator(
        Action<Builder>? buidling,
        CodecOptions? prototype = null
    ) {
        defaultCreator = buidling == null
            ? null
            : () => {
                var builder = prototype == null ? CreatePure() : Create(prototype);
                buidling?.Invoke(builder);
                return builder.Build();
            };
    }
    private static Func<CodecOptions>? defaultCreator;

    /// <summary>
    /// 预定义的默认配置
    /// </summary>
    public static CodecOptions Default { get; } = new();

    /// <summary>
    /// 由 DefaultCreator 创建的默认配置。
    /// 如果没有指定 DefaultCreator，会使用预定义的默认配置。
    /// </summary>
    public static CodecOptions CreateDefault() {
        return defaultCreator?.Invoke() ?? Default;
    }

    private CodecOptions() { }

    /// <summary>
    /// 以默认配置为基础创建一个配置构造器。
    /// </summary>
    /// <remarks>
    /// 默认通过 DefaultCreator() 创建而来，
    /// 若没有配置 DefaultCreator()，则构建器会创建一个新的 CodecOptions 对象，并在此基础上进行配置。
    /// </remarks>
    public static Builder Create() => new();

    /// <summary>
    /// 创建一个新的 CodecOptions 作为构建器配置的基础对象。
    /// </summary>
    public static Builder CreatePure() => new(new(), false);

    /// <summary>
    /// 把 prototype 作为原型，创建一个新的 CodecOptions 作为构建器配置的基础对象。
    /// </summary>
    /// <param name="prototype">配置原型</param>
    public static Builder Create(CodecOptions prototype) => new(prototype);

    public override int GetHashCode() {
        unchecked {
            int hash = 19;
            hash = hash * 113 + LineWidth;
            hash = hash * 113 + (int) LineEnding;
            hash = hash * 113 + (UpperCase ? 37 : 73);
            return hash;
        }
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(obj, this)) { return true; }

        var opts = obj as CodecOptions;
        return Equals(opts);
    }

    public bool Equals(CodecOptions? other) {
        if (other == null) { return false; }
        return LineWidth == other.LineWidth
            && LineEnding == other.LineEnding
            && UpperCase == other.UpperCase;
    }

    /// <summary>
    /// 如果 LineWidth > 0，按指定的字符数折行
    /// </summary>
    public int LineWidth { get; private set; } = 0;

    /// <summary>
    /// 指定换行符，默认使用 LF（包括在 Windows 下）
    /// </summary>
    public LineEndings LineEnding { get; private set; } = LineEndings.Lf;

    /// <summary>
    /// 对大小写不敏感的编码设置输出字符的大小写，默认为小写。
    /// 配置输出字符的大小写。仅对大小写不敏感的编码方式有效，比如 Hex。
    /// 对大小写敏感的编码方式无效，比如 Base64。
    /// </summary>
    public bool UpperCase { get; private set; } = false;
}
