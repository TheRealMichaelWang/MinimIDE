using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax.Parsing
{
    public partial class Lexer
    {
        private INode tokenizeCrement(string buf, int pos)
        {
            readWhileChar(whiteSpaces);
            UnaryOperatorNode.UnaryOperator op = buf == "inc" ? UnaryOperatorNode.UnaryOperator.Increment : UnaryOperatorNode.UnaryOperator.Decriment;
            readWhileChar(whiteSpaces);
            INode targetVar;
            matchTok(targetVar = tokenizeValue(), typeof(ReferenceNode));
            return new UnaryOperatorNode(op, targetVar);
        }

        StringLiteralNode tokenizeStringLiteral()
        {
            readWhileChar(whiteSpaces);
            int start = this.position;
            matchChar(readChar(), '\"');

            string buffer = readTillChar('\"');
            readChar();

            return new StringLiteralNode(buffer, start);
        }

        CharacterLiteral tokenizeCharLiteral()
        {
            readWhileChar(whiteSpaces);
            int start = this.position;
            matchChar(readChar(), '\'');
            string buffer = readTillChar('\'');
            readChar();
            return new CharacterLiteral(buffer, start);
        }

        INode tokenizeValue()
        {
            NumericalNode tokenizeNumerical()
            {
                int start = this.position;
                return new NumericalNode(double.Parse(readWhileChar(digits)), start);
            }

            ArrayLiteralNode tokenizeArrayLiteral()
            {
                matchChar(readChar(), '{');
                int start = this.position;

                List<INode> nodeArray = new List<INode>();

                while (peekChar() != '}')
                {
                    readWhileChar(whiteSpaces);
                    nodeArray.Add(tokenizeExpression());
                    readTillChar(',', '}');
                    if (peekChar() == ',')
                        readChar();
                }
                int stop = this.position;
                readChar();

                return new ArrayLiteralNode(nodeArray, start, stop);
            }

            readWhileChar(whiteSpaces);
            char lastChar = peekChar();

            if (digits.Contains(lastChar))
                return tokenizeNumerical();
            else if (lastChar == '\"')
                return tokenizeStringLiteral();
            else if (lastChar == '\'')
                return tokenizeCharLiteral();
            else if (lastChar == '{')
                return tokenizeArrayLiteral();
            else if (lastChar == '(')
            {
                readChar();
                INode expression = tokenizeExpression();
                matchChar(readChar(), ')');
                return expression;
            }
            else if (lastChar == '!' || lastChar == '-')
            {
                UnaryOperatorNode.UnaryOperator op = lastChar == '!' ? UnaryOperatorNode.UnaryOperator.Invert : UnaryOperatorNode.UnaryOperator.Negate;
                readChar();
                return new UnaryOperatorNode(op, tokenizeValue());
            }
            else if (idChars.Contains(lastChar))
            {
                int lastPos = this.position;
                string idBuffer = readWhileChar(idChars);
                if (idBuffer == "null" || idBuffer == "true" || idBuffer == "false")
                {
                    return new KeywordNode(idBuffer, lastPos, Color.DarkOrange);
                }
                else if (idBuffer == "alloc")
                {
                    readWhileChar(whiteSpaces);
                    matchChar(readChar(), '[');
                    INode allocSize = tokenizeValue();
                    matchChar(readChar(), ']');
                    return new UnaryOperatorNode(UnaryOperatorNode.UnaryOperator.Alloc, allocSize);
                }
                else if (idBuffer == "ref")
                {
                    readWhileChar(whiteSpaces);
                    INode targetVar;
                    matchTok(targetVar = tokenizeValue(), typeof(ReferenceNode));
                    return new UnaryOperatorNode(UnaryOperatorNode.UnaryOperator.Reference, targetVar);
                }
                else if (idBuffer == "extern" || idBuffer == "goproc" || idBuffer == "new" || idBuffer == "inc" || idBuffer == "dec")
                {
                    readWhileChar(whiteSpaces);
                    return this.statementTokenizers[idBuffer](idBuffer, lastPos);
                }
                else
                {
                    List<INode> accessors = new List<INode>();
                    accessors.Add(new KeywordNode(idBuffer, lastPos, Color.CornflowerBlue));
                    while (peekChar() == '.' || peekChar() == '[')
                    {
                        char accessorType = peekChar();
                        readChar();
                        if (accessorType == '.')
                            accessors.Add(tokenizeKeyword(Color.CornflowerBlue));
                        else
                        {
                            accessors.Add(tokenizeExpression());
                            readWhileChar(whiteSpaces);
                            matchChar(readChar(), ']');
                        }
                    }
                    return new ReferenceNode(accessors);
                }
            }
            throw new UnexpectedTokenException(this.position);
        }

        BinaryOperatorNode.BinaryOperator tokenizeBinOp()
        {
            char c = readChar();
            switch (c)
            {
                case '+':
                    return BinaryOperatorNode.BinaryOperator.Add;
                case '-':
                    return BinaryOperatorNode.BinaryOperator.Subtract;
                case '*':
                    return BinaryOperatorNode.BinaryOperator.Multiply;
                case '/':
                    return BinaryOperatorNode.BinaryOperator.Divide;
                case '%':
                    return BinaryOperatorNode.BinaryOperator.Modulo;
                case '^':
                    return BinaryOperatorNode.BinaryOperator.Power;
                case '=':
                    matchChar(readChar(), '=');
                    return BinaryOperatorNode.BinaryOperator.Equals;
                case '!':
                    matchChar(readChar(), '=');
                    return BinaryOperatorNode.BinaryOperator.NotEquals;
                case '>':
                    if (peekChar() == '=')
                    {
                        readChar();
                        return BinaryOperatorNode.BinaryOperator.MoreEqual;
                    }
                    return BinaryOperatorNode.BinaryOperator.More;
                case '<':
                    if (peekChar() == '=')
                    {
                        readChar();
                        return BinaryOperatorNode.BinaryOperator.LessEqual;
                    }
                    return BinaryOperatorNode.BinaryOperator.Less;
                case 'a':
                case 'o':
                    string keyword = c + readWhileChar(idChars);
                    if (keyword == "and")
                        return BinaryOperatorNode.BinaryOperator.And;
                    else if (keyword == "or")
                        return BinaryOperatorNode.BinaryOperator.Or;
                    throw new UnexpectedTokenException(this.position);
            }
            throw new UnexpectedTokenException(this.position);
        }

        INode tokenizeExpression(int minPrec = 0)
        {
            //would've used shunting-yard, but ultimatley order-of operations doesn't matter in syntax highlighting!
            INode lhs = tokenizeValue();

            readWhileChar(whiteSpaces);
            while(!end_reached && (peekChar() == '+' || peekChar() == '-' || peekChar() == '*' || peekChar() == '/' || peekChar() == '!' || peekChar() == '=' || peekChar() == '>' || peekChar() == '<' || peekChar() == '%' || peekChar() == '^' || peekChar() == 'a' || peekChar() == 'o') )
            {
                BinaryOperatorNode.BinaryOperator op = tokenizeBinOp();
                INode rhs = tokenizeValue();
                lhs = new BinaryOperatorNode(op, lhs, rhs);
                readWhileChar(whiteSpaces);
            }
            return lhs;
        }
    }
}
