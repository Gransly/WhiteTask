using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utrom
{
    class MainFolder : IMainFolder
    {
        string path;
        
        public MainFolder(string path) 
        {
            this.path = path;
        }

        public string GetPath() 
        {
            return path;
        }
        public bool CheckDirs()             
        {
            string[] dirs = GetDericrories(path);

            for (int i = 0; i < dirs.Length; i++)
            {
                Folder folder = new Folder(dirs[i]);
                return folder.CheckDir();
            }
            return false;
        }

        public void DelSameNameDirs()
        {
            string[] dirs = GetDericrories(path);
            for (int i = 0; i < dirs.Length - 1; i++)
            {
                Folder firstFolder = new  Folder(dirs[i]);
                string firstFolderPath = firstFolder.GetPath();
                for (int j = i + 1; j < dirs.Length; j++)
                {
                    Folder secondFolder = new Folder(dirs[j]);
                    string secondFolderPath = secondFolder.GetPath();
                    if (secondFolder.PathWithoutUUID() == firstFolder.PathWithoutUUID())
                    {
                        if (Directory.GetFiles(firstFolderPath).Length.Equals(0) && Directory.GetFiles(secondFolderPath).Length.Equals(0))
                        {
                            if (firstFolderPath.CompareTo(secondFolderPath) == 1)
                            {
                                secondFolder.DeleteFolder();
                            }
                            else
                            {
                                firstFolder.DeleteFolder();
                            }
                        }
                        else
                        if (!Directory.GetFiles(firstFolderPath).Length.Equals(0) && !Directory.GetFiles(secondFolderPath).Length.Equals(0))
                        {
                            if (firstFolder.CheckDirFiles(firstFolderPath, secondFolderPath))
                            {
                                if (firstFolder.NumberOfFileDir() > secondFolder.NumberOfFileDir())
                                {
                                    secondFolder.RenameFolder(true);
                                }
                                else if (firstFolder.NumberOfFileDir() < secondFolder.NumberOfFileDir())
                                {
                                    firstFolder.RenameFolder(true);
                                }
                                else
                                {
                                    if (firstFolderPath.CompareTo(secondFolderPath) == 1)
                                    {
                                        secondFolder.RenameFolder(true);
                                    }
                                    else
                                    {
                                        firstFolder.RenameFolder(true);
                                    }
                                }

                            }
                            else
                            {
                                if (firstFolderPath.CompareTo(secondFolderPath) == 1)
                                {
                                    secondFolder.DeleteFolder();
                                    

                                }
                                else
                                {
                                    firstFolder.DeleteFolder();
                                }
                            }
                        }
                        else
                        if (!Directory.GetFiles(firstFolderPath).Length.Equals(0) && Directory.GetFiles(secondFolderPath).Length.Equals(0))
                        {
                            secondFolder.DeleteFolder();
                        }
                        else
                        {
                            firstFolder.DeleteFolder();
                        }

                    }
                }
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                Folder folder = new Folder(dirs[i]);
                folder.RenameFolder(false);
            }
        }

        public void DelSameNameFiles()
        {
            string[] files = GetFiles(path);
            for (int i = 0; i < files.Length - 1; i++)
            {
                File firstFile = new File(files[i]);
                string firstFilePath = firstFile.GetPath();
                for (int j = i + 1; j < files.Length; j++)
                {
                    File secondFile = new File(files[j]);
                    string secondFilePath = secondFile.GetPath();
                    if (firstFile.PathWithoutUUID() == secondFile.PathWithoutUUID())
                    {

                        if (secondFile.ReadFile() != firstFile.ReadFile())
                        {
                            if (firstFilePath.CompareTo(secondFilePath) == 1)
                            {
                                secondFile.RenameFile(true);
                            }
                            else
                            {
                                firstFile.RenameFile(true);
                            }
                        }
                        else
                        {
                            if (firstFilePath.CompareTo(secondFilePath) == 1)
                            {
                                firstFile.DeleteFile();
                            }
                            else
                            {
                                firstFile.DeleteFile();
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < files.Length; i++)
            {
                File file = new File(files[i]);
                file.RenameFile(false);
            }
        }

        public string[] GetDericrories(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            return dirs;
        }

        public string[] GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            return files;
        }
    }
}
