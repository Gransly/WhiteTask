using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Utrom
{
    class Folder : IFolder
    {
        string path;
        public string GetPath()
        {
            return path;
        }

        public Folder(string path) 
        {
            this.path = path;
        }

        public void DeleteFolder()
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }

        public void RenameFolder( bool sameNames)
        {
            string pathWithoutUUID = PathWithoutUUID();
            if (sameNames)
            {
                pathWithoutUUID += " (1)";
                Directory.Move(path, pathWithoutUUID);
            }
            else if (!Directory.Exists(pathWithoutUUID))
            {
                Directory.Move(path, pathWithoutUUID);
            }
        }

        public int NumberOfFileDir()
        {
            string[] files1 = GetFiles(path);
            return files1.Length;
        }

        public string[] GetFiles(string dirName)
        {
            string[] files = Directory.GetFiles(dirName);

            return files;
        }

    public bool CheckDir()            
        {
            string[] dirs = Directory.GetDirectories(path);
            if (dirs.Length > 0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDirFiles(string dir1, string dir2)
        {
            string[] files1 = GetFiles(dir1);
            string[] files2 = GetFiles(dir2);
            int sameFileCounter = 0;
            if (files1.Length != files2.Length)
            {
                return true;
            }
            for (int i = 0; i < files1.Length; i++)
            {

                File file1 = new File(files1[i]);
                string fiel1Path = file1.GetPath();
                for (int j = 0; j < files2.Length; j++)
                {
                    File file2 = new File(files2[i]);
                    string fiel2Path = file2.GetPath();

                    if (fiel1Path == fiel2Path)
                    {
                        if (String.Equals(file1.ReadFile(), file2.ReadFile()))
                        {
                            sameFileCounter++;
                        }
                    }
                }
            }
            if (sameFileCounter == files1.Length && sameFileCounter == files2.Length)               
            {
                return false;
            }
            else
            {
                return true;
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
