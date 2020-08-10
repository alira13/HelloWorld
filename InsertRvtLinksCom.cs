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

[TransactionAttribute(TransactionMode.ReadOnly)]

public class InsertRvtLinksCom : IExternalCommand
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

        //file info
        List<string> prjFileName = new List<string> { @"Проект1.rvt", @"Проект2.rvt" };
        List<string> prjDir = new List<string> { @"E:\Файлы проектов1", @"E:\Файлы проектов2" };
        FileInfo[] projectFiles = new FileInfo[prjFileName.Count];
        string [] linkPath = new string[] { @"E:\Файлы проектов1\Проект1.rvt", @"E:\Файлы проектов\Проект2.rvt", @"E:\Файлы проектов\Проект2.rvt" };
        //createlinks
        for (int i = 0; i < prjFileName.Count; i++)
        {
            projectFiles[i] = new FileInfo(Path.Combine(prjDir[i], prjFileName[i]));
            Document newDoc = m_rvtApp.OpenDocumentFile(projectFiles[i].FullName);
            for(int j=0; j< linkPath.Length; j++)
            {
                docFuc.CreateRevitLink(newDoc, linkPath[j]);
            }
            newDoc.Save();
            newDoc.Close();
        }
        
        return Result.Succeeded;
    }
}