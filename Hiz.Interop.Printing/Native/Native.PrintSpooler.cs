using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    partial class Native
    {
        #region Printer Form Functions (All)

        /* 纸张定义注册表项位置:
         * 
         * HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Forms
         */

        /* EnumForms
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162624(v=vs.85).aspx
         * BOOL EnumForms(
         *   _In_  HANDLE  hPrinter,
         *   _In_  DWORD   Level,
         *   _Out_ LPBYTE  pForm,
         *   _In_  DWORD   cbBuf,
         *   _Out_ LPDWORD pcbNeeded,
         *   _Out_ LPDWORD pcReturned
         * );
         * 
         * Remarks:
         * If the caller is remote, and the Level is 2, the StringType value of the returned FORM_INFO_2 structures will always be STRING_LANGPAIR.
         * 
         * In Windows Vista, the form data returned by EnumForms is retrieved from a local cache when hPrinter refers to a remote print server or a printer hosted by a print server and there is at least one open connection to a printer on the remote print server.
         * In all other configurations, the form data is queried from the remote print server.
         * 
         * Note:
         * This is a blocking or synchronous function and might not return immediately.
         * How quickly this function returns depends on run-time factors such as network status, print server configuration, and printer driver implementation—factors that are difficult to predict when writing an application.
         * Calling this function from a thread that manages interaction with the user interface could make the application appear to be unresponsive.
         */
        /// <summary>
        /// The EnumForms function enumerates the forms supported by the specified printer.
        /// </summary>
        /// <param name="hPrinter">
        /// Handle to the printer for which the forms should be enumerated.
        /// 
        /// 经过测试: 可以用于 Print Server;
        /// </param>
        /// <param name="Level">Specifies the version of the structure to which pForm points. This value must be 1 or 2.</param>
        /// <param name="pForm">
        /// Pointer to one or more FORM_INFO_1 structures or to one or more FORM_INFO_2 structures.
        /// All the structures will have the same level.
        /// </param>
        /// <param name="cbBuf">Specifies the size, in bytes, of the buffer to which pForm points.</param>
        /// <param name="pcbNeeded">Pointer to a variable that receives the number of bytes copied to the array to which pForm points (if the operation succeeds) or the number of bytes required (if it fails because cbBuf is too small).</param>
        /// <param name="pcReturned">Pointer to a variable that receives the number of structures copied into the array to which pForm points.</param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "EnumFormsW", SetLastError = true)]
        static extern bool EnumForms(IntPtr hPrinter, int Level, IntPtr pForm, int cbBuf, out int pcbNeeded, out int pcReturned);

        /* AddForm
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd183328(v=vs.85).aspx
         * BOOL AddForm(
         *   _In_ HANDLE hPrinter,
         *   _In_ DWORD  Level,
         *   _In_ LPBYTE pForm
         * );
         */
        /// <summary>
        /// The AddForm function adds a form to the list of available forms that can be selected for the specified printer.
        /// </summary>
        /// <param name="hPrinter">
        /// A handle to the printer that supports printing with the specified form.
        /// Use the OpenPrinter or AddPrinter function to retrieve a printer handle.
        /// 
        /// 经过测试: 可以用于 Print Server;
        /// </param>
        /// <param name="Level">The level of the structure to which pForm points. This value must be 1 or 2.</param>
        /// <param name="pForm">A pointer to a FORM_INFO_1 or FORM_INFO_2 structure.</param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "AddFormW", SetLastError = true)]
        static extern bool AddForm(IntPtr hPrinter, Int32 Level, IntPtr pForm);

        /* DeleteForm
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd183536(v=vs.85).aspx
         * BOOL DeleteForm(
         *   _In_ HANDLE hPrinter,
         *   _In_ LPTSTR pFormName
         * );
         */
        /// <summary>
        /// The DeleteForm function removes a form name from the list of supported forms.
        /// 
        /// 经过测试: 可以用于 Print Server;
        /// </summary>
        /// <param name="hPrinter">Indicates the open printer handle that this function is to be performed upon.</param>
        /// <param name="pFormName">Pointer to the form name to be removed.</param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "DeleteFormW", SetLastError = true)]
        static extern bool DeleteForm(IntPtr hPrinter, [MarshalAs(UnmanagedType.LPTStr)]string pFormName);

        /* GetForm
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd144889(v=vs.85).aspx
         * BOOL GetForm(
         *   _In_  HANDLE  hPrinter,
         *   _In_  LPTSTR  pFormName,
         *   _In_  DWORD   Level,
         *   _Out_ LPBYTE  pForm,
         *   _In_  DWORD   cbBuf,
         *   _Out_ LPDWORD pcbNeeded
         * );
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hPrinter">
        /// A handle to the printer.
        /// 
        /// 经过测试: 不能用于 Print Server, 错误代码: ERROR_INVALID_HANDLE 句柄无效;
        /// </param>
        /// <param name="pFormName"></param>
        /// <param name="Level"></param>
        /// <param name="pForm"></param>
        /// <param name="cbBuf"></param>
        /// <param name="pcbNeeded"></param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "GetFormW", SetLastError = true)]
        static extern bool GetForm(IntPtr hPrinter, [MarshalAs(UnmanagedType.LPTStr)]string pFormName, int Level, IntPtr pForm, int cbBuf, out int pcbNeeded);

        /* SetForm
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162976(v=vs.85).aspx
         * BOOL SetForm(
         *   _In_ HANDLE hPrinter,
         *   _In_ LPTSTR pFormName,
         *   _In_ DWORD  Level,
         *   _In_ LPBYTE pForm
         * );
         */
        /// <summary>
        /// The SetForm function sets the form information for the specified printer.
        /// 
        /// 经过测试: 可以用于 Print Server;
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <param name="pFormName"></param>
        /// <param name="Level"></param>
        /// <param name="pForm">pForm.pName 尽量等于 pFormName 参数. 否则注册表项会较混乱;</param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "SetFormW", SetLastError = true)]
        static extern bool SetForm(IntPtr hPrinter, [MarshalAs(UnmanagedType.LPTStr)]string pFormName, int Level, IntPtr pForm);

        #endregion

        #region Print Job Functions

        /* OpenPrinter
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162751(v=vs.85).aspx
         * BOOL OpenPrinter(
         *   _In_  LPTSTR             pPrinterName,
         *   _Out_ LPHANDLE           phPrinter,
         *   _In_  LPPRINTER_DEFAULTS pDefault
         * );
         * 
         * System.Printing.dll
         * MS.Internal.PrintWin32Thunk.Win32ApiThunk.UnsafeNativeMethods.InvokeOpenPrinter();
         * 
         * ReachFramework.dll
         * MS.Internal.Printing.Configuration.UnsafeNativeMethods.OpenPrinterW();
         */
        /* Remarks
         * 
         */
        /// <summary>
        /// The OpenPrinter function retrieves a handle to the specified printer or print server or other types of handles in the print subsystem.
        /// </summary>
        /// <param name="pPrinterName">
        /// A pointer to a null-terminated string that specifies the name of the printer or print server, the printer object, the XcvMonitor, or the XcvPort.
        /// For a printer object use: PrinterName, Job xxxx.
        /// For an XcvMonitor, use: ServerName, XcvMonitor MonitorName.
        /// For an XcvPort, use: ServerName, XcvPort PortName.
        /// 
        /// If NULL, it indicates the local printer server.
        /// </param>
        /// <param name="phPrinter">
        /// A pointer to a variable that receives a handle (not thread safe) to the open printer or print server object.
        /// The phPrinter parameter can return an Xcv handle for use with the XcvData function.
        /// </param>
        /// <param name="pDefault">
        /// A pointer to a PRINTER_DEFAULTS structure.
        /// This value can be NULL. If pDefault is NULL, then the access rights are PRINTER_ACCESS_USE.
        /// </param>
        /// <returns></returns>
        // [SuppressUnmanagedCodeSecurity]
        // [PrintingPermission(SecurityAction.Demand, Level = PrintingPermissionLevel.DefaultPrinting)]
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "OpenPrinterW", SetLastError = true)]
        internal static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPTStr)]string pPrinterName, out IntPtr phPrinter, PrinterDefaults pDefault);

        /* OpenPrinter2
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162752(v=vs.85).aspx
         * 
         * BOOL OpenPrinter2(
         *   _In_  LPCTSTR            pPrinterName,
         *   _Out_ LPHANDLE           phPrinter,
         *   _In_  LPPRINTER_DEFAULTS pDefault,
         *   _In_  PPRINTER_OPTIONS   pOptions
         * );
         */
        /// <summary>
        /// Retrieves a handle to the specified printer, print server, or other types of handles in the print subsystem, while setting some of the printer options.
        /// </summary>
        /// <param name="pPrinterName">
        /// A pointer to a constant null-terminated string that specifies the name of the printer or print server, the printer object, the XcvMonitor, or the XcvPort.
        /// For a printer object use: PrinterName, Job xxxx.
        /// For an XcvMonitor, use: ServerName, XcvMonitor MonitorName.
        /// For an XcvPort, use: ServerName, XcvPort PortName.
        /// 
        /// Windows Vista: If NULL, it indicates the local print server.
        /// </param>
        /// <param name="phPrinter">A pointer to a variable that receives a handle to the open printer or print server object.</param>
        /// <param name="pDefault">A pointer to a "PRINTER_DEFAULTS" structure. This value can be NULL.</param>
        /// <param name="pOptions">A pointer to a "PRINTER_OPTIONS" structure. This value can be NULL.</param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "OpenPrinter2W", SetLastError = true)]
        internal static extern bool OpenPrinter2([MarshalAs(UnmanagedType.LPTStr)]string pPrinterName, out IntPtr phPrinter, PrinterDefaults pDefault, PrinterOptions pOptions);

        /* ClosePrinter
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd183446(v=vs.85).aspx
         * BOOL ClosePrinter(
         *   _In_ HANDLE hPrinter
         * );
         */
        /// <summary>
        /// The ClosePrinter function closes the specified printer object.
        /// </summary>
        /// <param name="hPrinter">
        /// A handle to the printer object to be closed.
        /// This handle is returned by the OpenPrinter or AddPrinter function.
        /// </param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, ExactSpelling = true, SetLastError = true)]
        internal static extern bool ClosePrinter(IntPtr hPrinter);

        /* SetPrinterData
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd145083(v=vs.85).aspx
         * DWORD SetPrinterData(
         *   _In_ HANDLE hPrinter,
         *   _In_ LPTSTR pValueName,
         *   _In_ DWORD  Type,
         *   _In_ LPBYTE pData,
         *   _In_ DWORD  cbData
         * );
         */
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "SetPrinterDataW ", SetLastError = true)]
        static extern bool SetPrinterData(IntPtr hPrinter, [MarshalAs(UnmanagedType.LPTStr)]string pValueName, int Type, IntPtr pData, int cbData);

        /* SetPrinterDataEx
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd145084(v=vs.85).aspx
         * DWORD SetPrinterDataEx(
         *   _In_ HANDLE  hPrinter,
         *   _In_ LPCTSTR pKeyName,
         *   _In_ LPCTSTR pValueName,
         *   _In_ DWORD   Type,
         *   _In_ LPBYTE  pData,
         *   _In_ DWORD   cbData
         * );
         */
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "SetPrinterDataExW", SetLastError = true)]
        static extern bool SetPrinterDataEx(IntPtr hPrinter, [MarshalAs(UnmanagedType.LPTStr)]string pKeyName, [MarshalAs(UnmanagedType.LPTStr)]string pValueName, int Type, IntPtr pData, int cbData);

        /* DocumentProperties
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd183576(v=vs.85).aspx
         * LONG DocumentProperties(
         *   _In_  HWND     hWnd,
         *   _In_  HANDLE   hPrinter,
         *   _In_  LPTSTR   pDeviceName,
         *   _Out_ PDEVMODE pDevModeOutput,
         *   _In_  PDEVMODE pDevModeInput,
         *   _In_  DWORD    fMode
         * );
         */
        /// <summary>
        /// The DocumentProperties function retrieves or modifies printer initialization information or displays a printer-configuration property sheet for the specified printer.
        /// </summary>
        /// <param name="hWnd">A handle to the parent window of the printer-configuration property sheet.</param>
        /// <param name="hPrinter">A handle to a printer object.</param>
        /// <param name="pDeviceName">A pointer to a null-terminated string that specifies the name of the device for which the printer-configuration property sheet is displayed.</param>
        /// <param name="pDevModeOutput">A pointer to a DEVMODE structure that receives the printer configuration data specified by the user.</param>
        /// <param name="pDevModeInput">
        /// A pointer to a DEVMODE structure that the operating system uses to initialize the property sheet controls.
        /// This parameter is only used if the DM_IN_BUFFER flag is set in the fMode parameter.
        /// If DM_IN_BUFFER is not set, the operating system uses the printer's default DEVMODE.
        /// </param>
        /// <param name="fMode">
        /// The operations the function performs.
        /// If this parameter is zero, the DocumentProperties function returns the number of bytes required by the printer driver's DEVMODE data structure.
        /// Otherwise, use one or more of the following constants to construct a value for this parameter;
        /// Note:
        /// However, that in order to change the print settings, an application must specify at least one input value and one output value.
        /// </param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "DocumentPropertiesW", SetLastError = true)]
        static extern int DocumentProperties(IntPtr hWnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPTStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);
        [Flags]
        internal enum DpMode : int
        {
            None = 0,

            /// <summary>
            /// DM_UPDATE
            /// </summary>
            Update = 0x01,
            /// <summary>
            /// DM_COPY
            /// </summary>
            Copy = 0x02,
            /// <summary>
            /// DM_PROMPT
            /// </summary>
            Prompt = 0x04,
            /// <summary>
            /// DM_MODIFY
            /// </summary>
            Modify = 0x08,

            /// <summary>
            /// DM_IN_BUFFER = DM_MODIFY;
            /// Input value.
            /// Before prompting, copying, or updating, the function merges the printer driver's current print settings with the settings in the DEVMODE structure specified by the pDevModeInput parameter.
            /// The function updates the structure only for those members specified by the DEVMODE structure's dmFields member.
            /// This value is also defined as DM_MODIFY.
            /// In cases of conflict during the merge, the settings in the DEVMODE structure specified by pDevModeInput override the printer driver's current print settings.
            /// </summary>
            InBuffer = Modify,

            /// <summary>
            /// DM_IN_PROMPT = DM_PROMPT;
            /// Input value.
            /// The function presents the printer driver's Print Setup property sheet and then changes the settings in the printer's DEVMODE data structure to those values specified by the user.
            /// This value is also defined as DM_PROMPT.
            /// </summary>
            InPrompt = Prompt,

            /// <summary>
            /// DM_OUT_BUFFER = DM_COPY
            /// Output value.
            /// The function writes the printer driver's current print settings, including private data, to the DEVMODE data structure specified by the pDevModeOutput parameter.
            /// The caller must allocate a buffer sufficiently large to contain the information. If the bit DM_OUT_BUFFER sets is clear, the pDevModeOutput parameter can be NULL.
            /// This value is also defined as DM_COPY.
            /// </summary>
            OutBuffer = Copy,

            /// <summary>
            /// DM_OUT_DEFAULT = DM_UPDATE
            /// </summary>
            OutDefault = Update,
        }

        #endregion

        #region Printer Functions

        /* EnumPrinters
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd162692(v=vs.85).aspx
         * BOOL EnumPrinters(
         *   _In_  DWORD   Flags,
         *   _In_  LPTSTR  Name,
         *   _In_  DWORD   Level,
         *   _Out_ LPBYTE  pPrinterEnum,
         *   _In_  DWORD   cbBuf,
         *   _Out_ LPDWORD pcbNeeded,
         *   _Out_ LPDWORD pcReturned
         * );
         * 
         * System.Drawing.dll:
         * System.Drawing.SafeNativeMethods.EnumPrinters();
         * 
         * System.Printing.dll:
         * MS.Internal.PrintWin32Thunk.Win32ApiThunk.UnsafeNativeMethods.InvokeEnumPrinters();
         */
        /* 参数 "Name":
         * Level 1:
         * a). Flags contains PRINTER_ENUM_NAME, and Name is non-NULL, then Name is a pointer to a null-terminated string that specifies the name of the object to enumerate.
         *     This string can be the name of a server, a domain, or a print provider.
         * b). Flags contains PRINTER_ENUM_NAME, and Name is NULL, then the function enumerates the available print providers.
         * 
         * c). Flags contains PRINTER_ENUM_REMOTE, and Name is NULL, then the function enumerates the printers in the user's domain.
         * 
         * Level 2/5:
         * a). Name is a pointer to a null-terminated string that specifies the name of a server whose printers are to be enumerated.
         *     If this string is NULL, then the function enumerates the printers installed on the local computer.
         * 
         * Level 4:
         * a). Name should be NULL. The function always queries on the local computer.
         * 
         * When Name is NULL, setting Flags to PRINTER_ENUM_LOCAL | PRINTER_ENUM_CONNECTIONS enumerates printers that are installed on the local machine.
         * These printers include those that are physically attached to the local machine as well as remote printers to which it has a network connection.
         * 
         * When Name is not NULL, setting Flags to PRINTER_ENUM_LOCAL | PRINTER_ENUM_NAME enumerates the local printers that are installed on the server Name.
         */
        /// <summary>
        /// The EnumPrinters function enumerates available printers, print servers, domains, or print providers.
        /// </summary>
        /// <param name="Flags">
        /// The types of print objects that the function should enumerate.
        /// </param>
        /// <param name="Name"></param>
        /// <param name="Level">
        /// The type of data structures pointed to by pPrinterEnum.
        /// Valid values are 1, 2, 4, and 5, which correspond to the PRINTER_INFO_1, PRINTER_INFO_2 , PRINTER_INFO_4, and PRINTER_INFO_5 data structures.</param>
        /// <param name="pPrinterEnum">
        /// A pointer to a buffer that receives an array of PRINTER_INFO_(*) structures.
        /// Each structure contains data that describes an available print object.
        /// The buffer must be large enough to receive the array of data structures and any strings or other data to which the structure members point.
        /// If the buffer is too small, the pcbNeeded parameter returns the required buffer size.
        /// </param>
        /// <param name="cbBuf">The size, in bytes, of the buffer pointed to by pPrinterEnum.</param>
        /// <param name="pcbNeeded">A pointer to a value that receives the number of bytes copied if the function succeeds or the number of bytes required if cbBuf is too small.</param>
        /// <param name="pcReturned">A pointer to a value that receives the number of PRINTER_INFO_(*) structures that the function returns in the array to which pPrinterEnum points.</param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "EnumPrintersW", SetLastError = true)]
        static extern bool EnumPrinters(PrinterFlags Flags, string Name, int Level, IntPtr pPrinterEnum, int cbBuf, out int pcbNeeded, out int pcReturned);

        /* GetPrinter
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd144911(v=vs.85).aspx
         * BOOL GetPrinter(
         *   _In_  HANDLE  hPrinter,
         *   _In_  DWORD   Level,
         *   _Out_ LPBYTE  pPrinter,
         *   _In_  DWORD   cbBuf,
         *   _Out_ LPDWORD pcbNeeded
         * );
         */
        /// <summary>
        /// The GetPrinter function retrieves information about a specified printer.
        /// </summary>
        /// <param name="hPrinter">A handle to the printer for which the function retrieves information.</param>
        /// <param name="Level">
        /// The level or type of structure that the function stores into the buffer pointed to by pPrinter.
        /// This value can be [1-9].
        /// </param>
        /// <param name="pPrinter">
        /// A pointer to a buffer that receives a structure containing information about the specified printer.
        /// The buffer must be large enough to receive the structure and any strings or other data to which the structure members point.
        /// If the buffer is too small, the pcbNeeded parameter returns the required buffer size.
        /// </param>
        /// <param name="cbBuf">The size, in bytes, of the buffer pointed to by pPrinter.</param>
        /// <param name="pcbNeeded">
        /// A pointer to a variable that the function sets to the size, in bytes, of the printer information.
        /// If cbBuf is smaller than this value, GetPrinter fails, and the value represents the required buffer size.
        /// If cbBuf is equal to or greater than this value, GetPrinter succeeds, and the value represents the number of bytes stored in the buffer.
        /// </param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "GetPrinterW", SetLastError = true)]
        static extern bool GetPrinter(IntPtr hPrinter, int Level, IntPtr pPrinter, int cbBuf, out int pcbNeeded);

        /* SetPrinter
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd145082(v=vs.85).aspx
         * BOOL SetPrinter(
         *   _In_ HANDLE hPrinter,
         *   _In_ DWORD  Level,
         *   _In_ LPBYTE pPrinter,
         *   _In_ DWORD  Command
         * );
         */
        /// <summary>
        /// The SetPrinter function sets the data for a specified printer or sets the state of the specified printer by pausing printing, resuming printing, or clearing all print jobs.
        /// </summary>
        /// <param name="hPrinter">A handle to the printer.</param>
        /// <param name="Level">
        /// The type of data that the function stores into the buffer pointed to by pPrinter.
        /// If the Command parameter is not equal to zero, the Level parameter must be zero.
        /// This value can be 0 or [2-9].
        /// </param>
        /// <param name="pPrinter">
        /// If the Command parameter is PRINTER_CONTROL_SET_STATUS, pPrinter must contain a DWORD value that specifies the new printer status to set.
        /// For a list of the possible status values, see the Status member of the PRINTER_INFO_2 structure.
        /// Note that PRINTER_STATUS_PAUSED and PRINTER_STATUS_PENDING_DELETION are not valid status values to set.
        /// 
        /// If Level is 0, but the Command parameter is not PRINTER_CONTROL_SET_STATUS, pPrinter must be NULL.
        /// </param>
        /// <param name="Command"></param>
        /// <returns></returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "SetPrinterW", SetLastError = true)]
        static extern bool SetPrinter(IntPtr hPrinter, int Level, IntPtr pPrinter, PrinterCommand Command);
        internal enum PrinterCommand
        {
            None = 0,
            /// <summary>
            /// PRINTER_CONTROL_PAUSE
            /// Pause the printer.
            /// </summary>
            Pause = 1,
            /// <summary>
            /// PRINTER_CONTROL_RESUME
            /// Resume a paused printer.
            /// </summary>
            Resume = 2,
            /// <summary>
            /// PRINTER_CONTROL_PURGE
            /// Delete all print jobs in the printer.
            /// </summary>
            Purge = 3,
            /// <summary>
            /// PRINTER_CONTROL_SET_STATUS
            /// Set the printer status.
            /// Set the pPrinter parameter to a pointer to a DWORD value that specifies the new printer status.
            /// </summary>
            SetStatus = 4
        }

        #endregion
    }
}
