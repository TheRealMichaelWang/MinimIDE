using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class RecordNode
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append("record ");
            builder.Append(this.labelKeyword.Identifier);
            if (extends != null)
                builder.Append(" extends " + this.extendLabel.Identifier);
            builder.AppendLine("{");
            base.Refractor(builder);
            builder.AppendLine();
            builder.Append('}');
        }
    }
}
