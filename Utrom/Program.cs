using System;
using System.IO;
using System.IO.Compression;

namespace Utrom
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "";
            string destName = "";


            if (args.Length == 4)
            {
               
                if (args[0] == "--src" && args[2] == "--dest")
                {
                    dirName = args[1];
                    destName = args[3];
                    Console.WriteLine($"dirName: {dirName}\ndest:{destName}");
                }
                else 
                {
                    Console.WriteLine($"Error. Wrong args");
                }

            }
            while (!Directory.Exists(dirName))
            {
                Console.Write("Source directory isn't exist\nWrite correct dirrectory (e.g: D:\\Utrom's secrets): ");
                dirName = Console.ReadLine();
            }

            while (!Directory.Exists(destName))
            {
                Console.Write("Destination directory isn't exist\nWrite correct dirrectory (e.g: D:\\Utrom's secrets1): ");
                destName = Console.ReadLine();
            }


            if (CheckDirs(dirName))                 
            {
                Console.WriteLine("Error, there directories in the directories");
                return;
            }

            DelSameNameDirs(dirName);
            DelSameNameFiles(dirName);
            DeleteUUIDFiles(dirName);
            DeleteUUIDDirectories(dirName);

            ZipFile.CreateFromDirectory(dirName, destName+ "\\Utrom's secrets.zip");

        }

        private static string[] GetDericrories(string dirName)
        {
            string[] dirs = Directory.GetDirectories(dirName);
            string[] dirNames = new string[dirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                string[] part = dirs[i].ToString().Split("\\");
                dirNames[i] = part[part.Length - 1];
            }

            return dirNames;
        }

        /// <summary>
        /// Удаление UUID из имен всех директорий
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        private static string[] DeleteUUIDDirectories(string dirName)             
        {
            string[] dirs = Directory.GetDirectories(dirName);
            string[] dirNames = new string[dirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                string[] part = dirs[i].ToString().Split("\\");
                dirNames[i] = DeleteUUIDDir(part[part.Length - 1]);
                Directory.Move(dirName + "\\" + part[part.Length - 1], dirName + "\\" + dirNames[i]);          
            }

            return dirNames;
        }

        /// <summary>
        /// Удаление UUID из имен всех файлов
        /// </summary>
        /// <param name="dirName"></param>
        private static void DeleteUUIDFiles(string dirName)            
        {
            string[] files = Directory.GetFiles(dirName);
            string[] filesNames = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                string[] part = files[i].ToString().Split("\\");
                filesNames[i] = DeleteUUIDFile(part[part.Length - 1]);
                File.Move(dirName + "\\" +part[part.Length-1], dirName + "\\" + filesNames[i]);                 
            }
        }

        /// <summary>
        /// Возвращает массив строк файлов
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        private static string[] GetFiles(string dirName)            
        {
            string[] files = Directory.GetFiles(dirName);
            string[] filesNames = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                string[] part = files[i].ToString().Split("\\");
                filesNames[i] = part[part.Length - 1];
            }
            return filesNames;
        }

        /// <summary>
        /// Возвращает UUID директории
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
        static string GetUUIDDir(string fileNameUUID) 
        {
            string[] part = fileNameUUID.Split(' ');
            int partUUIDnumb= part.Length - 1;
            return part[partUUIDnumb];
        }

        /// <summary>
        /// Возвращает строку без UUID 
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
        static string DeleteUUIDFile(string fileNameUUID) 
        {
            string[] part = fileNameUUID.Split(' ', '.');
            int partsNumbers = part.Length-1;
            string fileName = "";
            for (int i=0; i <= partsNumbers; i++) 
            {
                if (i == partsNumbers - 1)
                {
                    continue;
                }

                if (i == partsNumbers) 
                {
                    fileName += "." + part[i];
                    continue;
                }

                if(i== partsNumbers - 2) 
                {
                    fileName += part[i];

                    continue;
                }
                fileName += part[i] + " ";
            }

            return fileName;
        }

        /// <summary>
        /// Возвращает строку без UUID
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
        static string DeleteUUIDDir(string fileNameUUID)
        {
            string[] part = fileNameUUID.Split(' ');
            int partsNumbers = part.Length - 2;
            string fileName = "";
            for (int i = 0; i <= partsNumbers; i++)
            {
                if (i == partsNumbers )
                {
                    fileName += part[i];
                }
                else 
                {
                    fileName += part[i] + " ";
                }
            }
            return fileName;
        }

        /// <summary>
        /// Возвращает UUID файла
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
        static string GetUUIDFile(string fileNameUUID)
        {
            string[] part = fileNameUUID.Split(' ','.');
            int partUUIDnumb = part.Length - 2;
            return part[part.Length - 2];
        }

        /// <summary>
        /// Возвращает имя директории
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Возвращает имя файла
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает тип файла
        /// </summary>
        /// <param name="fileNameUUID"></param>
        /// <returns></returns>
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
            for (int i = 0; i < files.Length-1; i++) 
            {
                firstFile = files[i];
                for (int j = i+1; j < files.Length; j++) 
                {
                    secondFile = files[j];

                    if (GetNameFile(firstFile) == GetNameFile(secondFile) && GetTypeFile(firstFile) == GetTypeFile(secondFile)) 
                    {

                        if(ReadFile(dirName + "\\" + secondFile) != ReadFile(dirName + "\\" + firstFile)) 
                        {
                            if (firstFile.CompareTo(secondFile) == 1)
                            {
                                File.Move(dirName + "\\" + secondFile, dirName + "\\" + GetNameFile(secondFile) + "(1) " + GetUUIDFile(secondFile) + GetTypeFile(secondFile));
                            }
                            else
                            {
                                File.Move(dirName + "\\" + firstFile, dirName + "\\" + GetNameFile(firstFile) + "(1) " + GetUUIDFile(firstFile) + GetTypeFile(firstFile));
                            }
                        }
                        else 
                        {
                            if (firstFile.CompareTo(secondFile) == 1)
                            {
                                File.Delete(dirName + "\\" + secondFile);
                            }
                            else
                            {
                                File.Delete(dirName + "\\" + firstFile);
                            }
                        }
                        
                    }
                }
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
                    if (GetNameDir(firstDir) == GetNameDir(secondDir))
                    {
                        if (Directory.GetFiles(dirName + "\\" + firstDir).Length.Equals(0) && Directory.GetFiles(dirName + "\\" + secondDir).Length.Equals(0))
                        {
                            if (firstDir.CompareTo(secondDir) == 1)
                            {
                                Directory.Delete(dirName + "\\" + secondDir);
                            }
                            else
                            {
                                Directory.Delete(dirName + "\\" + firstDir);
                            }
                        }
                        else 
                        if(!Directory.GetFiles(dirName + "\\" + firstDir).Length.Equals(0) && !Directory.GetFiles(dirName + "\\" + secondDir).Length.Equals(0)) 
                        {
                            if(CheckDirFiles(dirName + "\\" + firstDir, dirName + "\\" + secondDir)) 
                            {
                                if(NumberOfFileDir(dirName + "\\" + firstDir) > NumberOfFileDir(dirName + "\\" + secondDir)) 
                                {
                                    Directory.Move(dirName + "\\" + secondDir, dirName + "\\" + GetNameDir(secondDir) + "(1) " + GetUUIDDir(secondDir));
                                }
                                else if(NumberOfFileDir(dirName + "\\" + firstDir) < NumberOfFileDir(dirName + "\\" + secondDir)) 
                                {
                                    Directory.Move(dirName + "\\" + firstDir, dirName + "\\" + GetNameDir(firstDir) + "(1) " + GetUUIDDir(firstDir));
                                }
                                else 
                                {
                                    if (firstDir.CompareTo(secondDir) == 1)       
                                    {
                                        Directory.Move(dirName + "\\" + secondDir, dirName + "\\" + GetNameDir(secondDir) + "(1) " + GetUUIDDir(secondDir));
                                    }
                                    else
                                    {
                                        Directory.Move(dirName + "\\" + firstDir, dirName + "\\" + GetNameDir(firstDir) + "(1) " + GetUUIDDir(firstDir));
                                    }
                                }

                                
                            }
                            else 
                            {
                                if (firstDir.CompareTo(secondDir) == 1)
                                {
                                    Directory.Delete(dirName + "\\" + secondDir);

                                }
                                else
                                {
                                    Directory.Delete(dirName + "\\" + firstDir, true);
                                }

                            }
                        }
                        else
                        if(!Directory.GetFiles(dirName + "\\" + firstDir).Length.Equals(0) && Directory.GetFiles(dirName + "\\" + secondDir).Length.Equals(0)) 
                        {
                            Directory.Delete(dirName + "\\" + secondDir);
                        }
                        else 
                        {
                            Directory.Delete(dirName + "\\" + firstDir);
                        }
 
                    }
                }
            }
        }

        /// <summary>
        /// Вывод данных из файла 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Проверка директорий в директориях
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        static bool CheckDirs(string dirName) 
        {
           
            string[] dirs = GetDericrories(dirName);

            for (int i = 0; i < dirs.Length ; i++)
            {
                if(GetDericrories(dirName+"\\"+GetNameDir(dirs[i])+GetUUIDDir(dirs[i])).Length > 0) 
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Проверка на одинаковые файлы в одинаковых папках
        /// </summary>
        /// <param name="dir1"></param>
        /// <param name="dir2"></param>
        /// <returns></returns>
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
                        if(String.Equals(ReadFile(dir1+"\\"+file1), ReadFile(dir2 + "\\" + file2))) 
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
    }
}
