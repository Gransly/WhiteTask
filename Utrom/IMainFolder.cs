using System;
using System.Collections.Generic;
using System.Text;

namespace Utrom
{
    interface IMainFolder
    {
        string[] GetDericrories(string path);
        string[] GetFiles(string path);
        void DelSameNameFiles();
        void DelSameNameDirs();
        bool CheckDirs();

        
    }
}
