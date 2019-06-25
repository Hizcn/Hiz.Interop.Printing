using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_4
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162847(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_4 {
     *   LPTSTR pPrinterName;
     *   LPTSTR pServerName;
     *   DWORD  Attributes;
     * } PRINTER_INFO_4, *PPRINTER_INFO_4;
     */
    /// <summary>
    /// The structure can be used to retrieve minimal printer information on a call to "EnumPrinters".
    /// Such a call is a fast and easy way to retrieve the names and attributes of all locally installed printers on a system and all remote printer connections that a user has established.
    /// </summary>
    /// <remarks>
    /// The PRINTER_INFO_4 structure provides an easy and extremely fast way to retrieve the names of the printers installed on a local machine, as well as the remote connections that a user has established.
    /// When "EnumPrinters" is called with a PRINTER_INFO_4 data structure, that function queries the registry for the specified information, then returns immediately.
    /// This differs from the behavior of EnumPrinters when called with other levels of PRINTER_INFO_(x) data structures.
    /// In particular, when EnumPrinters is called with a level 2 (PRINTER_INFO_2 ) data structure, it performs an OpenPrinter call on each remote connection.
    /// If a remote connection is down, if the remote server no longer exists, or if the remote printer no longer exists, the function must wait for RPC to time out and consequently fail the OpenPrinter call.
    /// This can take a while. Passing a PRINTER_INFO_4 structure lets an application retrieve a bare minimum of required information; if more detailed information is desired, a subsequent EnumPrinter level 2 call can be made.
    /// </remarks>
    [DebuggerDisplay("{pServerName}: {pPrinterName}: {Attributes}")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PrinterInfo4 : IPrinterInfo
    {
        /// <summary>
        /// Pointer to a null-terminated string that specifies the name of the printer (local or remote).
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrinterName;

        /// <summary>
        /// Pointer to a null-terminated string that is the name of the server.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pServerName;

        /// <summary>
        /// Specifies information about the returned data.
        /// PRINTER_ATTRIBUTE_LOCAL
        /// PRINTER_ATTRIBUTE_NETWORK
        /// </summary>
        public PrinterAttributes Attributes;
    }
}
