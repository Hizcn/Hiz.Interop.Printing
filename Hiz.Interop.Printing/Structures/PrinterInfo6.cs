using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_6
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162849(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_6 {
     *   DWORD dwStatus;
     * } PRINTER_INFO_6, *PPRINTER_INFO_6;
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo6 : IPrinterInfo
    {
        public PrinterStatus dwStatus;
    }
}