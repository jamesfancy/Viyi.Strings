using System;
using System.Collections.Generic;
using System.IO;
using Viyi.Strings.Codec.Abstract;

namespace Viyi.Strings.Codec
{
    public partial class Base64Codec : CodecBase
    {

        static readonly char[] base64Chars = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '='
        };

        public override byte[] Decode(string code) => Convert.FromBase64String(code);

        public override void Decode(Stream outStream, IEnumerable<string> lines)
        {
            new Decoder(outStream).Decode(lines);
        }

        protected override void Encode(ICharWriter writer, IEnumerable<byte[]> data)
        {
            new Encoder(writer).Encode(data);
        }
    }
}
