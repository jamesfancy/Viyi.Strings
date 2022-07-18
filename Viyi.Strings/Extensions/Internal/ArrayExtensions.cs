namespace Viyi.Strings.Extensions.Internal;

internal static partial class ArrayExtensions {
    public static int ToInt32(this byte[] bytes, int startIndex = 0) {
        return bytes[startIndex] << 24
            | bytes[startIndex + 1] << 16
            | bytes[startIndex + 2] << 8
            | bytes[startIndex + 3];
    }
}
