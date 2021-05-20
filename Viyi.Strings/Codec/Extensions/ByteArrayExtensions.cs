using System.Text;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string Encode(this byte[] bytes, string encoding)
        {
            if (IsEmpty(bytes)) { return ""; }

            var codec = TextCodec.Find(encoding);
            if (codec != null)
            {
                return codec.Encode(bytes);
            }

            return Encoding.GetEncoding(encoding).GetString(bytes);
        }

        public static bool IsEmpty(this byte[] bytes) =>
            bytes == null || bytes.Length == 0;
    }
}
