using System.Collections.Generic;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Property
    {
        public Property() { }
        public Property(string name, string type, string comment = null, string modifiers = null, List<Attribute> attributes = null)
        {
            Attributes = attributes;
            Comment = comment;
            Modifiers = modifiers;
            Name = name;
            Type = type;
        }

        public List<Attribute> Attributes { get; set; }
        public string Comment { get; set; }
        public string Modifiers { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
