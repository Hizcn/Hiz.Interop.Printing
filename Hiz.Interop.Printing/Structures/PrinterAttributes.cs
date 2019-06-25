using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* System.Printing.PrintQueueAttributes
     * https://msdn.microsoft.com/zh-cn/library/system.printing.printqueueattributes(v=vs.100).aspx
     * 
     * Status and Attribute Values
     * https://msdn.microsoft.com/en-us/library/cc244854.aspx
     */
    [Flags]
    public enum PrinterAttributes : int
    {
        None = 0,

        /// <summary>
        /// PRINTER_ATTRIBUTE_QUEUED
        /// If set, the printer spools and starts printing after the last page is spooled.
        /// If not set and PRINTER_ATTRIBUTE_DIRECT is not set, the printer spools and prints while spooling.
        /// </summary>
        Queued = 0x01,

        /// <summary>
        /// PRINTER_ATTRIBUTE_DIRECT
        /// Job is sent directly to the printer (it is not spooled).
        /// </summary>
        Direct = 0x02,

        // PRINTER_ATTRIBUTE_DEFAULT
        Default = 0x04,

        /// <summary>
        /// PRINTER_ATTRIBUTE_SHARED
        /// Printer is shared.
        /// </summary>
        Shared = 0x08,

        /// <summary>
        /// PRINTER_ATTRIBUTE_NETWORK
        /// Printer is a network printer connection.
        /// </summary>
        Network = 0x10,

        /// <summary>
        /// PRINTER_ATTRIBUTE_HIDDEN
        /// Reserved.
        /// </summary>
        Hidden = 0x20,

        /// <summary>
        /// PRINTER_ATTRIBUTE_LOCAL
        /// Printer is a local printer.
        /// </summary>
        Local = 0x40,

        /// <summary>
        /// PRINTER_ATTRIBUTE_ENABLE_DEVQ
        /// If set, DevQueryPrint is called.
        /// DevQueryPrint may fail if the document and printer setups do not match.
        /// Setting this flag causes mismatched documents to be held in the queue.
        /// </summary>
        EnableDevQuery = 0x80,

        /// <summary>
        /// PRINTER_ATTRIBUTE_KEEPPRINTEDJOBS
        /// If set, jobs are kept after they are printed. If unset, jobs are deleted.
        /// </summary>
        KeepPrintedJobs = 0x0100,

        /// <summary>
        /// PRINTER_ATTRIBUTE_DO_COMPLETE_FIRST
        /// If set and printer is set for print-while-spooling, any jobs that have completed spooling are scheduled to print before jobs that have not completed spooling.
        /// </summary>
        ScheduleCompletedJobsFirst = 0x0200,

        /// <summary>
        /// PRINTER_ATTRIBUTE_WORK_OFFLINE
        /// 
        /// Indicates whether the printer is currently connected.
        /// If the printer is not currently connected, print jobs continue to spool.
        /// </summary>
        WorkOffline = 0x0400,

        /// <summary>
        /// PRINTER_ATTRIBUTE_ENABLE_BIDI
        /// 已启用打印设备的双向通信;
        /// </summary>
        EnableBidi = 0x0800,

        /// <summary>
        /// PRINTER_ATTRIBUTE_RAW_ONLY
        /// Indicates that only raw data type print jobs can be spooled.
        /// 
        /// The printer cannot use enhanced metafile (EMF) printing.
        /// </summary>
        RawOnly = 0x1000,

        /// <summary>
        /// PRINTER_ATTRIBUTE_PUBLISHED
        /// Indicates whether the printer is published in the directory service.
        /// </summary>
        Published = 0x2000,

        #region >= Windows XP
        /// <summary>
        /// PRINTER_ATTRIBUTE_FAX
        /// If set, printer is a fax printer.
        /// This can only be set by "AddPrinter", but it can be retrieved by "EnumPrinters" and "GetPrinter".
        /// </summary>
        Fax = 0x4000,
        #endregion

        #region == Windows Server 2003
        /// <summary>
        /// PRINTER_ATTRIBUTE_TS
        /// Indicates the printer is currently connected through a terminal server.
        /// </summary>
        TerminalServer = 0x8000,
        #endregion

        #region >= Windows Vista
        /* The printer attribute pushed xxx bits below are used by the pushing printer connection code to keep track of the type of printer connection.
             * These bits are per user resources hence the local print provider has no knowledge about these bit and will not accepts them.
             * The remote print provider is responsible for storeing and persisting these bits.
             */
        /// <summary>
        /// PRINTER_ATTRIBUTE_PUSHED_USER
        /// The printer was installed by using the Push Printer Connections user policy.
        /// </summary>
        PushedUser = 0x020000,
        /// <summary>
        /// PRINTER_ATTRIBUTE_PUSHED_MACHINE
        /// The printer was installed by using the Push Printer Connections computer policy.
        /// </summary>
        PushedMachine = 0x040000,
        /// <summary>
        /// PRINTER_ATTRIBUTE_MACHINE
        /// Printer is a per-machine connection.
        /// </summary>
        Machine = 0x080000,
        /// <summary>
        /// PRINTER_ATTRIBUTE_FRIENDLY_NAME
        /// A computer has connected to this printer and given it a friendly name.
        /// </summary>
        FriendlyName = 0x100000,

        /// <summary>
        /// PRINTER_ATTRIBUTE_TS_GENERIC_DRIVER
        /// If the redirected TS printer is installed with generic TS printer driver (TSPRINT.dll) then this attribute is set by the UMRDP service and passed on to the spooler.
        /// </summary>
        TerminalServerGenericDriver = 0x200000,
        #endregion
    }
}
