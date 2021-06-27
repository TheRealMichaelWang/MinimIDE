using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public interface IMultiNode : INode
    {
        void Append(INode node);
    }

    public partial class CodeBlock : IMultiNode
    {
        protected int indentLevel;
        protected List<INode> topLevelCode;

        public CodeBlock(List<INode> topLevelCode, int indentLevel)
        {
            this.topLevelCode = topLevelCode;
            this.indentLevel = indentLevel;
        }

        public CodeBlock(int indentLevel)
        {
            this.topLevelCode = new List<INode>();
            this.indentLevel = indentLevel;
        }

        public void Append(INode node)
        {
            topLevelCode.Add(node);
        }

        public virtual bool Compare(INode node)
        {
            if (!typeof(CodeBlock).IsAssignableFrom(node.GetType()))
                return false;

            CodeBlock block = node as CodeBlock;

            for (int i = 0; i < this.topLevelCode.Count; i++)
            {
                if (!this.topLevelCode[i].Compare(block.topLevelCode[i]))
                    return false;
            }
            return true;
        }

        public virtual void GetDifference(List<INode> difference, INode node)
        {
            if (!typeof(CodeBlock).IsAssignableFrom(node.GetType()))
            {
                difference.Add(node);
                return;
            }

            CodeBlock block = node as CodeBlock;

            for (int i = 0; i < this.topLevelCode.Count && i < block.topLevelCode.Count; i++)
                this.topLevelCode[i].GetDifference(difference, block.topLevelCode[i]);
            for (int i = this.topLevelCode.Count; i < block.topLevelCode.Count; i++)
                difference.Add(block.topLevelCode[i]);
        }
    }

    public partial class ConditionalNode : CodeBlock
    {
        private KeywordNode typeKeyword;
        private INode _condition;

        public enum ConditinalType
        {
            If,
            Elif,
            Else,
            While
        }

        public ConditinalType Type
        {
            get; private set;
        }

        public INode Condition
        {
            get
            {
                if (this.Type == ConditinalType.Else)
                    throw new InvalidOperationException("Else statements do not have any conditions");
                return _condition;
            }
        }

        public ConditionalNode(ConditinalType type, INode condition, int indentLevel, int pos) : base(indentLevel + 1)
        {
            this.Type = type;
            this._condition = condition;
            if (base.indentLevel == 0)
                throw new ArgumentException("Nested code blocks must have an indent level greater than or equal to 1.", "indentLevel");
            switch (type)
            {
                case ConditinalType.If:
                    typeKeyword = new KeywordNode("if", pos, Color.DarkBlue);
                    break;
                case ConditinalType.Elif:
                    typeKeyword = new KeywordNode("elif", pos, Color.DarkBlue);
                    break;
                case ConditinalType.Else:
                    typeKeyword = new KeywordNode("elif", pos, Color.DarkBlue);
                    break;
                case ConditinalType.While:
                    typeKeyword = new KeywordNode("while", pos, Color.DarkBlue);
                    break;
            }
        }

        public override bool Compare(INode node)
        {
            if (!typeof(ConditionalNode).IsAssignableFrom(node.GetType()))
                return false;

            ConditionalNode conditional = node as ConditionalNode;

            if (this.Type != conditional.Type)
                return false;
            if (this.Type != ConditinalType.Else && !this.Condition.Compare(conditional))
                return false;

            return base.Compare(conditional);
        }

        public override void GetDifference(List<INode> difference, INode node)
        {
            if (!typeof(ConditionalNode).IsAssignableFrom(node.GetType()))
            {
                difference.Add(node);
                return;
            }
            ConditionalNode conditional = node as ConditionalNode;

            if (this.Type != conditional.Type)
            {
                difference.Add(node);
                return;
            }
            if (this.Type != ConditinalType.Else)
                this.Condition.GetDifference(difference, conditional.Condition);
            
            base.GetDifference(difference, node);
        }
    }

    public partial class ProcedureNode : CodeBlock
    {
        private KeywordNode procedureKeyword;
        private KeywordNode labelKeyword;
        private List<KeywordNode> _parameters;

        public string Label
        {
            get
            {
                return labelKeyword.Identifier;
            }
        }

        public string[] Parameters
        {
            get
            {
                string[] parameters = new string[_parameters.Count];
                for (int i = 0; i < _parameters.Count; i++)
                    parameters[i] = _parameters[i].Identifier;
                return parameters;
            }
        }

        public ProcedureNode(KeywordNode labelKeyword, List<KeywordNode> parameters, int pos, int indentDepth) : base(indentDepth)
        {
            this.labelKeyword = labelKeyword;
            this._parameters = parameters;
            this.procedureKeyword = new KeywordNode("proc", pos, Color.DarkBlue);
        }

        public override bool Compare(INode node)
        {
            if (!typeof(ProcedureNode).IsAssignableFrom(node.GetType()))
                return false;

            ProcedureNode procedure = node as ProcedureNode;

            if (this.Label != procedure.Label)
                return false;

            for (int i = 0; i < _parameters.Count; i++)
                if (!this._parameters[i].Compare(procedure._parameters[i]))
                    return false;

            if(!base.Compare(procedure))
                return false;

            return true;
        }

        public override void GetDifference(List<INode> difference, INode node)
        {
            if (!typeof(ProcedureNode).IsAssignableFrom(node.GetType()))
            {
                difference.Add(node);
                return;
            }

            ProcedureNode procedure = node as ProcedureNode;
            this.labelKeyword.GetDifference(difference, procedure.labelKeyword);

            for (int i = 0; i < this._parameters.Count; i++)
                this._parameters[i].GetDifference(difference, procedure._parameters[i]);

            base.GetDifference(difference, procedure);
        }
    }
}
