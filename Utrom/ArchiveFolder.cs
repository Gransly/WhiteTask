using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Utrom
{
    class ArchiveFolder : IArchiveFolder
    {
        private string path;

        public string GetPath() 
        {
            return path;
        }
        public ArchiveFolder(string path) 
        {
            this.path = path;
        }
        public void Archive(string directionFolder)
        {
            ZipFile.CreateFromDirectory(directionFolder, path + "\\Utrom's secrets.zip");
        }
    }
}
