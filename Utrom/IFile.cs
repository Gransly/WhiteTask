using System;
using System.Collections.Generic;
using System.Text;

namespace Utrom
{
    interface IFile
    {
        void DeleteFile();
        void RenameFile(bool sameNames);
        string ReadFile();
        string PathWithoutUUID();

    }
}
