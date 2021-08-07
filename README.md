# Viyi.Strings

## 1. 关于 (About)

Viyi.Strings 是从 [Viyi.Util][viyi_util] 中分离出来的，专注于处理字符串相关内容的一个库。目前主要提供对二进制数据进行十六进制编/解码和 Base64 编/解码。将来会提供更多的字符串工具。

Viyi.Strings is a library separated from [Viyi.Util][viyi_util], which focuses on processing strings. At present, Viyi.Strings suplies Base64 and Hex encoding/decoding by extension methods, as  well as a text encoding/decoding framework. More string tools will be avaliable in the future.

> 原 Viyi.Util 涉及的范围非常多，过于分散，而且其中部分工具已经有更好的替代品。所以没有对 Viyi.Util 直接进行升级，而是将其拆分：
>
> - Viyi.Strings 用于处理字符串 (Viyi.Strings is a string toolkit.)
> - Viyi.Bytes 用于处理二进制数据，计划中 (Viyi.Bytes is a binary data toolkit, in plan.)

### 1.1. 主要包括 (Mainly Includes)

- `byte[].EncodeBase64(...)` / `string.DecodeBase64(...)` 
- `byte[].EncodeHex(...)` / `string.DecodeHex(...)`
- `byte[].EncodeUtf8()` / `string.DecodeUtf8()`
- `byte[].Encode(string name)` / `string.Decode(string name)`
- `TextCodec` 注册中心 (Registery)

## 2. 安装

Viyi.Strings 发布在 NuGet 上，在 Visual Studio 中可以使用 [NuGet Package Manager][vs_nuget] 安装，也可以在 Powershell Manager 命令行安装：

```powershell
Install-Package Viyi.Strings
```

或通过 .NET CLI  安装：

```powershell
dotnet add package Viyi.Strings
```

## 3. Viyi.Strings API （扩展方法）

### 3.1. LineEndings 枚举

```csharp
public enum LineEndings
{
    ByEnvironment,  // 0, 根据当前运行的系统决定换行符
    Lf,             // 1, Unix/Mac 风格换行符 Linefeed ("\n", 0x0a)
    Crlf,           // 2, Windows 风格换行符 Carriage Return &LineFeed ("\r\n", "0x0d0a")
    Cr,             // 3, 旧 Mac 风格换行符 Carriage Return ("\r", 0x0d)
}
```

### 3.2. 配置对象 CodecOptions

> Full Name: `Viyi.Strings.Codec.Options.CodecOptions`

`CodecOptions` 对象是不可变的，所有属性只读。对象中包含的配置项及默认值如下：

```csharp
public sealed class CodecOptions {
    public LineEndings LineEnding { get; } = LineEndings.Lf;
    public int LineWidth { get; } = 0;
    public bool UpperCase { get; } = false;
}
```

注意：

1. `LineEnding` 默认是 `Lf`，不是 `Crlf`，哪怕是在 Windows 下也是如此。
2. `UpperCase` 配置仅在部分情况下有效，比如对十六进制编码有效，对 Base64 编码无效。

#### 3.2.1. CodecOptions.Default

`CodecOptions.Default` 是一个单例，保存着内置的默认配置。

#### 3.2.2. DefaultCreator 和 CreateDefault()

`CodecOptions.DefaultCreator` 是 `Func<CodecOptions>` 类型的工厂函数，可由用户指定以改变业务代码使用的默认配置。

`CodecOptions.CreateDefault()` 会使用 `CodecOptions.DefaultCreator` 来创建配置；如果 `CodecOptions.DefaultCreator` 不存在，则直接返回 `CodecOptions.Default`。

除 `CodecOptions.Builder` 外，Viyi.Strings 中所有使用的默认 `CodecOptions` 对象都由  `CodecOptions.CreateDefault()` 创建。

#### 3.2.3. 创建并配置 CodecOptions

`CodecOptions` 对象是不可变对象，只能通过 `CodecOptions.Builder` 来创建并设置属性。`CodecOptions.Create()` 有两个重载可以用来创建 `CodecOptions.Builder` 实例：

1. `CodecOptions.Create()` 创建一个包含默认配置的 Builder
2. `CodecOptions.Create(CodecOptions prototype)` 按照指定配置初始化并创建 Builder

使用 `builder.Build()` 获取设置好的配置对象。每次调用 `.Build()` 均会创建一个新的配置对象副本。

### 3.3. Base64 编码/解码

#### 3.3.1. `string EncodeBase64(this byte[] bytes)`

使用默认配置将字节数组编码为 Base64 字符串。结果字符串是完整的 Base64 编码，不含行结束符。

#### 3.3.2. `string EncodeBase64(this byte[] bytes, bool lineBreak)`

将字符串数组编码为 Base64 字符串，字符串会按每行 `76` 个字符进行折行处理，行结束符是默认的 `Lf`。不管最后一行是否满行，末尾均不会添加行结束符。

#### 3.3.3. `string EncodeBase64(this byte[] bytes, int lineWidth)`

将字符数组编码为 Base64 字符串，字符串会按指定的行宽 (`lineWidth`) 折行，行结束符是默认的 `Lf`。不管最后一行是否满行，末尾均不会添加行结束符。

#### 3.3.4. `string EncodeBase64(this byte[] bytes, CodecOptions? options = null)`

根据指定的配置进行 Base64 编码。

#### 3.3.5. `byte[] string.DecodeBase64(this string base64)`

将 Base64 字符串解码为字节数组。该方法不会判断 Base64 字符串的合法性，而是直接将非 Base64 字符会忽略（过滤）掉。Base64 结尾的等号 (`=`) 不是必须。但有效字符数量除以 `4` 的余数为 `1` 会引发 `CodecException`。

### 3.4. 十六进制 (Hex) 编码/解码

#### 3.4.1. `string EncodeHex(this byte[] bytes)`

使用默认配置将字节数组编码为十六进制字符串。结果字符串是完整的 Base64 编码，不含行结束符。

#### 3.4.2. `string EncodeHex(this byte[] bytes, bool upperCase, bool lineBreak = false)`

> - `string EncodeHex(this byte[] bytes, bool upperCase)`
> - `string EncodeHex(this byte[] bytes, bool upperCase, bool lineBreak)`

将字符串数组编码为十六进制字符串，`lineBreak` 为 `true` 时字符串会按每行 `64` 个字符进行折行处理，行结束符是默认的 `Lf`。不管最后一行是否满行，末尾均不会添加行结束符。`upperCase` 可以指定是否使用大写的  `A~F`。

#### 3.4.3. `string EncodeHex(this byte[] bytes, bool upperCase, int lineWidth)`

将字符数组编码为十六进制字符串，字符串会按指定的行宽 (`lineWidth`) 折行，行结束符是默认的 `Lf`。不管最后一行是否满行，末尾均不会添加行结束符。`upperCase` 可以指定是否使用大写的  `A~F`。

#### 3.4.4. `string EncodeHex(this byte[] bytes, CodecOptions? options = null)`

根据指定的配置进行十六进制编码。

#### 3.4.5. `byte[] string.DecodeHex(this string base64)`

将十六进制字符串解码为字节数组。该方法不会判断十六进制字符串的合法性，而是直接将非十六进制字符会忽略（过滤）掉。如果有效的十六进制字符数是单数，则**最后一个**字符会被忽略掉。

### 3.5. 通用编/解码 (string ⇔ byte[])

#### 3.5.1. Viyi.Strings.Codec.Abstract.ITextCodec

`ITextCodec` 接口描述了一个基于文本的编/解码器，提供了最基本的编/解码接口

- `string Encode(byte[] data, CodecOptions? options = null)`
- `string Encode(byte[] data, int start, int count, CodecOptions? options = null)`
- `byte[] Decode(string code, CodecOptions? options = null)`

以及创建编码器和解码器的接口

- `ITextEncoder CreateEncoder(CodecOptions? options = null)`
- `ITextDecoder CreateDecoder(CodecOptions? options = null)`

#### 3.5.2. Viyi.Strings.Codec.Abstract.ITextEncoder

```csharp
public interface ITextEncoder
{
    string Encode(byte[] data);
    string Encode(byte[] data, int start, int count);
    void Encode(TextWriter output, Stream input);
}
```

#### 3.5.3. Viyi.Strings.Codec.Abstract.ITextDecoder

```csharp
public interface ITextDecoder
{
    byte[] Decode(string codes);
    void Decode(Stream output, TextReader input);
}
```

#### 3.5.4. Viyi.Strings.Codec.TextCodec 注册中心

`Viyi.Strings.Codec.TextCodec` 注册中心提供了 `Register()`、`Unregister()` 等方法用于注册 `ITextCodec` 工厂函数 (`Func<ITextCodec>`)。

- `void Register(string name, Func<ITextCodec> factory)`
- `void Unregister(string name)`
- `bool IsRegistered(string name)`
- `IEnumerable<string> GetRegistered()`，获取所有已注册编/解码器的名称
- `ITextCodec? CreateOrNull(string name)`，`name` 无效时返回 `null`
- `ITextCodec Create(string name)`，`name` 无效时抛出 [`ArgumentNullException`][argumentnullexception]或 [`NotSupportedException`](notsupportedexception)。

注册中心不区分名称的大小写。若对同一个名称（不区分大小写）注册多个 `ITextCodec`，最后一个覆盖掉之前的注册，成为该名称在册的唯一编/解码器。

#### 3.5.5. 通用编/解码扩展方法

通用编/解码扩展方法会根据名称在注册中心查找对应名称的编/解码器 (`ITextCodec`)，创建其实例来进行编/解码。如果没找到注册的 `ITextCodec`，会尝试通过 [`System.Text.Encoding.GetEncoding(name)`](https://docs.microsoft.com/dotnet/api/system.text.encoding.getencoding) 来获取基于 Code Page 的 `Encoding` 对象来进行编/解码。如果仍然未找到，则抛出 [`ArgumentException`][argumentexception] 或 [`NotSupportedException`][notsupportedexception]

- `string Encode(this byte[] bytes, string encoding)`
- `byte[] Decode(this string? str, string encoding)`

#### 3.5.6. 快捷编/解码扩展方法

除了通用编/解码方法，以及预置的 Base64 和 Hex 扩展方法外，也对系统编/解码提供了快捷扩展方法。目前仅支持 UTF-8。

- `string EncodeUtf8(this byte[] bytes)`
- `byte[] DecodeUtf8(this string? str)`

## 4. 支持和贡献

如果您有新的想法或者发现 BUG，请在 [Issue](https://gitee.com/jamesfancy/viyi-strings/issues) 系统中提出来，经过讨论之后确定是否添加/修改，以及如何进行。

如果您愿意贡献代码，请 Fork 本库，从 `develop` 分支创建功能/BUG/任务分支，根据 Issue 修改之后，向 `develop` 分支提起 PR。

非常感谢各位的支持和贡献！



[viyi_util]: https://www.nuget.org/packages/Viyi.Util/	"Viyi.Util (deprecated)"
[argumentnullexception]: https://docs.microsoft.com/dotnet/api/system.argumentnullexception "ArgumentNullException"
[argumentexception]: https://docs.microsoft.com/dotnet/api/system.argumentexception	" ArgumentException"
[notsupportedexception]: https://docs.microsoft.com/dotnet/api/system.notsupportedexception	"NotSupportedException"
[vs_nuget]: https://docs.microsoft.com/zh-cn/nuget/consume-packages/install-use-packages-visual-studio

