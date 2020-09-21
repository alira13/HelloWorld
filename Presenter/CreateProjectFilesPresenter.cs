using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.View;
using HelloWorld.Model;
using System.IO;

namespace HelloWorld.Presenter
{
    public class CreateProjectFilesPresenter
    {
        private readonly IOpenConfigFileForm _form;
        private readonly ICreateProjectFileInfo _fileInfo;
        private readonly IUserMessageFunc _messageFunc;

        public CreateProjectFilesPresenter(IOpenConfigFileForm form, ICreateProjectFileInfo fileInfo, IUserMessageFunc messageFunc)
        {
            _form = form;
            _fileInfo = fileInfo;
            _messageFunc = messageFunc;

            _form.FileOpenClick += _form_FileOpenClick;
            _form.SaveClick += _form_SaveClick;
        }

        private void _form_SaveClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _form_FileOpenClick(object sender, EventArgs e)
        {
            try {
                string filePath = _form.FilePath;
                bool isExist = _fileInfo.IsExist(filePath);
                if (!isExist)
                {
                    _messageFunc.UserError("Выбранный файл не существует");
                    return;
                }

                FileInfo[] projectFiles;
                FileInfo[] templateFiles;

                _fileInfo.GetCreateProjectInfo(filePath, out projectFiles, out templateFiles);
            }
            catch(Exception ex) {
                _messageFunc.UserError(ex.Message);
            }
        }
    }
}
