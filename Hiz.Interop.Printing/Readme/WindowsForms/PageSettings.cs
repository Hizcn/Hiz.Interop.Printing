using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PageSettings
     * https://msdn.microsoft.com/zh-cn/library/system.drawing.printing.pagesettings(v=vs.100).aspx
     * 
     * 备注:
     * PageSettings 类用于指定修改页面打印方式的设置。
     * 通常可以通过 PrintDocument.DefaultPageSettings 属性为所有要打印的页面设置默认设置。
     * 若要逐页指定设置，请分别处理 PrintDocument.PrintPage 或 PrintDocument.QueryPageSettings 事件并修改 PrintPageEventArgs 或 QueryPageSettingsEventArgs 中包含的 PageSettings 参数。
     */

    /* PageSettings
     * 默认数据取至: PrinterSettings; 但是无关: PrinterSettings.DefaultPageSettings;
     * 本类所有数值属性单位: 百分之一英寸(0.01in).
     * 
     * public PageSettings() : this(new PrinterSettings()) { }
     * public PageSettings(PrinterSettings printerSettings);
     * 
     * // 关联打印设备
     * public PrinterSettings PrinterSettings {
     *     get { return printerSettings; }
     *     set {
     *         if (value == null)
     *             value = new PrinterSettings();
     *         printerSettings = value;
     *     }
     * }
     * 
     * // 是否彩色打印
     * // Default: 由打印机决定;
     * public bool Color {
     *     get {
     *         if (color.IsDefault)
     *             // 获取打印设备 DevMode 相关数据;
     *             return printerSettings.GetModeField(ModeField.Color, SafeNativeMethods.DMCOLOR_MONOCHROME) == SafeNativeMethods.DMCOLOR_COLOR;
     *         else
     *             return (bool)color;
     *     }
     *     set {
     *         color = value;
     *     }
     * }
     * 
     * // 是否横板打印;
     * // Default: 由打印机决定;
     * public bool Landscape {
     *     get {
     *         if (landscape.IsDefault)
     *             // 获取打印设备 DevMode 相关数据;
     *             return printerSettings.GetModeField(ModeField.Orientation, DMORIENT_PORTRAIT) == SafeNativeMethods.DMORIENT_LANDSCAPE;
     *         else
     *             return (bool)landscape;
     *     }
     *     set {
     *         landscape = value;
     *     }
     * }
     * 
     * // 选择打印纸张
     * // Default: 由打印机决定;
     * public PaperSize PaperSize {
     *     get {
     *         if (paperSize != null)
     *             return paperSize;
     *         // 获取打印设备 DevMode 相关数据;
     *     }
     *     set {
     *         paperSize = value;
     *     }
     * }
     * 
     * public Rectangle Bounds {
     *     get {
     *         var size = this.PaperSize();
     *         if (!this.Landscape) {
     *             return new Rectangle(0, 0, size.Width, size.Height);
     *         }
     *         else { // 横板打印: 宽高对换
     *             return new Rectangle(0, 0, size.Height, size.Width);
     *         }
     *     }
     * }
     * 
     * // 水平物理边界 (无关打印方向: 纵向/横向)
     * // Default: 由打印机决定;
     * public float HardMarginX {
     *     get {
     *         var hardMarginX = 0f;
     *         var dc = printerSettings.CreateDeviceContext(this);
     *         try {
     *             int dpiX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(dc, dc.Hdc), SafeNativeMethods.LOGPIXELSX);
     *             int hardMarginX_DU = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(dc, dc.Hdc), SafeNativeMethods.PHYSICALOFFSETX);
     *             hardMarginX = hardMarginX_DU * 100 / dpiX;
     *         }
     *         finally {
     *             dc.Dispose();
     *         }
     *         return hardMarginX;
     *     }
     * }
     * // 垂直物理边界
     * // Default: 由打印机决定;
     * public float HardMarginY { get; } // 逻辑同上 LOGPIXELSY/PHYSICALOFFSETY;
     * 
     * // 打印区域界限 // 无论横板竖版, 结果一样;
     * public RectangleF PrintableArea {
     *     get {
     *         RectangleF printableArea = new RectangleF();
     *         DeviceContext dc = printerSettings.CreateInformationContext(this);
     *         HandleRef hdc = new HandleRef(dc, dc.Hdc);
     *     
     *         try {
     *             int dpiX = UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.LOGPIXELSX);
     *             int dpiY = UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.LOGPIXELSY);
     *             if (!this.Landscape) {
     *                 // Need to convert the printable area to 100th of an inch from the device units
     *                 printableArea.X = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.PHYSICALOFFSETX) * 100 / dpiX;
     *                 printableArea.Y = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.PHYSICALOFFSETY) * 100 / dpiY;
     *                 printableArea.Width = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.HORZRES) * 100 / dpiX;
     *                 printableArea.Height = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.VERTRES) * 100 / dpiY;
     *             }
     *             else {
     *                 // Need to convert the printable area to 100th of an inch from the device units
     *                 printableArea.Y = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.PHYSICALOFFSETX) * 100 / dpiX;
     *                 printableArea.X = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.PHYSICALOFFSETY) * 100 / dpiY;
     *                 printableArea.Height = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.HORZRES) * 100 / dpiX;
     *                 printableArea.Width = (float)UnsafeNativeMethods.GetDeviceCaps(hdc, SafeNativeMethods.VERTRES) * 100 / dpiY;
     *             }
     *         }
     *         finally {
     *             dc.Dispose();
     *         }
     *     
     *         return printableArea;
     *     }
     * }
     * 
     * // 选择打印纸盒
     * // Default: 由打印机决定;
     * public PaperSource PaperSource {
     *     get {
     *         if (paperSource != null) {
     *             return paperSource;
     *         }
     *         // 获取打印设备 DevMode 相关数据;
     *     }
     *     set {
     *         paperSource = value;
     *     }
     * }
     * 
     * // 选择打印品质
     * // Default: 由打印机决定;
     * public PrinterResolution PrinterResolution {
     *     get {
     *         if (printerResolution != null) {
     *             return printerResolution;
     *         }
     *         // 获取打印设备 DevMode 相关数据;
     *     }
     *     set {
     *         printerResolution = value;
     *     }
     * }
     * 
     * // 打印边距
     * // Default: new Margins(100, 100, 100, 100); // 无关打印设备;
     * public Margins Margins { get; set; }
     * 
     * public object Clone() {
     *     var result = (PageSettings)this.MemberwiseClone();
     *     result.margins = (Margins)this.margins.Clone();
     *     return result;
     * }
     * 
     * public void CopyToHdevmode(IntPtr hdevmode);
     * public void SetHdevmode(IntPtr hdevmode);
     */
}
