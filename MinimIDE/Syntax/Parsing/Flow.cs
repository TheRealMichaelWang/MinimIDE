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
        private INode tokenizeProcedure(string keyword, int pos)
        {
            KeywordNode labelKeyword = tokenizeKeyword(Color.CornflowerBlue);

            readWhileChar(whiteSpaces);
            List<KeywordNode> parameters = new List<KeywordNode>();

            readWhileChar(whiteSpaces);
            if (peekChar() == '(')
            {
                readChar();
                while (peekChar() != ')')
                {
                    readWhileChar(',');
                    parameters.Add(tokenizeKeyword(Color.CornflowerBlue));
                    readTillChar(',', ')');
                }
                readChar();
            }

            readWhileChar(whiteSpaces);
            matchChar(readChar(), '{');
            
            ProcedureNode procedure = new ProcedureNode(labelKeyword, parameters, pos, current_depth++);

            tokenizeBody(procedure);
            return procedure;
        }

        private INode tokenizeConditional(string keyword, int pos)
        {
            ConditionalNode.ConditinalType type;
            switch (keyword)
            {
                case "if":
                    type = ConditionalNode.ConditinalType.If;
                    break;
                case "elif":
                    type = ConditionalNode.ConditinalType.Elif;
                    break;
                case "else":
                    type = ConditionalNode.ConditinalType.Else;
                    break;
                case "while":
                    type = ConditionalNode.ConditinalType.While;
                    break;
                default:
                    throw new UnexpectedTokenException(this.position);
            }

            INode condition = null;
            if (type != ConditionalNode.ConditinalType.Else)
                condition = tokenizeExpression();

            readTillChar('{');
            ConditionalNode conditional = new ConditionalNode(type, condition, current_depth++, pos);

            tokenizeBody(conditional);

            return conditional;
        }
    }
}
