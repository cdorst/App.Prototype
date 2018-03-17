using System.Collections.Generic;

namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Method
    {
        public string ArrowClauseExpression { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<string> Block { get; set; }
        public string Comment { get; set; }
        public string Modifiers { get; set; }
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Type { get; set; }
        public List<string> TypeParameters { get; set; }
    }

    public class Property
    {
        public List<Attribute> Attributes { get; set; }
        public string Comment { get; set; }
        public string Modifiers { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
