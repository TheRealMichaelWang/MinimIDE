using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class BinaryOperatorNode : INode
    {
        public enum BinaryOperator
        {
            And,
            Or,
            Equals,
            NotEquals,
            More,
            Less,
            MoreEqual,
            LessEqual,
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulo,
            Power
        }

        public BinaryOperator Operator
        {
            get; private set;
        }

        public INode LeftOperand
        {
            get; private set;
        }

        public INode RightOperand
        {
            get; private set;
        }

        public BinaryOperatorNode(BinaryOperator @operator, INode leftOperand, INode rightOperand)
        {
            this.Operator = @operator;
            this.LeftOperand = leftOperand;
            this.RightOperand = rightOperand;
        }

        public bool Compare(INode node)
        {
            if (!typeof(BinaryOperatorNode).IsAssignableFrom(node.GetType()))
                return false;

            BinaryOperatorNode binaryOperator = node as BinaryOperatorNode;

            return this.Operator == binaryOperator.Operator && this.LeftOperand.Compare(binaryOperator.LeftOperand) && this.RightOperand.Compare(binaryOperator.RightOperand);
        }

        public void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(BinaryOperatorNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            BinaryOperatorNode binaryOperator = node as BinaryOperatorNode;

            if(this.Operator != binaryOperator.Operator)
            {
                differences.Add(node);
                return;
            }

            this.LeftOperand.Compare(binaryOperator.LeftOperand);
            this.RightOperand.Compare(binaryOperator.RightOperand);
        }
    }

    public partial class UnaryOperatorNode : INode
    {
        public enum UnaryOperator
        {
            Invert,
            Negate,
            Alloc,
            Increment,
            Decriment,
            Reference
        }

        public UnaryOperator Operator
        {
            get; private set;
        }

        public INode Operand
        {
            get; private set;
        }

        public UnaryOperatorNode(UnaryOperator @operator, INode operand)
        {
            this.Operator = @operator;
            this.Operand = operand;
        }

        public bool Compare(INode node)
        {
            if (!typeof(UnaryOperatorNode).IsAssignableFrom(node.GetType()))
                return false;

            UnaryOperatorNode unaryOperator = node as UnaryOperatorNode;

            return this.Operator == unaryOperator.Operator && this.Operand.Compare(unaryOperator.Operand);
        }

        public void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(UnaryOperatorNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            UnaryOperatorNode unaryOperator = node as UnaryOperatorNode;
            if(unaryOperator.Operator != this.Operator)
            {
                differences.Add(node);
                return;
            }

            this.Operand.GetDifference(differences, unaryOperator.Operand);
        }
    }
}
