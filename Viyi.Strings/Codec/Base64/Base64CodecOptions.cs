using System;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

public enum Schemes {
    /// <summary>原始的 Base 64</summary>
    Base64,
    /// <summary>Base 64 Url</summary>
    Base64Url,
    /// <summary>解码时兼容 Base 64 和 Base 64 Url</summary>
    Compatible
}

public partial class Base64CodecOptions : CodecOptions, IEquatable<Base64CodecOptions> {
    public static new Func<Base64CodecOptions>? DefaultCreator { get; private set; }

    public static void SetDefaultCreator(
        Action<Builder>? building,
        Base64CodecOptions? prototype = null
    ) => SetDefaultCreator(building, (CodecOptions?) prototype);

    public static void SetDefaultCreator(
        Action<Builder>? building,
        CodecOptions? prototype = null
    ) {
        if (building == null) {
            DefaultCreator = null;
            return;
        }

        Func<Builder> createBuilder = prototype == null
            ? () => CreatePure()
            : prototype is Base64CodecOptions b64Prototype
                ? () => Create(b64Prototype)
                : () => Create(prototype);

        DefaultCreator = () => {
            var builder = createBuilder();
            building?.Invoke(builder);
            return builder.Build();
        };
    }

    /// <summary>
    /// 预定义的默认配置
    /// </summary>
    public static new Base64CodecOptions Default { get; } = new();

    /// <summary>
    /// 由 DefaultCreator 创建的默认配置。
    /// 如果没有指定 DefaultCreator，会使用预定义的默认配置。
    /// </summary>
    public static new Base64CodecOptions CreateDefault() {
        return DefaultCreator?.Invoke() ?? Default;
    }

    /// <summary>
    /// 以默认配置为基础创建一个配置构造器。
    /// </summary>
    /// <remarks>
    /// 默认通过 DefaultCreator() 创建而来，
    /// 若没有配置 DefaultCreator()，则构建器会创建一个新的 Base64CodecOptions 对象，并在此基础上进行配置。
    /// </remarks>
    public static new Builder Create() => new();

    /// <summary>
    /// 创建一个新的 Base64CodecOptions 作为构建器配置的基础对象。
    /// </summary>
    public static new Builder CreatePure() => new(new(), false);

    /// <summary>
    /// 把 prototype 作为原型，创建一个新的 Base64CodecOptions 作为构建器配置的基础对象。
    /// </summary>
    /// <param name="prototype">配置原型</param>
    public static Builder Create(Base64CodecOptions prototype) => new(prototype);

    /// <summary>
    /// 把 prototype 作为原型，创建一个新的 Base64CodecOptions 作为构建器配置的基础对象。
    /// </summary>
    /// <param name="prototype">配置原型</param>
    public static new Builder Create(CodecOptions prototype) => new(prototype);

    /// <summary>
    /// 指定编/解码的方案，是原始的 Base 64 (Base64) 还是可用于 Url 的 Base 64 (Base64Url)。
    /// 默认值是 Base64
    /// </summary>
    /// <remarks>
    /// 如果指定为 Compatible，在编码时将使用 Base 64 Url 方案编码，
    /// 除非指定 EncodingSchema 为 Base64。
    /// </remarks>
    public Schemes Scheme { get; private set; }

    /// <summary>
    /// 指定 Base 64 编码方案。只有明确指定为 Base64 的时候才会使用原始的 Base 64 编码，
    /// 否则使用 Base 64 Url 编码。
    /// </summary>
    public Schemes EncodingScheme {
        get => encodingScheme ?? Scheme;
        private set => encodingScheme = value;
    }
    Schemes? encodingScheme;

    public bool Equals(Base64CodecOptions other) {
        if (other == null) { return false; }
        if (!base.Equals(other)) { return false; }
        return Scheme == other.Scheme
            && (EncodingScheme == Schemes.Base64 ^ (other.EncodingScheme != Schemes.Base64));
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(obj, this)) { return true; }
        return Equals(obj as CodecOptions);
    }

    public override int GetHashCode() {
        unchecked {
            int hash = base.GetHashCode();
            hash = hash * 113 + (int) Scheme;
            hash = hash * 113 + EncodingScheme == Schemes.Base64 ? 37 : 73;
            return hash;
        }
    }
}
