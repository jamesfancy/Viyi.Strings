using System.Text;

namespace Viyi.Strings.Codec.Extensions
{
    public static class StringExtensions
    {
        public static byte[] DecodeUtf8(this string? @this)
        {
            return @this == null
                ? new byte[0]
                : Encoding.UTF8.GetBytes(@this);
        }

        public static byte[] Decode(this string? @this, string encoding)
        {
            if (@this == null) { return new byte[0]; }

            var codec = TextCodec.Find(encoding);
            if (codec != null)
            {
                return codec.Decode(@this);
            }

            return Encoding.GetEncoding(encoding).GetBytes(@this);
        }
    }
}
