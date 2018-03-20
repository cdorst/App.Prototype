namespace ConsoleApp6
{
    public static class CommitMessageWriter
    {
        private const string Add = nameof(Add);
        private const string Update = nameof(Update);

        public static string CommitMessage(bool update, string fileName)
            => $"{GetPrefix(update)} {fileName}";

        private static string GetPrefix(bool update)
            => update ? Update : Add;
    }
}
