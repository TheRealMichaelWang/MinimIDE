using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimIDE.Syntax
{
    public partial class NumericalNode : HighlightNode
    {
        public double Double
        {
            get;private set;
        }

        public NumericalNode(double @double, int pos) : base(pos, pos + @double.ToString().Length)
        {
            this.Double = @double;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(NumericalNode).IsAssignableFrom(node.GetType()))
                return false;

            NumericalNode numerical = node as NumericalNode;

            return this.Double == numerical.Double;
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(NumericalNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            NumericalNode numerical = node as NumericalNode;
            if (this.Double != numerical.Double)
                differences.Add(node);
        }
    }

    public partial class StringLiteralNode : HighlightNode
    {
        public string String
        {
            get; private set;
        }
        
        public StringLiteralNode(string @string, int pos):base(pos, pos + @string.Length+2)
        {
            this.String = @string;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(StringLiteralNode).IsAssignableFrom(node.GetType()))
                return false;

            StringLiteralNode stringLiteral = node as StringLiteralNode;

            return this.String == stringLiteral.String;
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(StringLiteralNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }
            StringLiteralNode stringLiteral = node as StringLiteralNode;

            if (this.String != stringLiteral.String)
                differences.Add(node);
        }
    }

    public partial class ArrayLiteralNode : HighlightNode
    {
        private List<INode> array;

        public int Size
        {
            get
            {
                return array.Count;
            }
        }

        public ArrayLiteralNode(List<INode> array, int start, int stop) :base(start, stop)
        {
            this.array = array;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(ArrayLiteralNode).IsAssignableFrom(node.GetType()))
                return false;
            
            ArrayLiteralNode arrayLiteral = node as ArrayLiteralNode;

            if (arrayLiteral.Size != this.Size)
                return false;

            for(int i = 0; i < this.Size; i++)
            {
                if (!this.array.Equals(arrayLiteral.array[i]))
                    return false;
            }
            return true;
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(ArrayLiteralNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            ArrayLiteralNode arrayLiteral = node as ArrayLiteralNode;

            if (this.Size != arrayLiteral.Size)
            {
                differences.Add(node);
                return;
            }

            for (int i = 0; i < this.Size && i < arrayLiteral.Size; i++)
                this.array[i].GetDifference(differences, arrayLiteral.array[i]);
            for (int i = this.Size; i < arrayLiteral.Size; i++)
                differences.Add(arrayLiteral.array[i]);
        }
    }

    public partial class CreateRecordNode : GotoNode
    {
        public string RecordType
        {
            get
            {
                return this.labelKeyword.Identifier;
            }
        }

        public CreateRecordNode(KeywordNode labelKeyword, List<INode> arguments, int pos) : base(new KeywordNode("new", pos, Color.DarkBlue), labelKeyword, arguments)
        {

        }

        public override bool Compare(INode node)
        {
            if (!typeof(CreateRecordNode).IsAssignableFrom(node.GetType()))
                return false;

            CreateRecordNode createRecord = node as CreateRecordNode;

            if (this.RecordType != createRecord.RecordType)
                return false;

            return base.Compare(createRecord);
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(CreateRecordNode).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }

            CreateRecordNode createRecord = node as CreateRecordNode;

            if (this.RecordType != createRecord.RecordType)
            {
                differences.Add(node);
                return;
            }

            base.GetDifference(differences, createRecord);
        }
    }

    public partial class CharacterLiteral : HighlightNode
    {
        public string Char
        {
            get; private set;
        }

        public CharacterLiteral(string @char, int pos) : base(pos , pos + @char.Length + 2)
        {
            this.Char = @char;
        }

        public override bool Compare(INode node)
        {
            if (!typeof(CharacterLiteral).IsAssignableFrom(node.GetType()))
                return false;

            CharacterLiteral character = node as CharacterLiteral;

            return (this.Char == character.Char);
        }

        public override void GetDifference(List<INode> differences, INode node)
        {
            if (!typeof(CharacterLiteral).IsAssignableFrom(node.GetType()))
            {
                differences.Add(node);
                return;
            }
            CharacterLiteral character = node as CharacterLiteral;

            if (this.Char != character.Char)
            {
                differences.Add(node);
                return;
            }
        }
    }
}
