using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_OPTIONS
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162857(v=vs.85).aspx
     * typedef struct _PRINTER_OPTIONS {
     *   UINT  cbSize;
     *   DWORD dwFlags;
     * } PRINTER_OPTIONS, *PPRINTER_OPTIONS;
     */
    [StructLayout(LayoutKind.Sequential)]
    internal class PrinterOptions
    {
        /// <summary>
        /// The size of the PRINTER_OPTIONS structure.
        /// </summary>
        public int cbSize;

        /// <summary>
        /// A set of PRINTER_OPTION_FLAGS that specifies how the handle to a printer returned by "OpenPrinter2" will be used by other functions.
        /// </summary>
        public PrinterOptionFlags dwFlags;
    }

    /* PRINTER_OPTION_FLAGS
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162858(v=vs.85).aspx
     * 
     * Specifies the caching of a handle for a printer opened with OpenPrinter2.
     * Printer option flags that can be passed to OpenPrinter2 for controlling whether the cached or non cached handle is used.
     */
    internal enum PrinterOptionFlags : int
    {
        /// <summary>
        /// PRINTER_OPTION_NO_CACHE
        /// 
        /// The handle is not cached.
        /// All functions applied to a handle returned by "OpenPrinter2" will go to the remote computer.
        /// </summary>
        NoCache = 0x01,

        /// <summary>
        /// PRINTER_OPTION_CACHE
        /// 
        /// The handle is cached.
        /// All functions applied to a handle returned by "OpenPrinter2" will go to the local cache.
        /// </summary>
        Cache = 0x02,

        /// <summary>
        /// PRINTER_OPTION_CLIENT_CHANGE
        /// 
        /// The handle returned by "OpenPrinter2" can be used by "SetPrinter" to rename the printer connection.
        /// </summary>
        ClientChange = 0x04,

        #region Undocumented
        /// <summary>
        /// PRINTER_OPTION_NO_CLIENT_DATA
        /// </summary>
        NoClientData = 0x08,
        #endregion
    }
}
