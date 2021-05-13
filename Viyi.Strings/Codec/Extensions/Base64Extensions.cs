using System;
using System.Text;
using Viyi.Strings.Codec.Base64;
using Viyi.Strings.Codec.Extensions;

namespace Viyi.Strings.Codec.Base64
{
    /// <summary>
    /// 通过对byte[]和string扩展方法实现对数据按BASE64算法进行编码或解码。
    /// </summary>
    public static class Base64Extensions
    {
        public static byte[] DecodeBase64(this string base64)
        {
            return new Base64Codec().Decode(base64);
        }

        /// <summary>
        /// 将BASE64编码文本进行解码，得到原来的2进制数据
        /// </summary>
        /// <param name="base64">编码文本</param>
        /// <returns>解码出来的2进制数据</returns>
        public static byte[] Base64Decode(this string base64) => Convert.FromBase64String(base64);

        /// <summary>
        /// 将字符串进行BASE64编码，得到编码文本。
        /// </summary>
        /// <param name="source">需要编码的字符串</param>
        /// <param name="encoding">编码前将源字符串转换成2进制数据的编码方法</param>
        /// <param name="isBreakLines">是否在编码结果中每76个字符使用换行符进行分隔。</param>
        /// <returns>BASE64编码文本</returns>
        public static string Base64Encode(this string source, bool isBreakLines = false)
        {
            // TODO 默认使用 UTF-8 编码，不再提供多编码方式，如果需要多编码方式，自己用 Encoding 转
            return string.IsNullOrEmpty(source)
                ? source
                : source.DecodeUtf8().Base64Encode(isBreakLines);
        }

        /// <summary>
        /// 将字符串进行BASE64编码，得到编码文本。
        /// </summary>
        /// <param name="source">需要编码的字符串</param>
        /// <param name="encodingName">编码前将源字符串转换成2进制数据的编码方法</param>
        /// <param name="isBreakLines">是否在编码结果中每76个字符使用换行符进行分隔。</param>
        /// <returns>BASE64编码文本</returns>
        /// <exception cref="ArgumentException">encoding不是有效的代码页名称</exception>
        public static string Base64Encode(this string source, string encodingName,
            bool isBreakLines = false)
        {
            return source.Base64Encode(Encoding.GetEncoding(encodingName), isBreakLines);
        }

        /// <summary>
        /// 将BASE64编码文本进行解码，并将得到的二进制数据按 <c>encoding</c> 指定的编码生成字符串
        /// </summary>
        /// <param name="base64">BASE64编码数据</param>
        /// <param name="encoding">原始数据字符串的编码</param>
        /// <returns></returns>
        public static string Base64Decode(this string base64, Encoding encoding)
        {
            return string.IsNullOrEmpty(base64)
                ? base64
                : base64.Base64Decode().GetString(encoding ?? Settings.Global.DefaultEncoding);
        }

        /// <summary>
        /// 将BASE64编码文本进行解码，并将得到的二进制数据按 <c>encoding</c> 指定的编码生成字符串
        /// </summary>
        /// <param name="base64">BASE64编码数据</param>
        /// <param name="encodingName">原始数据字符串的编码</param>
        /// <returns></returns>
        public static string Base64Decode(this string base64, string encodingName)
        {
            return base64.Base64Decode(Encoding.GetEncoding(encodingName));
        }
    }
}
