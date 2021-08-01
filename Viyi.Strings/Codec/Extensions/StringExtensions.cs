using System;
using System.Text;

namespace Viyi.Strings.Codec.Extensions
{
    public static class StringExtensions
    {
#if NET5_0_OR_GREATER
        static readonly byte[] EmptyBytes = Array.Empty<byte>();
#else
        static readonly byte[] EmptyBytes = new byte[0];
#endif

        public static byte[] DecodeUtf8(this string? str)
        {
            return str == null
                ? new byte[0]
                : Encoding.UTF8.GetBytes(str);
        }

        public static byte[] Decode(this string? str, string encoding)
        {

            if (str == null) { return EmptyBytes; }


            var codec = TextCodec.Find(encoding);
            if (codec != null)
            {
                return codec.Decode(str);
            }

            return Encoding.GetEncoding(encoding).GetBytes(str);
        }
    }
}
