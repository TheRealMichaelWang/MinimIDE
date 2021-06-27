using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public partial class ReferenceNode
    {
        public void Highlight(RichTextBox editor)
        {
            foreach (INode accessor in this.Accessors)
                accessor.Highlight(editor);
        }
    }

    public partial class SetNode
    {
        public void Highlight(RichTextBox editor)
        {
            this.setKeyword.Highlight(editor);
            this.Destination.Highlight(editor);
            this.Value.Highlight(editor);
        }
    }

    public partial class ReturnNode
    {
        public void Highlight(RichTextBox editor)
        {
            this.returnKeyword.Highlight(editor);
            this.Value.Highlight(editor);
        }
    }

    public partial class GotoNode
    {
        public virtual void Highlight(RichTextBox editor)
        {
            this.gotoKeyword.Highlight(editor);
            this.labelKeyword.Highlight(editor);

            foreach (INode argument in this.Arguments)
                argument.Highlight(editor);
        }
    }

    public partial class GotoAsNode
    {
        public override void Highlight(RichTextBox editor)
        {
            base.Highlight(editor);
            this.TargetObject.Highlight(editor);
        }
    }

    public partial class IncludeNode
    {
        public void Highlight(RichTextBox editor)
        {
            this.includeKeyword.Highlight(editor);
            this.filePathLiteral.Highlight(editor);
        }
    }

    public partial class RemarkNode
    {
        public override void Highlight(RichTextBox editor)
        {
            this.Select(editor);
            editor.ChangeForeColor(Color.DarkGreen);
            this.Deselect(editor);
        }
    }
}
