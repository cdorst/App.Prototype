using ConsoleApp6.Templates.CodeGenDeclarations;
using ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups;
using ConsoleApp6.Templates.CSharpTypeMembers;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.Implementation
{
    public static class CDorstGitHubAccount
    {
        private static readonly List<ICodeGeneratable> EmptyRepoList = new List<ICodeGeneratable>();

        public static List<ICodeGeneratable> GetRepositories()
            => (GetIndividualRepositories() ?? EmptyRepoList)
                .Concat(GetRepositoryGroups()?.SelectMany(group => group.GetRepositories()) ?? EmptyRepoList)
                .ToList();

        private static List<IRepositoryGroup> GetRepositoryGroups()
            => new List<IRepositoryGroup>
            {
                new Entity
                {
                    Name = "Addresses.States",
                    Description = "Contains US State component of addresses",
                    Version = "1.0.4",
                    EntityTypeId = 1,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "Abbreviation",
                            Type = "string"
                        },
                        new EntityProperty
                        {
                            Name = "Name",
                            Type = "string"
                        }
                    }
                },
                new Entity
                {
                    Name = "Addresses.ZipCodes",
                    DependsOn = "Addresses.States",
                    Description = "Contains US ZIP Plus4 component of addresses",
                    Version = "1.0.4",
                    EntityTypeId = 2,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "Zip",
                            Type = "string"
                        },
                        new EntityProperty
                        {
                            Name = "PlusFour",
                            Type = "string"
                        }
                    }
                },
                new Entity
                {
                    Name = "Addresses.StreetAddressLines",
                    DependsOn = "Addresses.ZipCodes",
                    Description = "Contains street address line information",
                    Version = "1.0.4",
                    EntityTypeId = 3,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "AddressLine",
                            Type = "string"
                        }
                    }
                },
                new Entity
                {
                    Name = "Addresses.StreetAddresses",
                    DependsOn = "Addresses.StreetAddressLines",
                    Description = "Contains street address line information",
                    Version = "1.0.4",
                    EntityTypeId = 7,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "AddressLine1",
                            Type = "StreetAddressLine",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.StreetAddressLines"
                        },
                        new EntityProperty
                        {
                            Name = "AddressLine2",
                            Type = "StreetAddressLine",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.StreetAddressLines"
                        },
                    }
                },
                new Entity
                {
                    Name = "Addresses.Cities",
                    DependsOn = "Addresses.StreetAddresses",
                    Description = "Contains US City component of addresses",
                    Version = "1.0.4",
                    EntityTypeId = 8,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "Name",
                            Type = "string"
                        }
                    }
                },
                new Entity
                {
                    Name = "Addresses.StateCities",
                    DependsOn = "Addresses.Cities",
                    Description = "Contains US State-City pair component of addresses",
                    Version = "1.0.4",
                    EntityTypeId = 9,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "State",
                            Type = "State",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.States"
                        },
                        new EntityProperty
                        {
                            Name = "City",
                            Type = "City",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.Cities"
                        },
                    }
                },
                new Entity
                {
                    Name = "Addresses.StateCityZips",
                    DependsOn = "Addresses.StateCities",
                    Description = "Contains US State-City-ZIP group component of addresses",
                    Version = "1.0.4",
                    EntityTypeId = 10,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "StateAndCity",
                            Type = "StateCity",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.StateCities"
                        },
                        new EntityProperty
                        {
                            Name = "ZipPlusFour",
                            Type = "ZipCode",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.ZipCodes"
                        },
                    }
                },
                new Entity
                {
                    Name = "Addresses.Addresses",
                    DependsOn = "Addresses.StateCityZips",
                    Description = "Contains US addresses",
                    Version = "1.0.4",
                    EntityTypeId = 11,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "StateCityZip",
                            Type = "StateCityZip",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.StateCityZips"
                        },
                        new EntityProperty
                        {
                            Name = "StreetAddress",
                            Type = "StreetAddress",
                            ForeignKeyType = "int",
                            TypeNamespace = "Addresses.StreetAddresses"
                        },
                    }
                },
                new Entity
                {
                    Name = "Entities.FooBars",
                    Description = "Contains the FooBar entity type",
                    Version = "1.0.21",
                    EntityTypeId = 5,
                    Editable = true,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "Baz",
                            Type = "int"
                        }
                    }
                },
                new Entity
                {
                    Name = "Entities.StaticFooBars",
                    DependsOn = "Entities.FooBars",
                    Description = "Contains the StaticFooBar entity type",
                    Version = "1.0.19",
                    EntityTypeId = 4,
                    Properties = new List<EntityProperty>
                    {
                        new EntityProperty
                        {
                            Name = "Baz",
                            Type = "int"
                        },
                        new EntityProperty
                        {
                            Name = "Qux",
                            Type = "DateTimeOffset",
                            TypeNamespace = "System"
                        },
                        new EntityProperty
                        {
                            Name = "Thing",
                            Type = "string"
                        },
                        new EntityProperty
                        {
                            Name = "OtherThing",
                            Type = "string"
                        }
                    }
                }
            };

        private static List<ICodeGeneratable> GetIndividualRepositories()
            => new List<ICodeGeneratable>
            {
                new Metapackage(
                    "DevOps.Code.DataAccess.Metapackages.ApiControllers",
                    "Metapackage for entity API controllers",
                    "1.0.1", null,
                    new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "Microsoft.AspNetCore.Mvc.Core",
                            Version = "2.1.0-preview1-final"
                        },
                        new PackageReference
                        {
                            Name = "Microsoft.AspNetCore.JsonPatch",
                            Version = "2.1.0-preview1-final"
                        },
                        new PackageReference
                        {
                            Name = "Microsoft.Extensions.Logging.Abstractions",
                            Version = "2.1.0-preview1-final"
                        }
                    },
                    "DevOps.Code.DataAccess.Interfaces.Repository"),
                new Metapackage(
                    "Azure.Storage.Metapackage",
                    "Metapackage for Microsoft Azure Storage dependencies",
                    "1.0.3", null,
                    new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "WindowsAzure.Storage",
                            Version = "9.1.0"
                        }
                    }),
                new Metapackage(
                    "DevOps.Code.Entities.Metapackages.Annotations",
                    "Metapackage for entity data-annotation attributes",
                    "1.0.1", null,
                    new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "protobuf-net",
                            Version = "2.3.7"
                        },
                        new PackageReference
                        {
                            Name = "System.ComponentModel.Annotations",
                            Version = "4.4.1"
                        }
                    }),
                new Metapackage(
                    "DevOps.Code.Entities.Metapackages.EntityFrameworkCore",
                    "Metapackage for EntityFrameworkCore dependencies",
                    "1.0.4", null,
                    new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "Microsoft.EntityFrameworkCore",
                            Version = "2.1.0-preview1-final"
                        }
                    }),
                new Metapackage(
                    "DevOps.Code.Entities.Metapackages.AnnotatedEntityFramework",
                    "Metapackage for EntityFrameworkCore and entity annotations dependencies",
                    "1.0.0", null, null,
                    "DevOps.Code.Entities.Metapackages.Annotations",
                    "DevOps.Code.Entities.Metapackages.EntityFrameworkCore"),
                new Interface(
                    "DevOps.Code.DataAccess.Interfaces.CacheService",
                    "Interface for a generic data-access cache",
                    "1.0.1", typeParameters: new List<string> { "TEntity" },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TEntity", "class")
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Comment = "Returns the entity given a key value",
                            Name = "FindAsync",
                            Type = "Task<TEntity>",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "string")
                            }
                        },
                        new Method
                        {
                            Comment = "Removes the entity at the given a key value from the cache",
                            Name = "RemoveAsync",
                            Type = "Task",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "string")
                            }
                        },
                        new Method
                        {
                            Comment = "Saves the entity to the cache",
                            Name = "SaveAsync",
                            Type = "Task",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "string"),
                                new Parameter("entity", "TEntity")
                            }
                        },
                    },
                    usingDirectives: new List<string>
                    {
                        "System.Threading.Tasks"
                    }),
                new Class(
                    "DevOps.Code.DataAccess.Options.CacheExpiration",
                    "CacheSlidingExpiration",
                    "Contains properties indicating how long objects should be held in cache",
                    "1.0.0",
                    properties: new List<Property>
                    {
                        new Property("Days", "int?", "Indicates how many days to cache entities", "public"),
                        new Property("Hours", "int?", "Indicates how many hours to cache entities", "public"),
                        new Property("Minutes", "int?", "Indicates how many minutes to cache entities", "public"),
                        new Property("Seconds", "int?", "Indicates how many seconds to cache entities", "public")
                    }),
                new Class(
                    "DevOps.Code.DataAccess.Services.CacheService",
                    "CacheService",
                    "Generic data-access caching service for EntityFrameworkCore entities",
                    "1.0.4",
                    sameAccountDependencies: new[]
                    {
                        "DevOps.Code.Entities.Metapackages.AnnotatedEntityFramework",
                        "DevOps.Code.DataAccess.Interfaces.CacheService",
                        "DevOps.Code.DataAccess.Options.CacheExpiration"
                    },
                    typeParameters: new List<string> { "TEntity" },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TEntity", "class")
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            Comment = "Reference to a distributed cache (Redis)",
                            Modifiers = "private readonly",
                            Name = "_cache",
                            Type = "IDistributedCache"
                        },
                        new Field
                        {
                            Comment = "Options for the distributed cache (expiry, etc.)",
                            Modifiers = "private readonly",
                            Name = "_expiration",
                            Type = "DistributedCacheEntryOptions"
                        },
                        new Field
                        {
                            Comment = "Logger",
                            Modifiers = "private readonly",
                            Name = "_logger",
                            Type = "ILogger<CacheService<TEntity>>"
                        },
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor
                        {
                            Comment = "Constructs an instance of the cache service",
                            Block = new List<string>
                            {
                                "_cache = cache ?? throw new ArgumentNullException(nameof(cache));",
                                "_logger = logger ?? throw new ArgumentNullException(nameof(logger));",
                                "var expiration = options?.Value ?? new CacheSlidingExpiration();",
                                @"_expiration = new DistributedCacheEntryOptions { SlidingExpiration = new TimeSpan(expiration.Days ?? 0, expiration.Hours ?? 0, expiration.Minutes ?? 0, expiration.Seconds ?? 0)  };"
                            },
                            Parameters = new List<Parameter>
                            {
                                new Parameter("cache", "IDistributedCache"),
                                new Parameter("logger", "ILogger<CacheService<TEntity>>"),
                                new Parameter("options", "IOptions<CacheSlidingExpiration>")
                            },
                            Modifiers = "public"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Comment = "Returns the entity given a key value",
                            Name = "FindAsync",
                            Type = "Task<TEntity>",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "string")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation($\"Find entry: {key}\");",
                                "var cacheEntry = await _cache.GetAsync(key);",
                                "if (cacheEntry != null) return Deserialize(cacheEntry);",
                                "_logger.LogInformation(\"Cache miss\");",
                                "return null;"
                            },
                            Modifiers = "public async"
                        },
                        new Method
                        {
                            Comment = "De-serializes the cached entity",
                            Name = "Deserialize",
                            Type = "TEntity",
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"Cache hit\");",
                                "return Serializer.Deserialize<TEntity>(new MemoryStream(cacheEntry));",
                            },
                            Modifiers = "private",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("cacheEntry", "byte[]")
                            }
                        },
                        new Method
                        {
                            Comment = "Removes the entity at the given a key value from the cache",
                            Name = "RemoveAsync",
                            Type = "Task",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "string")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation($\"Removing entry: {key}\");",
                                "await _cache.RemoveAsync(key);"
                            },
                            Modifiers = "public async"
                        },
                        new Method
                        {
                            Comment = "Saves the entity to the cache",
                            Name = "SaveAsync",
                            Type = "Task",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "string"),
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation($\"Setting entry: {key}\");",
                                "var value = Serialize(entity);",
                                "await _cache.SetAsync(key, value, _expiration);"
                            },
                            Modifiers = "public async"
                        },
                        new Method
                        {
                            Comment = "Serializes the entity",
                            Name = "Serialize",
                            Type = "byte[]",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"ProtoBuf serializing record\");",
                                "using (var stream = new MemoryStream()) { Serializer.Serialize(stream, entity); return stream.ToArray(); }"
                            },
                            Modifiers = "private"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.DataAccess.Options.CacheExpiration",
                        "Microsoft.Extensions.Caching.Distributed",
                        "Microsoft.Extensions.Logging",
                        "Microsoft.Extensions.Options",
                        "ProtoBuf",
                        "System",
                        "System.IO",
                        "System.Threading.Tasks"
                    }),
                new Interface(
                    "DevOps.Code.DataAccess.Interfaces.Repository",
                    "Represents a generic data-access repository",
                    "1.0.1",
                    sameAccountDependencies: new[]
                    {
                        "DevOps.Code.Entities.Interfaces.Entity",
                        "DevOps.Code.Entities.Metapackages.EntityFrameworkCore"
                    },
                    typeParameters: new List<string>
                    {
                        "TDbContext",
                        "TEntity",
                        "TKey"
                    },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TDbContext", "DbContext"),
                        new ConstraintClause("TEntity", "class", "IEntity<TKey>"),
                        new ConstraintClause("TKey", "struct")
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Name = "AddAsync",
                            Type = "Task<TEntity>",
                            Comment = "Adds the entity to the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            }
                        },
                        new Method
                        {
                            Name = "FindAsync",
                            Type = "Task<TEntity>",
                            Comment = "Finds an entity with the given key",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "TKey")
                            }
                        },
                        new Method
                        {
                            Name = "RemoveAsync",
                            Type = "Task",
                            Comment = "Removes the entity from the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "TKey")
                            }
                        },
                        new Method
                        {
                            Name = "UpdateAsync",
                            Type = "Task<TEntity>",
                            Comment = "Replaces an entity in the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            }
                        },
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.Interfaces.Entity",
                        "Microsoft.EntityFrameworkCore",
                        "System.Threading.Tasks"
                    }),
                new Class(
                    "DevOps.Code.DataAccess.Services.DataRepository",
                    "Repository",
                    "Represents a generic data-access repository",
                    "1.0.2",
                    sameAccountDependencies: new[]
                    {
                        "DevOps.Code.DataAccess.Interfaces.Repository"
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.DataAccess.Interfaces.Repository",
                        "DevOps.Code.Entities.Interfaces.Entity",
                        "Microsoft.EntityFrameworkCore",
                        "Microsoft.Extensions.Logging",
                        "System",
                        "System.Threading.Tasks"
                    },
                    typeParameters: new List<string>
                    {
                        "TDbContext",
                        "TEntity",
                        "TKey"
                    },
                    bases: new List<Base>
                    {
                        new Base("IRepository", "TDbContext", "TEntity", "TKey")
                    },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TDbContext", "DbContext"),
                        new ConstraintClause("TEntity", "class", "IEntity<TKey>"),
                        new ConstraintClause("TKey", "struct")
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            Comment = "Represents the EntityFrameworkCore database context",
                            Name = "_context",
                            Type = "TDbContext",
                            Modifiers = "private readonly"
                        },
                        new Field
                        {
                            Comment = "Represents the EntityFrameworkCore DbSet (database table)",
                            Name = "_set",
                            Type = "DbSet<TEntity>",
                            Modifiers = "private readonly"
                        },
                        new Field
                        {
                            Comment = "Logger",
                            Name = "_logger",
                            Type = "ILogger<Repository<TDbContext, TEntity, TKey>>",
                            Modifiers = "protected readonly"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor
                        {
                            Comment = "Constructs a repository instance using the given database context",
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("context", "TDbContext"),
                                new Parameter("logger", "ILogger<Repository<TDbContext, TEntity, TKey>>")
                            },
                            Block = new List<string>
                            {
                                "_context = context ?? throw new ArgumentNullException(nameof(context));",
                                "_logger = logger ?? throw new ArgumentNullException(nameof(logger));",
                                "_set = _context.Set<TEntity>();"
                            }
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Modifiers = "public virtual async",
                            Name = "AddAsync",
                            Type = "Task<TEntity>",
                            Comment = "Adds the entity to the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "if (entity == null) return null;",
                                "_logger.LogInformation(\"Adding entity to db context\");",
                                "_set.Add(entity);",
                                "await _context.SaveChangesAsync();",
                                "return entity;"
                            }
                        },
                        new Method
                        {
                            Modifiers = "public virtual async",
                            Name = "FindAsync",
                            Type = "Task<TEntity>",
                            Comment = "Finds an entity with the given key",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "TKey")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"Finding entity\");",
                                "return await _set.FindAsync(key);"
                            }
                        },
                        new Method
                        {
                            Modifiers = "public virtual async",
                            Name = "RemoveAsync",
                            Type = "Task",
                            Comment = "Removes the entity from the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "TKey")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"Removing entity\");",
                                "var entity = await FindAsync(key);",
                                "if (entity == null) return;",
                                "_set.Remove(entity);",
                                "await _context.SaveChangesAsync();"
                            }
                        },
                        new Method
                        {
                            Modifiers = "public virtual async",
                            Name = "UpdateAsync",
                            Type = "Task<TEntity>",
                            Comment = "Replaces an entity in the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "if (entity == null) return null;",
                                "_logger.LogInformation(\"Updating entity\");",
                                "_context.Entry(entity).State = EntityState.Modified;",
                                "await _context.SaveChangesAsync();",
                                "return entity;"
                            }
                        }
                    }),
                new Class(
                    "DevOps.Code.DataAccess.Services.CachedDataRepository",
                    "CachedRepository",
                    "Represents a generic data-access repository with caching",
                    "1.0.6",
                    sameAccountDependencies: new[]
                    {
                        "DevOps.Code.DataAccess.Interfaces.CacheService",
                        "DevOps.Code.DataAccess.Services.DataRepository"
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.DataAccess.Interfaces.CacheService",
                        "DevOps.Code.DataAccess.Services.DataRepository",
                        "DevOps.Code.Entities.Interfaces.Entity",
                        "Microsoft.EntityFrameworkCore",
                        "Microsoft.Extensions.Logging",
                        "System",
                        "System.Threading.Tasks"
                    },
                    typeParameters: new List<string>
                    {
                        "TDbContext",
                        "TEntity",
                        "TKey"
                    },
                    bases: new List<Base>
                    {
                        new Base("Repository", "TDbContext", "TEntity", "TKey")
                    },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TDbContext", "DbContext"),
                        new ConstraintClause("TEntity", "class", "IEntity<TKey>"),
                        new ConstraintClause("TKey", "struct")
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            Comment = "Represents a data-repository cache",
                            Name = "_cache",
                            Type = "ICacheService<TEntity>",
                            Modifiers = "private readonly"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor
                        {
                            Comment = "Constructs a repository instance using the given cache and database context",
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("cache", "ICacheService<TEntity>"),
                                new Parameter("context", "TDbContext"),
                                new Parameter("logger", "ILogger<Repository<TDbContext, TEntity, TKey>>")
                            },
                            ConstructorBaseInitializer = new ConstructorBaseInitializer
                            {
                                Arguments = new List<string>
                                {
                                    "context",
                                    "logger"
                                }
                            },
                            Block = new List<string>
                            {
                                "_cache = cache ?? throw new ArgumentNullException(nameof(cache));"
                            }
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Modifiers = "public override async",
                            Name = "AddAsync",
                            Type = "Task<TEntity>",
                            Comment = "Adds the entity to the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "if (entity == null) return null;",
                                "_logger.LogInformation(\"Adding entity to database\");",
                                "entity = await base.AddAsync(entity);",
                                "await SaveCacheEntry(entity);",
                                "return entity;"
                            }
                        },
                        new Method
                        {
                            Modifiers = "public override async",
                            Name = "FindAsync",
                            Type = "Task<TEntity>",
                            Comment = "Finds an entity with the given key",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "TKey")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"Finding entity in cache\");",
                                "var cached = await _cache.FindAsync(key.ToString());",
                                "if (cached != null) return cached;",
                                "_logger.LogInformation(\"Finding record in database\");",
                                "var entity = await base.FindAsync(key);",
                                "if (entity != null) await SaveCacheEntry(entity);",
                                "return entity;"
                            }
                        },
                        new Method
                        {
                            Modifiers = "public override async",
                            Name = "RemoveAsync",
                            Type = "Task",
                            Comment = "Removes the entity from the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("key", "TKey")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"Removing entity from cache\");",
                                "await _cache.RemoveAsync($\"{typeof(TEntity).FullName}:{key.ToString()}\");",
                                "_logger.LogInformation(\"Removing record from database\");",
                                "await base.RemoveAsync(key);"
                            }
                        },
                        new Method
                        {
                            Modifiers = "public override async",
                            Name = "UpdateAsync",
                            Type = "Task<TEntity>",
                            Comment = "Replaces an entity in the data repository",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "if (entity == null) return null;",
                                "_logger.LogInformation(\"Updating entity in database\");",
                                "entity = await base.UpdateAsync(entity);",
                                "_logger.LogInformation(\"Updating entity in cache\");",
                                "await _cache.SaveAsync(GetCacheKey(entity), entity);",
                                "return entity;"
                            }
                        },
                        new Method
                        {
                            Modifiers = "private static",
                            Type = "string",
                            Name = "GetCacheKey",
                            Comment = "Gets the string key used to identity the given object in the cache",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            ArrowClauseExpression = "$\"{typeof(TEntity).FullName}:{entity.GetKey()}\""
                        },
                        new Method
                        {
                            Comment = "Saves the given entity object to the cache",
                            Modifiers = "private async",
                            Type = "Task",
                            Name = "SaveCacheEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter("entity", "TEntity")
                            },
                            Block = new List<string>
                            {
                                "_logger.LogInformation(\"Saving cache entry\");",
                                "await _cache.SaveAsync(GetCacheKey(entity), entity);"
                            }
                        }
                    }),
                new Interface(
                    "DevOps.Code.Entities.Interfaces.Entity",
                    "Common interface for code-generated entity types",
                    "1.0.4",
                    typeParameters: new List<string> { "TKey" },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TKey", "struct")
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Comment = "Returns the unique identifier of the entity type",
                            Name = "GetEntityType",
                            Type = "int"
                        },
                        new Method
                        {
                            Comment = "Returns the unique identifier of this entity instance",
                            Name = "GetKey",
                            Type = "TKey"
                        }
                    }),
                new Interface(
                    "DevOps.Code.Entities.Interfaces.StaticEntity",
                    "Common interface for code-generated uneditable entity types",
                    "1.0.6",
                    sameAccountDependencies: new[]
                    {
                        "DevOps.Code.Entities.Interfaces.Entity"
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.Interfaces.Entity",
                        "System",
                        "System.Linq.Expressions"
                    },
                    bases: new List<Base> { new Base("IEntity", "TKey") },
                    typeParameters: new List<string> { "TEntity", "TKey" },
                    constraintClauses: new List<ConstraintClause>
                    {
                        new ConstraintClause("TEntity", "class"),
                        new ConstraintClause("TKey", "struct")
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Comment = "Returns an expression that EntityFrameworkCore uses to set a unique index on this entity type",
                            Name = "GetUniqueIndex",
                            Type = "Expression<Func<TEntity, object>>"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Connection.GetConnectionString",
                    "ConnectionStringGetter",
                    "Function returns the AZURE_STORAGE_CONNECTION_STRING environment variable value",
                    "4.0.9",
                    new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Description = "Connection string to your Azure Storage instance",
                            Name = "AZURE_STORAGE_CONNECTION_STRING"
                        }
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "System.Environment"
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            Comment = "Name of the environment variable containing the Azure Storage connection string",
                            DefaultValue = "\"AZURE_STORAGE_CONNECTION_STRING\"",
                            Modifiers = "private const",
                            Name = "EnvironmentVariableName",
                            Type = "string"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "GetEnvironmentVariable(EnvironmentVariableName)",
                            Comment = "Returns the AZURE_STORAGE_CONNECTION_STRING environment variable value",
                            Modifiers = "public static",
                            Name = "ConnectionString",
                            Type = "string"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Connection.GetCloudStorageAccount",
                    "CloudStorageAccountGetter",
                    "Function returns an instance of Microsoft Azure CloudStorageAccount using the given connection string",
                    "4.0.9",
                    sameAccountDependencies: new[] { "Azure.Storage.Metapackage" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "CloudStorageAccount.Parse(connectionString)",
                            Comment = "Returns an instance of Microsoft Azure CloudStorageAccount using the given connection string",
                            Modifiers = "public static",
                            Name = "StorageAccount",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                }
                            },
                            Type = "CloudStorageAccount"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetTableClient",
                    "TableClientGetter",
                    "Function returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                    "4.0.9",
                    sameAccountDependencies: new[] { "Azure.Storage.Connection.GetCloudStorageAccount" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Connection.GetCloudStorageAccount.CloudStorageAccountGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "StorageAccount(connectionString).CreateCloudTableClient()",
                            Comment = "Returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                            Modifiers = "public static",
                            Name = "TableClient",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                }
                            },
                            Type = "CloudTableClient"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetTableReference",
                    "TableReferenceGetter",
                    "Function returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                    "4.0.9",
                    sameAccountDependencies: new[] { "Azure.Storage.Table.GetTableClient" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetTableClient.TableClientGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "TableClient(connectionString).GetTableReference(tableName)",
                            Comment = "Returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                            Modifiers = "public static",
                            Name = "TableReference",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "tableName",
                                    Type = "string"
                                }
                            },
                            Type = "CloudTable"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetOrCreateTableReference",
                    "TableReferenceGetterOrCreator",
                    "Function returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                    "4.0.9",
                    sameAccountDependencies: new[] { "Azure.Storage.Table.GetTableReference" },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetTableReference.TableReferenceGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block = new List<string>
                            {
                                "var table = TableReference(connectionString, tableName);",
                                "await table.CreateIfNotExistsAsync();",
                                "return table;"
                            },
                            Comment = "Returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                            Modifiers = "public static async",
                            Name = "GetOrCreateAzureTable",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "tableName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<CloudTable>"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetAzureTable",
                    "AzureTableGetter",
                    "Function returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                    "4.0.9",
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Description = "Connection string to your Azure Storage instance",
                            Name = "AZURE_STORAGE_CONNECTION_STRING"
                        }
                    },
                    sameAccountDependencies: new[]
                    {
                        "Azure.Storage.Connection.GetConnectionString",
                        "Azure.Storage.Table.GetOrCreateTableReference"
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Connection.GetConnectionString.ConnectionStringGetter",
                        "Azure.Storage.Table.GetOrCreateTableReference.TableReferenceGetterOrCreator"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "await GetOrCreateAzureTable(ConnectionString(), tableName)",
                            Comment = "Returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                            Modifiers = "public static async",
                            Name = "AzureTable",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "tableName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<CloudTable>"
                        }
                    }),
                new Class(
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger",
                    "AppveyorBuildTable",
                    "Azure Table Storage entity representing a successfully completed AppVeyor build",
                    "4.0.10",
                    sameAccountDependencies: new[] { "Azure.Storage.Metapackage" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    bases: new List<Base>
                    {
                        new Base
                        {
                            Name = "TableEntity"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor { Modifiers = "public" },
                        new Constructor
                        {
                            Block = new List<string>
                            {
                                "Dependencies = dependencies;",
                                "FileHashes = fileHashes;",
                                "PartitionKey = name;",
                                "RowKey = version;"
                            },
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    DefaultValue = "null",
                                    Name = "dependencies",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "fileHashes",
                                    Type = "string",
                                    DefaultValue = "null"
                                }
                            }
                        }
                    },
                    properties: new List<Property>
                    {
                        new Property
                        {
                            Comment = "Comma-delimited string of repository dependencies in {name}|{version} format",
                            Name = "Dependencies",
                            Type = "string",
                            Modifiers = "public"
                        },
                        new Property
                        {
                            Comment = "Comma-delimited string of repository file hashes",
                            Name = "FileHashes",
                            Type = "string",
                            Modifiers = "public"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder",
                    "AppveyorBuildTableHelper",
                    "Function returns an instance of AppveyorBuildTable",
                    "4.0.10",
                    sameAccountDependencies: new[] { "DevOps.Build.AppVeyor.AzureStorageTableLedger" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "new AppveyorBuildTable(name, version, dependencies, fileHashes)",
                            Comment = "Returns an instance of AppveyorBuildTable",
                            Modifiers = "public static",
                            Name = "BuildTableEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "dependencies",
                                    Type = "string",
                                    DefaultValue = "null"
                                },
                                new Parameter
                                {
                                    Name = "fileHashes",
                                    Type = "string",
                                    DefaultValue = "null"
                                }
                            },
                            Type = "AppveyorBuildTable",
                        }
                    }),
                new Class(
                    "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger",
                    "RepositoryVersionTable",
                    "Azure Table Storage entity representing a current repository version",
                    "1.0.10",
                    sameAccountDependencies: new[] { "Azure.Storage.Metapackage" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    bases: new List<Base>
                    {
                        new Base
                        {
                            Name = "TableEntity"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor { Modifiers = "public" },
                        new Constructor
                        {
                            Block = new List<string>
                            {
                                "PartitionKey = accountName;",
                                "RowKey = repositoryName;",
                                "Version = version;"
                            },
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            }
                        }
                    },
                    properties: new List<Property>
                    {
                        new Property
                        {
                            Comment = "Current version of repository",
                            Name = "Version",
                            Type = "string",
                            Modifiers = "public"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger.Builder",
                    "RepositoryVersionTableHelper",
                    "Function returns an instance of RepositoryVersionTable",
                    "1.0.10",
                    sameAccountDependencies: new[] { "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "new RepositoryVersionTable(accountName, repositoryName, version)",
                            Comment = "Returns an instance of RepositoryVersionTable",
                            Modifiers = "public static",
                            Name = "RepositoryVersionTableEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            },
                            Type = "RepositoryVersionTable",
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.GetAzureTable",
                    "AzureTableGetter",
                    "Function returns an Azure CloudTable reference for a table named appveyor",
                    "1.0.10",
                    sameAccountDependencies: new[] {
                        "Azure.Storage.Table.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetAzureTable.AzureTableGetter"
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            DefaultValue = "nameof(appveyor)",
                            Modifiers = "private const",
                            Name = "appveyor",
                            Type = "string"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "await AzureTable(appveyor)",
                            Comment = "Returns an Azure CloudTable reference for a table named \"appveyor\" in the storage account for the connection string stored in the environment",
                            Modifiers = "public static async",
                            Name = "GetTable",
                            Type = "Task<CloudTable>"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AddBuild",
                    "BuildAdder",
                    "Function adds the given repository build information to an Azure Storage Table ledger",
                    "1.0.10",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder.AppveyorBuildTableHelper"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var entry = BuildTableEntry(name, version, dependencies, fileHashes);",
                                "var operation = TableOperation.InsertOrReplace(entry);",
                                "var table = await GetTable();",
                                "await table.ExecuteAsync(operation);"
                            },
                            Comment = "Adds a table entry to an Azure Table named \"appveyor\"",
                            Modifiers = "public static async",
                            Name = "AddBuildAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "dependencies",
                                    Type = "string",
                                    DefaultValue = "null"
                                },
                                new Parameter
                                {
                                    Name = "fileHashes",
                                    Type = "string",
                                    DefaultValue = "null"
                                }
                            },
                            Type = "Task"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.GetBuildRecord",
                    "BuildRecordGetter",
                    "Function gets the given repository's build record from the Azure Storage Table AppVeyor build ledger",
                    "1.0.10",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger",
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var operation = TableOperation.Retrieve<AppveyorBuildTable>(name, version);",
                                "var table = await GetTable();",
                                "var result = await table.ExecuteAsync(operation);",
                                "if (result?.Result == null) return null;",
                                "return (AppveyorBuildTable)result.Result;"
                            },
                            Comment = "Returns the given repository's dependency string from the Azure Storage Table AppVeyor build ledger",
                            Modifiers = "public static async",
                            Name = "GetBuildRecordAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            },
                            Type = "Task<AppveyorBuildTable>"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AddRepositoryVersion",
                    "RepositoryVersionAdder",
                    "Function adds the given repository build information to an Azure Storage Table ledger",
                    "1.0.10",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger.Builder"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter",
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger.Builder.RepositoryVersionTableHelper"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var entry = RepositoryVersionTableEntry(accountName, repositoryName, version);",
                                "var operation = TableOperation.InsertOrReplace(entry);",
                                "var table = await GetTable();",
                                "await table.ExecuteAsync(operation);"
                            },
                            Comment = "Adds a table entry to an Azure Table named \"appveyor\"",
                            Modifiers = "public static async",
                            Name = "AddRepositoryVersionAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            },
                            Type = "Task"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.GetRepositoryVersionRecord",
                    "RepositoryVersionRecordGetter",
                    "Function gets the given repository's version record from the Azure Storage Table AppVeyor build ledger",
                    "2.0.14",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger",
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var operation = TableOperation.Retrieve<RepositoryVersionTable>(accountName, repositoryName);",
                                "var table = await GetTable();",
                                "var result = await table.ExecuteAsync(operation);",
                                "if (result?.Result == null) return null;",
                                "return (RepositoryVersionTable)result.Result;"
                            },
                            Comment = "Returns the given repository's version record from the Azure Storage Table AppVeyor build ledger",
                            Modifiers = "public static async",
                            Name = "GetRepositoryVersionRecordAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<RepositoryVersionTable>"
                        }
                    }),
                new Class(
                    "DevOps.Code.Entities.EntityTypeLedger",
                    "EntityTypeTable",
                    "Azure Table Storage entity representing an entity-type",
                    "1.0.4",
                    sameAccountDependencies: new[] { "Azure.Storage.Metapackage" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    bases: new List<Base>
                    {
                        new Base
                        {
                            Name = "TableEntity"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor { Modifiers = "public" },
                        new Constructor
                        {
                            Block = new List<string>
                            {
                                "EntityTypeId = entityTypeId;",
                                "PartitionKey = accountName;",
                                "RowKey = repositoryName;"
                            },
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "entityTypeId",
                                    Type = "int"
                                }
                            }
                        }
                    },
                    properties: new List<Property>
                    {
                        new Property
                        {
                            Comment = "Entity type unique identifier",
                            Name = "EntityTypeId",
                            Type = "int",
                            Modifiers = "public"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.EntityTypeLedger.Builder",
                    "EntityTypeTableHelper",
                    "Function returns an instance of EntityTypeTable",
                    "1.0.4",
                    sameAccountDependencies: new[] { "DevOps.Code.Entities.EntityTypeLedger" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "new EntityTypeTable(accountName, repositoryName, entityTypeId)",
                            Comment = "Returns an instance of EntityTypeTable",
                            Modifiers = "public static",
                            Name = "EntityTypeTableEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "entityTypeId",
                                    Type = "int"
                                }
                            },
                            Type = "EntityTypeTable",
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.GetAzureTable",
                    "AzureTableGetter",
                    "Function returns an Azure CloudTable reference for a table named entities",
                    "1.0.4",
                    sameAccountDependencies: new[] {
                        "Azure.Storage.Table.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetAzureTable.AzureTableGetter"
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            DefaultValue = "nameof(entities)",
                            Modifiers = "private const",
                            Name = "entities",
                            Type = "string"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "await AzureTable(entities)",
                            Comment = "Returns an Azure CloudTable reference for a table named \"entities\" in the storage account for the connection string stored in the environment",
                            Modifiers = "public static async",
                            Name = "GetTable",
                            Type = "Task<CloudTable>"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.AddEntityTypeRecord",
                    "EntityTypeRecordAdder",
                    "Function adds the given entity type information to an Azure Storage Table ledger",
                    "1.0.4",
                    sameAccountDependencies: new[] {
                        "DevOps.Code.Entities.GetAzureTable",
                        "DevOps.Code.Entities.EntityTypeLedger.Builder"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.GetAzureTable.AzureTableGetter",
                        "DevOps.Code.Entities.EntityTypeLedger.Builder.EntityTypeTableHelper"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var entry = EntityTypeTableEntry(accountName, repositoryName, entityTypeId);",
                                "var operation = TableOperation.InsertOrReplace(entry);",
                                "var table = await GetTable();",
                                "await table.ExecuteAsync(operation);"
                            },
                            Comment = "Adds a table entry to an Azure Table named \"entities\"",
                            Modifiers = "public static async",
                            Name = "AddEntityTypeAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "entityTypeId",
                                    Type = "int"
                                }
                            },
                            Type = "Task"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.GetEntityTypeRecord",
                    "EntityTypeRecordGetter",
                    "Function gets the given entity-type's ID record from the Azure Storage Table entity-types ledger",
                    "1.0.5",
                    sameAccountDependencies: new[] {
                        "DevOps.Code.Entities.GetAzureTable",
                        "DevOps.Code.Entities.EntityTypeLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.EntityTypeLedger",
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.GetAzureTable.AzureTableGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var operation = TableOperation.Retrieve<EntityTypeTable>(accountName, repositoryName);",
                                "var table = await GetTable();",
                                "var result = await table.ExecuteAsync(operation);",
                                "if (result?.Result == null) return null;",
                                "return (EntityTypeTable)result.Result;"
                            },
                            Comment = "Returns the given entity-type's ID record from the Azure Storage Table entity-types ledger",
                            Modifiers = "public static async",
                            Name = "GetEntityTypeRecordAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<EntityTypeTable>"
                        }
                    })
            };
    }
}
