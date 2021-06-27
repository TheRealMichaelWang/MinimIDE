using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class ReferenceNode
    {
        public void Refractor(StringBuilder builder)
        {
            this.Accessors[0].Refractor(builder);

            for(int i = 1; i < this.Accessors.Count; i++)
            {
                if (this.Accessors[i].GetType() == typeof(KeywordNode))
                {
                    builder.Append('.');
                    this.Accessors[i].Refractor(builder);
                }
                else
                {
                    builder.Append('[');
                    this.Accessors[i].Refractor(builder);
                    builder.Append(']');
                }
            }
        }
    }

    public partial class SetNode
    {
        public void Refractor(StringBuilder builder)
        {
            builder.Append("set ");
            this.Destination.Refractor(builder);
            builder.Append(" to ");
            this.Value.Refractor(builder);
        }
    }

    public partial class ReturnNode
    {
        public void Refractor(StringBuilder builder)
        {
            builder.Append("return ");
            this.Value.Refractor(builder);
        }
    }

    public partial class GotoNode
    {
        protected void refractorArguments(StringBuilder builder)
        {
            if (this.Arguments.Count > 0)
            {
                builder.Append('(');
                bool comma = false;
                foreach (INode argument in this.Arguments)
                {
                    if (comma)
                        builder.Append(", ");
                    else
                        comma = true;
                    argument.Refractor(builder);
                }
                builder.Append(')');
            }
        }

        public virtual void Refractor(StringBuilder builder)
        {
            this.gotoKeyword.Refractor(builder);
            builder.Append(' ');
            this.labelKeyword.Refractor(builder);
            refractorArguments(builder);
        }
    }

    public partial class GotoAsNode
    {
        public override void Refractor(StringBuilder builder)
        {
            this.gotoKeyword.Refractor(builder);
            builder.Append(' ');
            this.labelKeyword.Refractor(builder);
            builder.Append(" as ");
            this.TargetObject.Refractor(builder);
            base.refractorArguments(builder);
        }
    }

    public partial class IncludeNode
    {
        public void Refractor(StringBuilder builder)
        {
            builder.Append("include \"");
            builder.Append(this.filePathLiteral);
            builder.Append('\"');
        }
    }

    public partial class RemarkNode
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append("rem ");
            builder.Append(this.Remarks);
        }
    }
}
