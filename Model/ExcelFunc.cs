using System;
using System.IO.Packaging;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab


namespace HelloWorld.ExcelFuncNamespace
{
    public static class ExcelFunc
    {
        public static void PrintExcelData(string[,] excelData)
        {
            for (int i = 0; i < excelData.GetLength(0); i++)
            {
                for (int j = 0; j < excelData.GetLength(1); j++)
                {
                    //new line
                    if (j == 0 && i != 0) Console.Write("\r\n");
                    Console.Write(excelData[i, j] + "\t");
                }
            }
        }

        public static string[,] ReadExcelFile(string filePath, int sheetNumFrom1)
        {
            //TODO: If excel has different number of columns for different strings?
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNumFrom1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            string[,] excelData = new string[rowCount, colCount];

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        excelData[i - 1, j - 1] = xlRange.Cells[i, j].Value2.ToString();
                    }
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            return excelData;
        }
    }
}

