using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PRINTER_DEFAULTS
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162839(v=vs.85).aspx
     * typedef struct _PRINTER_DEFAULTS {
     *   LPTSTR      pDatatype;
     *   LPDEVMODE   pDevMode;
     *   ACCESS_MASK DesiredAccess;
     * } PRINTER_DEFAULTS, *PPRINTER_DEFAULTS;
     * 
     * 
     * System.Printing.dll
     * MS.Internal.PrintWin32Thunk.PrinterDefaults
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class PrinterDefaults
    {
        /// <summary>
        /// Pointer to a null-terminated string that specifies the default data type for a printer.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public String pDatatype;

        /// <summary>
        /// Pointer to a DEVMODE structure that identifies the default environment and initialization data for a printer.
        /// </summary>
        public IntPtr pDevMode;

        /// <summary>
        /// Specifies desired access rights for a printer.
        /// The OpenPrinter function uses this member to set access rights to the printer.
        /// These rights can affect the operation of the SetPrinter and DeletePrinter functions.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int DesiredAccess;
    }

    /* Access rights for printers (WinSpool.h)
     * 
     * System.Printing.dll
     * System.Printing.PrintSystemDesiredAccess {
     *     AdministrateServer = 0x000F0001, // = STANDARD_RIGHTS_REQUIRED | SERVER_ACCESS_ADMINISTER;
     *     EnumerateServer = 0x00020002, // = STANDARD_RIGHTS_EXECUTE | SERVER_ACCESS_ENUMERATE;
     *     UsePrinter = 0x00020008, // = STANDARD_RIGHTS_EXECUTE | PRINTER_ACCESS_USE;
     *     AdministratePrinter = 0x000F000C, // = STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_USE|PRINTER_ACCESS_ADMINISTER;
     * }
     */
    [Flags]
    internal enum PrintAccessRights : int
    {
        None = 0,

        #region Print Server
        /// <summary>
        /// SERVER_ACCESS_ADMINISTER
        /// </summary>
        ServerAdministrate = 0x01,
        /// <summary>
        /// SERVER_ACCESS_ENUMERATE
        /// </summary>
        ServerEnumerate = 0x02,

        /// <summary>
        /// 以下两值相同
        /// SERVER_READ = STANDARD_RIGHTS_READ | SERVER_ACCESS_ENUMERATE;
        /// SERVER_EXECUTE = STANDARD_RIGHTS_EXECUTE | SERVER_ACCESS_ENUMERATE;
        /// </summary>
        ServerGenericRead = StandardAccessRights.Read | ServerEnumerate,
        /// <summary>
        /// SERVER_WRITE = STANDARD_RIGHTS_WRITE | SERVER_ACCESS_ENUMERATE | SERVER_ACCESS_ADMINISTER;
        /// </summary>
        ServerGenericWrite = StandardAccessRights.Write | ServerEnumerate | ServerAdministrate,
        /// <summary>
        /// SERVER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SERVER_ACCESS_ENUMERATE | SERVER_ACCESS_ADMINISTER;
        /// </summary>
        ServerGenericAll = StandardAccessRights.Required | ServerEnumerate | ServerAdministrate,
        #endregion

        #region Printer
        /// <summary>
        /// PRINTER_ACCESS_ADMINISTER
        /// To perform administrative tasks, such as those provided by "SetPrinter".
        /// </summary>
        PrinterAdministrate = 0x04,
        /// <summary>
        /// PRINTER_ACCESS_USE
        /// To perform basic printing operations.
        /// </summary>
        PrinterUse = 0x08,

        #region >= Windows 8.1
        /// <summary>
        /// PRINTER_ACCESS_MANAGE_LIMITED
        /// To perform administrative tasks, such as those provided by "SetPrinter" and "SetPrinterData".
        /// This value is available starting from Windows 8.1.
        /// </summary>
        PrinterManageLimited = 0x40,
        #endregion

        /// <summary>
        /// 以下三值相同
        /// PRINTER_READ = STANDARD_RIGHTS_READ | PRINTER_ACCESS_USE
        /// PRINTER_WRITE = STANDARD_RIGHTS_WRITE | PRINTER_ACCESS_USE
        /// PRINTER_EXECUTE = STANDARD_RIGHTS_EXECUTE | PRINTER_ACCESS_USE
        /// </summary>
        PrinterGenericExecute = StandardAccessRights.Execute | PrinterUse,
        /// <summary>
        /// PRINTER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE
        /// </summary>
        PrinterGenericAll = StandardAccessRights.Required | PrinterAdministrate | PrinterUse,
        #endregion
    }
}
