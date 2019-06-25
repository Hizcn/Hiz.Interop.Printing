using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_1
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162844(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_1 {
     *   DWORD  Flags;
     *   LPTSTR pDescription;
     *   LPTSTR pName;
     *   LPTSTR pComment;
     * } PRINTER_INFO_1, *PPRINTER_INFO_1;
     */
    [DebuggerDisplay("{pName}: {Flags}")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo1 : IPrinterInfo
    {
        /// <summary>
        /// Specifies information about the returned data.
        /// 
        /// Following are the values for this member.
        /// PRINTER_ENUM_EXPAND;
        /// PRINTER_ENUM_CONTAINER;
        /// PRINTER_ENUM_ICON1;
        /// PRINTER_ENUM_ICON2;
        /// PRINTER_ENUM_ICON3;
        /// PRINTER_ENUM_ICON4;
        /// PRINTER_ENUM_ICON5;
        /// PRINTER_ENUM_ICON6;
        /// PRINTER_ENUM_ICON7;
        /// PRINTER_ENUM_ICON8;
        /// PRINTER_ENUM_HIDE;
        /// </summary>
        public PrinterFlags Flags;

        /// <summary>
        /// Pointer to a null-terminated string that describes the contents of the structure.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDescription;

        /// <summary>
        /// Pointer to a null-terminated string that names the contents of the structure.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pName;

        /// <summary>
        /// Pointer to a null-terminated string that names the contents of the structure.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pComment;
    }
}
