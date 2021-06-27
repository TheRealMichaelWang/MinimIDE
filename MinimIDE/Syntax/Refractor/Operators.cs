using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class BinaryOperatorNode
    {
        private static string GetOpString(BinaryOperator @operator)
        {
            switch (@operator)
            {
                case BinaryOperator.And:
                    return "and";
                case BinaryOperator.Or:
                    return "or";
                case BinaryOperator.Equals:
                    return "==";
                case BinaryOperator.NotEquals:
                    return "!=";
                case BinaryOperator.LessEqual:
                    return "<=";
                case BinaryOperator.MoreEqual:
                    return ">=";
                case BinaryOperator.More:
                    return ">";
                case BinaryOperator.Less:
                    return "<";
                case BinaryOperator.Add:
                    return "+";
                case BinaryOperator.Subtract:
                    return "-";
                case BinaryOperator.Multiply:
                    return "*";
                case BinaryOperator.Divide:
                    return "/";
                case BinaryOperator.Modulo:
                    return "%";
                case BinaryOperator.Power:
                    return "^";
            }
            throw new NotImplementedException();
        }

        public void Refractor(StringBuilder builder)
        {
            this.LeftOperand.Refractor(builder);
            builder.Append(' ');
            builder.Append(GetOpString(this.Operator));
            builder.Append(' ');
            this.RightOperand.Refractor(builder);
        }
    }

    public partial class UnaryOperatorNode
    {
        private static string GetOpString(UnaryOperator @operator)
        {
            switch (@operator)
            {
                case UnaryOperator.Alloc:
                    return "alloc";
                case UnaryOperator.Invert:
                    return "!";
                case UnaryOperator.Negate:
                    return "-";
                case UnaryOperator.Increment:
                    return "inc";
                case UnaryOperator.Decriment:
                    return "dec";
                case UnaryOperator.Reference:
                    return "ref";
            }
            throw new NotImplementedException();
        }

        public void Refractor(StringBuilder builder)
        {
            builder.Append(GetOpString(this.Operator));
            if(this.Operator == UnaryOperator.Alloc)
            {
                builder.Append('[');
                this.Operand.Refractor(builder);
                builder.Append(']');
            }
            else
            {
                if (this.Operator == UnaryOperator.Increment || this.Operator == UnaryOperator.Decriment || this.Operator == UnaryOperator.Reference)
                    builder.Append(' ');
                this.Operand.Refractor(builder);
            }
        }
    }
}
