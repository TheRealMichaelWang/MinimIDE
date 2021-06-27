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
        private INode tokenizeRecord(string keyword, int pos)
        {
            KeywordNode labelKeyword = tokenizeKeyword(Color.CornflowerBlue);

            readWhileChar(whiteSpaces);

            KeywordNode extend = null;
            KeywordNode extendLabel = null;
            if (peekChar() == 'e')
            {
                extend = tokenizeKeyword(Color.DarkBlue);
                extendLabel = tokenizeKeyword(Color.CornflowerBlue);
                readWhileChar(whiteSpaces);
            }

            matchChar(readChar(), '{');

            RecordNode recordNode = new RecordNode(labelKeyword, extend, extendLabel, pos);

            while(peekChar() != '}')
            {
                readWhileChar(whiteSpaces);

                int prop_pos = this.position;

                if (!idChars.Contains(peekChar()))
                    throw new UnexpectedTokenException(this.position);

                string buffer = readWhileChar(idChars);
                readWhileChar(whiteSpaces);
                if (buffer == "proc" || buffer == "rem")
                    recordNode.Append(this.statementTokenizers[buffer](buffer, prop_pos));
                else
                    recordNode.Append(new KeywordNode(buffer, prop_pos, Color.CornflowerBlue));

                readWhileChar(whiteSpaces);
            }
            readChar();

            return recordNode;
        }

        private INode tokenizeCreateRecord(string keyword, int pos)
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
            return new CreateRecordNode(labelKeyword, arguments, pos);
        }
    }
}
