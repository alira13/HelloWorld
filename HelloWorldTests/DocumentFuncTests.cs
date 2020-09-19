using NUnit.Framework;
using Autodesk.Revit.DB;

namespace HelloWorldTests
{
    public class DocumentFunc
    {
        DocumentFunc docFunc = new DocumentFunc();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            UIApplication rvtUIApp;
            //UIDocument rvtUIDoc;
            Application m_rvtApp;
            //Document m_rvtDoc;

            rvtUIApp = commandData.Application;
            //rvtUIDoc = rvtUIApp.ActiveUIDocument;
            m_rvtApp = rvtUIApp.Application;
            //m_rvtDoc = rvtUIDoc.Document;

            DocumentFunc docFuc = new DocumentFunc();

            List<string> templateFileName = new List<string> { @"ADSK_ШаблонПроекта_ОВ_r2019_v1.0.1.rte", @"ADSK_ШаблонПроекта_ОВ_r2019_v1.0.1.rte" };
            List<string> templateDir = new List<string> { @"E:\Файлы проектов", @"E:\Файлы проектов" };

            List<string> prjFileName = new List<string> { @"Проект1.rvt", @"Проект2.rvt" };
            List<string> prjDir = new List<string> { @"E:\Файлы проектов1", @"E:\Файлы проектов2" };

            List<string> linkPath = new List<string> { @"E:\Файлы проектов\Проект2.rvt", @"E:\Файлы проектов\Проект2.rvt", @"E:\Файлы проектов\Проект2.rvt" };

            if (templateFileName.Count != prjFileName.Count) { TaskDialog.Show("Error", "Количество проектов не совпадает с кодичеством шаблонов"); }

            FileInfo[] templateFiles = new FileInfo[prjFileName.Count];
            FileInfo[] projectFiles = new FileInfo[prjFileName.Count];

            for (int i = 0; i < prjFileName.Count; i++)
            {
                templateFiles[i] = new FileInfo(Path.Combine(templateDir[i], templateFileName[i]));
                projectFiles[i] = new FileInfo(Path.Combine(prjDir[i], prjFileName[i]));
                docFuc.CreateNewProjectByTemplate(m_rvtApp, templateFiles[i], projectFiles[i]);
            }

            docFunc.CreateNewProjectByTemplate();
            Assert.Pass();
        }
    }
}