using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* System.Printing.PrintQueueStatus
     * https://msdn.microsoft.com/zh-cn/library/system.printing.printqueuestatus(v=vs.100).aspx
     * 
     * Status and Attribute Values
     * https://msdn.microsoft.com/en-us/library/cc244854.aspx
     */
    [Flags]
    internal enum PrinterStatus : int
    {
        None = 0,

        /// <summary>
        /// PRINTER_STATUS_PAUSED
        /// The printer is paused.
        /// </summary>
        Paused = 0x01,

        /// <summary>
        /// PRINTER_STATUS_ERROR
        /// The printer is in an error state.
        /// </summary>
        Error = 0x02,

        /// <summary>
        /// PRINTER_STATUS_PENDING_DELETION
        /// The printer is being deleted.
        /// </summary>
        PendingDeletion = 0X04,

        /// <summary>
        /// PRINTER_STATUS_PAPER_JAM
        /// Paper is jammed in the printer
        /// </summary>
        PaperJam = 0x08,

        /// <summary>
        /// PRINTER_STATUS_PAPER_OUT
        /// The printer is out of paper.
        /// </summary>
        PaperOut = 0x10,

        /// <summary>
        /// PRINTER_STATUS_MANUAL_FEED
        /// The printer is in a manual feed state.
        /// </summary>
        ManualFeed = 0x20,

        /// <summary>
        /// PRINTER_STATUS_PAPER_PROBLEM
        /// The printer has a paper problem.
        /// </summary>
        PaperProblem = 0x40,

        /// <summary>
        /// PRINTER_STATUS_OFFLINE
        /// The printer is offline.
        /// </summary>
        Offline = 0x80,

        /// <summary>
        /// PRINTER_STATUS_IO_ACTIVE
        /// The printer is in an active input/output state
        /// </summary>
        IOActive = 0x0100,

        /// <summary>
        /// PRINTER_STATUS_BUSY
        /// The printer is busy.
        /// </summary>
        Busy = 0x0200,

        /// <summary>
        /// PRINTER_STATUS_PRINTING
        /// The printer is printing.
        /// </summary>
        Printing = 0x0400,

        /// <summary>
        /// PRINTER_STATUS_OUTPUT_BIN_FULL
        /// The printer's output bin is full.
        /// </summary>
        OutputBinFull = 0x0800,

        /// <summary>
        /// PRINTER_STATUS_NOT_AVAILABLE
        /// The printer is not available for printing.
        /// </summary>
        NotAvailable = 0x1000,

        /// <summary>
        /// PRINTER_STATUS_WAITING
        /// The printer is waiting.
        /// </summary>
        Waiting = 0x2000,

        /// <summary>
        /// PRINTER_STATUS_PROCESSING
        /// The printer is processing a print job.
        /// </summary>
        Processing = 0x4000,

        /// <summary>
        /// PRINTER_STATUS_INITIALIZING
        /// The printer is initializing.
        /// </summary>
        Initializing = 0x8000,

        /// <summary>
        /// PRINTER_STATUS_WARMING_UP
        /// The printer is warming up.
        /// </summary>
        WarmingUp = 0x010000,

        /// <summary>
        /// PRINTER_STATUS_TONER_LOW
        /// The printer is low on toner.
        /// </summary>
        TonerLow = 0x020000,

        /// <summary>
        /// PRINTER_STATUS_NO_TONER
        /// The printer is out of toner.
        /// </summary>
        NoToner = 0x040000,

        /// <summary>
        /// PRINTER_STATUS_PAGE_PUNT
        /// The printer cannot print the current page.
        /// </summary>
        PagePunt = 0x080000,

        /// <summary>
        /// PRINTER_STATUS_USER_INTERVENTION
        /// The printer has an error that requires the user to do something.
        /// </summary>
        UserIntervention = 0x100000,

        /// <summary>
        /// PRINTER_STATUS_OUT_OF_MEMORY
        /// The printer has run out of memory.
        /// </summary>
        OutOfMemory = 0x200000,

        /// <summary>
        /// PRINTER_STATUS_DOOR_OPEN
        /// The printer door is open.
        /// </summary>
        DoorOpen = 0x400000,

        /// <summary>
        /// PRINTER_STATUS_SERVER_UNKNOWN
        /// The printer status is unknown.
        /// </summary>
        ServerUnknown = 0x800000,

        /// <summary>
        /// PRINTER_STATUS_POWER_SAVE
        /// The printer is in power save mode.
        /// </summary>
        PowerSave = 0x01000000,

        #region Undocumented
        /// <summary>
        /// PRINTER_STATUS_SERVER_OFFLINE
        /// 
        /// The printer is offline.
        /// </summary>
        ServerOffline = 0x02000000,

        // PRINTER_STATUS_DRIVER_UPDATE_NEEDED
        DriverUpdateNeeded = 0x04000000,
        #endregion
    }
}
