using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class KeywordNode
    {
        public override void Refractor(StringBuilder builder)
        {
            builder.Append(this.Identifier);
        }
    }
}
