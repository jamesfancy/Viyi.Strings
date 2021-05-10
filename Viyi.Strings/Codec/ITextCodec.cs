using System.Collections.Generic;
using System.IO;

namespace Viyi.Strings.Codec
{
    public interface ITextCodec
    {
        /// <summary>
        /// 一次性编码所有 data，得到编码结果，适用于内容不是很多的情况
        /// </summary>
        string Encode(byte[] data);

        /// <summary>
        /// 一次性编码 data 中指定的段，得到编码结果，适用于内容不是很多的情况
        /// </summary>
        string Encode(byte[] data, int start, int length);

        /// <summary>
        /// 从 data 依次取出 byte[] 数据进行编码。同样只适用于 data 不是特别大的情况
        /// </summary>
        string Encode(IEnumerable<byte[]> data);

        /// <summary>
        /// 如果数据较大，可以通过 Stream 传入，一次性得到所有结果。适用于编码结果不是特别大的情况
        /// </summary>
        string Encode(Stream inStream);

        /// <summary>
        /// 如果输入输出内容都较多的情况，使用这个方法比较适合
        /// <summary>
        void Encode(TextWriter writer, IEnumerable<byte[]> data);

        /// <summary>
        /// 如果输入输出内容都较多的情况，使用这个方法比较适合
        /// <summary>
        void Encode(TextWriter writer, Stream inStream);

        /// <summary>
        /// 一次解码所有数据，适用于数据量不是特别大的情况
        /// <summary>
        byte[] Decode(string code);

        /// <summary>
        /// 如果输入输出内容都较多的情况，使用这个方法比较适合
        /// <summary>
        void Decode(Stream outStream, IEnumerable<string> lines);

        /// <summary>
        /// 如果输入输出内容都较多的情况，使用这个方法比较适合
        /// <summary>
        void Decode(Stream outStream, TextReader inStream);
    }
}
