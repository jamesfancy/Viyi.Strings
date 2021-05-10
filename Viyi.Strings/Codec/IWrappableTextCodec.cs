using System;

namespace Viyi.Strings.Codec
{
    [Obsolete("在 Writer 中处理换行的问题")]
    public interface IWrappableTextCodec : ITextCodec
    {
        /// <summary>
        /// 如果 `LineLength > 0`，则在编译时达到指定长度后换行，换行符由 `EndOfLine` 指定
        /// </summary>
        int LineLength { set; get; }
    }
}
