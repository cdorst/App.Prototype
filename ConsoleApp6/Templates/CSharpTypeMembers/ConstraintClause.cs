using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class ConstraintClause
    {
        public ConstraintClause() { }
        public ConstraintClause(string name, params string[] constraints)
        {
            Name = name;
            Constraints = constraints?.ToList();
        }

        public string Name { get; set; }
        public List<string> Constraints { get; set; }
    }
}
