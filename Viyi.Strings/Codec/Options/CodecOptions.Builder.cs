namespace Viyi.Strings.Codec.Options;

partial class CodecOptions {
    public class Builder {
        protected readonly CodecOptions codecOptions;

        static CodecOptions Clone(CodecOptions prototype) => Clone(new CodecOptions(), prototype);

        protected static CodecOptions Clone(CodecOptions target, CodecOptions prototype) {
            target.LineWidth = prototype.LineWidth;
            target.LineEnding = prototype.LineEnding;
            target.UpperCase = prototype.UpperCase;
            return target;
        }

        public virtual CodecOptions Build() {
            return Clone(this.codecOptions);
        }

        // createDefaultOptions() 与 CodecOptions.CreateDefault() 的区别在于：
        // 没有 `DefaultCreator` 的时候，
        // createDefaultOptions() 始终产生新的对象，
        // CodecOptions.CreateDefault() 始终返回同一个对象，即 CodecOptions.Default。
        private static CodecOptions CreateDefaultOptions() {
            return defaultCreator?.Invoke() ?? new CodecOptions();
        }

        internal Builder() {
            this.codecOptions = CreateDefaultOptions();
        }

        internal Builder(CodecOptions prototype, bool needClone = true) {
            codecOptions = needClone ? Clone(prototype) : prototype;
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
