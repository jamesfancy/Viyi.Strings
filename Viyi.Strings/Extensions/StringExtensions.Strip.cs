namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    private const char Lf = '\n';
    private const char Ht = '\t';
    private const char Sp = ' ';

    extension(string text) {
        public string StripIndent(bool toSpaces = false, int tabSize = 4) {
            tabSize = tabSize < 1 ? 4 : tabSize;

            string[] lines = text.Split(Lf);

            (IEnumerable<string> newLines, int stripCount) = toSpaces
                ? preDealWithConverting(lines)
                : ((IEnumerable<string>) lines, calcMinLeading(lines));

            if (stripCount == 0) {
                return newLines.JoinString(Lf);
            }
            else {
                return newLines.Select(line => line.Slice(stripCount)).JoinString(Lf);
            }

            //////// local functions ///////////////////////////////////////////////
            int calcMinLeading(IEnumerable<string> lines) {
                return lines
                    .Where(line => line.Length > 0)
                    .Select(findNonSpaceIndex)
                    .DefaultIfEmpty()
                    .Min();
            }

            int findNonSpaceIndex(string s) {
                return s.Select((ch, i) => ch != Sp && ch != Ht ? i : -1)
                        .FirstOrDefault(i => i != -1);
            }

            (List<string>, int) preDealWithConverting(IEnumerable<string> lines) {
                List<string> result = [];
                int minSpaces = int.MaxValue;
                foreach (var line in lines) {
                    if (line.Length == 0) {
                        result.Add("");
                    }
                    else {
                        var spaces = calcIndent(line);
                        minSpaces = Math.Min(minSpaces, spaces);
                        result.Add(Sp.Repeat(spaces) + line.TrimStart());
                    }
                }
                return (result, minSpaces);
            }

            int calcIndent(string line) {
                int indent = 0;
                foreach (char ch in line) {
                    switch (ch) {
                        case Sp:
                            indent++;
                            break;
                        case Ht:
                            indent += tabSize;
                            indent -= indent % tabSize;
                            break;
                        default:
                            return indent;
                    }
                }
                return indent;
            }
        }
    }
}
