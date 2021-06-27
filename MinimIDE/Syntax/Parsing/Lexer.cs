using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax.Parsing
{
    public class UnexpectedTokenException : Exception
    {
        public int Position
        {
            get; private set;
        }

        public UnexpectedTokenException(int Position)
        {
            this.Position = Position;
        }
    }

    public partial class Lexer
    {
        private delegate INode StatmentTokenizer(string keyword, int pos);

        private static char[] whiteSpaces =
        {
            ' ',
            '\t',
            '\r',
            '\n'
        };

        private static char[] digits =
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            '.'
        };

        private static char[] idChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_0123456789".ToArray();

        public static INode Tokenize(RichTextBox editor)
        {
            Lexer lexer = new Lexer(editor);
            
            CodeBlock block = new CodeBlock(0);
            lexer.tokenizeBody(block);

            return block;
        }

        private Dictionary<string, StatmentTokenizer> statementTokenizers = new Dictionary<string, StatmentTokenizer>();

        private RichTextBox editor;
        private StringReader reader;
        int position;
        bool end_reached;
        private int current_depth;

        public Lexer(RichTextBox editor)
        {
            this.editor = editor;
            this.position = 0;
            this.current_depth = 0;
            this.reader = new StringReader(editor.Text);
            statementTokenizers.Add("set", this.tokenizeSet);
            statementTokenizers.Add("return", this.tokenizeReturn);
            statementTokenizers.Add("goproc", this.tokenizeGoto);
            statementTokenizers.Add("extern", this.tokenizeExtern);
            statementTokenizers.Add("include", this.tokenizeInclude);
            statementTokenizers.Add("proc", this.tokenizeProcedure);
            statementTokenizers.Add("if", this.tokenizeConditional);
            statementTokenizers.Add("elif", this.tokenizeConditional);
            statementTokenizers.Add("else", this.tokenizeConditional);
            statementTokenizers.Add("while", this.tokenizeConditional);
            statementTokenizers.Add("rem", this.tokenizeRemark);
            statementTokenizers.Add("record", this.tokenizeRecord);
            statementTokenizers.Add("new", this.tokenizeCreateRecord);
            statementTokenizers.Add("inc", this.tokenizeCrement);
            statementTokenizers.Add("dec", this.tokenizeCrement);

            if (editor.Text.Length == 0)
                end_reached = true;
        }

        private char readChar()
        {
            position++;
            int c = reader.Read();
            if(c == -1)
                throw new InvalidOperationException("Reader already at EOF");

            if (reader.Peek() == -1)
                end_reached = true;

            return (char)c;
        }

        private char peekChar()
        {
            int c = reader.Peek();
            if (c == -1)
                throw new InvalidOperationException("Reader already at EOF");
            return (char)c;
        }

        private string readWhileChar(params char[] readWhile)
        {
            string buffer = string.Empty;
            while (!end_reached && readWhile.Contains(peekChar()))
                buffer += readChar();
            return buffer;
        }

        private string readTillChar(params char[] readTill)
        {
            string buffer = string.Empty;
            while (!end_reached && !readTill.Contains(peekChar()))
                buffer += readChar();
            return buffer;
        }

        private KeywordNode tokenizeKeyword(Color highlightColor)
        {
            readWhileChar(whiteSpaces);
            int start = this.position;
            return new KeywordNode(readWhileChar(idChars), start, highlightColor);
        }

        private void matchChar(char tomatch, char expected)
        {
            if (tomatch != expected)
                throw new UnexpectedTokenException(this.position);
        }

        private void matchTok(INode node, Type type)
        {
            if (node.GetType() != type)
                throw new UnexpectedTokenException(this.position);
        }

        private void matchKeyword(KeywordNode keyword, string expected)
        {
            if (keyword.Identifier != expected)
                throw new UnexpectedTokenException(this.position);
        }

        private void tokenizeBody(CodeBlock codeBlock)
        {
            if (!end_reached && peekChar() == '{')
                readChar();
            readWhileChar(whiteSpaces);
            while(!end_reached && peekChar() != '}')
            {
                readWhileChar(whiteSpaces);

                int start = this.position;
                string keyword = readWhileChar(idChars);

                readWhileChar(whiteSpaces);

                try
                {
                    codeBlock.Append(statementTokenizers[keyword].Invoke(keyword, start));
                }
                catch (KeyNotFoundException)
                {
                    throw new UnexpectedTokenException(this.position);
                }

                readWhileChar(whiteSpaces);
            }

            if (current_depth > 0)
            {
                matchChar(peekChar(), '}');
                readChar();
            }
            current_depth--;
        }
    }
}
