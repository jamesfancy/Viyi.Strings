using System;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;
public partial class Base64CodecOptions {
    new public class Builder : CodecOptions.Builder {
        readonly Base64CodecOptions base64CodecOptions;

        protected static Base64CodecOptions Clone(Base64CodecOptions target, Base64CodecOptions proto) {
            CodecOptions.Builder.Clone(target, proto);
            target.Scheme = proto.Scheme;
            target.EncodingScheme = proto.EncodingScheme;
            return target;
        }

        protected static Base64CodecOptions Clone(Base64CodecOptions target, CodecOptions proto) {
            CodecOptions.Builder.Clone(target, proto);
            return target;
        }

        static Base64CodecOptions Clone(Base64CodecOptions proto) => Clone(new Base64CodecOptions(), proto);
        static Base64CodecOptions Clone(CodecOptions proto) => Clone(new Base64CodecOptions(), proto);

        internal Builder() : base(new Base64CodecOptions(), false) {
            base64CodecOptions = (Base64CodecOptions) base.codecOptions;
        }

        internal Builder(Base64CodecOptions proto, bool needClone = true)
            : base(needClone ? Clone(proto) : proto, false) {
            base64CodecOptions = (Base64CodecOptions) base.codecOptions;
        }

        internal Builder(CodecOptions proto) : base(Clone(proto), false) {
            base64CodecOptions = (Base64CodecOptions) base.codecOptions;
        }

        public new Base64CodecOptions Build() {
            return Clone(this.base64CodecOptions);
        }

        /// <summary>
        /// 在使用父类属性设置方法的时候，返回的是父类型的 Builder 引用。
        /// 通过 Also 可以在设置完成之后依然返回当前类型的 Builder 引用，方便链式操作。
        /// </summary>
        /// <example><code>
        /// b64oBuilder
        ///     .Also(builder => {
        ///         builder.SetLineWidth(80).SetLineEnding(LineEndings.Lf)
        ///     })
        ///     .UseScheme(Schemes.Base64Url);
        /// </code></example>
        public Builder Also(Action<Builder> fn) {
            fn(this);
            return this;
        }

        public Builder UseScheme(Schemes schemes) {
            base64CodecOptions.Scheme = schemes;
            return this;
        }

        public Builder UseEncodingScheme(Schemes scheme) {
            base64CodecOptions.EncodingScheme = scheme;
            return this;
        }
    }
}
