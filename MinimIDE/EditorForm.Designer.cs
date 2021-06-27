
namespace MinimIDE
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Layout = new System.Windows.Forms.TableLayoutPanel();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.EditorTabs = new System.Windows.Forms.TabControl();
            this.ControlSections = new System.Windows.Forms.TabControl();
            this.MainControls = new System.Windows.Forms.TabPage();
            this.FileGroupBox = new System.Windows.Forms.GroupBox();
            this.SaveCurrentButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.SaveAllButton = new System.Windows.Forms.Button();
            this.SaveAndCloseButton = new System.Windows.Forms.Button();
            this.NewTabButton = new System.Windows.Forms.Button();
            this.MinimaGroupBox = new System.Windows.Forms.GroupBox();
            this.NewInstanceButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.Layout.SuspendLayout();
            this.ControlSections.SuspendLayout();
            this.MainControls.SuspendLayout();
            this.FileGroupBox.SuspendLayout();
            this.MinimaGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Layout
            // 
            this.Layout.ColumnCount = 1;
            this.Layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Layout.Controls.Add(this.OutputLabel, 0, 2);
            this.Layout.Controls.Add(this.EditorTabs, 0, 1);
            this.Layout.Controls.Add(this.ControlSections, 0, 0);
            this.Layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layout.Location = new System.Drawing.Point(0, 0);
            this.Layout.Name = "Layout";
            this.Layout.RowCount = 3;
            this.Layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.Layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.Layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.Layout.Size = new System.Drawing.Size(1050, 672);
            this.Layout.TabIndex = 0;
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputLabel.Location = new System.Drawing.Point(3, 637);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(1044, 35);
            this.OutputLabel.TabIndex = 1;
            // 
            // EditorTabs
            // 
            this.EditorTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorTabs.Location = new System.Drawing.Point(3, 103);
            this.EditorTabs.Name = "EditorTabs";
            this.EditorTabs.SelectedIndex = 0;
            this.EditorTabs.Size = new System.Drawing.Size(1044, 531);
            this.EditorTabs.TabIndex = 2;
            // 
            // ControlSections
            // 
            this.ControlSections.Controls.Add(this.MainControls);
            this.ControlSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlSections.Location = new System.Drawing.Point(3, 3);
            this.ControlSections.Name = "ControlSections";
            this.ControlSections.SelectedIndex = 0;
            this.ControlSections.Size = new System.Drawing.Size(1044, 94);
            this.ControlSections.TabIndex = 3;
            // 
            // MainControls
            // 
            this.MainControls.Controls.Add(this.MinimaGroupBox);
            this.MainControls.Controls.Add(this.FileGroupBox);
            this.MainControls.Location = new System.Drawing.Point(4, 22);
            this.MainControls.Name = "MainControls";
            this.MainControls.Padding = new System.Windows.Forms.Padding(3);
            this.MainControls.Size = new System.Drawing.Size(1036, 68);
            this.MainControls.TabIndex = 1;
            this.MainControls.Text = "Main";
            this.MainControls.UseVisualStyleBackColor = true;
            // 
            // FileGroupBox
            // 
            this.FileGroupBox.Controls.Add(this.NewTabButton);
            this.FileGroupBox.Controls.Add(this.SaveAndCloseButton);
            this.FileGroupBox.Controls.Add(this.SaveAllButton);
            this.FileGroupBox.Controls.Add(this.OpenFileButton);
            this.FileGroupBox.Controls.Add(this.SaveCurrentButton);
            this.FileGroupBox.Location = new System.Drawing.Point(6, 6);
            this.FileGroupBox.Name = "FileGroupBox";
            this.FileGroupBox.Size = new System.Drawing.Size(411, 55);
            this.FileGroupBox.TabIndex = 3;
            this.FileGroupBox.TabStop = false;
            this.FileGroupBox.Text = "File";
            // 
            // SaveCurrentButton
            // 
            this.SaveCurrentButton.Location = new System.Drawing.Point(168, 19);
            this.SaveCurrentButton.Name = "SaveCurrentButton";
            this.SaveCurrentButton.Size = new System.Drawing.Size(75, 23);
            this.SaveCurrentButton.TabIndex = 3;
            this.SaveCurrentButton.Text = "Save";
            this.SaveCurrentButton.UseVisualStyleBackColor = true;
            this.SaveCurrentButton.Click += new System.EventHandler(this.SaveCurrentButton_Click);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(87, 19);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(75, 23);
            this.OpenFileButton.TabIndex = 2;
            this.OpenFileButton.Text = "Open";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // SaveAllButton
            // 
            this.SaveAllButton.Location = new System.Drawing.Point(249, 19);
            this.SaveAllButton.Name = "SaveAllButton";
            this.SaveAllButton.Size = new System.Drawing.Size(75, 23);
            this.SaveAllButton.TabIndex = 4;
            this.SaveAllButton.Text = "SaveAll";
            this.SaveAllButton.UseVisualStyleBackColor = true;
            this.SaveAllButton.Click += new System.EventHandler(this.SaveAllButton_Click);
            // 
            // SaveAndCloseButton
            // 
            this.SaveAndCloseButton.Location = new System.Drawing.Point(330, 19);
            this.SaveAndCloseButton.Name = "SaveAndCloseButton";
            this.SaveAndCloseButton.Size = new System.Drawing.Size(75, 23);
            this.SaveAndCloseButton.TabIndex = 5;
            this.SaveAndCloseButton.Text = "Close";
            this.SaveAndCloseButton.UseVisualStyleBackColor = true;
            this.SaveAndCloseButton.Click += new System.EventHandler(this.SaveAndCloseButton_Click);
            // 
            // NewTabButton
            // 
            this.NewTabButton.Location = new System.Drawing.Point(6, 19);
            this.NewTabButton.Name = "NewTabButton";
            this.NewTabButton.Size = new System.Drawing.Size(75, 23);
            this.NewTabButton.TabIndex = 6;
            this.NewTabButton.Text = "New";
            this.NewTabButton.UseVisualStyleBackColor = true;
            this.NewTabButton.Click += new System.EventHandler(this.NewTabButton_Click);
            // 
            // MinimaGroupBox
            // 
            this.MinimaGroupBox.Controls.Add(this.RunButton);
            this.MinimaGroupBox.Controls.Add(this.NewInstanceButton);
            this.MinimaGroupBox.Location = new System.Drawing.Point(424, 6);
            this.MinimaGroupBox.Name = "MinimaGroupBox";
            this.MinimaGroupBox.Size = new System.Drawing.Size(170, 55);
            this.MinimaGroupBox.TabIndex = 4;
            this.MinimaGroupBox.TabStop = false;
            this.MinimaGroupBox.Text = "Minima";
            // 
            // NewInstanceButton
            // 
            this.NewInstanceButton.Location = new System.Drawing.Point(6, 19);
            this.NewInstanceButton.Name = "NewInstanceButton";
            this.NewInstanceButton.Size = new System.Drawing.Size(75, 23);
            this.NewInstanceButton.TabIndex = 6;
            this.NewInstanceButton.Text = "REPL";
            this.NewInstanceButton.UseVisualStyleBackColor = true;
            this.NewInstanceButton.Click += new System.EventHandler(this.NewInstanceButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(87, 19);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 7;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 672);
            this.Controls.Add(this.Layout);
            this.Name = "EditorForm";
            this.Text = "MinimIDE";
            this.Layout.ResumeLayout(false);
            this.Layout.PerformLayout();
            this.ControlSections.ResumeLayout(false);
            this.MainControls.ResumeLayout(false);
            this.FileGroupBox.ResumeLayout(false);
            this.MinimaGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Layout;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.TabControl EditorTabs;
        private System.Windows.Forms.TabControl ControlSections;
        private System.Windows.Forms.TabPage MainControls;
        private System.Windows.Forms.GroupBox FileGroupBox;
        private System.Windows.Forms.Button SaveCurrentButton;
        private System.Windows.Forms.Button SaveAllButton;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button SaveAndCloseButton;
        private System.Windows.Forms.Button NewTabButton;
        private System.Windows.Forms.GroupBox MinimaGroupBox;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button NewInstanceButton;
    }
}

