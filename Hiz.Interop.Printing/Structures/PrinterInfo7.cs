using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_7
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162850(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_7 {
     *   LPTSTR pszObjectGUID;
     *   DWORD  dwAction;
     * } PRINTER_INFO_7, *PPRINTER_INFO_7;
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo7 : IPrinterInfo
    {
        /// <summary>
        /// A pointer to a null-terminated string containing the GUID of the directory service print queue object associated with a published printer.
        /// Use the GetPrinter function to retrieve this GUID.
        /// 
        /// Before calling SetPrinter, set pszObjectGUID to NULL.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszObjectGUID;

        /// <summary>
        /// Indicates the action for the SetPrinter function to perform.
        /// For the GetPrinter function, this member indicates whether the specified printer is published.
        /// </summary>
        public PrinterDsAction dwAction;
    }

    [Flags]
    internal enum PrinterDsAction : uint
    {
        /// <summary>
        /// DSPRINT_PUBLISH
        /// GetPrinter: Indicates the printer is published.
        /// SetPrinter: Publishes the printer's data in the DS.
        /// </summary>
        Publish = 0x01,

        /// <summary>
        /// DSPRINT_UPDATE
        /// GetPrinter: Never returns this value.
        /// SetPrinter: Updates the printer's published data in the DS.
        /// </summary>
        Update = 0x02,

        /// <summary>
        /// DSPRINT_UNPUBLISH
        /// GetPrinter: Indicates the printer is not published. 
        /// SetPrinter: Removes the printer's published data from the DS.
        /// </summary>
        Unpublish = 0x04,

        /// <summary>
        /// DSPRINT_REPUBLISH
        /// GetPrinter: Never returns this value.
        /// 
        /// SetPrinter: The DS data for the printer is unpublished and then published again, refreshing all properties in the published printer.
        /// Re-publishing also changes the GUID of the published printer.
        /// </summary>
        Republish = 0x08,

        /// <summary>
        /// DSPRINT_PENDING
        /// GetPrinter: Indicates that the system is attempting to complete a publish or unpublish operation started by a SetPrinter call.
        /// SetPrinter: This value is not valid. 
        /// </summary>
        Pending = 0x80000000,
    }
}
