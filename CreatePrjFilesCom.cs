using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.IO;
using Application = Autodesk.Revit.ApplicationServices.Application;
using System.Windows.Forms;
using System;
using HelloWorld.ExcelFuncNamespace;
using HelloWorld.DocumentFuncNamespace;

[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]

    public class CreatePrjFilesCom : IExternalCommand
    {
        public void UserMessageFunc(string funcName, string userMessage)
        {
            TaskDialog.Show(funcName, userMessage);
        }


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication rvtUIApp;
            //UIDocument rvtUIDoc;
            Application m_rvtApp;
            //Document m_rvtDoc;

            rvtUIApp = commandData.Application;
            //rvtUIDoc = rvtUIApp.ActiveUIDocument;
            m_rvtApp = rvtUIApp.Application;
            //m_rvtDoc = rvtUIDoc.Document;

            string[,] excelData = ExcelFunc.ReadExcelFile(@"E:\RvtProjectConfigFile\RvrProjectConfigFile.xlsx", 1);
            int prjFileNameColumn = 0;
            int templateFileNameColumn = 1;

            FileInfo[] templateFiles = new FileInfo[excelData.GetLength(0) - 2];
            FileInfo[] projectFiles = new FileInfo[excelData.GetLength(0) - 2];
            DocumentFunc docFuc = new DocumentFunc();
            docFuc.userMessagefunc = UserMessageFunc;

            //Action<string, string> userMessageFunc = UserMessageFunc;

            TaskDialog.Show("Excel size", excelData.GetLength(0) + "x" + excelData.GetLength(1));
            for (int i = 2; i < excelData.GetLength(0); i++)
            {

                //MessageBox.Show("Create file: " + (string)excelData.GetValue(i, prjFileNameColumn) + " with template:" + (string)excelData.GetValue(i, templateFileNameColumn));
                templateFiles[i - 2] = new FileInfo((string)excelData.GetValue(i, templateFileNameColumn));
                projectFiles[i - 2] = new FileInfo((string)excelData.GetValue(i, prjFileNameColumn));
                docFuc.CreateNewProjectByTemplate(m_rvtApp, templateFiles[i - 2], projectFiles[i - 2]);
            }

            return Result.Succeeded;
        }
    }