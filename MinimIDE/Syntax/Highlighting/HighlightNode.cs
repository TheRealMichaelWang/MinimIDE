using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public abstract class HighlightNode : INode
    {
        public int Start
        {
            get; private set;
        }

        public int Stop
        {
            get; private set;
        }

        public int Length
        {
            get
            {
                return Stop - Start;
            }
        }

        public HighlightNode(int start, int stop){
            if (stop < start)
                throw new InvalidOperationException("Highlight stop must be greater than or equal to highlight start");
            this.Start = start;
            this.Stop = stop;
        }

        public abstract void Refractor(StringBuilder builder);
        public abstract void Highlight(RichTextBox editor);
        public abstract bool Compare(INode node);
        public abstract void GetDifference(List<INode> differences, INode node);

        public void Select(RichTextBox editor)
        {
            editor.Select(Start, Stop - Start);
        }

        public void Deselect(RichTextBox editor)
        {
            editor.DeselectAll();
            editor.Select(this.Stop, 0);
            editor.SelectionColor = Color.Black;
            editor.DeselectAll();
        }
    }

    static class HighlightingExtensions
    {
        public static void ChangeForeColor(this RichTextBox editor, Color color)
        {
            editor.SelectionColor = color;
        }
    }
}
