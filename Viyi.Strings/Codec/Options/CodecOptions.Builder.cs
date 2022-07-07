namespace Viyi.Strings.Codec.Options;

partial class CodecOptions {
    public class Builder {
        protected readonly CodecOptions codecOptions;

        static CodecOptions Clone(CodecOptions proto) => Clone(new CodecOptions(), proto);

        protected static CodecOptions Clone(CodecOptions target, CodecOptions proto) {
            target.LineWidth = proto.LineWidth;
            target.LineEnding = proto.LineEnding;
            target.UpperCase = proto.UpperCase;
            return target;
        }

        public virtual CodecOptions Build() {
            return Clone(this.codecOptions);
        }

        // createDefaultOptions() 与 CodecOptions.CreateDefault() 的区别在于：
        // 没有 `DefaultCreator` 的时候，
        // createDefaultOptioss() 始终产生新的对象，
        // CodecOptions.CreateDefault() 始终返回同一个对象，即 CodecOptions.Default。
        private static CodecOptions CreateDefaultOptions() {
            return defaultCreator?.Invoke() ?? new CodecOptions();
        }

        internal Builder() {
            this.codecOptions = CreateDefaultOptions();
        }

        internal Builder(CodecOptions proto, bool needClone = true) {
            codecOptions = needClone ? Clone(proto) : proto;
        }

        public Builder SetLineWidth(int value) {
            codecOptions.LineWidth = value < 0 ? NoLineWidth : value;
            return this;
        }

        public Builder SetLineEnding(LineEndings value) {
            codecOptions.LineEnding = value;
            return this;
        }

        public Builder UseUpperCase(bool upperCase = true) {
            codecOptions.UpperCase = upperCase;
            return this;
        }

        public Builder UseLowerCase() {
            codecOptions.UpperCase = false;
            return this;
        }
    }
}
