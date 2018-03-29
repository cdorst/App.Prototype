using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using System.Collections.Generic;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public static class EntityApiControllerBuilder
    {
        private const string ApiController = nameof(ApiController);
        private const string ControllerBase = nameof(ControllerBase);
        private const string Route = nameof(Route);
        private const string RouteAttributeArguments = "(\"api/[controller]\")";

        private static readonly HashSet<string> IntKeyTypes = new HashSet<string>
        {
            TypeConstants.Byte,
            TypeConstants.Int,
            TypeConstants.Long,
            TypeConstants.Short
        };

        public static Class Build(string entityNamespace, string entityTypeName, string version, string keyType, string tableName, bool @static)
        {
            var typeName = $"{tableName}Controller";
            return new Class($"{entityNamespace}.ApiController", typeName, $"ASP.NET Core web API controller for {entityTypeName} entities", version, sameAccountDependencies: GetSameAccountDependencies(entityNamespace), attributes: GetAttributes(), bases: GetBase(), constructors: GetConstructor(typeName, entityTypeName, keyType), fields: GetFields(typeName, entityTypeName, keyType), methods: GetMethods(typeName, entityTypeName, keyType, @static), usingDirectives: GetUsings(entityNamespace));
        }

        private static List<Attribute> GetAttributes()
            => new List<Attribute>
            {
                new Attribute(ApiController),
                new Attribute(Route, RouteAttributeArguments)
            };

        private static List<Base> GetBase()
            => new List<Base>
            {
                new Base(ControllerBase)
            };

        private static List<Constructor> GetConstructor(string typeName, string entityTypeName, string keyType)
            => new List<Constructor>
            {
                new Constructor
                {
                    Modifiers = "public",
                    Comment = $"Constructs an API controller for {entityTypeName} entities using the given repository service",
                    Parameters = new List<Parameter>
                    {
                        new Parameter("logger", $"ILogger<{typeName}>"),
                        new Parameter("repository", $"IRepository<{entityTypeName}DbContext, {entityTypeName}, {keyType}>")
                    },
                    Block = new List<string>
                    {
                        "_logger = logger ?? throw new ArgumentNullException(nameof(logger));",
                        "_repository = repository ?? throw new ArgumentNullException(nameof(repository));"
                    }
                }
            };

        private static List<Field> GetFields(string typeName, string entityTypeName, string keyType)
            => new List<Field>
            {
                new Field
                {
                    Comment = "Represents the application events logger",
                    Modifiers = "private readonly",
                    Name = "_logger",
                    Type = $"ILogger<{typeName}>"
                },
                new Field
                {
                    Comment = $"Represents repository of {entityTypeName} entity data",
                    Modifiers = "private readonly",
                    Name = "_repository",
                    Type = $"IRepository<{entityTypeName}DbContext, {entityTypeName}, {keyType}>"
                }
            };

        private static List<Method> GetMethods(string typeName, string entityTypeName, string keyType, bool @static)
        {
            var methods = new List<Method>
            {
                // HEAD
                new Method
                {
                    ArrowClauseExpression = "null",
                    Attributes = new List<Attribute>
                    {
                        new Attribute("HttpHead", "(\"{id}\")")
                    },
                    Comment = $"Handles HTTP HEAD requests to access {entityTypeName} resources at the given ID",
                    Modifiers = "public",
                    Name = "Head",
                    Parameters = new List<Parameter>
                    {
                        new Parameter("id", keyType)
                    },
                    Type = $"ActionResult<{entityTypeName}>"
                },
                // GET
                new Method
                {
                    Attributes = new List<Attribute>
                    {
                        new Attribute("HttpGet", "(\"{id}\")")
                    },
                    Block = new List<string>
                    {
                        IntKeyTypes.Contains(keyType) ? "if (id < 1) return NotFound();" : string.Empty,
                        "var resource = await _repository.FindAsync(id);",
                        "if (resource == null) return NotFound();",
                        "return resource;"
                    },
                    Comment = $"Handles HTTP GET requests to access {entityTypeName} resources at the given ID",
                    Modifiers = "public async",
                    Name = "Get",
                    Parameters = new List<Parameter>
                    {
                        new Parameter("id", keyType)
                    },
                    Type = $"Task<ActionResult<{entityTypeName}>>"
                },
                // POST
                new Method
                {
                    Attributes = new List<Attribute>
                    {
                        new Attribute("HttpPost"),
                        new Attribute("ProducesResponseType", "(201)")
                    },
                    Block = new List<string>
                    {
                        "var saved = await _repository.AddAsync(resource);",
                        "return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);"
                    },
                    Comment = $"Handles HTTP POST requests to save {entityTypeName} resources",
                    Modifiers = "public async",
                    Name = "Post",
                    Parameters = new List<Parameter>
                    {
                        new Parameter("resource", entityTypeName)
                    },
                    Type = $"Task<ActionResult<{entityTypeName}>>"
                },
                // GET (search) TODO:
            };
            if (!@static)
            {
                methods.AddRange(new Method[]
                {
                    // DELETE
                    new Method
                    {
                        Attributes = new List<Attribute>
                        {
                            new Attribute("HttpDelete", "(\"{id}\")")
                        },
                        Block = new List<string>
                        {
                            "await _repository.RemoveAsync(id);",
                            "return Ok();"
                        },
                        Comment = $"Handles HTTP DELETE requests to remove {entityTypeName} resources at the given ID",
                        Modifiers = "public async",
                        Name = "Delete",
                        Parameters = new List<Parameter>
                        {
                            new Parameter("id", keyType)
                        },
                        Type = "Task"
                    },
                    // PATCH
                    new Method
                    {
                        Attributes = new List<Attribute>
                        {
                            new Attribute("HttpPatch", "(\"{id}\")")
                        },
                        Block = new List<string>
                        {
                            IntKeyTypes.Contains(keyType) ? "if (id < 1) return NotFound();" : string.Empty,
                            "var resource = await _repository.FindAsync(id);",
                            "if (resource == null) return NotFound();",
                            "patch.ApplyTo(resource, ModelState);",
                            "if (!ModelState.IsValid) return BadRequest(ModelState);",
                            "return await _repository.UpdateAsync(resource);"
                        },
                        Comment = $"Handles HTTP PATCH requests to modify {entityTypeName} resources at the given ID",
                        Modifiers = "public async",
                        Name = "Patch",
                        Parameters = new List<Parameter>
                        {
                            new Parameter("id", keyType),
                            new Parameter("patch", $"JsonPatchDocument<{entityTypeName}>")
                        },
                        Type = $"Task<ActionResult<{entityTypeName}>>"
                    },
                    // PUT
                    new Method
                    {
                        ArrowClauseExpression = "entity.GetKey() == 0 ? await _repository.AddAsync(resource) : await _repository.UpdateAsync(resource)",
                        Attributes = new List<Attribute>
                        {
                            new Attribute("HttpPut")
                        },
                        Comment = $"Handles HTTP PUT requests to add or update {entityTypeName} resources",
                        Modifiers = "public async",
                        Name = "Put",
                        Parameters = new List<Parameter>
                        {
                            new Parameter("resource", entityTypeName)
                        },
                        Type = $"Task<ActionResult<{entityTypeName}>>"
                    },
                });
            }
            return methods;
        }

        private static IEnumerable<string> GetSameAccountDependencies(string entityNamespace)
        {
            yield return EntityDbContextBuilder.GetDbContextNamespace(entityNamespace);
            yield return EntityNamespaceConstants.DevOpsCodeDataAccessMetapackagesApiControllers;
        }

        private static List<string> GetUsings(string entityNamespace)
            => new List<string>
            {
                EntityDbContextBuilder.GetDbContextNamespace(entityNamespace),
                EntityNamespaceConstants.DevOpsCodeDataAccessInterfacesRepository,
                EntityNamespaceConstants.MicrosoftAspNetCoreMvc,
                EntityNamespaceConstants.MicrosoftExtensionsLogging,
                EntityNamespaceConstants.SystemThreadingTasks
            };
    }
}
