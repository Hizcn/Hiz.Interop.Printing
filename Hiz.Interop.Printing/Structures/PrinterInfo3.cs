using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_3
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162846(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_3 {
     *   PSECURITY_DESCRIPTOR pSecurityDescriptor;
     * } PRINTER_INFO_3, *PPRINTER_INFO_3;
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo3 : IPrinterInfo
    {
        public IntPtr pSecurityDescriptor;
    }
}
