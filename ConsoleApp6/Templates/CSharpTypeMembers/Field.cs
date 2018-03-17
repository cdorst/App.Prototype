using System.Collections.Generic;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Field
    {
        public List<Attribute> Attributes { get; set; }
        public string Comment { get; set; }
        public string DefaultValue { get; set; }
        public string Modifiers { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
