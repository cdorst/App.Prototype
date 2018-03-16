using DevOps.Primitives.SourceGraph.Helpers.Consolidated;

namespace ConsoleApp6.Templates
{
    public class TemplateContent
    {
        public TemplateContent() { }
        public TemplateContent(Code code, Metapackage metapackage)
        {
            Code = code;
            Metapackage = metapackage;
        }
        public TemplateContent(Code code)
            : this(code, null)
        {
        }
        public TemplateContent(Metapackage metapackage)
            : this(null, metapackage)
        {
        }

        public Code Code { get; set; }
        public Metapackage Metapackage { get; set; }
    }
}
