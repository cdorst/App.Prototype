using System.Collections.Generic;

namespace ConsoleApp6.Templates.CodeGenDeclarations
{
    internal static class AgentNounsHelper
    {
        private static readonly HashSet<string> AddTer = new HashSet<string>
        {
            "Get",
            "Set"
        };

        public static string GetAgentNoun(this string verb)
        {
            if (AddTer.Contains(verb)) return $"{verb}ter";
            var or = $"{verb}or";
            return AgentNounDictionary.AgentNounsEndingInOr.Contains(or)
                ? or
                : $"{verb}er";
        }
    }
}
