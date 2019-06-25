using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    partial class PrintSpooler
    {
        static object[] GetPrintersWithLevel5()
        {
            var level = 5;
            var array = Native.ThunkEnumPrinters(PrinterFlags.Local | PrinterFlags.Connections, null, level).Cast<PrinterInfo5>().ToArray();
            //return array;
            return null;
        }

        static void Test2()
        {
            var s1 = Marshal.SizeOf(typeof(FormInfo1));


            string p0 = null;
            var p1 = @"\\ZZ-PC\Brother HL-1110 series";
            var p2 = @"\\ZZ-PC\Jolimark 24-pin printer";
            var p3 = "Microsoft XPS Document Writer";
            SetForm(p3);
            //var ps1 = GetForms(p1).Cast<FormInfo1>().Where(p => p.Flags == FormInfoFlags.Printer).ToArray();
            //var ps2 = GetForms(p2).Cast<FormInfo1>().Where(p => p.Flags == FormInfoFlags.Printer).ToArray();

            //var ps3 = GetForms(p3).Cast<FormInfo1>().Where(p => p.Flags == FormInfoFlags.Printer).ToArray();
            //var ps0 = GetForms(p0).Cast<FormInfo1>().Where(p => p.Flags == FormInfoFlags.Printer).ToArray();



            var w2 = (int)(215.9f * 1000);
            var h2 = (int)(139.7f * 1000);
            //AddCustomPaperSize(p3, "Test241/2", w2, h2);


            var ps32 = GetPrinterFormsWithLevel1(p3).Cast<FormInfo2>().Where(p => p.Flags == FormInfoFlags.User).ToArray();
            //var ps32 = GetForms(p3).Cast<FormInfo1>().Where(p => p.Flags == FormInfoFlags.User).ToArray();
            //var ps02 = GetForms(p0).Cast<FormInfo1>().Where(p => p.Flags == FormInfoFlags.User).ToArray();

            var settings3 = new System.Drawing.Printing.PrinterSettings();
            settings3.PrinterName = p3;
            var papers3 = settings3.PaperSizes.Cast<PaperSize>().Where(p => p.Kind == PaperKind.Custom).ToArray();

            var papers4 = GetPrinterPapers(p3).Where(p => p.Kind == PaperKind.Custom).ToArray();
        }

        static void SetForm(string pPrinterName)
        {
            PrinterDefaults pDefault = null;
            if (pPrinterName != null)
                pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.PrinterGenericExecute };
            else // String.Empty 将抛异常: 打印机名无效;
                pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.ServerGenericRead };

            IntPtr hPrinter;
            if (!Native.OpenPrinter(pPrinterName, out hPrinter, pDefault))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var w2 = (int)(215.9f * 1000);
            var h2 = (int)(279.4f * 1000);
            var pFormName = "Test241/2";
            var formInfo1 = new FormInfo1()
            {
                pName = pFormName,
                Flags = FormInfoFlags.User,
                // Unit: 0.001mm.
                Size = new SIZE(w2, h2),
                // Unit: 0.001mm.
                ImageableArea = new RECT(0, 0, w2, h2),
            };
            Native.ThunkSetForm(hPrinter, pFormName, formInfo1);

            var ps32 = GetPrinterFormsWithLevel1(pPrinterName).Cast<FormInfo2>().Where(p => p.Flags == FormInfoFlags.User).ToArray();
        }



        static void Test3()
        {
            //var settings = new System.Drawing.Printing.PrinterSettings();
            //settings.PrinterName = @"\\ZZ-PC\Brother HL-1110 series";
            //var papers0 = settings.PaperSizes.Cast<PaperSize>().ToArray();


            //var papers1 = GetPrinterPapers(@"\\ZZ-PC\Brother HL-1110 series");

            var p2 = @"\\ZZ-PC\Jolimark 24-pin printer";
            var papers2 = GetPrinterPapers(p2);

        }

        static void Test4()
        {
            //var p1 = "Jolimark 24-pin printer";
            //var w1 = (int)(215.9f * 1000);
            //var h1 = (int)(139.7f * 1000);
            //AddCustomPaperSize(p1, "Werp241/2", w1, h1);
            //var settings1 = new System.Drawing.Printing.PrinterSettings();
            //settings1.PrinterName = p1;
            //var papers1 = settings1.PaperSizes.Cast<PaperSize>().ToArray();

            var p2 = @"\\ZZ-PC\Jolimark 24-pin printer";
            var w2 = 215.9;
            var h2 = 139.7;
            AddFormOfServer(p2, "TestWerp241/2", w2, h2, PrintSystemUnit.Millimeter);

            var settings2 = new System.Drawing.Printing.PrinterSettings();
            settings2.PrinterName = p2;
            var papers2 = settings2.PaperSizes.Cast<PaperSize>().ToArray();
        }

        static void Test5()
        {
            string p0 = null;
            var p1 = @"\\ZZ-PC\Brother HL-1110 series";
            var p2 = @"\\ZZ-PC\Jolimark 24-pin printer";
            var p3 = "Microsoft XPS Document Writer";

            GetPrinterDevMode(p3);
        }

        /* The following table shows the EnumPrinters output for various Flags values when the Level parameter is set to 1.
         * 
         * In the Name parameter column of the table, you should substitute an appropriate name for Print Provider, Domain, and Machine.
         * For example, for "{ProviderName}", you could use the name of the network print provider or the name of the local print provider.
         * To retrieve print provider names, call EnumPrinters with Name set to NULL.
         * 
         * |====================================================================================================================================================================|
         * | Flags parameter             | Name parameter                                | Result                                                                               |
         * |=============================|===============================================|======================================================================================|
         * | PRINTER_ENUM_LOCAL          | The Name parameter is ignored.                | All local printers.                                                                  |
         * | (and not PRINTER_ENUM_NAME) |
         * |=============================|===============================================|======================================================================================|
         * |                             | An empty string: ""                           | All local printers.                                                                  |
         * |                             |===============================================|======================================================================================|
         * |                             | NULL                                          | All print providers in the computer's domain.                                        |
         * |                             |===============================================|======================================================================================|
         * |                             | "{ProviderName}"                              | All domain names.                                                                    |
         * |                             |                                               | ReturnItem.pName = "{ProviderName}!{DomainName}"                                     |
         * | PRINTER_ENUM_NAME           |===============================================|======================================================================================|
         * |                             | "{ProviderName}!{DomainName}"                 | All printers and print servers in the computer's domain.                             |
         * |                             |                              ReturnItem.pName : "{ProviderName}!{DomainName}!\\{MachineName}" Or "\\{MachineName}\{SharedName}"      |
         * |                             |===============================================|======================================================================================|
         * |                             | "{ProviderName}!!\\{MachineName}"             | All printers shared at \\Machine.                                                    |
         * |                             | "{ProviderName}!{DomainName}!\\{MachineName}" | ReturnItem.pName = "\\{MachineName}\{SharedName}"                                    |
         * |=============================|===============================================|======================================================================================|
         * | PRINTER_ENUM_CONNECTIONS    | The Name parameter is ignored.                | All connected remote printers.                                                       |
         * |=============================|===============================================|======================================================================================|
         * | PRINTER_ENUM_NETWORK        | The Name parameter is ignored.                | All printers in the computer's domain.                                               |
         * |=============================|===============================================|======================================================================================|
         * |                             | NULL / An empty string: ""                    | All printers and print servers in the computer's domain.                             |
         * |                             |===============================================|======================================================================================|
         * |                             | "{ProviderName}"                              | Same as PRINTER_ENUM_NAME.                                                           |
         * | PRINTER_ENUM_REMOTE         |===============================================|======================================================================================|
         * |                             | "{ProviderName}!{DomainName}"                 | All printers and print servers in computer's domain, regardless of Domain specified. | // 忽略域名, 指定无效;
         * |                             |===============================================|======================================================================================|
         * |                             |               Above results: ReturnItem.pName : "{ProviderName}!!\\{MachineName}" Or "\\{MachineName}\{SharedName}"                  |
         * |====================================================================================================================================================================|
         * 分析: {DomainName} 省略代表本机当前域名(例如: WORKGROUP);
         */
        static void TestGetPrintersWithLevel1()
        {
            const int level = 1;

            /* Windows10 x64 zh-CN:
             * 
             * "Windows NT Local Print Providor"; Flags: Container | Icon1;
             * "LanMan 打印服务"; Flags: Expand | Container | Icon1;
             * "Windows NT Internet Provider"; Flags: Container | Icon1 | Hide;
             */
            var providers = Native.ThunkEnumPrinters(PrinterFlags.Name, null, level).Cast<PrinterInfo1>().ToArray();
            var localProvidor = providers.Where(i => i.pName.Contains("Local")).Single().pName;
            var lanManProvidor = providers.Where(i => i.pName.Contains("LanMan")).Single().pName;
            var internetProvidor = providers.Where(i => i.pName.Contains("Internet")).Single().pName; // Flags 含有 Hide 标识, 不能用于枚举, 否则将抛参数错误异常;

            // 安装的本地打印机 (不含链接的外部共享打印机) // 以下三种方式结果相同;
            var locals1 = Native.ThunkEnumPrinters(PrinterFlags.Local, null, level).Cast<PrinterInfo1>().ToArray();
            var locals2 = Native.ThunkEnumPrinters(PrinterFlags.Name, string.Empty, level).Cast<PrinterInfo1>().ToArray();
            var locals3 = Native.ThunkEnumPrinters(PrinterFlags.Name, localProvidor, level).Cast<PrinterInfo1>().ToArray();

            // 链接的外部共享打印机 (不含安装的本地打印机)
            var connections = Native.ThunkEnumPrinters(PrinterFlags.Connections, null, level).Cast<PrinterInfo1>().ToArray();

            // 链接的网络打印机 (有线网线/无线方式)?
            var networks = Native.ThunkEnumPrinters(PrinterFlags.Network, null, level).Cast<PrinterInfo1>().ToArray();

            // 返回结果: 开启共享的本地打印机 + 所有链接的外部共享打印机;
            var shareds = Native.ThunkEnumPrinters(PrinterFlags.Shared | PrinterFlags.Local | PrinterFlags.Connections, null, 1).Cast<PrinterInfo1>().ToArray();


            // 本机所有域名 ?
            // [0] = { pName = "LanMan 打印服务!WORKGROUP", pDescription = "WORKGROUP", pComment = "登录的域", Flags = Expand | Container | Icon2 };
            var domains = Native.ThunkEnumPrinters(PrinterFlags.Name, lanManProvidor, level).Cast<PrinterInfo1>().ToArray();

            // 发现当前局域网在线的设备(电脑/下载盒子/...); 结果存在两种可能; 计算机/打印机;
            // 计算机[*] = { pName = "LanMan 打印服务!WORKGROUP!\\ZZ-PC", pDescription = "ZZ-PC", pComment = "登录的域", Flags = Container | Icon3 };
            // 打印机[*] = { pName = "\\ZZ-PC\ZZ-Brother HL-1110 series", pDescription = "{SharedName},{设备名称}", pComment = "登录的域", Flags = Icon8 };
            var machines = Native.ThunkEnumPrinters(PrinterFlags.Name, domains[0].pName, level).Cast<PrinterInfo1>().ToArray();

            var hasShared = "ZZ-PC"; // 测试环境中拥有共享打印机的计算机; // 设备名称: Brother HL-1110 series; 共享名称: ZZ-Brother HL-1110 series;
            // name = "LanMan 打印服务!WORKGROUP!\\ZZ-PC";
            var machinePrinters1 = Native.ThunkEnumPrinters(PrinterFlags.Name, machines.Where(m => m.pDescription == hasShared).Single().pName, level).Cast<PrinterInfo1>().ToArray();
            // name = "LanMan 打印服务!!\\ZZ-PC"; 省略域名;
            // [*] = { pName = "\\ZZ-PC\ZZ-Brother HL-1110 series", pDescription = "{ZZ-Brother HL-1110 series},{Brother HL-1110 series}", pComment = "登录的域", Flags = Icon8 };
            var machinePrinters2 = Native.ThunkEnumPrinters(PrinterFlags.Name, lanManProvidor + @"!!\\" + hasShared, level).Cast<PrinterInfo1>().ToArray();
            // name = "\\ZZ-PC";
            // [*] = { pName = "\\ZZ-PC\Brother HL-1110 series", pDescription = "\\ZZ-PC\Brother HL-1110 series,Brother HL-1110 series,", pComment = "null", Flags = Icon8 };
            var machinePrinters3 = Native.ThunkEnumPrinters(PrinterFlags.Name, @"\\" + hasShared, level).Cast<PrinterInfo1>().ToArray();


            // 发现当前局域网在线的设备(电脑/下载盒子/...); 结果存在两种可能; 计算机/打印机;
            // 计算机[*] = { pName = "LanMan 打印服务!!\\ZZ-PC", pDescription = "ZZ-PC", pComment = "登录的域", Flags = Container | Icon3 };
            // 打印机[*] = { pName = "\\ZZ-PC\ZZ-Brother HL-1110 series", pDescription = "{SharedName},{设备名称}", pComment = "登录的域", Flags = Icon8 };
            // 以下结果全部一样, Name 参数无效; 跟 PrinterFlags.Name 的区别是: 返回结果省略域名;
            var remotes1 = Native.ThunkEnumPrinters(PrinterFlags.Remote, null, level).Cast<PrinterInfo1>().ToArray();
            var remotes2 = Native.ThunkEnumPrinters(PrinterFlags.Remote, string.Empty, level).Cast<PrinterInfo1>().ToArray();
            var remotes3 = Native.ThunkEnumPrinters(PrinterFlags.Remote, localProvidor, level).Cast<PrinterInfo1>().ToArray();
            var remotes4 = Native.ThunkEnumPrinters(PrinterFlags.Remote, lanManProvidor + @"!!\\" + hasShared, level).Cast<PrinterInfo1>().ToArray();
        }

        static void TestPrinterInfoSizes()
        {
            var types = new[]{
                typeof(PrinterInfo1),
                typeof(PrinterInfo2),
                typeof(PrinterInfo3),
                typeof(PrinterInfo4),
                typeof(PrinterInfo5),
                typeof(PrinterInfo6),
                typeof(PrinterInfo7),
                typeof(PrinterInfo8),
                typeof(PrinterInfo9),
            };
            var sizes = types.Select(t => Marshal.SizeOf(t)).ToArray();
        }
    }
}
