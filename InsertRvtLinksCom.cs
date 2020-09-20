using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using Application = Autodesk.Revit.ApplicationServices.Application;
using HelloWorld.Model;

namespace HelloWorld
{
    [TransactionAttribute(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    //TODO Create Presenter(MVP and create instanses for Model, View, Presenter here)

    public class InsertRvtLinksCom : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication rvtUIApp;
            Application m_rvtApp;

            rvtUIApp = commandData.Application;
            m_rvtApp = rvtUIApp.Application;

            string[,] excelData = ExcelFunc.ReadExcelFile(@"E:\RvtProjectConfigFile\RvrProjectConfigFile.xlsx", 2);
            int prjFileNameColumn = 0;
            FileInfo[] projectFiles = new FileInfo[excelData.GetLength(0) - 2];

            //create links
            Document newDoc = null;
            for (int i = 2; i < excelData.GetLength(0); i++)
            {
                try
                {
                    projectFiles[i - 2] = new FileInfo((string)excelData.GetValue(i, 0));
                    newDoc = m_rvtApp.OpenDocumentFile(projectFiles[i - 2].FullName);
                    for (int j = prjFileNameColumn + 1; j < excelData.GetLength(1); j++)
                    {
                        DocumentFunc.CreateRevitLink(newDoc, (string)excelData.GetValue(i, j));
                    }
                }
                finally { }
            }

            return Result.Succeeded;
        }
    }
}