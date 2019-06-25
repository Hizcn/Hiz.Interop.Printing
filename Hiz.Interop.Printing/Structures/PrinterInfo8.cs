using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_8
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162851(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_8 {
     *   LPDEVMODE pDevMode;
     * } PRINTER_INFO_8, *PPRINTER_INFO_8;
     */
    /// <summary>
    /// The PRINTER_INFO_8 structure specifies the global default printer settings.
    /// </summary>
    /// <remarks>
    /// The global defaults are set by the administrator of a printer that can be used by anyone.
    /// In contrast, the per-user defaults will affect a particular user or anyone else who uses the profile.
    /// For per-user defaults, use PRINTER_INFO_9.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo8 : IPrinterInfo
    {
        /// <summary>
        /// A pointer to a DEVMODE structure that defines the global default printer data such as the paper orientation and the resolution.
        /// </summary>
        public IntPtr pDevMode;
    }
}
