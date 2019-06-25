using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    internal class ExternDll
    {
        /* @"C:\Windows\System32\winspool.drv";
         * @"C:\Windows\SysWOW64\winspool.drv";
         */
        public const string WinSpool = "winspool.drv";

        public const string Gdi32 = "gdi32.dll";
    }
}