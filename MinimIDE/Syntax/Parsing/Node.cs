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
        private INode tokenizeSet(string keyword, int pos)
        {
            INode value = tokenizeExpression();
            matchTok(value, typeof(ReferenceNode));
            ReferenceNode dest = value as ReferenceNode;

            KeywordNode to = tokenizeKeyword(Color.DarkBlue);

            INode node = tokenizeExpression();

            return new SetNode(dest, node, pos);
        }

        private INode tokenizeReturn(string keyword, int pos)
        {
            INode value = tokenizeExpression();
            return new ReturnNode(value, pos);
        }

        private INode tokenizeGoto(string keyword, int pos)
        {
            KeywordNode labelKeyword = tokenizeKeyword(Color.CornflowerBlue);

            readWhileChar(whiteSpaces);

            KeywordNode asKeyword = null;
            INode targetObject = null;

            if(peekChar() == 'a')
            {
                asKeyword = tokenizeKeyword(Color.DarkBlue);
                matchKeyword(asKeyword, "as");
                targetObject = tokenizeExpression();
            }

            List<INode> arguments = new List<INode>();

            readWhileChar(whiteSpaces);
            if(peekChar() == '(')
            {
                readChar();
                while(peekChar() != ')')
                {
                    readWhileChar(',');
                    arguments.Add(tokenizeExpression());
                    readTillChar(',', ')');
                }
                readChar();
            }

            if (asKeyword != null)
                return new GotoAsNode(labelKeyword, pos, targetObject, arguments);
            else
                return new GotoNode(labelKeyword, pos, arguments);
        }

        private INode tokenizeExtern(string keyword, int pos)
        {
            KeywordNode labelKeyword = tokenizeKeyword(Color.CornflowerBlue);

            readWhileChar(whiteSpaces);
            List<INode> arguments = new List<INode>();

            readWhileChar(whiteSpaces);
            if (peekChar() == '(')
            {
                readChar();
                while (peekChar() != ')')
                {
                    readWhileChar(',');
                    arguments.Add(tokenizeExpression());
                    readTillChar(',', ')');
                }
                readChar();
            }
            return new ExternalGoto(labelKeyword, pos, arguments);
        }

        private INode tokenizeInclude(string keyword, int pos)
        {
            StringLiteralNode filePathLiteral = tokenizeStringLiteral();
            return new IncludeNode(filePathLiteral, pos);
        }

        private INode tokenizeRemark(string keyword, int pos)
        {
            string remarks = readTillChar('\n');
            return new RemarkNode(pos, this.position, remarks);
        }
    }
}
