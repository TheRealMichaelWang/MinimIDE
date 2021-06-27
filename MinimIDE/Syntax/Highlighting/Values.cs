using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public partial class NumericalNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.Select(editor);
            editor.ChangeForeColor(Color.DarkOrange);
            this.Deselect(editor);
        }
    }

    public partial class StringLiteralNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.Select(editor);
            editor.ChangeForeColor(Color.IndianRed);
            this.Deselect(editor);
        }
    }

    public partial class ArrayLiteralNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.Select(editor);
            editor.ChangeForeColor(Color.Black);
            this.Deselect(editor);
            foreach (INode value in this.array)
                value.Highlight(editor);
        }
    }

    public partial class CharacterLiteral
    {
        public override void Highlight(RichTextBox editor)
        {
            this.Select(editor);
            editor.ChangeForeColor(Color.IndianRed);
            this.Deselect(editor);
        }
    }
}
