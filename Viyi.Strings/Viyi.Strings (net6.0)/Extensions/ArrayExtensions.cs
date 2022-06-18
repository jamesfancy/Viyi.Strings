using System;

namespace Viyi.Strings.Extensions;

internal static partial class ArrayExtensions {
    // NOTE netstandard2.0 中没有 Array.Fill 方法，所以封装扩展方法 Fill 作为适配器
    public static void Fill<T>(this T[] array, T element, int start, int count) {
        Array.Fill<T>(array, element, start, count);
    }
}
