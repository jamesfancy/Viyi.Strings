using System.Text;

namespace Viyi.Strings.Codec.Extensions
{
    public static class ByteArrayExtensions
    {
        public static bool IsEmpty(this byte[] bytes) =>
            bytes == null || bytes.Length == 0;

        public static string Encode(this byte[] bytes, string encoding)
        {
            if (IsEmpty(bytes)) { return string.Empty; }

            var codec = TextCodec.Find(encoding);
            if (codec != null)
            {
                return codec.Encode(bytes);
            }

            return Encoding.GetEncoding(encoding).GetString(bytes);
        }

        public static string EncodeUtf8(this byte[] bytes)
        {
            if (IsEmpty(bytes)) { return string.Empty; }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
