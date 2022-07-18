using System;

namespace Viyi.Strings.Extensions.Internal;

internal static partial class ArrayExtensions {
    public static void Fill<T>(this T[] array, T element, int startIndex, int count) {
        if (startIndex < 0 || startIndex >= array.Length) {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }
        if (count < 0 || startIndex + count > array.Length) {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var end = startIndex + count;
        for (int i = startIndex; i < end; i++) {
            array[i] = element;
        }
    }
}
