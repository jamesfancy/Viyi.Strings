using System.Text;

#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace Viyi.Strings.Codec.Extensions
{
    public static class StringExtensions
    {
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("this")]
#endif
        public static byte[]? DecodeUtf8(this string? @this)
        {
            return @this == null
                ? null
                : Encoding.UTF8.GetBytes(@this);
        }


#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("this")]
#endif
        public static byte[]? Decode(this string? @this, string encoding)
        {
            if (@this == null) { return null; }

            var codec = TextCodec.Find(encoding);
            if (codec != null)
            {
                return codec.Decode(@this);
            }

            return Encoding.GetEncoding(encoding).GetBytes(@this);
        }
    }
}
