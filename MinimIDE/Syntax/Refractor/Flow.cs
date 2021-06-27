using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class CodeBlock
    {
        protected static void refractorIndent(StringBuilder builder, int indentLevel)
        {
            for (int i = 0; i < indentLevel; i++)
                builder.Append('\t');
        }

        public virtual void Refractor(StringBuilder builder)
        {
            foreach(INode instruction in this.topLevelCode)
            {
                refractorIndent(builder, this.indentLevel);
                instruction.Refractor(builder);
                builder.AppendLine();
            }
        }
    }

    public partial class ConditionalNode
    {
        private string GetTypeString(ConditinalType type)
        {
            switch (type)
            {
                case ConditinalType.If:
                    return "if";
                case ConditinalType.Elif:
                    return "elif";
                case ConditinalType.Else:
                    return "else";
                case ConditinalType.While:
                    return "while";
            }
            throw new NotImplementedException();
        }

        public override void Refractor(StringBuilder builder)
        {
            refractorIndent(builder, this.indentLevel - 1);
            builder.Append(GetTypeString(this.Type));
            builder.Append(' ');

            if (this.Type != ConditinalType.Else)
            {
                this._condition.Refractor(builder);
                builder.Append(' ');
            }
            builder.Append('{');

            builder.AppendLine();
            base.Refractor(builder);

            refractorIndent(builder, this.indentLevel - 1);
            builder.Append('}');
        }
    }

    public partial class ProcedureNode
    {
        public override void Refractor(StringBuilder builder)
        {
            refractorIndent(builder, this.indentLevel - 1);
            builder.Append("proc ");
            builder.Append(this.Label);

            builder.Append('(');
            bool comma = false;

            foreach(string param in this.Parameters)
            {
                if (comma)
                    builder.Append(", ");
                else
                    comma = true;

                builder.Append(param);
            }
            builder.AppendLine(") {");

            base.Refractor(builder);

            refractorIndent(builder, this.indentLevel - 1);
            builder.Append('}');
        }
    }
}
