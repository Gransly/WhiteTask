using System;
using System.Collections.Generic;
using System.Text;

namespace Utrom
{
    interface IArchiveFolder
    {
        void Archive(string directionFolder, string zipName);
    }
}
