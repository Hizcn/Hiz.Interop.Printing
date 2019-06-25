using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_9
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162852(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_9 {
     *   LPDEVMODE pDevMode;
     * } PRINTER_INFO_9, *PPRINTER_INFO_9;
     */
    /// <summary>
    /// The PRINTER_INFO_9 structure specifies the per-user default printer settings.
    /// </summary>
    /// <remarks>
    /// The per-user defaults will affect only a particular user or anyone who uses the profile.
    /// In contrast, the global defaults are set by the administrator of a printer that can be used by anyone.
    /// For global defaults, use PRINTER_INFO_8.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo9 : IPrinterInfo
    {
        /// <summary>
        /// A pointer to a DEVMODE structure that defines the per-user default printer data such as the paper orientation and the resolution.
        /// The DEVMODE is stored in the user's registry.
        /// </summary>
        public IntPtr pDevMode;
    }
}
