using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PageSetupDialog
     * https://msdn.microsoft.com/zh-cn/library/system.windows.forms.pagesetupdialog(v=vs.100).aspx
     * 
     * 备注:
     * PageSetupDialog  对话框对给定 Document 的 PageSettings 和 PrinterSettings 信息进行修改。
     * 用户可启用该对话框的各个部分以控制打印和边距，控制纸张方向、大小和来源，还可以显示“帮助”和网络按钮。
     * MinMargins  属性定义用户可选择的最小边距。
     * 
     * 因为 PageSetupDialog 需要显示页面设置，所以需要在调用 ShowDialog 之前设置 Document、PrinterSettings 或 PageSettings 属性；否则将会发生异常。
     * 异常消息:
     * PageSetupDialog 需要 PageSettings 对象才能显示。请设置 PageSetupDialog.Document (首选)、 PageSetupDialog.PrinterSettings 或 PageSetupDialog.PageSettings。
     * 实际情况:
     * 仅仅设置 PrinterSettings 属性也会发生异常; 因为 AllowPrinter 属性过时?
     * 
     * 
     * 总结: (我们假设: this.PageSettings != this.PrinterSettings.DefaultPageSettings)
     * 1. 窗体显示:
     *    Margins: 固定取至: this.PageSettings.Margins;
     *    纸张大小列表/纸张来源列表/纸张方向: 如果 this.PrinterSettings != null 取至这个属性; 否则取至: this.PageSettings.PrinterSettings;
     * 2. 修改保存:
     *    固定存至: this.PageSettings; (Margins/PaperSize/Landscape/PaperSource)
     *    如果 this.PrinterSettings != null 同样影响: this.PrinterSettings.DefaultPageSettings (其中 Margins 不受影响);
     */

    /* PageSetupDialog
     * 
     * public PageSetupDialog() {
     *     this.Reset();
     * }
     * 
     * // Default: null;
     * public PrintDocument Document {
     *     get { return printDocument; }
     *     set {
     *         printDocument = value;
     *         if (printDocument != null) {
     *             pageSettings = printDocument.DefaultPageSettings;
     *             printerSettings = printDocument.PrinterSettings;
     *         }
     *     }
     * }
     * 
     * // Default: null;
     * public PageSettings PageSettings {
     *     get { return pageSettings; }
     *     set {
     *         pageSettings = value;
     *         printDocument = null;
     *     }
     * }
     * 
     * // Default: null;
     * public PrinterSettings PrinterSettings {
     *     get { return printerSettings; }
     *     set {
     *         printerSettings = value;
     *         printDocument = null;
     *     }
     * }
     * 
     * // 启用边距部分;
     * // Default: true;
     * public bool AllowMargins { get; set; }
     * 
     * // 启用方向部分;
     * // Default: true;
     * public bool AllowOrientation { get; set; }
     * 
     * // 启用纸张部分
     * // Default: true;
     * public bool AllowPaper { get; set; }
     * 
     * // 启用 "打印机(&P)..." 按钮; 在右下角; 可以修改打印设备; 类似弹出 PrintDialog;
     * // 已经过时; 如果应用程序在 Windows Vista 上运行，将此属性设置为 true 将无效。
     * // Default: true;
     * public bool AllowPrinter { get; set; }
     * 
     * // 最小边距
     * // Default: null;
     * public Margins MinMargins { get; set; }
     * 
     * // 启用公制单位
     * // Default: false;
     * public bool EnableMetric { get; set; }
     * 
     * // 显示 "帮助" 按钮; 点击触发事件: HelpRequest;
     * // Default: false;
     * public bool ShowHelp { get; set; }
     * 
     * // 已经过时; 没有作用;
     * // Default: true;
     * public bool ShowNetwork { get; set; }
     * 
     * public override void Reset();
     * 
     * protected override bool RunDialog(IntPtr hwndOwner) {
     *     IntSecurity.SafePrinting.Demand();
     * 
     *     var hookProcPtr = new NativeMethods.WndProc(this.HookProc);
     *     if (pageSettings == null)
     *         throw new ArgumentException(SR.GetString(SR.PSDcantShowWithoutPage));
     * 
     *     var data = new NativeMethods.PAGESETUPDLG();
     *     data.lStructSize = Marshal.SizeOf(data);
     *     data.Flags = GetFlags();
     *     data.hwndOwner = hwndOwner;
     *     data.lpfnPageSetupHook = hookProcPtr;
     *     
     *     // 当前 PAGESETUPDLG 用的度量单位;
     *     var toUnit = PrinterUnit.ThousandthsOfAnInch; // 0.001in;
     *     // Refer VSWhidbey: 331160. Below was a breaking change from RTM and EVERETT even though this was a correct FIX.
     *     // EnableMetric is a new Whidbey property which we allow the users to choose between the AutoConversion or not.
     *     if (EnableMetric) // 启用公制单位: 毫米;
     *     {
     *         // Take the Units of Measurement while determining the PrinterUnits...
     *         var sb = new StringBuilder(2);
     *         int result = UnsafeNativeMethods.GetLocaleInfo(NativeMethods.LOCALE_USER_DEFAULT,NativeMethods.LOCALE_IMEASURE, sb,sb.Capacity);
     *         if (result > 0 && Int32.Parse(sb.ToString(), CultureInfo.InvariantCulture) == 0) {
     *             toUnit = PrinterUnit.HundredthsOfAMillimeter; // 0.01mm;
     *         }
     *     }
     *     
     *     if (MinMargins != null) { // 设置最小边距;
     *         Margins margins = PrinterUnitConvert.Convert(MinMargins, PrinterUnit.Display, toUnit);
     *         data.minMarginLeft = margins.Left;
     *         data.minMarginTop = margins.Top;
     *         data.minMarginRight = margins.Right;
     *         data.minMarginBottom = margins.Bottom;
     *     }
     * 
     *     if (pageSettings.Margins != null) { // 设置打印边距;
     *         Margins margins = PrinterUnitConvert.Convert(pageSettings.Margins, PrinterUnit.Display, toUnit);
     *         data.marginLeft = margins.Left;
     *         data.marginTop = margins.Top;
     *         data.marginRight = margins.Right;
     *         data.marginBottom = margins.Bottom;
     *     }
     * 
     *     // Ensure that the margins are >= minMargins.
     *     // This is a requirement of the PAGESETUPDLG structure.
     *     // 确保 打印边距 大于等于 最小边距;
     *     data.marginLeft = Math.Max(data.marginLeft, data.minMarginLeft);
     *     data.marginTop = Math.Max(data.marginTop, data.minMarginTop);
     *     data.marginRight = Math.Max(data.marginRight, data.minMarginRight);
     *     data.marginBottom = Math.Max(data.marginBottom, data.minMarginBottom);           
     *     
     *     // 如果已设打印设备设置, 优先使用; 否则将使用页面设置关联的打印设备设置;
     *     PrinterSettings printer = (printerSettings == null) ? pageSettings.PrinterSettings : printerSettings;
     * 
     *     // GetHDevmode demands AllPrintingAndUnmanagedCode Permission : Since we are calling that function we should Assert the permision,
     *     IntSecurity.AllPrintingAndUnmanagedCode.Assert();
     *     try {
     *         // 获取 DevMode;
     *         data.hDevMode = printer.GetHdevmode(pageSettings);
     *         data.hDevNames = printer.GetHdevnames();
     *     }
     *     finally {
     *         CodeAccessPermission.RevertAssert();
     *     }
     * 
     *     try {
     *         // 弹出窗体
     *         bool status = UnsafeNativeMethods.PageSetupDlg(data);
     *         if (!status) {
     *             // Debug.WriteLine(Windows.CommonDialogErrorToString(Windows.CommDlgExtendedError()));
     *             return false;
     *         }
     *         // 更新设置
     *         UpdateSettings(data, pageSettings, printerSettings); // Yes, printerSettings, not printer
     *         return true;
     *     }
     *     finally {
     *         UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevMode));
     *         UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevNames));
     *     }
     * }
     * 
     * private static void UpdateSettings(NativeMethods.PAGESETUPDLG data, PageSettings pageSettings, PrinterSettings printerSettings) {
     *     // SetHDevMode demands AllPrintingAndUnmanagedCode Permission : Since we are calling that function we should Assert the permision,
     *     IntSecurity.AllPrintingAndUnmanagedCode.Assert();
     *     try {
     *         // 更新 this.PageSettings; // 忽略 Margins 属性;
     *         pageSettings.SetHdevmode(data.hDevMode);
     *         // 更新 this.PrinterSettings.DefaultPageSettings; // 忽略 Margins 属性;
     *         if (printerSettings != null) {
     *             printerSettings.SetHdevmode(data.hDevMode);
     *             printerSettings.SetHdevnames(data.hDevNames);
     *         }
     *     }
     *     finally {
     *         CodeAccessPermission.RevertAssert();
     *     }
     *     
     *     // 更新边距
     *     var newMargins = new Margins();
     *     newMargins.Left = data.marginLeft;
     *     newMargins.Top = data.marginTop;
     *     newMargins.Right = data.marginRight;
     *     newMargins.Bottom = data.marginBottom;
     *     // 当前 PAGESETUPDLG 用的度量单位;
     *     PrinterUnit fromUnit = ((data.Flags & NativeMethods.PSD_INHUNDREDTHSOFMILLIMETERS) != 0)
     *                            ? PrinterUnit.HundredthsOfAMillimeter
     *                            : PrinterUnit.ThousandthsOfAnInch;
     *     // 此处更新 this.PageSettings.Margins 属性;
     *     pageSettings.Margins = PrinterUnitConvert.Convert(newMargins, fromUnit, PrinterUnit.Display);
     * }
     */
}
