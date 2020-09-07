using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Windows.Forms;

[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]

public class CreateWorksetsCom : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        //comment
        UIApplication rvtUIApp;
        //UIDocument rvtUIDoc;
        Application m_rvtApp;
        //Document m_rvtDoc;

        rvtUIApp = commandData.Application;
        //rvtUIDoc = rvtUIApp.ActiveUIDocument;
        m_rvtApp = rvtUIApp.Application;
        //m_rvtDoc = rvtUIDoc.Document;

        DocumentFunc docFuc = new DocumentFunc();

        string[,] excelData = ExcelFunc.ReadExcelFile(@"E:\RvtProjectConfigFile\RvrProjectConfigFile.xlsx",3);
        int prjFileNameColumn = 0;
        FileInfo[] projectFiles = new FileInfo[excelData.GetLength(0)-2];

        //createlinks
        TaskDialog.Show("Excel size", excelData.GetLength(0) + "x" + excelData.GetLength(1));
        Document newDoc = null;
        for (int i = 2; i < excelData.GetLength(0); i++)
        {
            try
            {
                projectFiles[i - 2] = new FileInfo((string)excelData.GetValue(i, 0));
                newDoc = m_rvtApp.OpenDocumentFile(projectFiles[i - 2].FullName);
                docFuc.EnableWorksharing(newDoc, (string)excelData.GetValue(i, 1), (string)excelData.GetValue(i, 2));
                //for (int j = prjFileNameColumn + 1; j < excelData.GetLength(1); j++)
                //{
                //    MessageBox.Show("Create worksets in: "+(string)excelData.GetValue(i, 0)+" link:"+(string)excelData.GetValue(i, j));
                //    docFuc.CreateWorksets(newDoc, null);
                //}
            }
            finally {}
        }

        return Result.Succeeded;
    }
}