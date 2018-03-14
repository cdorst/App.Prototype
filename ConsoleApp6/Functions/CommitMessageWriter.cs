namespace ConsoleApp6
{
    public static class CommitMessageWriter
    {
        public static string CommitMessage(bool update, string fileName)
            => update
                ? $"Update {fileName}"
                : $"Add {fileName}";
    }
}
