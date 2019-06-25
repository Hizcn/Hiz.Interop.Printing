using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_INFO_2
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162845(v=vs.85).aspx
     * typedef struct _PRINTER_INFO_2 {
     *   LPTSTR               pServerName;
     *   LPTSTR               pPrinterName;
     *   LPTSTR               pShareName;
     *   LPTSTR               pPortName;
     *   LPTSTR               pDriverName;
     *   LPTSTR               pComment;
     *   LPTSTR               pLocation;
     *   LPDEVMODE            pDevMode;
     *   LPTSTR               pSepFile;
     *   LPTSTR               pPrintProcessor;
     *   LPTSTR               pDatatype;
     *   LPTSTR               pParameters;
     *   PSECURITY_DESCRIPTOR pSecurityDescriptor;
     *   DWORD                Attributes;
     *   DWORD                Priority;
     *   DWORD                DefaultPriority;
     *   DWORD                StartTime;
     *   DWORD                UntilTime;
     *   DWORD                Status;
     *   DWORD                cJobs;
     *   DWORD                AveragePPM;
     * } PRINTER_INFO_2, *PPRINTER_INFO_2;
     */
    [DebuggerDisplay("{pServerName}: {pPrinterName}: {Attributes}")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PrinterInfo2 : IPrinterInfo
    {
        /// <summary>
        /// A pointer to a null-terminated string identifying the server that controls the printer.
        /// If this string is NULL, the printer is controlled locally.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pServerName;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the name of the printer.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrinterName;

        /// <summary>
        /// A pointer to a null-terminated string that identifies the share point for the printer.
        /// This string is used only if the PRINTER_ATTRIBUTE_SHARED constant was set for the Attributes member.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pShareName;

        /// <summary>
        /// A pointer to a null-terminated string that identifies the port(s) used to transmit data to the printer.
        /// If a printer is connected to more than one port, the names of each port must be separated by commas (for example, "LPT1:,LPT2:,LPT3:").
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPortName;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the name of the printer driver.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDriverName;

        /// <summary>
        /// A pointer to a null-terminated string that provides a brief description of the printer.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pComment;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the physical location of the printer (for example, "Bldg. 38, Room 1164").
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pLocation;

        /// <summary>
        /// A pointer to a "DEVMODE" structure that defines default printer data such as the paper orientation and the resolution.
        /// </summary>
        public IntPtr pDevMode;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the name of the file used to create the separator page.
        /// This page is used to separate print jobs sent to the printer.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pSepFile;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the name of the print processor used by the printer.
        /// You can use the "EnumPrintProcessors" function to obtain a list of print processors installed on a server.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrintProcessor;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the data type used to record the print job.
        /// You can use the "EnumPrintProcessorDatatypes" function to obtain a list of data types supported by a specific print processor.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDatatype;

        /// <summary>
        /// A pointer to a null-terminated string that specifies the default print-processor parameters.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pParameters;

        /// <summary>
        /// A pointer to a "SECURITY_DESCRIPTOR" structure for the printer. This member may be NULL.
        /// </summary>
        public IntPtr pSecurityDescriptor;

        /// <summary>
        /// The printer attributes. This member can be any reasonable combination of the following values.
        /// </summary>
        public PrinterAttributes Attributes;

        /// <summary>
        /// A priority value that the spooler uses to route print jobs.
        /// </summary>
        public int Priority;

        /// <summary>
        /// The default priority value assigned to each print job.
        /// </summary>
        public int DefaultPriority;

        /// <summary>
        /// The earliest time at which the printer will print a job.
        /// This value is expressed as minutes elapsed since 12:00 AM GMT (Greenwich Mean Time).
        /// </summary>
        public int StartTime;

        /// <summary>
        /// The latest time at which the printer will print a job.
        /// This value is expressed as minutes elapsed since 12:00 AM GMT (Greenwich Mean Time).
        /// </summary>
        public int UntilTime;

        /// <summary>
        /// The printer status.
        /// </summary>
        public PrinterStatus Status;

        /// <summary>
        /// The number of print jobs that have been queued for the printer.
        /// </summary>
        public int cJobs;

        /// <summary>
        /// The average number of pages per minute that have been printed on the printer.
        /// </summary>
        public int AveragePPM;
    }
}
