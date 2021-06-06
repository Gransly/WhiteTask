using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Utrom
{
    class ArchiveFolder : IArchiveFolder
    {
        private string path;

        public string Path { get { return path; } private set { path = value; } }

        public ArchiveFolder(string path) 
        {
            this.path = path;
        }
        public void Archive(string directionFolder , string zipName)
        {
            ZipFile.CreateFromDirectory(directionFolder, path + $"\\{zipName}.zip");
        }
    }
}
