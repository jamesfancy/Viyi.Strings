using System;
using System.Collections.Generic;
using Viyi.Strings.Codec.Base64;

namespace Viyi.Strings.Codec.Abstract
{
    public static class TextCodec
    {
        static Dictionary<string, Func<ITextCodec>> factories;

        static TextCodec()
        {
            factories = new(StringComparer.OrdinalIgnoreCase)
            {
                ["base64"] = () => new Base64Codec(),
                ["hex"] = () => new HexCodec()
            };
        }

        public static ITextCodec Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!factories.TryGetValue(name, out var factory))
            {
                throw new NotSupportedException(
                    $"not support text codec algorithm named '{name}'");
            }

            return factory();
        }

        public static ITextCodec? Find(string name) =>
            factories.TryGetValue(name, out var factory) ? factory() : null;

        public static bool IsRegistered(string name) => factories.ContainsKey(name);

        public static void Register(string name, Func<ITextCodec> factory) =>
            factories[name] = factory;

        public static void Unregister(string name) =>
            factories.Remove(name);
    }
}
