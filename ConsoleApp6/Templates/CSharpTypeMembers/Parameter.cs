using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Parameter
    {
        public Parameter() { }
        public Parameter(string name, string type, string defaultValue = null, params Attribute[] attributes)
        {
            Attributes = attributes?.ToList();
            DefaultValue = defaultValue;
            Name = name;
            Type = type;
        }

        public List<Attribute> Attributes { get; set; }
        public string DefaultValue { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
