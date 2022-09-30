namespace Viyi.Strings.Extensions.Internal;

internal static class ArrayExtensions {
    public static int ToInt32(this byte[] bytes, int startIndex = 0) {
        return bytes[startIndex] << 24
            | bytes[startIndex + 1] << 16
            | bytes[startIndex + 2] << 8
            | bytes[startIndex + 3];
    }

#if NETSTANDARD2_0
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
#else
    public static void Fill<T>(this T[] array, T element, int start, int count) {
        Array.Fill<T>(array, element, start, count);
    }
#endif
}
