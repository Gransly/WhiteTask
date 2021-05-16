using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Utrom
{
    class File : IFile
    {
        string path;
        public string GetPath() 
        {
            return path;
        }

        public File(string path) 
        {
            this.path = path;
        }

        public void DeleteFile()
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

        }
        public string ReadFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        public void RenameFile(bool sameNames)
        {
            string pathWithoutUUID = PathWithoutUUID();
            if (sameNames && System.IO.File.Exists(path))
            {
                string[] paths = pathWithoutUUID.Split('.');
                pathWithoutUUID = paths[0] + " (1)." + paths[1];
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Move(path, pathWithoutUUID);
            }
            
        }

        public string PathWithoutUUID()
        {
            string pattern = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(path, replacement);
            return result;
        }
    }
}
