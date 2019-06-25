using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PrintSpoolerLibrary.Internal {
    internal static class DllNames {
        public const string WinSpool = "winspool.drv";
    }

    [SecurityCritical(SecurityCriticalScope.Everything), SuppressUnmanagedCodeSecurity]
    internal sealed class NativeMethods {
        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumPrinters(
            [In] uint Flags,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string Name,
            uint Level,
            [Out, Optional] IntPtr pPrinterEnum,
            uint cbBuf,
            [Out] uint pcbNeeded,
            [Out] uint pcReturned
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetSpoolFileHandle(
            [In] IntPtr hPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CommitSpoolData(
            [In] IntPtr hPrinter,
            [In] IntPtr hSpoolFile,
            uint cbCommit
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseSpoolFileHandle(
            [In] IntPtr hPrinter,
            [In] IntPtr hSpoolFile
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenPrinter(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pPrinterName,
            [Out] IntPtr phPrinter,
            [In, Optional] PRINTER_DEFAULTS pDefault
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ResetPrinter(
            [In] IntPtr hPrinter,
            [In, Optional] PRINTER_DEFAULTS pDefault
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetJob(
            [In] IntPtr hPrinter,
            uint JobId,
            uint Level,
            [In, Optional] IntPtr pJob,
            uint Command
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetJob(
            [In] IntPtr hPrinter,
            uint JobId,
            uint Level,
            [Out, Optional] IntPtr pJob,
            uint cbBuf,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumJobs(
            [In] IntPtr hPrinter,
            uint FirstJob,
            uint NoJobs,
            uint Level,
            [Out, Optional] IntPtr pJob,
            uint cbBuf,
            [Out] uint pcbNeeded,
            [Out] uint pcReturned
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr AddPrinter(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            uint Level,
            [In] IntPtr pPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeletePrinter(
            IntPtr hPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPrinter(
            [In] IntPtr hPrinter,
            uint Level,
            [In, Optional] IntPtr pPrinter,
            uint Command
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPrinter(
            [In] IntPtr hPrinter,
            uint Level,
            [Out, Optional] IntPtr pPrinter,
            uint cbBuf,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddPrinterDriver(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            uint Level,
            [In] IntPtr pDriverInfo
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddPrinterDriverEx(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            uint Level,
            [In] IntPtr pDriverInfo,
            uint dwFileCopyFlags
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumPrinterDrivers(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            uint Level,
            [In] IntPtr pDriverInfo,
            uint cbBuf,
            [Out] uint pcbNeeded,
            [Out] uint pcReturned
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPrinterDriverDirectory(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            uint Level,
            [In] IntPtr pDriverDirectory,
            uint cbBuf,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeletePrinterDriver(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pDriverName
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeletePrinterDriverEx(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pDriverName,
            uint dwDeleteFlag,
            uint dwVersionFlag
            );


        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddPrintProcessor(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pPathName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pPrintProcessorName
            );


        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumPrintProcessors(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            uint Level,
            [Out, Optional] IntPtr pPrintProcessorInfo,
            uint cbBuf,
            [Out] uint pcbNeeded,
            [Out] uint pcReturned
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPrintProcessorDirectory(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            uint Level,
            [Out, Optional] IntPtr pPrintProcessorInfo,
            uint cbBuf,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumPrintProcessorDatatypes(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pPrintProcessorName,
            uint Level,
            [Out, Optional] IntPtr pPrintProcessorInfo,
            uint cbBuf,
            [Out] uint pcbNeeded,
            [Out] uint pcReturned
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeletePrintProcessor(
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string pEnvironment,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pPrintProcessorName
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint StartDocPrinter(
            [In] IntPtr hPrinter,
            uint Level,
            uint fdwOptions,
            [In] IntPtr pDocInfo
            );


        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StartPagePrinter(
            [In] IntPtr hPrinter
            );


        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrinter(
            [In] IntPtr hPrinter,
            [In] IntPtr pBuf,
            uint cbBuf,
            [Out] uint pcWritten
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlushPrinter(
            [In] IntPtr hPrinter,
            [In] IntPtr pBuf,
            uint cbBuf,
            [Out] uint pcWritten,
            uint cSleep
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EndPagePrinter(
            [In] IntPtr hPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AbortPrinter(
            [In] IntPtr hPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadPrinter(
            [In] IntPtr hPrinter,
            [In] IntPtr pBuf,
            uint cbBuf,
            [Out] uint pNoBytesRead
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EndDocPrinter(
            [In] IntPtr hPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddJob(
            [In] IntPtr hPrinter,
            uint Level,
            [Out, Optional] IntPtr pData,
            uint cbBuf,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ScheduleJob(
            [In] IntPtr hPrinter,
            uint JobId
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrinterProperties(
            [In] IntPtr hWnd,
            [In] IntPtr hPrinter
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DocumentProperties(
            [In, Optional] IntPtr hWnd,
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
            [Out, Optional] DEVMODE pDevModeOutput,
            [In, Optional] DEVMODE pDevModeInput,
            uint fMode
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int AdvancedDocumentProperties(
            [In] IntPtr hWnd,
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
            [In, Out, Optional] DEVMODE pDevModeOutput,
            [In, Optional] DEVMODE pDevModeInput
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetPrinterData(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pValueName,
            [Out, Optional] uint pType,
            [Out, Optional] IntPtr pData,
            uint nSize,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetPrinterDataEx(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pKeyName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pValueName,
            [Out, Optional] uint pType,
            [Out, Optional] IntPtr pData,
            uint nSize,
            [Out] uint pcbNeeded
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint EnumPrinterData(
            [In] IntPtr hPrinter,
            uint dwIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)] string pValueName,
            uint cbValueName,
            [Out] uint pcbValueName,
            [Out, Optional] uint pType,
            IntPtr pData,
            uint cbData,
            [Out, Optional] uint pcbData
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint EnumPrinterDataEx(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pKeyName,
            [Out, Optional] IntPtr pEnumValues,
            uint cbEnumValues,
            [Out] uint pcbEnumValues,
            [Out] uint pnEnumValues
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint EnumPrinterKey(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pKeyName,
            [Out, Optional, MarshalAs(UnmanagedType.LPWStr)] IntPtr pSubkey,
            uint cbSubkey,
            [Out] uint pcbSubkey
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint SetPrinterData(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pValueName,
            uint Type,
            [In] IntPtr pData,
            uint cbData
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint SetPrinterDataEx(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pKeyName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pValueName,
            uint Type,
            [In] IntPtr pData,
            uint cbData
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint DeletePrinterData(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pValueName
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint DeletePrinterDataEx(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pKeyName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pValueName
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint DeletePrinterKey(
            [In] IntPtr hPrinter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pKeyName
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindFirstPrinterChangeNotification(
            [In] IntPtr hPrinter,
            uint fwdFlags,
            uint fdwOptions,
            [In, Optional] PRINTER_NOTIFY_OPTIONS pPrinterNotifyOptions
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextPrinterChangeNotification(
            [In] IntPtr hChange,
            [Out, Optional] uint pdwChange,
            [In, Optional] IntPtr pvReserved,
            [Out, Optional] IntPtr ppPrinterNotifyInfo //PRINTER_NOTIFY_OPTIONS insteadof IntPtr?
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreePrinterNotifyInfo(
            [In] PRINTER_NOTIFY_INFO pPrinterNotifyInfo
            );

        [DllImport(DllNames.WinSpool, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClosePrinterChangeNotification(
            [In] IntPtr hChange
            );


        //TODO: TBD
        //#define PRINTER_DRIVER_PACKAGE_AWARE    0x00000001


        //// FLAGS for dwDriverAttributes
        //#define DRIVER_KERNELMODE                0x00000001
        //#define DRIVER_USERMODE                  0x00000002

        //// FLAGS for DeletePrinterDriverEx.
        //#define DPD_DELETE_UNUSED_FILES          0x00000001
        //#define DPD_DELETE_SPECIFIC_VERSION      0x00000002
        //#define DPD_DELETE_ALL_FILES             0x00000004

        //// FLAGS for AddPrinterDriverEx.
        //#define APD_STRICT_UPGRADE               0x00000001
        //#define APD_STRICT_DOWNGRADE             0x00000002
        //#define APD_COPY_ALL_FILES               0x00000004
        //#define APD_COPY_NEW_FILES               0x00000008

        //#if (NTDDI_VERSION >= NTDDI_WINXP)
        //    #define APD_COPY_FROM_DIRECTORY      0x00000010
        //#endif // (NTDDI_VERSION >= NTDDI_WINXP)


        //#define DI_CHANNEL              1    // start direct read/write channel,
        //#define DI_READ_SPOOL_JOB       3
        //#define DI_MEMORYMAP_WRITE   0x00000001

        //#define SPOOL_FILE_PERSISTENT    0x00000001
        //#define SPOOL_FILE_TEMPORARY     0x00000002
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_1 {
        public uint Flags;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDescription;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pComment;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_2 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pServerName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrinterName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pShareName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPortName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pComment;
        [MarshalAs(UnmanagedType.LPWStr)] public string pLocation;
        public DEVMODE pDevMode;
        [MarshalAs(UnmanagedType.LPWStr)] public string pSepFile;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrintProcessor;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        [MarshalAs(UnmanagedType.LPWStr)] public string pParameters;
        public IntPtr pSecurityDescriptor;
        public uint Attributes;
        public uint Priority;
        public uint DefaultPriority;
        public uint StartTime;
        public uint UntilTime;
        public uint Status;
        public uint cJobs;
        public uint AveragePPM;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_3 {
        public IntPtr pSecurityDescriptor;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_4 {
        [MarshalAs(UnmanagedType.LPWStr)] public uint Attributes;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrinterName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pServerName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_5W {
        public uint Attributes;
        public uint DeviceNotSelectedTimeout;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPortName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrinterName;
        public uint TransmissionRetryTimeout;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_6 {
        public uint dwStatus;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_7 {
        public uint dwAction;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszObjectGUID;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_8 {
        public DEVMODE pDevMode;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_INFO_9 {
        public DEVMODE pDevMode;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class JOB_INFO_1 {
        public uint JobId;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrinterName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pMachineName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pUserName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocument;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        [MarshalAs(UnmanagedType.LPWStr)] public string pStatus;
        public uint Status;
        public uint Priority;
        public uint Position;
        public uint TotalPages;
        public uint PagesPrinted;
        public SYSTEMTIME Submitted;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class JOB_INFO_2 {
        public uint JobId;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrinterName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pMachineName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pUserName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocument;
        [MarshalAs(UnmanagedType.LPWStr)] public string pNotifyName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrintProcessor;
        [MarshalAs(UnmanagedType.LPWStr)] public string pParameters;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverName;
        public DEVMODE pDevMode;
        [MarshalAs(UnmanagedType.LPWStr)] public string pStatus;
        public IntPtr pSecurityDescriptor;
        public uint Status;
        public uint Priority;
        public uint Position;
        public uint StartTime;
        public uint UntilTime;
        public uint TotalPages;
        public uint Size;
        public SYSTEMTIME Submitted; // Time the job was spooled
        public uint Time; // How many miliseconds the job has been printing
        public uint PagesPrinted;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class JOB_INFO_3 {
        public uint JobId;
        public uint NextJobId;
        public uint Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class JOB_INFO_4 {
        public uint JobId;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrinterName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pMachineName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pUserName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocument;
        [MarshalAs(UnmanagedType.LPWStr)] public string pNotifyName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        [MarshalAs(UnmanagedType.LPWStr)] public string pPrintProcessor;
        [MarshalAs(UnmanagedType.LPWStr)] public string pParameters;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverName;
        public DEVMODE pDevMode;
        [MarshalAs(UnmanagedType.LPWStr)] public string pStatus;
        public IntPtr pSecurityDescriptor;
        public uint Status;
        public uint Priority;
        public uint Position;
        public uint StartTime;
        public uint UntilTime;
        public uint TotalPages;
        public uint Size;
        public SYSTEMTIME Submitted;
        public uint Time;
        public uint PagesPrinted;
        public ulong SizeHigh;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class ADDJOB_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string Path;
        public uint JobId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_2 {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment; // Win32 x86
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverPath; // c:\drivers\pscript.dll
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataFile; // c:\drivers\QMS810.PPD
        [MarshalAs(UnmanagedType.LPWStr)] public string pConfigFile; // c:\drivers\PSCRPTUI.DLL
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_3 {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment; // Win32 x86
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverPath; // c:\drivers\pscript.dll
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataFile; // c:\drivers\QMS810.PPD
        [MarshalAs(UnmanagedType.LPWStr)] public string pConfigFile; // c:\drivers\PSCRPTUI.DLL
        [MarshalAs(UnmanagedType.LPWStr)] public string pHelpFile; // c:\drivers\PSCRPTUI.HLP
        [MarshalAs(UnmanagedType.LPWStr)] public string pDependentFiles; // PSCRIPT.DLL\0QMS810.PPD\0PSCRIPTUI.DLL\0PSCRIPTUI.HLP\0PSTEST.TXT\0\0
        [MarshalAs(UnmanagedType.LPWStr)] public string pMonitorName; // "PJL monitor"
        [MarshalAs(UnmanagedType.LPWStr)] public string pDefaultDataType; // "EMF"
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_4 {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment; // Win32 x86
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverPath; // c:\drivers\pscript.dll
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataFile; // c:\drivers\QMS810.PPD
        [MarshalAs(UnmanagedType.LPWStr)] public string pConfigFile; // c:\drivers\PSCRPTUI.DLL
        [MarshalAs(UnmanagedType.LPWStr)] public string pHelpFile; // c:\drivers\PSCRPTUI.HLP
        [MarshalAs(UnmanagedType.LPWStr)] public string pDependentFiles; // PSCRIPT.DLL\0QMS810.PPD\0PSCRIPTUI.DLL\0PSCRIPTUI.HLP\0PSTEST.TXT\0\0
        [MarshalAs(UnmanagedType.LPWStr)] public string pMonitorName; // "PJL monitor"
        [MarshalAs(UnmanagedType.LPWStr)] public string pDefaultDataType; // "EMF"
        [MarshalAs(UnmanagedType.LPWStr)] public string pszzPreviousNames; // "OldName1\0OldName2\0\0
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_5 {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment; // Win32 x86
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverPath; // c:\drivers\pscript.dll
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataFile; // c:\drivers\QMS810.PPD
        [MarshalAs(UnmanagedType.LPWStr)] public string pConfigFile; // c:\drivers\PSCRPTUI.DLL
        public uint dwDriverAttributes; // driver attributes (like UMPD/KMPD)
        public uint dwConfigVersion; // version number of the config file since reboot
        public uint dwDriverVersion; // version number of the driver file since reboot
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_6 {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment; // Win32 x86
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverPath; // c:\drivers\pscript.dll
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataFile; // c:\drivers\QMS810.PPD
        [MarshalAs(UnmanagedType.LPWStr)] public string pConfigFile; // c:\drivers\PSCRPTUI.DLL
        [MarshalAs(UnmanagedType.LPWStr)] public string pHelpFile; // c:\drivers\PSCRPTUI.HLP
        [MarshalAs(UnmanagedType.LPWStr)] public string pDependentFiles; // PSCRIPT.DLL\0QMS810.PPD\0PSCRIPTUI.DLL\0PSCRIPTUI.HLP\0PSTEST.TXT\0\0
        [MarshalAs(UnmanagedType.LPWStr)] public string pMonitorName; // "PJL monitor"
        [MarshalAs(UnmanagedType.LPWStr)] public string pDefaultDataType; // "EMF"
        [MarshalAs(UnmanagedType.LPWStr)] public string pszzPreviousNames; // "OldName1\0OldName2\0\0
        public FILETIME ftDriverDate;
        public ulong dwlDriverVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszMfgName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszOEMUrl;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszHardwareID;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszProvider;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DRIVER_INFO_8 {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName; // QMS 810
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment; // Win32 x86
        [MarshalAs(UnmanagedType.LPWStr)] public string pDriverPath; // c:\drivers\pscript.dll
        [MarshalAs(UnmanagedType.LPWStr)] public string pDataFile; // c:\drivers\QMS810.PPD
        [MarshalAs(UnmanagedType.LPWStr)] public string pConfigFile; // c:\drivers\PSCRPTUI.DLL
        [MarshalAs(UnmanagedType.LPWStr)] public string pHelpFile; // c:\drivers\PSCRPTUI.HLP
        [MarshalAs(UnmanagedType.LPWStr)] public string pDependentFiles; // PSCRIPT.DLL\0QMS810.PPD\0PSCRIPTUI.DLL\0PSCRIPTUI.HLP\0PSTEST.TXT\0\0
        [MarshalAs(UnmanagedType.LPWStr)] public string pMonitorName; // "PJL monitor"
        [MarshalAs(UnmanagedType.LPWStr)] public string pDefaultDataType; // "EMF"
        [MarshalAs(UnmanagedType.LPWStr)] public string pszzPreviousNames; // "OldName1\0OldName2\0\0
        public FILETIME ftDriverDate;
        public ulong dwlDriverVersion;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszMfgName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszOEMUrl;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszHardwareID;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszProvider;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszPrintProcessor;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszVendorSetup;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszzColorProfiles;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszInfPath;
        public uint dwPrinterDriverAttributes;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszzCoreDriverDependencies;
        public FILETIME ftMinInboxDriverVerDate;
        public ulong dwlMinInboxDriverVerVersion;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DOC_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DOC_INFO_2 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        public uint dwMode;
        public uint JobId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DOC_INFO_3 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        public uint dwFlags;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class FORM_INFO_1 {
        public uint Flags;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
        public SIZEL Size;
        public RECTL ImageableArea;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class FORM_INFO_2 {
        // Only >= VISTA
        public uint Flags;
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
        public SIZEL Size;
        public RECTL ImageableArea;
        [MarshalAs(UnmanagedType.LPWStr)] public string pKeyword;
        public uint StringType;
        [MarshalAs(UnmanagedType.LPWStr)] public string pMuiDll;
        public uint dwResourceId;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDisplayName;
        public ushort wLangId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTPROCESSOR_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTPROCESSOR_CAPS_1 {
        // Only >= WINXP
        public uint dwLevel;
        public uint dwNupOptions;
        public uint dwPageOrderFlags;
        public uint dwNumberOfCopies;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PORT_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PORT_INFO_2 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pPortName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pMonitorName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDescription;
        public uint fPortType;
        public uint Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PORT_INFO_3 {
        public uint dwStatus;
        [MarshalAs(UnmanagedType.LPWStr)] public string pszStatus;
        public uint dwSeverity;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class MONITOR_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class MONITOR_INFO_2 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
        [MarshalAs(UnmanagedType.LPWStr)] public string pEnvironment;
        [MarshalAs(UnmanagedType.LPWStr)] public string pDLLName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DATATYPES_INFO_1 {
        [MarshalAs(UnmanagedType.LPWStr)] public string pName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_DEFAULTS {
        [MarshalAs(UnmanagedType.LPWStr)] public string pDatatype;
        public DEVMODE pDevMode;
        public ACCESS_MASK DesiredAccess;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_ENUM_VALUES {
        [MarshalAs(UnmanagedType.LPWStr)] public string pValueName;
        public uint cbValueName;
        public uint dwType;
        public IntPtr pData;
        public uint cbData;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class DEVMODE {
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_NOTIFY_OPTIONS_TYPE {
        public ushort Type;
        public ushort Reserved0;
        public uint Reserved1;
        public uint Reserved2;
        public uint Count;
        public ushort pFields;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_NOTIFY_OPTIONS {
        public uint Version;
        public uint Flags;
        public uint Count;
        public PRINTER_NOTIFY_OPTIONS_TYPE[] pTypes;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_NOTIFY_INFO_DATA {
        public ushort Type;
        public ushort Field;
        public uint Reserved;
        public uint Id;
        public NotifyData NotifyData;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct NotifyData
    {
        //public unsafe fixed uint adwData[2];
        public uint[] adwData;
        public uint cbBuf;
        public IntPtr pBuf;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class PRINTER_NOTIFY_INFO {
        public uint Version;
        public uint Flags;
        public uint Count;
        public PRINTER_NOTIFY_INFO_DATA aData;
    }

    internal enum PRIORITY {
        DEF_PRIORITY = 1,
        MAX_PRIORITY = 99,
        MIN_PRIORITY = 1,
        NO_PRIORITY = 0
    }

    internal enum PRINTER_CONTROL {
        PAUSE = 1,
        RESUME = 2,
        PURGE = 3,
        SET_STATUS = 4
    }

    internal enum JOB_CONTROL {
        PAUSE = 1,
        RESUME = 2,
        CANCEL = 3,
        RESTART = 4,
        DELETE = 5,
        SENT_TO_PRINTER = 6,
        LAST_PAGE_EJECTED = 7,
        RETAIN = 8, //Only >= VISTA
        RELEASE = 9 //Only >= VISTA
    }

    [Flags]
    internal enum JOB_STATUS {
        PAUSED = 0x00000001,
        ERROR = 0x00000002,
        DELETING = 0x00000004,
        SPOOLING = 0x00000008,
        PRINTING = 0x00000010,
        OFFLINE = 0x00000020,
        PAPEROUT = 0x00000040,
        PRINTED = 0x00000080,
        DELETED = 0x00000100,
        BLOCKED_DEVQ = 0x00000200,
        USER_INTERVENTION = 0x00000400,
        RESTART = 0x00000800,
        COMPLETE = 0x00001000, // Only >= WINXP
        RETAINED = 0x00002000, // Only >= VISTA
        RENDERING_LOCALLY = 0x00004000 // Only >= VISTA
    }

    [Flags]
    internal enum DS_PRINT : uint {
        PUBLISH = 0x00000001,
        UPDATE = 0x00000002,
        UNPUBLISH = 0x00000004,
        REPUBLISH = 0x00000008,
        PENDING = 0x80000000
    }

    [Flags]
    internal enum PRINTER_STATUS {
        PAUSED = 0x00000001,
        ERROR = 0x00000002,
        PENDING_DELETION = 0x00000004,
        PAPER_JAM = 0x00000008,
        PAPER_OUT = 0x00000010,
        MANUAL_FEED = 0x00000020,
        PAPER_PROBLEM = 0x00000040,
        OFFLINE = 0x00000080,
        IO_ACTIVE = 0x00000100,
        BUSY = 0x00000200,
        PRINTING = 0x00000400,
        OUTPUT_BIN_FULL = 0x00000800,
        NOT_AVAILABLE = 0x00001000,
        WAITING = 0x00002000,
        PROCESSING = 0x00004000,
        INITIALIZING = 0x00008000,
        WARMING_UP = 0x00010000,
        TONER_LOW = 0x00020000,
        NO_TONER = 0x00040000,
        PAGE_PUNT = 0x00080000,
        USER_INTERVENTION = 0x00100000,
        OUT_OF_MEMORY = 0x00200000,
        DOOR_OPEN = 0x00400000,
        SERVER_UNKNOWN = 0x00800000,
        POWER_SAVE = 0x01000000,
        SERVER_OFFLINE = 0x02000000,
        DRIVER_UPDATE_NEEDED = 0x04000000
    }

    [Flags]
    internal enum PRINTER_ATTRIBUTE {
        QUEUED = 0x00000001,
        DIRECT = 0x00000002,
        DEFAULT = 0x00000004,
        SHARED = 0x00000008,
        NETWORK = 0x00000010,
        HIDDEN = 0x00000020,
        LOCAL = 0x00000040,
        ENABLE_DEVQ = 0x00000080,
        KEEPPRINTEDJOBS = 0x00000100,
        DO_COMPLETE_FIRST = 0x00000200,
        WORK_OFFLINE = 0x00000400,
        ENABLE_BIDI = 0x00000800,
        RAW_ONLY = 0x00001000,
        PUBLISHED = 0x00002000,
        FAX = 0x00004000, // Only >= WINXP
        TS = 0x00008000, // Only >= WS03 || WIN2KSP4
        PUSHED_USER = 0x00020000, // Only >= VISTA
        PUSHED_MACHINE = 0x00040000, // Only >= VISTA
        MACHINE = 0x00080000, // Only >= VISTA
        FRIENDLY_NAME = 0x00100000, // Only >= VISTA
        TS_GENERIC_DRIVER = 0x00200000 // Only >= VISTA
    }

    [Flags]
    internal enum FORM {
        FORM_USER = 0x00000000,
        FORM_BUILTIN = 0x00000001,
        FORM_PRINTER = 0x00000002
    }

    [Flags]
    internal enum PRINTPROCESSOR_CAPS_PAGE_ORDER {
        NORMAL = 0x00000000,
        REVERSE = 0x00000001
    }

    [Flags]
    internal enum PRINTPROCESSOR_CAPS_DIRECTION {
        RIGHT_THEN_DOWN = 0x00000001,
        DOWN_THEN_RIGHT = 0x00000002,
        LEFT_THEN_DOWN = 0x00000004,
        DOWN_THEN_LEFT = 0x00000008
    }

    [Flags]
    internal enum PRINTPROCESSOR_CAPS_BORDER_PRINT {
        BORDER_PRINT = 0x00000001,
    }

    [Flags]
    internal enum PRINTPROCESSOR_CAPS_BOOKLET_EDGE {
        BOOKLET_EDGE = 0x00000001,
    }

    [Flags]
    internal enum PRINTPROCESSOR_CAPS_DUPLEXHANDLING {
        REVERSE_PAGES_FOR_REVERSE_DUPLEX = 0x00000001,
        DONT_SEND_EXTRA_PAGES_FOR_DUPLEX = 0x00000002
    }

    [Flags]
    internal enum PRINTPROCESSOR_CAPS_SCALING {
        SQUARE_SCALING = 0x00000001,
    }

    [Flags]
    internal enum PORT_TYPE {
        WRITE = 0x0001,
        READ = 0x0002,
        REDIRECTED = 0x0004,
        NET_ATTACHED = 0x0008,
    }

    internal enum PORT_STATUS_TYPE {
        PORT_STATUS_TYPE_ERROR = 1,
        PORT_STATUS_TYPE_WARNING = 2,
        PORT_STATUS_TYPE_INFO = 3,
    }

    internal enum PORT_STATUS {
        OFFLINE = 1,
        PAPER_JAM = 2,
        PAPER_OUT = 3,
        OUTPUT_BIN_FULL = 4,
        PAPER_PROBLEM = 5,
        NO_TONER = 6,
        DOOR_OPEN = 7,
        USER_INTERVENTION = 8,
        OUT_OF_MEMORY = 9,
        TONER_LOW = 10,
        WARMING_UP = 11,
        POWER_SAVE = 12
    }

    [Flags]
    internal enum PRINTER_ENUM {
        DEFAULT = 0x00000001,
        LOCAL = 0x00000002,
        CONNECTIONS = 0x00000004,
        FAVORITE = 0x00000004,
        NAME = 0x00000008,
        REMOTE = 0x00000010,
        SHARED = 0x00000020,
        NETWORK = 0x00000040,
        EXPAND = 0x00004000,
        CONTAINER = 0x00008000,
        ICONMASK = 0x00ff0000,
        ICON1 = 0x00010000,
        ICON2 = 0x00020000,
        ICON3 = 0x00040000,
        ICON4 = 0x00080000,
        ICON5 = 0x00100000,
        ICON6 = 0x00200000,
        ICON7 = 0x00400000,
        ICON8 = 0x00800000,
        HIDE = 0x01000000
    }

    internal enum NOTIFY_TYPE {
        PRINTER = 0x00,
        JOB = 0x01
    }

    internal enum PRINTER_NOTIFY_FIELD {
        SERVER_NAME = 0x00,
        PRINTER_NAME = 0x01,
        SHARE_NAME = 0x02,
        PORT_NAME = 0x03,
        DRIVER_NAME = 0x04,
        COMMENT = 0x05,
        LOCATION = 0x06,
        DEVMODE = 0x07,
        SEPFILE = 0x08,
        PRINT_PROCESSOR = 0x09,
        PARAMETERS = 0x0A,
        DATATYPE = 0x0B,
        SECURITY_DESCRIPTOR = 0x0C,
        ATTRIBUTES = 0x0D,
        PRIORITY = 0x0E,
        DEFAULT_PRIORITY = 0x0F,
        START_TIME = 0x10,
        UNTIL_TIME = 0x11,
        STATUS = 0x12,
        STATUS_STRING = 0x13,
        CJOBS = 0x14,
        AVERAGE_PPM = 0x15,
        TOTAL_PAGES = 0x16,
        PAGES_PRINTED = 0x17,
        TOTAL_BYTES = 0x18,
        BYTES_PRINTED = 0x19,
        OBJECT_GUID = 0x1A,
        FRIENDLY_NAME = 0x1B // Only >= VISTA
    }

    internal enum JOB_NOTIFY_FIELD {
        PRINTER_NAME = 0x00,
        MACHINE_NAME = 0x01,
        PORT_NAME = 0x02,
        USER_NAME = 0x03,
        NOTIFY_NAME = 0x04,
        DATATYPE = 0x05,
        PRINT_PROCESSOR = 0x06,
        PARAMETERS = 0x07,
        DRIVER_NAME = 0x08,
        DEVMODE = 0x09,
        STATUS = 0x0A,
        STATUS_STRING = 0x0B,
        SECURITY_DESCRIPTOR = 0x0C,
        DOCUMENT = 0x0D,
        PRIORITY = 0x0E,
        POSITION = 0x0F,
        SUBMITTED = 0x10,
        START_TIME = 0x11,
        UNTIL_TIME = 0x12,
        TIME = 0x13,
        TOTAL_PAGES = 0x14,
        PAGES_PRINTED = 0x15,
        TOTAL_BYTES = 0x16,
        BYTES_PRINTED = 0x17,
        REMOTE_JOB_ID = 0x18
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class SYSTEMTIME {
        public ushort wDay;
        public ushort wDayOfWeek;
        public ushort wHour;
        public ushort wMilliseconds;
        public ushort wMinute;
        public ushort wMonth;
        public ushort wSecond;
        public ushort wYear;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class FILETIME {
        public uint dwLowDateTime;
        public uint dwHighDateTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class SIZEL {
        public uint cx;
        public uint cy;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class RECTL {
        public uint left;
        public uint top;
        public uint right;
        public uint bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class ACCESS_MASK {
    }
}