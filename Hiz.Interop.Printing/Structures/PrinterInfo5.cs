using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_5
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162848(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_5 {
     *   LPTSTR pPrinterName;
     *   LPTSTR pPortName;
     *   DWORD  Attributes;
     *   DWORD  DeviceNotSelectedTimeout;
     *   DWORD  TransmissionRetryTimeout;
     * } PRINTER_INFO_5, *PPRINTER_INFO_5;
     */
    [DebuggerDisplay("{pPrinterName}: {pPortName}: {Attributes}")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo5 : IPrinterInfo
    {
        /// <summary>
        /// A pointer to a null-terminated string that specifies the name of the printer.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrinterName;

        /// <summary>
        /// A pointer to a null-terminated string that identifies the port(s) used to transmit data to the printer.
        /// If a printer is connected to more than one port, the names of each port must be separated by commas (for example, "LPT1:,LPT2:,LPT3:").
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPortName;

        /// <summary>
        /// The printer attributes. This member can be any reasonable combination of the following values.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public PrinterAttributes Attributes;

        /// <summary>
        /// This value is not used.
        /// </summary>
        public int DeviceNotSelectedTimeout;

        /// <summary>
        /// This value is not used.
        /// </summary>
        public int TransmissionRetryTimeout;
    }
}
