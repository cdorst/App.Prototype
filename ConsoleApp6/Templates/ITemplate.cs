using System;
using System.Collections.Generic;

namespace ConsoleApp6.Templates
{
    public interface ITemplate
    {
        string Description { get; set; }
        string Name { get; set; }
        IEnumerable<string> SameAccountDependencies { get; set; }
        string Version { get; set; }

        /// <summary>Gets an instance of this template's declaration contents. A dictionary of dependencies and GitHub account name are provided</summary>
        Func<string, IDictionary<string, ITemplate>, TemplateContent> GetContent();
    }
}
