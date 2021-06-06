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
            MainFolder mainFolder = null;
            ArchiveFolder archiveFolder = null;

            if (args.Length == 4)
            {

                if (args[0] == "--src" && args[2] == "--dest")
                {
                    mainFolder = new MainFolder(args[1]);
                    archiveFolder = new ArchiveFolder(args[3]);
                    Console.WriteLine($"dirName: {mainFolder.GetPath()}\ndest:{archiveFolder.Path}");
                }
                else
                {
                    Console.WriteLine($"Error. Wrong args");
                    Console.WriteLine($"Example: --src D:\\Utrom's secrets --dest D:\\Utrom's secrets2 ");
                    return;
                }

            }
            while (mainFolder == null || !Directory.Exists(mainFolder.GetPath()))
            {
                Console.Write("Source directory isn't exist\nWrite correct dirrectory (e.g: D:\\Utrom's secrets): ");
                mainFolder = new MainFolder(Console.ReadLine());
            }

            while (archiveFolder == null || !Directory.Exists(archiveFolder.Path))
            {
                Console.Write("Destination directory isn't exist\nWrite correct dirrectory (e.g: D:\\Utrom's secrets1): ");
                archiveFolder = new ArchiveFolder(Console.ReadLine());
            }


            if (mainFolder.CheckDirs())
            {
                Console.WriteLine("Error, there directories in the directories");
                return;
            }

            mainFolder.DelSameNameDirs();
            mainFolder.DelSameNameFiles();
            archiveFolder.Archive(mainFolder.GetPath(), "Utrom's secret");
            mainFolder.DeleteCopyFolder();

            

        }

      
    }
}
