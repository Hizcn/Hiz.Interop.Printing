using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    partial class Native
    {
        static readonly int SizeOfWChar = 2;

        #region Printer Form Functions

        internal static object[] ThunkEnumForms(IntPtr hPrinter, int level)
        {
            Type type = null;
            switch (level)
            {
                case 1:
                    type = typeof(FormInfo1);
                    break;
                case 2:
                    type = typeof(FormInfo2);
                    break;
                default:
                    throw Error.ArgumentOutOfRange("level");
            }

            int pcbNeeded;
            int pcReturned;
            if (!EnumForms(hPrinter, level, IntPtr.Zero, 0, out pcbNeeded, out pcReturned))
                Error.ThrowIfLastErrorIsNotInsufficientBuffer();

            if (pcbNeeded > 0)
            {
                var pBuffer = IntPtr.Zero;
                try
                {
                    // 分配本地内存
                    pBuffer = Marshal.AllocHGlobal(pcbNeeded);
                    if (!EnumForms(hPrinter, level, pBuffer, pcbNeeded, out pcbNeeded, out pcReturned))
                        Error.ThrowLastError();

                    var p = pBuffer;
                    var size = Marshal.SizeOf(type);
                    var result = new object[pcReturned];
                    for (var i = 0; i < pcReturned; i++)
                    {
                        result[i] = Marshal.PtrToStructure(p, type);
                        p += size;
                    }
                    return result;
                }
                finally
                {
                    // 释放本地内存
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                }
            }
            return new object[0];
        }

        /* 经过测试:
         * 
         * a). 同个打印服务器内的所有打印机, 结果全都一样;
         * 例如:
         * GetForms("安装的本地打印机") == GetForms(NULL);
         * GetForms("链接的共享打印机") 结果来自共享的计算机;
         * 
         * b). 调用 AddForm 添加表单, 即使指定打印机名, 本机(不含链接)所有支持该尺寸的打印机都会加上;
         * 
         * c). 通过系统操作:
         *     安装打印机后, 打开打印机首选项 => 高级 => 纸张规格: 已包含支持的本机自定义的纸张;
         * 
         * 结论:
         * 添加纸张针对的是: "打印服务器", 该服务器下的所有打印机, 只要支持该尺寸都会显示在可选的纸张列表里;
         */
        internal static void ThunkAddForm(IntPtr hPrinter, FormInfo1 pForm)
        {
            var level = 1;
            var type = typeof(FormInfo1);

            var pBuffer = IntPtr.Zero;
            try
            {
                pBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(type));
                Marshal.StructureToPtr(pForm, pBuffer, false/*仅在指针位置存在旧的同类数据时才设置为真*/);

                if (!AddForm(hPrinter, level, pBuffer))
                    Error.ThrowLastError();
            }
            finally
            {
                if (pBuffer != IntPtr.Zero)
                {
                    Marshal.DestroyStructure(pBuffer, type); // 用于销毁结构的引用字段占用的内存;
                    Marshal.FreeHGlobal(pBuffer);
                }
            }
        }

        internal static void ThunkDeleteForm(IntPtr hPrinter, string pFormName)
        {
            if (!DeleteForm(hPrinter, pFormName))
                Error.ThrowLastError();
        }

        // 用于替换指定名称纸张数据;
        internal static void ThunkSetForm(IntPtr hPrinter, string pFormName, FormInfo1 pForm)
        {
            var level = 1;
            var type = typeof(FormInfo1);

            var pBuffer = IntPtr.Zero;
            try
            {

                pBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(type));
                Marshal.StructureToPtr(pForm, pBuffer, false/*仅在指针位置存在旧的同类数据时才设置为真*/);

                if (!SetForm(hPrinter, pFormName, level, pBuffer))
                    Error.ThrowLastError();
            }
            finally
            {
                if (pBuffer != IntPtr.Zero)
                {
                    Marshal.DestroyStructure(pBuffer, type); // 用于销毁结构的引用字段占用的内存;
                    Marshal.FreeHGlobal(pBuffer);
                }
            }
        }

        internal static object ThunkGetForm(IntPtr hPrinter, string pFormName, int level)
        {
            Type type;
            switch (level)
            {
                case 1:
                    type = typeof(FormInfo1);
                    break;
                case 2:
                    type = typeof(FormInfo2);
                    break;
                default:
                    throw Error.ArgumentOutOfRange("level");
            }

            int pcbNeeded;
            if (!GetForm(hPrinter, pFormName, level, IntPtr.Zero, 0, out pcbNeeded))
                Error.ThrowIfLastErrorAreNot(Error.ERROR_INSUFFICIENT_BUFFER, Error.ERROR_INVALID_FORM_NAME);

            if (pcbNeeded > 0)
            {
                var pBuffer = IntPtr.Zero;
                try
                {
                    pBuffer = Marshal.AllocHGlobal(pcbNeeded);
                    if (!GetForm(hPrinter, pFormName, level, pBuffer, pcbNeeded, out pcbNeeded))
                        Error.ThrowLastError();

                    return Marshal.PtrToStructure(pBuffer, type);
                }
                finally
                {
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                }
            }
            return null;
        }

        #endregion

        #region Printer Functions

        /* If Level is 4, you can only use the PRINTER_ENUM_CONNECTIONS and PRINTER_ENUM_LOCAL constants.
         * Note:
         * 3D print devices are not enumerated by default.
         * You must include both PRINTER_ENUM_CATEGORY_3D and PRINTER_ENUM_LOCAL to enumerate only 3D printers.
         * To include 3D printers, along with all other local printers, use PRINTER_ENUM_CATEGORY_ALL and PRINTER_ENUM_LOCAL.
         */
        const PrinterFlags EnumPrintersFlagsMaskLevel1 = PrinterFlags.Local | PrinterFlags.Connections | PrinterFlags.Name | PrinterFlags.Shared | PrinterFlags.Remote | PrinterFlags.Network;
        const PrinterFlags EnumPrintersFlagsMaskLevel2 = PrinterFlags.Local | PrinterFlags.Connections | PrinterFlags.Name | PrinterFlags.Shared;
        const PrinterFlags EnumPrintersFlagsMaskLevel4 = PrinterFlags.Local | PrinterFlags.Connections;
        const PrinterFlags EnumPrintersFlagsMaskLevel5 = PrinterFlags.Local | PrinterFlags.Connections | PrinterFlags.Name | PrinterFlags.Shared;

        /* System.Printing.dll
         * MS.Internal.PrintWin32Thunk.PrinterThunkHandler.ThunkEnumPrinters();
         * 
         * System.Drawing.dll:
         * System.Drawing.Printing.PrinterSettings.InstalledPrinters.Get;
         */
        internal static object[] ThunkEnumPrinters(PrinterFlags flags, string name, int level)
        {
            Type type = null;
            switch (level)
            {
                case 1:
                    type = typeof(PrinterInfo1);
                    break;
                case 2:
                    type = typeof(PrinterInfo2);
                    break;
                case 4:
                    type = typeof(PrinterInfo4);
                    break;
                case 5:
                    type = typeof(PrinterInfo5);
                    break;
                default:
                    throw Error.ArgumentOutOfRange("level");
            }

            // 所需缓存字节数量
            int pcbNeeded;
            // 数据条目数量
            int pcReturned;
            // 首次调用 为了获知: 所需缓存字节数量
            if (!EnumPrinters(flags, name, level, IntPtr.Zero, 0, out pcbNeeded, out pcReturned))
                Error.ThrowIfLastErrorIsNotInsufficientBuffer();

            if (pcbNeeded > 0)
            {
                var pBuffer = IntPtr.Zero;
                try
                {
                    // 分配本地内存
                    pBuffer = Marshal.AllocHGlobal(pcbNeeded);
                    if (!EnumPrinters(flags, name, level, pBuffer, pcbNeeded, out pcbNeeded, out pcReturned))
                        Error.ThrowLastError();

                    var p = pBuffer; // 当前数据起始指针
                    var size = Marshal.SizeOf(type); // 单个数据指针增量
                    var infos = new object[pcReturned];
                    for (int i = 0; i < pcReturned; i++)
                    {
                        infos[i] = Marshal.PtrToStructure(p, type);
                        p += size;
                    }
                    return infos;
                }
                finally
                {
                    // 释放本地内存
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                }
            }
            return new object[0];
        }

        internal static object ThunkGetPrinter(IntPtr hPrinter, int level)
        {
            Type type;
            switch (level)
            {
                case 1:
                    type = typeof(PrinterInfo1);
                    break;
                case 2:
                    type = typeof(PrinterInfo2);
                    break;
                case 3:
                    type = typeof(PrinterInfo3);
                    break;
                case 4:
                    type = typeof(PrinterInfo4);
                    break;
                case 5:
                    type = typeof(PrinterInfo5);
                    break;
                case 6:
                    type = typeof(PrinterInfo6);
                    break;
                case 7:
                    type = typeof(PrinterInfo7);
                    break;
                case 8:
                    type = typeof(PrinterInfo8);
                    break;
                case 9:
                    type = typeof(PrinterInfo9);
                    break;
                default:
                    throw Error.ArgumentOutOfRange("level");
            }

            int pcbNeeded;
            if (!GetPrinter(hPrinter, level, IntPtr.Zero, 0, out pcbNeeded))
                Error.ThrowIfLastErrorIsNotInsufficientBuffer();

            if (pcbNeeded > 0)
            {
                var pBuffer = IntPtr.Zero;
                try
                {
                    pBuffer = Marshal.AllocHGlobal(pcbNeeded);
                    if (!GetPrinter(hPrinter, level, pBuffer, pcbNeeded, out pcbNeeded))
                        Error.ThrowLastError();

                    return Marshal.PtrToStructure(pBuffer, type);
                }
                finally
                {
                    //if (pBuffer != IntPtr.Zero)
                    //{
                    //    Marshal.FreeHGlobal(pBuffer);
                    //    pBuffer = IntPtr.Zero;
                    //}
                }
            }

            return null;
        }

        internal static void ThunkSetPrinter(IntPtr hPrinter, IPrinterInfo pPrinter)
        {
            if (pPrinter == null)
                Error.ArgumentNull("pPrinter");

            var type = pPrinter.GetType();
            var level = GetPrinterInfoLevel(type);
            if (level < 2)
                Error.ArgumentOutOfRange("pPrinter");

            var pBuffer = IntPtr.Zero;
            try
            {
                pBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(type));
                Marshal.StructureToPtr(pPrinter, pBuffer, false);

                if (!SetPrinter(hPrinter, level, pBuffer, PrinterCommand.None))
                    Error.ThrowLastError();
            }
            finally
            {
                if (pBuffer != IntPtr.Zero)
                {
                    Marshal.DestroyStructure(pBuffer, type);
                    Marshal.FreeHGlobal(pBuffer);
                }
            }
        }
        static int GetPrinterInfoLevel(Type type)
        {
            if (type == typeof(PrinterInfo1))
                return 1;
            if (type == typeof(PrinterInfo2))
                return 2;
            if (type == typeof(PrinterInfo3))
                return 3;
            if (type == typeof(PrinterInfo4))
                return 4;
            if (type == typeof(PrinterInfo5))
                return 5;
            if (type == typeof(PrinterInfo6))
                return 6;
            if (type == typeof(PrinterInfo7))
                return 7;
            if (type == typeof(PrinterInfo8))
                return 8;
            if (type == typeof(PrinterInfo9))
                return 9;
            return 0;
        }

        #endregion
    }
}