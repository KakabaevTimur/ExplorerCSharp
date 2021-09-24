using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExplorerCSharp
{
    public class FileSystem
    {
        public static string GetContent(string path)
        {
            if (path == null)
            {
                return "Directory does not exist";
            }

            DirectoryInfo di = new DirectoryInfo(path);

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(path) == false)
            {
                return "Directory does not exist";
            }

            // Copy each file into it's new directory.
            Dictionary<string, long> files = new Dictionary<string, long>();

            string result = "";
            foreach (FileInfo fi in di.GetFiles())
            {
                files.Add(fi.Name, GetSize(fi));
                // result += fi.Name + " " + GetSize(fi) + " <br>";
            }

            foreach (DirectoryInfo subdi in di.GetDirectories())
            {
                files.Add("<a href=\"?path=" + subdi.FullName + "\"> " + subdi.Name + "</a>", GetSize(subdi));
                //result += "<a href=\"?path=" + subdi.FullName + "\"> " + subdi.Name + "</a>" + " " + GetSize(subdi) + " <br>";
            }

            foreach (KeyValuePair<string, long> item in files.OrderBy(x => x.Value))
            {
                result += item.Key + " " + item.Value / 1024 + "kb" + "<br>";
            }


            string head = "<!DOCTYPE HTML><html><head><meta charset = \"utf-8\"><title>" + path + "</title></head><body>";
            string tail = "</body></html>";

            return head + result + tail;
        }

        public static long GetSize(FileInfo fileInfo)
        {
            return fileInfo.Length;
        }

        public static long GetSize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            foreach (FileInfo fi in directoryInfo.GetFiles())
            {
                size += GetSize(fi);
            }

            foreach (DirectoryInfo subdi in directoryInfo.GetDirectories())
            {
                size += GetSize(subdi);
            }

            return size;
        }

    }
}
