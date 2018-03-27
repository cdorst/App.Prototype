namespace ConsoleApp6.Templates.CSharpTypeMembers
{
    public class Attribute
    {
        public Attribute() { }
        public Attribute(string name, string expression = null)
        {
            ArgumentListExpression = expression;
            Name = name;
        }

        public string ArgumentListExpression { get; set; }
        public string Name { get; set; }
    }
}
