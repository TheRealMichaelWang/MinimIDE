using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public partial class BinaryOperatorNode
    {
        public void Highlight(RichTextBox editor)
        {
            this.LeftOperand.Highlight(editor);
            this.RightOperand.Highlight(editor);
        }
    }

    public partial class UnaryOperatorNode
    {
        public void Highlight(RichTextBox editor)
        {
            this.Operand.Highlight(editor);
        }
    }
}
