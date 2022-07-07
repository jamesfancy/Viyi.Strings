using System;
using System.Collections.Generic;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Base16;
using Viyi.Strings.Codec.Base64;

namespace Viyi.Strings.Codec;

public static class TextCodec {
    static readonly Dictionary<string, Func<ITextCodec>> factories;
    static readonly LazyStorage storage = new();

    public static Base64Codec Base64 => storage.base64 ??= new();
    public static Base16Codec Base16 => storage.base16 ??= new();
    public static Base16Codec Hex => storage.hex ??= new();

    static TextCodec() {
        factories = new(StringComparer.OrdinalIgnoreCase) {
            ["base64"] = () => new Base64Codec(),
            ["base16"] = () => new Base16Codec(),
            ["hex"] = () => new HexCodec(),
        };
    }

    /// <summary>
    /// 根据名称创建编/解码器对象，若不能创建会抛出异常。
    /// </summary>
    /// <param name="name">
    /// 编/解码器的名称。
    /// 已注册的编/解码器名称可通过 GetRegistered() 获得；
    /// 也可以使用 IsRegistered(name) 来判断名称是否注册。
    /// </param>
    /// <exception cref="ArgumentNullException">如果 name 为 null 或全为空白字符时抛此异常</exception>
    /// <exception cref="NotSupportedException">如果 name 未注册时抛此异常</exception>
    public static ITextCodec Create(string name) {
        if (string.IsNullOrWhiteSpace(name)) {
            throw new ArgumentNullException(nameof(name));
        }

        if (!factories.TryGetValue(name, out var factory)) {
            throw new NotSupportedException(
                $"not support text codec algorithm named '{name}'");
        }

        return factory();
    }

    /// <summary>
    /// 获取所有已注册的编/解码器名称
    /// </summary>
    public static IEnumerable<string> GetRegistered() => factories.Keys;

    /// <summary>
    /// 根据名称创建编/解码器对象，如果名称无效，返回 null。
    /// </summary>
    /// <param name="name"></param>
    /// <remarks>此方法不会抛出异常</remarks>
    public static ITextCodec? CreateOrNull(string name) =>
        factories.TryGetValue(name, out var factory) ? factory() : null;

    public static bool IsRegistered(string name) => factories.ContainsKey(name);

    public static void Register(string name, Func<ITextCodec> factory) =>
        factories[name] = factory;

    public static void Unregister(string name) => factories.Remove(name);

    class LazyStorage {
        internal Base64Codec? base64;
        internal Base16Codec? base16;
        internal HexCodec? hex;
    }

}
