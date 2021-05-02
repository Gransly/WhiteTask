using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Utrom
{
    class Program
    {
        static void Main(string[] args)
        {
            string directionName = "";
            string destanationName = "";


            if (args.Length == 4)
            {

                if (args[0] == "--src" && args[2] == "--dest")
                {
                    directionName = args[1];
                    destanationName = args[3];
                    Console.WriteLine($"dirName: {directionName}\ndest:{destanationName}");
                }
                else
                {
                    Console.WriteLine($"Error. Wrong args");
                }

            }
            while (!Directory.Exists(directionName))
            {
                Console.Write("Source directory isn't exist\nWrite correct dirrectory (e.g: D:\\Utrom's secrets): ");
                directionName = Console.ReadLine();
            }

            while (!Directory.Exists(destanationName))
            {
                Console.Write("Destination directory isn't exist\nWrite correct dirrectory (e.g: D:\\Utrom's secrets1): ");
                destanationName = Console.ReadLine();
            }


            if (CheckDirs("D:\\Utrom's secrets\\"))
            {
                Console.WriteLine("Error, there directories in the directories");
                return;
            }

            DelSameNameDirs(directionName);
            DelSameNameFiles(directionName);

            ZipFile.CreateFromDirectory(directionName, destanationName + "\\Utrom's secrets.zip");


        }

        private static string[] GetDericrories(string dirName)
        {
            string[] dirs = Directory.GetDirectories(dirName);
            return dirs;
        }

        private static string[] GetFiles(string dirName)
        {
            string[] files = Directory.GetFiles(dirName);

            return files;
        }

        static string GetNameDir(string fileNameUUID)
        {
            string[] part = fileNameUUID.Split(' ');
            int partsNumbers = part.Length - 2;
            string fileName = "";
            for(int i = 0; i <=partsNumbers; i++) 
            {
                fileName += part[i] + " "; 
            }
            return fileName;
        }
        
        
        static string GetNameFile(string fileNameUUID)
        {
            string[] part = fileNameUUID.Split(' ', '.');
            int partsNumbers = part.Length - 2;
            string fileName = "";
            for (int i = 0; i < partsNumbers; i++)
            {
                fileName += part[i] + " ";
            }
            return fileName;

        }

        static string GetTypeFile(string fileNameUUID)
        {
            string[] part = fileNameUUID.Split('.');
            int partsNumbers = part.Length - 1;
            
            return "."+part[partsNumbers];

        }

        static void DelSameNameFiles(string dirName)
        {
            string[] files = GetFiles(dirName);
            string firstFile;
            string secondFile;
            for (int i = 0; i < files.Length - 1; i++)
            {
                firstFile = files[i];
                for (int j = i + 1; j < files.Length; j++)
                {
                    secondFile = files[j];

                    if (PathWithoutUUID(firstFile) == PathWithoutUUID(secondFile))
                    {

                        if (ReadFile(secondFile) != ReadFile(firstFile))
                        {
                            if (firstFile.CompareTo(secondFile) == 1)
                            {
                                Rename(secondFile, true);
                            }
                            else
                            {
                                Rename(firstFile, true);
                            }
                        }
                        else
                        {
                            if (firstFile.CompareTo(secondFile) == 1)
                            {
                                Delete(secondFile);
                            }
                            else
                            {
                                Delete(firstFile);
                            }
                        }
                    }  
                }
            }

            for (int i = 0; i < files.Length ; i++) 
            {
                Rename(files[i], false);
            }
        }

        static void DelSameNameDirs(string dirName)
        {
            string[] dirs = GetDericrories(dirName);
            string firstDir;
            string secondDir;
            for (int i = 0; i < dirs.Length - 1; i++)
            {
                firstDir = dirs[i];
                for (int j = i + 1; j < dirs.Length; j++)
                {
                    secondDir = dirs[j];
                    if (PathWithoutUUID(firstDir) == PathWithoutUUID(secondDir))
                    {
                        if (Directory.GetFiles(firstDir).Length.Equals(0) && Directory.GetFiles(secondDir).Length.Equals(0))
                        {
                            if (firstDir.CompareTo(secondDir) == 1)
                            {
                                Delete(secondDir);
                            }
                            else
                            {
                                Delete(firstDir);
                            }
                        }
                        else
                        if (!Directory.GetFiles(firstDir).Length.Equals(0) && !Directory.GetFiles(secondDir).Length.Equals(0))
                        {
                            if (CheckDirFiles(firstDir, secondDir))
                            {
                                if (NumberOfFileDir(firstDir) > NumberOfFileDir(secondDir))
                                {
                                    Rename(secondDir, true);
                                    
                                }
                                else if (NumberOfFileDir(firstDir) < NumberOfFileDir(secondDir))
                                {
                                    Rename(firstDir, true);
                                }
                                else
                                {
                                    if (firstDir.CompareTo(secondDir) == 1)
                                    {
                                        Rename(secondDir, true);
                                    }
                                    else
                                    {
                                        Rename(firstDir, true);
                                    }
                                }

                            }
                            else
                            {
                                if (firstDir.CompareTo(secondDir) == 1)
                                {
                                    Delete(secondDir);

                                }
                                else
                                {
                                    Delete(firstDir);
                                }
                            }
                        }
                        else
                        if (!Directory.GetFiles(firstDir).Length.Equals(0) && Directory.GetFiles(secondDir).Length.Equals(0))
                        {
                            Delete(secondDir);
                        }
                        else
                        {
                            Delete(firstDir);
                        }

                    }
                }
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                Rename(dirs[i], false);
            }
        }


        static string ReadFile(string path) 
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


        static bool CheckDirs(string dirName)
        {

            string[] dirs = GetDericrories(dirName);

            for (int i = 0; i < dirs.Length; i++)
            {
                if (GetDericrories(dirs[i]).Length > 0)
                {
                    return true;
                }
            }
            return false;

        }


        static bool CheckDirFiles(string dir1, string dir2)                   
        {
            string[] files1 = GetFiles(dir1);
            string[] files2 = GetFiles(dir2);
            string file1 = "";
            string file2 = "";
            int sameFileCounter = 0;
            if (files1.Length != files2.Length)
            {
                return true;
            }
            for (int i = 0; i < files1.Length; i++) 
            {
                file1 = files1[i];
                for(int j = 0; j < files2.Length; j++) 
                {
                    file2 = files2[j];

                    if(file1 == file2) 
                    {
                        if(String.Equals(ReadFile(file1), ReadFile(file2))) 
                        {
                            sameFileCounter++;
                        }

                    }
                    
                }
            }
            if(sameFileCounter == files1.Length && sameFileCounter == files2.Length) 
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        static int NumberOfFileDir(string dir) 
        {
            string[] files1 = GetFiles(dir);
            return files1.Length;
        }

        static string PathWithoutUUID(string path) 
        {
            string pattern = @" [0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(path, replacement);
            return result;
        }


        static void Rename(string path, bool sameNames) 
        {

            string pathWithoutUUID = PathWithoutUUID(path);
            if (sameNames && File.Exists(path)) 
            {
                string[] paths = pathWithoutUUID.Split('.');
                pathWithoutUUID = paths[0] + " (1)."+ paths[1];
            }
            else  if (sameNames && Directory.Exists(path)) 
            {
                pathWithoutUUID += " (1)";
            }
                if (File.Exists(path))
                {
                    File.Move(path, pathWithoutUUID);
                }
                else if (Directory.Exists(path))
                {
                    Directory.Move(path, pathWithoutUUID);
                }     
        }

        static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path);                            
            }

        }
    }
}
