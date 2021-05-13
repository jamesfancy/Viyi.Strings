using System;
using System.Collections.Generic;
using System.IO;
using Viyi.Strings.Codec.Abstract;

namespace Viyi.Strings.Codec
{
    public partial class Base64Codec : CodecBase
    {
        public override byte[] Decode(string code) => Convert.FromBase64String(code);

        public override void Decode(Stream outStream, IEnumerable<string> lines)
        {
            new Decoder().Decode(lines);
        }

        protected override void Encode(ICharWriter writer, IEnumerable<byte[]> data)
        {
            new Encoder(writer).Encode(data);
        }
    }
}
