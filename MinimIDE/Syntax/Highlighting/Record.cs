using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public partial class RecordNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.recordKeyword.Highlight(editor);
            this.labelKeyword.Highlight(editor);
            if (extends != null)
                extends.Highlight(editor);
            if (extendLabel != null)
                extendLabel.Highlight(editor);
            base.Highlight(editor);
        }
    }
}
