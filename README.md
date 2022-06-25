# Viyi.Strings

## 1. 关于 (About)

Viyi.Strings 是从 [Viyi.Util][viyi_util] 中分离出来的，专注于处理字符串相关内容的一个库。目前主要提供对二进制数据进行十六进制编/解码和 Base64 编/解码。将来会提供更多的字符串工具。

> ###### English [en]
>
> Viyi.Strings is a library separated from [Viyi.Util][viyi_util], which focuses on processing strings. At present, Viyi.Strings suplies Base64 and Hex encoding/decoding by extension methods, as  well as a text encoding/decoding framework. More string tools will be avaliable in the future.

Viyi.Strings 使用木兰宽松许可证第 2 版（Mulan Permissive Software License, Version 2）。

> 原 Viyi.Util 涉及的范围非常多，过于分散，而且其中部分工具已经有更好的替代品。所以没有对 Viyi.Util 直接进行升级，而是将其拆分：
>
> - Viyi.Strings 用于处理字符串 (Viyi.Strings is a string toolkit.)
> - Viyi.Bytes 用于处理二进制数据，计划中 (Viyi.Bytes is a binary data toolkit, in planning.)

## 2. 安装 (Install)

Viyi.Strings [发布在 NuGet 上][viyi_strings]，在 Visual Studio 中可以使用 [NuGet Package Manager][vs_nuget] 安装，也可以在 Powershell Manager 命令行安装：

```powershell
Install-Package Viyi.Strings
```

或通过 .NET CLI  安装：

```powershell
dotnet add package Viyi.Strings
```

或使用其他 NuGet 支持的方式进行安装。

## 3. 源代码 (Source Codes)

源代码托管在 [gitee.com](https://gitee.com/) 上：[传送门 (Follow this link)](https://gitee.com/jamesfancy/viyi-strings)

## 4. 主要功能 (Main Features)

- [x] [基于文本的编/解码](https://gitee.com/jamesfancy/viyi-strings/wikis/%E6%96%87%E6%9C%AC%E7%BC%96%E7%A0%81%E5%92%8C%E8%A7%A3%E7%A0%81%20(Viyi.Strings.Codec))
    - Base 64 编/解码：`EncodeBase64()`/`DecodeBase64()`
    
    - Base 16（十六进制）编/解码：`EncodeBase16()`/`DecodeBase16()`
    
        > 别名：`EncodeHex()`/`DecodeHex()`
    
    - Base 32 编/解码：`EncodeBase32()`/`DecodeBase32()`
    
    - Utf8 编/解码：`EncodeUtf8()`/`DecodeUtf8()`
    
        > 使用 `System.Text.Encoding.UTF8` 实现
    
    - 其他 Encoding 编/解码快捷调用扩展方法：`Encode()`/`Decode()`
    
    - `TextCodec` 管理各心及抽象化接口
    
- [x] [空字符串和空白字符串](https://gitee.com/jamesfancy/viyi-strings/wikis/%E7%A9%BA%E5%AD%97%E7%AC%A6%E4%B8%B2%E5%92%8C%E7%A9%BA%E7%99%BD%E5%AD%97%E7%AC%A6%E4%B8%B2)
    
    - `IsEmpty()` 和 `IsSpaces()` 灵活判断
    - `EmptyAs()` 和 `SpacesAs()` 灵活赋予默认值
    
- [x] [命名风格转换（大小规则写转换）](https://gitee.com/jamesfancy/viyi-strings/wikis/%E5%91%BD%E5%90%8D%E9%A3%8E%E6%A0%BC%E8%BD%AC%E6%8D%A2%20CaseConvert)
    - `CamelCase()`/`PascalCase()`/`KebabCase()`/`SnakeCase()` 扩展方法
    - `CaseTo(string)` 扩展方法按自定义规则转换
    - `CaseConvert` 注册中心和 `ICaseConverter` 接口
    
- [x] [在整数和字符串之间进行 2~36 进制转换](https://gitee.com/jamesfancy/viyi-strings/wikis/%E6%95%B4%E6%95%B0%E7%9A%84%E8%BF%9B%E5%88%B6%E8%BD%AC%E6%8D%A2)
    - `ToString(int radix)`
    - `ToInt32(int radix)`/`ToUInt32(int radix)`
    - `ToInt64(int radix)`/`ToUInt64(int radix)`
    
- [x] [解析为布尔值](https://gitee.com/jamesfancy/viyi-strings/wikis/%E8%A7%A3%E6%9E%90%E4%B8%BA%E5%B8%83%E5%B0%94%E7%B1%BB%E5%9E%8B%20(bool))，灵活支持 `on/off`、`yes/no` 及其他字符串内容
    - `ToBoolean()` 系列扩展方法
    - `CreatePredicator()` 工具方法创建断言函数

## 5. 支持和贡献

如果您有新的想法或者发现 BUG，请在 [Issue](https://gitee.com/jamesfancy/viyi-strings/issues) 系统中提出来，经过讨论之后确定是否添加/修改，以及如何进行。

如果您愿意贡献代码，请 Fork 本库，从 `develop` 分支创建功能/BUG/任务分支，根据 Issue 修改之后，向 `develop` 分支提起 PR。

非常感谢各位的支持和贡献！



[viyi_util]: https://www.nuget.org/packages/Viyi.Util/	"Viyi.Util"
[viyi_strings]: https://www.nuget.org/packages/Viyi.Strings/	"Viyi.Strings in NuGet"
[argumentnullexception]: https://docs.microsoft.com/dotnet/api/system.argumentnullexception "ArgumentNullException"
[argumentexception]: https://docs.microsoft.com/dotnet/api/system.argumentexception	" ArgumentException"
[notsupportedexception]: https://docs.microsoft.com/dotnet/api/system.notsupportedexception	"NotSupportedException"
[vs_nuget]: https://docs.microsoft.com/zh-cn/nuget/consume-packages/install-use-packages-visual-studio

