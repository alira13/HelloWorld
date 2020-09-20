using System;
using System.Text;
using System.IO;

//TODO delete this code

namespace HelloWorld.Model
{
    public class FileManager
    {
        private static readonly Encoding defaultEncoding = Encoding.GetEncoding(1251);

        public static bool IsExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public string GetContent(string filePath, Encoding encoding)
        {
            return File.ReadAllText(filePath, encoding);
        }

        public string GetContent(string FilePath)
        {
            return GetContent(FilePath, defaultEncoding);
        }

        public void SaveContent(string content, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, content, encoding);
        }

        public void SaveContent(string content, string filePath)
        {
            SaveContent(filePath, content, defaultEncoding);
        }
    }
}
