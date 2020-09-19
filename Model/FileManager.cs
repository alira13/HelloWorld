using System;
using System.Text;
using System.IO;

namespace HelloWorld.FileManagerNamespace
{
    public class FileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        public bool IsExist(string filePath)
        {
            if (File.Exists(filePath)) return true;
            else return false;
        }

        public string GetContent(string filePath, Encoding encoding)
        {
            string content = File.ReadAllText(filePath, encoding);
            return content;
        }

        public string GetContent(string FilePath)
        {
            return GetContent(FilePath, _defaultEncoding);
        }

        public void SaveContent(string content, string filePath, Encoding encoding)
        {
            File.WriteAllText(filePath, content, encoding);
        }

        public void SaveContent(string content, string filePath)
        {
            SaveContent(filePath, content, _defaultEncoding);
        }
    }
}
