using System;
using System.Collections.Generic;

namespace Viyi.Strings.Codec
{
    public static class TextCodec
    {
        static Dictionary<string, Func<ITextCodec>> factories;

        static TextCodec()
        {
            factories = new Dictionary<string, Func<ITextCodec>>(StringComparer.OrdinalIgnoreCase)
            {
                ["base64"] = () => new Base64Codec(),
                ["hex"] = () => new HexCodec()
            };
        }

        /// <summary>
        /// 根据给定的名称 `name` 创建 `ITextCodec` 对象。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITextCodec Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Func<ITextCodec> factory;
            factories.TryGetValue(name, out factory);

            if (factory == null)
            {
                throw new NotSupportedException($"not suport text codec algorithm named {name}");
            }

            return factory();
        }

        public static bool IsRegistered(string name) => factories.ContainsKey(name);

        /// <summary>
        /// 注册一个 `ITextCodec` 创建器。如果指定名称的创建器已经存在，
        /// 替换它并将旧的创建器返回出来，否则返回 `null`。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static Func<ITextCodec> Register(string name, Func<ITextCodec> factory)
        {
            Func<ITextCodec> existFactory;
            factories.TryGetValue(name, out existFactory);
            factories[name] = factory;
            return existFactory;
        }

        /// <summary>
        /// 删除指定名称的创建器，并将它返回出来。如果不存在指定名称的创建器，返回 `null`。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Func<ITextCodec> Deregister(string name)
        {
            Func<ITextCodec> existFactory;
            factories.TryGetValue(name, out existFactory);
            factories.Remove(name);
            return existFactory;
        }
    }
}
