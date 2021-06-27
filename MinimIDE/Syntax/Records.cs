using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class RecordNode : CodeBlock
    {
        KeywordNode recordKeyword;
        KeywordNode labelKeyword;
        KeywordNode extends;
        KeywordNode extendLabel;

        public RecordNode(KeywordNode labelKeyword, KeywordNode extends, KeywordNode extendLabel, int start) : base(1)
        {
            this.labelKeyword = labelKeyword;
            this.recordKeyword = new KeywordNode("record", start, Color.DarkBlue);
        }

        public override bool Compare(INode node)
        {
            if (!typeof(RecordNode).IsAssignableFrom(node.GetType()))
                return false;

            RecordNode recordNode = node as RecordNode;

            if (this.labelKeyword.Identifier != recordNode.labelKeyword.Identifier)
                return false;

            if (this.extendLabel == null)
            {
                if (recordNode.extendLabel != null)
                    return false;
            }
            else if (this.extendLabel.Identifier != recordNode.extendLabel.Identifier)
                return false;

            return base.Compare(recordNode);
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(RecordNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            RecordNode recordNode = node as RecordNode;

            if (this.extends != null)
                this.extendLabel.GetDifference(differences, recordNode.extendLabel);

            this.labelKeyword.GetDifference(differences, recordNode.labelKeyword);
            base.GetDifference(differences, recordNode);
        }
    }
}
