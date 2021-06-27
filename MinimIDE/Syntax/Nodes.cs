using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public interface INode
    {
        void Refractor(StringBuilder builder);
        void Highlight(RichTextBox editor);

        bool Compare(INode node);

        void GetDifference(List<INode> differences, INode node);
    }

    public partial class ReferenceNode : INode
    {
        public List<INode> Accessors
        {
            get; private set;
        }

        public ReferenceNode(List<INode> accessors)
        {
            this.Accessors = accessors;

            if (this.Accessors.Count < 1)
                throw new ArgumentException("Accessors must have one or more elements.", "accessors");

            if (this.Accessors[0].GetType() != typeof(KeywordNode))
                throw new ArgumentException("First accessor must be a keyword.", "accessors");
        }

        public bool Compare(INode node)
        {
            if (!typeof(ReferenceNode).IsAssignableFrom(node.GetType()))
                return false;

            ReferenceNode reference = node as ReferenceNode;

            for (int i = 0; i < this.Accessors.Count; i++)
            {
                if (!this.Accessors[i].Compare(reference.Accessors[i]))
                    return false;
            }
            return true;
        }

        public void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(ReferenceNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            ReferenceNode reference = node as ReferenceNode;

            for (int i = 0; i < this.Accessors.Count; i++)
                this.Accessors[i].GetDifference(differences, reference.Accessors[i]);
        }
    }

    public partial class SetNode : INode
    {
        private KeywordNode setKeyword;

        public ReferenceNode Destination
        {
            get; private set;
        }

        public INode Value
        {
            get; private set;
        }

        public SetNode(ReferenceNode destination, INode value, int pos)
        {
            this.Destination = destination;
            this.Value = value;
            this.setKeyword = new KeywordNode("set", pos, Color.DarkBlue);
        }

        public bool Compare(INode node)
        {
            if (!typeof(SetNode).IsAssignableFrom(node.GetType()))
                return false;
            SetNode setNode = node as SetNode;
            return this.Destination.Compare(setNode.Destination) && this.Value.Compare(setNode.Value);
        }

        public void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(SetNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            SetNode setNode = node as SetNode;
            this.Destination.GetDifference(differences, setNode.Destination);
            this.Value.GetDifference(differences, setNode.Value);
        }
    }

    public partial class ReturnNode : INode
    {
        private KeywordNode returnKeyword;

        public INode Value
        {
            get; private set;
        }

        public ReturnNode(INode value, int pos)
        {
            this.Value = value;
            this.returnKeyword = new KeywordNode("return", pos, Color.DarkBlue);
        }

        public bool Compare(INode node)
        {
            if (!typeof(ReturnNode).IsAssignableFrom(node.GetType()))
                return false;

            ReturnNode returnNode = node as ReturnNode;

            return this.Value.Compare(returnNode.Value);
        }

        public void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(ReturnNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            ReturnNode returnNode = node as ReturnNode;
            this.Value.GetDifference(differences, returnNode.Value);
        }
    }

    public partial class GotoNode : INode
    {
        protected KeywordNode gotoKeyword;
        protected KeywordNode labelKeyword;

        public List<INode> Arguments
        {
            get; private set;
        }

        public string Label
        {
            get
            {
                return labelKeyword.Identifier;
            }
        }

        public GotoNode(KeywordNode labelKeyword, int pos, List<INode> arguments)
        {
            this.labelKeyword = labelKeyword;
            this.Arguments = arguments;
            this.gotoKeyword = new KeywordNode("goproc", pos, Color.DarkBlue);
        }

        protected GotoNode(KeywordNode gotoKeyword, KeywordNode labelKeyword, List<INode> arguments)
        {
            this.labelKeyword = labelKeyword;
            this.gotoKeyword = gotoKeyword;
            this.Arguments = arguments;
        }

        public virtual bool Compare(INode node)
        {
            if (!typeof(GotoNode).IsAssignableFrom(node.GetType()))
                return false;

            GotoNode gotoNode = node as GotoNode;

            if (this.Arguments.Count != gotoNode.Arguments.Count)
                return false;

            if (!this.labelKeyword.Compare(gotoNode.labelKeyword) || !this.gotoKeyword.Compare(gotoNode.gotoKeyword))
                return false;

            for (int i = 0; i < Arguments.Count; i++)
                if (!this.Arguments[i].Compare(gotoNode.Arguments[i]))
                    return false;
            return true;
        }

        public virtual void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(GotoNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            GotoNode gotoNode = node as GotoNode;
            
            if (this.Arguments.Count != gotoNode.Arguments.Count)
            {
                differences.Add(node);
                return;
            }

            this.labelKeyword.GetDifference(differences, gotoNode.labelKeyword);
            for (int i = 0; i < this.Arguments.Count; i++)
                this.Arguments[i].GetDifference(differences, gotoNode.Arguments[i]);
        }
    }

    public partial class ExternalGoto : GotoNode
    {
        public ExternalGoto(KeywordNode labelKeyword, int pos, List<INode> arguments) : base(new KeywordNode("extern", pos, Color.Gray), labelKeyword, arguments)
        {

        }
    }

    public partial class GotoAsNode : GotoNode
    {
        public INode TargetObject
        {
            get; private set;
        }

        public GotoAsNode(KeywordNode labelKeyword, int pos, INode targetObject, List<INode> arguments) : base(labelKeyword, pos, arguments)
        {
            this.TargetObject = targetObject;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(GotoAsNode).IsAssignableFrom(node.GetType()) || !base.Compare(node))
                return false;

            GotoAsNode gotoAs = node as GotoAsNode;
            return this.TargetObject.Compare(gotoAs.TargetObject);
        }

        public override void GetDifference(List<INode> differences, INode node)
        { 
            if (!typeof(GotoAsNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }
            base.GetDifference(differences, node);

            GotoAsNode gotoAs = node as GotoAsNode;
            this.TargetObject.GetDifference(differences, gotoAs.TargetObject);
        }
    }

    public partial class IncludeNode : INode
    {
        private KeywordNode includeKeyword;
        private StringLiteralNode filePathLiteral;

        public string FilePath
        {
            get
            {
                return filePathLiteral.String;
            }
        }

        public IncludeNode(StringLiteralNode filePathLiteral, int pos)
        {
            this.filePathLiteral = filePathLiteral;
            this.includeKeyword = new KeywordNode("include", pos, Color.Gray);
        }

        public bool Compare(INode node)
        {
            if (!typeof(IncludeNode).IsAssignableFrom(node.GetType()))
                return false;

            IncludeNode includeNode = node as IncludeNode;

            return this.FilePath == includeNode.FilePath;
        }

        public void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(IncludeNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }
            IncludeNode includeNode = node as IncludeNode;
            this.filePathLiteral.GetDifference(differences, includeNode.filePathLiteral);
        }
    }

    public partial class RemarkNode : HighlightNode
    {
        public string Remarks
        {
            get; private set;
        }

        public RemarkNode(int start, int end, string remarks) : base(start, end)
        {
            this.Remarks = remarks;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(RemarkNode).IsAssignableFrom(node.GetType()))
                return false;

            RemarkNode remarkNode = node as RemarkNode;
            return remarkNode.Remarks == this.Remarks;
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(RemarkNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            RemarkNode remarkNode = node as RemarkNode;
            if (this.Remarks != remarkNode.Remarks)
                differences.Add(node);
        }
    }
}