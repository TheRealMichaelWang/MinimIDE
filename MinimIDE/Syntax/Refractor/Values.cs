using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class NumericalNode
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append(this.Double);
        }
    }

    public partial class StringLiteralNode
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append('\"');
            builder.Append(this.String);
            builder.Append('\"');
        }
    }

    public partial class ArrayLiteralNode
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append('{');

            bool print_comma = false;
            
            foreach(INode value in this.array)
            {
                if (print_comma)
                    builder.Append(", ");
                else
                    print_comma = true;
                value.Refractor(builder);
            }
            builder.Append('}');
        }
    }

    public partial class CharacterLiteral
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append('\'');
            builder.Append(this.Char);
            builder.Append('\'');
        }
    }
}
