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

[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]

public class HelloWorldCom : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message,  ElementSet elements)
    {
        UIApplication rvtUIApp;
       // UIDocument rvtUIDoc;
        Application m_rvtApp;
        //Document m_rvtDoc;

        DocumentFunc docFuc = new DocumentFunc();

        rvtUIApp = commandData.Application;
        //rvtUIDoc = rvtUIApp.ActiveUIDocument;
        m_rvtApp = rvtUIApp.Application;
        //m_rvtDoc = rvtUIDoc.Document;

        //---------------------------------------create files------------------------------------
        /*
        List<string> templateFileName = new List<string> { @"ADSK_ШаблонПроекта_ОВ_r2019_v1.0.1.rte" , @"ADSK_ШаблонПроекта_ОВ_r2019_v1.0.1.rte" };
        List<string> templateDir = new List<string>{ @"E:\Файлы проектов", @"E:\Файлы проектов" };

        List<string> prjFileName = new List<string> { @"Проект1.rvt", @"Проект2.rvt" };
        List<string> prjDir = new List<string> { @"E:\Файлы проектов1", @"E:\Файлы проектов2" };

        List<string> linkPath = new List<string> { @"E:\Файлы проектов\Проект2.rvt", @"E:\Файлы проектов\Проект2.rvt", @"E:\Файлы проектов\Проект2.rvt" };

        if(templateFileName.Count!= prjFileName.Count) { TaskDialog.Show("Error", "Количество проектов не совпадает с кодичеством шаблонов"); }

        FileInfo [] templateFiles = new FileInfo[prjFileName.Count];
        FileInfo [] projectFiles = new FileInfo[prjFileName.Count];

        for (int i=0; i<templateFileName.Count; i++) {
            templateFiles[i] = new FileInfo(Path.Combine(templateDir[i], templateFileName[i]));
            projectFiles[i] = new FileInfo(Path.Combine(prjDir[i], prjFileName[i]));
            docFuc.CreateNewProjectByTemplate(m_rvtApp, templateFiles[i], templateFiles[i]);
        }

        //Document newDoc = m_rvtApp.OpenDocumentFile(projectFile.FullName);
        //docFuc.CreateRevitLink(newDoc, linkPath);
        //docFuc.CreateWorksets(newDoc);
        //GetAllElemOfCategory(newDoc, BuiltInCategory.OST_Levels);
        //GetAllElemOfCategory(newDoc, BuiltInCategory.OST_Grids);
        //newDoc.Save();
        //newDoc.Close();
        */

        //string[][] exData = ExcelData.getExcelFile(@"D:\OneDrive\RvtProjectConfigFile\RvrProjectConfigFile.xlsx");

        return Result.Succeeded;
    }
}