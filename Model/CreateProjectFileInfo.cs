using System;
using System.Text;
using System.IO;


namespace HelloWorld.Model
{
    public interface ICreateProjectFileInfo
    {
        void GetCreateProjectInfo(string filePath, out FileInfo[] projectFiles, out FileInfo[] templateFiles);
        bool IsExist(string filePath);
    }

    public class CreateProjectFileInfo : ICreateProjectFileInfo
    {
        public bool IsExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public void GetCreateProjectInfo(string filePath, out FileInfo[] projectFiles, out FileInfo[] templateFiles)
        {
            int prjFileNameColumn = 0;
            int templateFileNameColumn = 1;
            int worksheetNum = 1;
            string[,] excelData = ExcelFunc.ReadExcelFile(filePath, worksheetNum);

            projectFiles = new FileInfo[excelData.GetLength(0) - 2];
            templateFiles = new FileInfo[excelData.GetLength(0) - 2];

            for (int i = 2; i < excelData.GetLength(0); i++)
            {
                templateFiles[i-2] = new FileInfo((string)excelData.GetValue(i, templateFileNameColumn));
                projectFiles[i-2] = new FileInfo((string)excelData.GetValue(i, prjFileNameColumn));
            }
        }
    }
}
