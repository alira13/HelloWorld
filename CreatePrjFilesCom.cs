using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using Application = Autodesk.Revit.ApplicationServices.Application;
using HelloWorld.Model;
using HelloWorld.View;

namespace HelloWorld
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    public class CreatePrjFilesCom : IExternalCommand
    {
        //TODO Create Presenter(MVP and create instanses for Model, View, Presenter here)
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication rvtUIApp;
            Application m_rvtApp;

            rvtUIApp = commandData.Application;
            m_rvtApp = rvtUIApp.Application;

            //TODO get filepath from OpenConfigFileForm
            //TODO move config file example to project directory

            string[,] excelData = ExcelFunc.ReadExcelFile(@"E:\RvtProjectConfigFile\RvrProjectConfigFile.xlsx", 1);
            int prjFileNameColumn = 0;
            int templateFileNameColumn = 1;

            UserMessageFunc messageFunc = new UserMessageFunc();

            DocumentFunc.userMessagefunc = messageFunc.UserMessage;

            for (int i = 2; i < excelData.GetLength(0); i++)
            {
                var templateFiles = new FileInfo((string)excelData.GetValue(i, templateFileNameColumn));
                var projectFiles = new FileInfo((string)excelData.GetValue(i, prjFileNameColumn));
                DocumentFunc.CreateNewProjectByTemplate(m_rvtApp, templateFiles, projectFiles);
            }

            return Result.Succeeded;
        }
    }
}