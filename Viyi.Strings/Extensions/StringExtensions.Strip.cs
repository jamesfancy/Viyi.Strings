namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    extension(string text) {
        public string StripIndent(int indent = 0, bool toSpace = false, int tabSize = 4) {
            tabSize = tabSize < 1 ? 4 : tabSize;

            string[] lines = text.Split('\n');

            if (toSpace) {
                var newLines = preDealWithConverting(lines, out int stripCount);
                if (stripCount == 0) {
                    return newLines.JoinString('\n');
                }
                else {
                    return newLines.Select(line => line.Slice(stripCount)).JoinString('\n');
                }
            }
            else {
                int stripCount = calcMinLoading(lines);
                if (stripCount == 0) {
                    return lines.JoinString('\n');
                }
                return lines.Select(line => line.Slice(stripCount)).JoinString('\n');
            }


            //////// local functions ///////////////////////////////////////////////
            int calcMinLoading(IEnumerable<string> lines) {
                return lines.Select(findNonSpaceIndex).DefaultIfEmpty().Min();
            }

            int findNonSpaceIndex(string s) {
                return s.Select((ch, i) => ch == ' ' || ch == '\t' ? i : -1)
                        .FirstOrDefault(i => i != -1);
            }

            List<string> preDealWithConverting(IEnumerable<string> lines, out int minLeading) {
                List<string> result = [];
                int minSpaces = int.MaxValue;
                foreach (var line in lines) {
                    var spaces = calcIndent(line);
                    minSpaces = Math.Min(minSpaces, spaces);
                    result.Add(' '.Repeat(spaces) + line.TrimStart());
                }
                minLeading = minSpaces;
                return result;
            }

            int calcIndent(string line) {
                int indent = 0;
                foreach (char ch in line) {
                    switch (ch) {
                        case ' ':
                            indent++;
                            break;
                        case '\t':
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
