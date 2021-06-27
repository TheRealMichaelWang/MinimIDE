using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class KeywordNode : HighlightNode
    {
        public Color HighlightColor
        {
            get; private set;
        }

        public string Identifier
        {
            get; private set;
        }

        public KeywordNode(string identifier, int pos, Color highlightColor) : base(pos, pos + identifier.Length)
        {
            this.Identifier = identifier;
            this.HighlightColor = highlightColor;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(KeywordNode).IsAssignableFrom(node.GetType()))
                return false;

            KeywordNode keyword = node as KeywordNode;

            return keyword.Identifier == this.Identifier;
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(KeywordNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            KeywordNode keyword = node as KeywordNode;

            if (this.Identifier != keyword.Identifier)
                differences.Add(node);
        }
    }
}
