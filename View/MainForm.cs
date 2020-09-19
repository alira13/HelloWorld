using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloWorld
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        event EventHandler FileOpenClick;
        event EventHandler SaveClick;
        event EventHandler ContentChanged;

    }
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            butOpenFile.Click += ButOpenFile_Click;
            butSaveFile.Click += ButSaveFile_Click;
            fldContent.TextChanged += FldContent_TextChanged;
            butSelectFile.Click += ButSelectFile_Click;
        }

        private void ButSelectFile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #region Проброс событий
        private void FldContent_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null) ContentChanged(this, EventArgs.Empty);
        }

        private void ButOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }

        private void ButSaveFile_Click(object sender, EventArgs e)
        {
            if (SaveClick != null) SaveClick(this, EventArgs.Empty);
        }
        #endregion
        #region IMainForm
        public string FilePath
        {
            get { return fldFilePath.Text; }
        }

        public string Content { 
            get { return fldContent.Text; } 
            set { fldContent.Text = value; }
        }

        public event EventHandler FileOpenClick;
        public event EventHandler SaveClick;
        public event EventHandler ContentChanged;

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void butSelectFile_Click_1(object sender, EventArgs e)
        {

        }

        private void butOpenFile_Click_1(object sender, EventArgs e)
        {

        }

        private void butSaveFile_Click_1(object sender, EventArgs e)
        {

        }
    }
}
