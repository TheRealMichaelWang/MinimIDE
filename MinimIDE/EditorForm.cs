using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinimIDE.Syntax;
using MinimIDE.Syntax.Parsing;

namespace MinimIDE
{
    public partial class EditorForm : Form
    {
        public class Editor : TabPage
        {
            public bool IsUntitled
            {
                get;private set;
            }

            public bool ShouldSave;

            private RichTextBox richTextBox;
            private string filePath;

            public Editor(EditorForm parentForm)
            {
                richTextBox = new RichTextBox();
                richTextBox.Font = new Font("Consolas", 12);
                richTextBox.TextChanged += parentForm.Highlight;
                richTextBox.Dock = DockStyle.Fill;
                richTextBox.AcceptsTab = true;
                this.ShouldSave = false;
                filePath = string.Empty;
                this.Controls.Add(richTextBox);
                this.Text = "Untitled" + (parentForm.EditorTabs.TabCount + 1);
                this.Focus();
                IsUntitled = true;
            }

            public void Open(string filePath)
            {
                this.filePath = filePath;
                this.Text = filePath;
                richTextBox.Text = File.ReadAllText(filePath);
                IsUntitled = false;
                this.ShouldSave = false;
            }

            public void Save(bool prompt)
            {
                if (this.filePath != string.Empty)
                {
                    File.WriteAllText(filePath, richTextBox.Text);
                    this.ShouldSave = false;
                }
                else if (prompt)
                {
                    using (SaveFileDialog dialog = new SaveFileDialog())
                    {
                        dialog.Title = "Save " + this.Text;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            this.filePath = dialog.FileName;
                            File.WriteAllText(filePath, richTextBox.Text);
                            this.Text = filePath;
                            IsUntitled = false;
                            this.ShouldSave = false;
                        }
                    }
                }
            }
        }

        private INode lastParse;

        public EditorForm()
        {
            InitializeComponent();
            this.lastParse = null;
            NewEditorTab();
        }

        private void NewEditorTab()
        {
            TabPage tab = new Editor(this);
            this.EditorTabs.TabPages.Add(tab);
            this.EditorTabs.SelectedTab = tab;
            this.OutputLabel.Text = "READY";
        }

        private void Highlight(object sender, EventArgs e)
        {
            Editor currentEditor = EditorTabs.SelectedTab as Editor;
            currentEditor.ShouldSave = true;
            RichTextBox editor = (RichTextBox)sender;
            try
            {
                int oldstart = editor.SelectionStart;
                int oldlen = editor.SelectionLength;

                INode node = Lexer.Tokenize(editor);

                if (lastParse == null)
                    node.Highlight(editor);
                else
                {
                    List<INode> difference = new List<INode>();
                    lastParse.GetDifference(difference, node);
                    foreach (INode toHighlight in difference)
                        toHighlight.Highlight(editor);
                }

                lastParse = node;
                editor.Select(oldstart, oldlen);
                this.OutputLabel.Text = "NO ERRORS FOUND";
            }
            catch (UnexpectedTokenException error)
            {
                int line = editor.GetLineFromCharIndex(error.Position);
                this.OutputLabel.Text = "SYNTAX ERROR at ROW:" + line + ", COL:" + (error.Position - line);
            }
            catch (InvalidOperationException)
            {
                this.OutputLabel.Text = "HIGHLIGHTING...";
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog())
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    if(EditorTabs.TabCount == 0 || !((Editor)EditorTabs.SelectedTab).IsUntitled)
                        NewEditorTab();
                    Editor addedTab = EditorTabs.SelectedTab as Editor;
                    addedTab.Open(dialog.FileName);
                }
        }

        private void SaveCurrentButton_Click(object sender, EventArgs e)
        {
            Editor editorTab = EditorTabs.SelectedTab as Editor;
            editorTab.Save(true);
        }

        private void SaveAndCloseButton_Click(object sender, EventArgs e)
        {
            Editor editorTab = EditorTabs.SelectedTab as Editor;

            if (editorTab.ShouldSave)
            {
                if (MessageBox.Show("Would you like to save this file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    editorTab.Save(true);
                else
                    editorTab.Save(false);
            }
            EditorTabs.TabPages.Remove(editorTab);

            if (EditorTabs.TabCount == 0)
                NewEditorTab();
        }

        private void NewTabButton_Click(object sender, EventArgs e)
        {
            NewEditorTab();
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            foreach(Editor editorTab in EditorTabs.TabPages)
            {
                if (editorTab.ShouldSave)
                    editorTab.Save(true);
            }
        }

        private void NewInstanceButton_Click(object sender, EventArgs e)
        {
            Minima.NewInstance(null);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            Editor editorTab = EditorTabs.SelectedTab as Editor;
            if (!editorTab.IsUntitled)
            {
                if (editorTab.ShouldSave)
                {
                    editorTab.Save(false);
                    editorTab.ShouldSave = false;
                }
                Minima.NewInstance(editorTab.Text);
            }
            else if(MessageBox.Show("You must save a file before you run it? Would you like to save it now?", "Cannot run program!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                editorTab.Save(true);
                Minima.NewInstance(editorTab.Text);
            }
        }
    }
}
