using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{ 
    public partial class CodeBlock
    {
        public virtual void Highlight(RichTextBox editor)
        {
            foreach (INode instruction in this.topLevelCode)
                instruction.Highlight(editor);
        }
    }

    public partial class ConditionalNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.typeKeyword.Highlight(editor);
            if(this.Type != ConditinalType.Else)
                this.Condition.Highlight(editor);
            base.Highlight(editor);
        }
    }

    public partial class ProcedureNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.procedureKeyword.Highlight(editor);
            this.labelKeyword.Highlight(editor);

            foreach (KeywordNode param in this._parameters)
                param.Highlight(editor);
            base.Highlight(editor);
        }
    }
}
