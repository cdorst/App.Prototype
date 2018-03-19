using System.Collections.Generic;

namespace ConsoleApp6.Templates.CodeGenDeclarations
{
    internal static class AgentNounsHelper
    {
        private static HashSet<string> Suffixes = new HashSet<string>
        {
            "er",
            "ar",
            "or",
            "ist"
        };

        public static string GetVerb(this string agentNoun)
        {
            var lastThree = agentNoun.Substring(agentNoun.Length - 3, 3);
            var lastTwo = lastThree.Substring(1);
            return
                Suffixes.Contains(lastThree) ? GetVerb(agentNoun, 3)
                : Suffixes.Contains(lastTwo) ? GetVerb(agentNoun, 2)
                : agentNoun;
        }

        private static string GetVerb(string agentNoun, byte suffixLength)
            => agentNoun.Substring(0, agentNoun.Length - suffixLength);
    }
}
