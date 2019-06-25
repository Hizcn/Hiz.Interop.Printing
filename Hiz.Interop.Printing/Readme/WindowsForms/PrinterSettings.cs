using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PrinterSettings
     * https://msdn.microsoft.com/zh-cn/library/system.drawing.printing.printersettings(v=vs.100).aspx
     * 
     * 备注:
     * 通常可以通过 PrintDocument.PrinterSettings 或 PageSettings.PrinterSettings 属性访问 PrinterSettings 以修改打印机设置。
     * 最常用的打印机设置是 PrinterName，它指定要打印到的打印机。
     */

    /* PrinterSettings
     * 
     * public PrinterSettings() {
     *     defaultPageSettings = new PageSettings(this);
     * }
     * // 打印每页默认设置
     * // Default: 由打印机决定; 可以覆盖定制;
     * public PageSettings DefaultPageSettings { get { return defaultPageSettings; } }
     * 
     * // 打印设备名称
     * // Default: 当前系统默认打印设备名称;
     * public string PrinterName { get; set; }
     * 
     * // 是否支持双面打印 // DeviceCapabilities()
     * public bool CanDuplex { get; }
     * 
     * // 打印份数
     * // Default: 由打印机决定 DevMode;
     * public short Copies { get; set; }
     * 
     * // 是否逐份打印
     * // Default: 由打印机决定 DevMode;
     * public bool Collate { get; set; }
     * 
     * // 双面打印设置
     * // Default: 由打印机决定 DevMode;
     * public Duplex Duplex { get; set; }
     * 
     * // 打印范围
     * // Default: AllPages
     * public PrintRange PrintRange { get; set; }
     * // 要打印的起始页码
     * // Default: 0;
     * public int FromPage { get; set; }
     * // 要打印的截至页码
     * // Default: 0;
     * public int ToPage { get; set; }
     * // 最小页码
     * // Default: 0;
     * public int MinimumPage { get; set; }
     * // 最大页码
     * // Default: 9999;
     * public int MaximumPage { get; set; }
     * 
     * // 是否默认打印设备
     * public bool IsDefaultPrinter { get; }
     * 
     * // 是否绘图仪器 // 由打印机决定; // GetDeviceCaps();
     * public bool IsPlotter { get; }
     * 
     * // 是否支持彩色打印 // 由打印机决定; // GetDeviceCaps();
     * public bool SupportsColor { get; }
     * 
     * // 是否有效打印设备 // DeviceCapabilities();
     * public bool IsValid { get; }
     * 
     * // 横版旋转角度 // 由打印机决定; // DeviceCapabilities();
     * public int LandscapeAngle { get; }
     * 
     * // 最大打印份数 // 由打印机决定; // DeviceCapabilities();
     * public int MaximumCopies { get; }
     * 
     * // 可选纸张 // 由打印机决定; // 实时查询: DeviceCapabilities();
     * public PrinterSettings.PaperSizeCollection PaperSizes { get; }
     * 
     * // 可选纸盒 // 由打印机决定; // DeviceCapabilities();
     * public PrinterSettings.PaperSourceCollection PaperSources { get; }
     * 
     * // 可选打印质量 // 由打印机决定; // DeviceCapabilities();
     * public PrinterSettings.PrinterResolutionCollection PrinterResolutions { get; }
     * 
     * // 输出文件
     * // Default: false
     * public bool PrintToFile { get; set; }
     * // 文件路径
     * public string PrintFileName { get; set; }
     * 
     * 
     * // 本地可用打印设备
     * public static PrinterSettings.StringCollection InstalledPrinters { get; }
     */
}
