using System.Runtime.CompilerServices;

namespace Viyi.Strings.Extensions.Internal;

internal static class ArrayExtensions {
    extension(byte[] bytes) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int ToInt32(int startIndex = 0) {
            return bytes[startIndex] << 24
                | bytes[startIndex + 1] << 16
                | bytes[startIndex + 2] << 8
                | bytes[startIndex + 3];
        }
    }

    extension<T>(T[] array) {
#if NETSTANDARD2_0
        internal void Fill(T element, int startIndex, int count) {
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Fill(T element, int start, int count) => Array.Fill<T>(array, element, start, count);
#endif
    }
}
