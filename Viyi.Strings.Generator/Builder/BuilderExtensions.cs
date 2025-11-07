namespace Viyi.Strings.Build.Builder;

static class BuilderExtensions {
    public static string GetTargetRoot(this IBuilder _) {
        const string targetName = "Viyi.Strings";
        var startFolder = AppDomain.CurrentDomain.BaseDirectory;
        return Enumerable.Empty<Func<string, string>>()
            .Append(tryGetFromCurrent)
            .Append(tryGetByRelative)
            .Append(tryFind)
            .Select(fn => Path.GetFullPath(fn(startFolder)))
            .FirstOrDefault(path => path != null && Directory.Exists(path))
            ?? throw new DirectoryNotFoundException("Not found target directory.");

        static string tryGetFromCurrent(string startFolder) {
            return Path.Combine(startFolder, targetName);
        }

        static string tryGetByRelative(string startFolder) {
            return Path.Combine(startFolder, "../../../..", targetName);
        }

        static string tryFind(string startFolder) {
            const string projName = "Viyi.Strings.Build";
            var path = Path.GetFullPath(startFolder);
            var root = Path.GetPathRoot(path);
            var projPath = getPathPart().FirstOrDefault(path => Path.GetFileName(path) == projName);
            if (projPath == null) { return "/"; }

            return Path.Combine(projPath, "..", targetName);

            IEnumerable<string?> getPathPart() {
                string? last = null;
                while (path != root && path != last) {
                    yield return path;
                    last = path;
                    path = Path.GetDirectoryName(path);
                }
            }
        }
    }
}
