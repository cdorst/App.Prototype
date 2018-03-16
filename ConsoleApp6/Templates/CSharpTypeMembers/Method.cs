using System.Collections.Generic;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Method
    {
        public List<Attribute> Attributes { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Type { get; set; }
    }
}
