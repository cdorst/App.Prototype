namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public class EntityProperty
    {
        public string ForeignKeyType { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Type of value-type or foreign-key target entity
        /// </summary>
        public string Type { get; set; }
        public string TypeNamespace { get; set; }
    }
}
