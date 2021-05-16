using System;
using System.Collections.Generic;
using System.Text;

namespace Utrom
{
    interface IFolder
    {
        void DeleteFolder();
        void RenameFolder(bool sameNames);
        int NumberOfFileDir();
        bool CheckDir();
        bool CheckDirFiles(string dir1, string dir2);
        string[] GetFiles(string dirName);
        string PathWithoutUUID();
    }
}
