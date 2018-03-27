namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    internal static class ToCamelCaseStringExtension
    {
        public static string ToCamelCase(this string instance)
            => $"{instance[0].ToString().ToLower()}{instance.Substring(1)}";
    }
}
