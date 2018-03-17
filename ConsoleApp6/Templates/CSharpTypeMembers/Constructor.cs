using System.Collections.Generic;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Constructor
    {
        public List<Attribute> Attributes { get; set; }
        public List<string> Block { get; set; }
        public string Comment { get; set; }
        public ConstructorBaseInitializer ConstructorBaseInitializer { get; set; }
        public string Modifiers { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
