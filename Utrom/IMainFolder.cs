using System;
using System.Collections.Generic;
using System.Text;

namespace Utrom
{
    interface IMainFolder
    {
        string[] GetDirecrories(string path);
        string[] GetFiles(string path);
        void DelSameNameFiles();
        void DelSameNameDirs();
        bool CheckDirs();
    }
}
