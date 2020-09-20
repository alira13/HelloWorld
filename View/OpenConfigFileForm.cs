using System;
using System.Windows.Forms;

namespace HelloWorld.View
{
    public interface IOpenConfigFileForm
    {
        string FilePath { get; }
        event EventHandler FileOpenClick;
        event EventHandler SaveClick;
    }

    public partial class OpenConfigFileForm : Form, IOpenConfigFileForm
    {
        public OpenConfigFileForm()
        {
            InitializeComponent();
            butOpenFile.Click += ButOpenFile_Click;
            butSaveFile.Click += ButSaveFile_Click;
        }

        #region Проброс событий
        private void ButOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }

        private void ButSaveFile_Click(object sender, EventArgs e)
        {
            if (SaveClick != null) SaveClick(this, EventArgs.Empty);
        }
        #endregion

        #region IOPenConfigFileForm
        public string FilePath
        {
            get {return fldFilePath.Text; }
        }

        public event EventHandler FileOpenClick;
        public event EventHandler SaveClick;
        #endregion

        private void OpenConfigFileForm_Load(object sender, EventArgs e)
        {

        }

        private void butSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Все файлы Excel|*.xlsx|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = dlg.FileName;
                if (FileOpenClick != null)
                    FileOpenClick(this, EventArgs.Empty);
            }
        }

        private void butOpenFile_Click(object sender, EventArgs e)
        {

        }

        private void butSaveFile_Click(object sender, EventArgs e)
        {

        }
    }
}
