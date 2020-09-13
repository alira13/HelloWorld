namespace HelloWorld
{
    partial class MainForm
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
            this.lblSelectFilePath = new System.Windows.Forms.Label();
            this.fldFilePath = new System.Windows.Forms.TextBox();
            this.butSelectFile = new System.Windows.Forms.Button();
            this.butOpenFile = new System.Windows.Forms.Button();
            this.fldContent = new System.Windows.Forms.TextBox();
            this.butSaveFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSelectFilePath
            // 
            this.lblSelectFilePath.AutoSize = true;
            this.lblSelectFilePath.Location = new System.Drawing.Point(12, 16);
            this.lblSelectFilePath.Name = "lblSelectFilePath";
            this.lblSelectFilePath.Size = new System.Drawing.Size(113, 17);
            this.lblSelectFilePath.TabIndex = 0;
            this.lblSelectFilePath.Text = "Выберете файл";
            //this.lblSelectFilePath.Click += new System.EventHandler(this.lblOpenFile_Click);
            // 
            // fldFilePath
            // 
            this.fldFilePath.Location = new System.Drawing.Point(131, 13);
            this.fldFilePath.Name = "fldFilePath";
            this.fldFilePath.Size = new System.Drawing.Size(451, 22);
            this.fldFilePath.TabIndex = 1;
            // 
            // butSelectFile
            // 
            this.butSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSelectFile.Location = new System.Drawing.Point(595, 12);
            this.butSelectFile.Name = "butSelectFile";
            this.butSelectFile.Size = new System.Drawing.Size(75, 39);
            this.butSelectFile.TabIndex = 2;
            this.butSelectFile.Text = "Выбрать";
            this.butSelectFile.UseVisualStyleBackColor = true;
            // 
            // butOpenFile
            // 
            this.butOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butOpenFile.Location = new System.Drawing.Point(676, 12);
            this.butOpenFile.Name = "butOpenFile";
            this.butOpenFile.Size = new System.Drawing.Size(77, 39);
            this.butOpenFile.TabIndex = 3;
            this.butOpenFile.Text = "Открыть";
            this.butOpenFile.UseVisualStyleBackColor = true;
            // 
            // fldContent
            // 
            this.fldContent.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.fldContent.Location = new System.Drawing.Point(15, 70);
            this.fldContent.Multiline = true;
            this.fldContent.Name = "fldContent";
            this.fldContent.Size = new System.Drawing.Size(740, 353);
            this.fldContent.TabIndex = 4;
            // 
            // butSaveFile
            // 
            this.butSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butSaveFile.Location = new System.Drawing.Point(652, 429);
            this.butSaveFile.Name = "butSaveFile";
            this.butSaveFile.Size = new System.Drawing.Size(103, 49);
            this.butSaveFile.TabIndex = 5;
            this.butSaveFile.Text = "Cохранить";
            this.butSaveFile.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 490);
            this.Controls.Add(this.butSaveFile);
            this.Controls.Add(this.fldContent);
            this.Controls.Add(this.butOpenFile);
            this.Controls.Add(this.butSelectFile);
            this.Controls.Add(this.fldFilePath);
            this.Controls.Add(this.lblSelectFilePath);
            this.Name = "MainForm";
            this.Text = "Выбор конфигрурационного файа";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectFilePath;
        private System.Windows.Forms.TextBox fldFilePath;
        private System.Windows.Forms.Button butSelectFile;
        private System.Windows.Forms.Button butOpenFile;
        private System.Windows.Forms.TextBox fldContent;
        private System.Windows.Forms.Button butSaveFile;
    }
}