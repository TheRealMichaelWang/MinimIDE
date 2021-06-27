using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public partial class KeywordNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.Select(editor);
            editor.ChangeForeColor(HighlightColor);
            this.Deselect(editor);
        }
    }
}
