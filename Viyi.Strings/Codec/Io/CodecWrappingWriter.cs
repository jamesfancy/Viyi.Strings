using System;
using System.IO;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io; 
public partial class CodecWrappingWriter : CodecTextWriter {
    // 该数组元素（数量及顺序）与 LineEndings 枚举保持一致
    static readonly string[] EndOfLines = new[] {
        Environment.NewLine,
        "\n",
        "\r\n",
        "\r",
    };

    readonly string lineEnding;
    readonly int lineWidth;
    int restOfLine;

    public CodecWrappingWriter(TextWriter writer, CodecOptions options)
        : base(writer, options) {
        if (options.LineWidth <= 0) {
            throw new ArgumentException(
                "options.LineWidth should be positive number.",
                nameof(options)
            );
        }

        restOfLine = lineWidth = options.LineWidth;
        lineEnding = EndOfLines[(int)options.LineEnding];
    }

    public override void Write(char[] data) => Write(data, 0, data.Length);

    public override void Write(char[] data, int offset, int length) =>
        Write(new ArrayCharSequence(data, offset, length));

    public override void Write(string segment) => Write(new StringCharSequence(segment));

    protected void Write(ICharSequence sequence) {
        while (sequence.HasMore) {
            if (restOfLine == 0) {
                Writer.Write(lineEnding);
                restOfLine = lineWidth;
            }
            int count = sequence.ToWriter(Writer, restOfLine);
            restOfLine -= count;
        }
    }
}
