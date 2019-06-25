using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PrintDialog
     * https://msdn.microsoft.com/zh-cn/library/system.windows.forms.printdialog(v=vs.100).aspx
     * 
     * 备注:
     * 创建 PrintDialog 的实例时，读/写属性将被设置为初始值。 有关这些值的列表，请参见 PrintDialog 构造函数。
     * 要获取由用户在 PrintDialog 中修改的打印机设置，请使用 PrinterSettings 属性。
     */

    /* PrintDialog
     * 
     * public PrintDialog() {
     *     this.Reset();
     * }
     * 
     * // Default: null;
     * public PrintDocument Document {
     *     get { return this.printDocument; }
     *     set {
     *         printDocument = value;
     *         if (printDocument == null) {
     *             settings = new PrinterSettings();
     *         }
     *         else {
     *             settings = printDocument.PrinterSettings;
     *         }
     *     }
     * }
     * 
     * // Default: null;
     * public PrinterSettings PrinterSettings {
     *     get {
     *         if (settings == null)
     *             settings = new PrinterSettings();
     *         return settings;
     *     }
     *     set {
     *         if (value != this.PrinterSettings) {
     *             settings = value;
     *             printDocument = null;
     *         }
     *     }
     * }
     * 
     * // 如果获取页面设置;
     * private PageSettings PageSettings {
     *     get {
     *         if (this.Document != null) {
     *             return this.Document.DefaultPageSettings; 
     *         }
     *         else {
     *             return this.PrinterSettings.DefaultPageSettings;
     *         }
     *     }
     * }
     * 
     * // 是否显示 "当前页面" 选项按钮。(需要: UseEXDialog = false)
     * // Default: false;
     * public bool AllowCurrentPage { get; set; }
     * 
     * // 是否显示 "选定范围" 选项按钮。
     * // Default: false
     * public bool AllowSelection { get; set; }
     * 
     * // 是否显示 "页码范围" 选项按钮。
     * // Default: false
     * public bool AllowSomePages { get; set; }
     * 
     * // Default: true;
     * public bool AllowPrintToFile { get; set; }
     * 
     * // Default: false
     * public bool PrintToFile { get; set; }
     * 
     * // 如果此属性设置为 true，则 ShowHelp 和 ShowNetwork 将被忽略，因为这些属性在 Windows 2000 和更高版本的 Windows 中已过时。
     * // Default: false
     * public bool UseEXDialog { get; set; }
     * 
     * // 已经过时 (需要: UseEXDialog = false)
     * // 显示 "帮助" 按钮; 点击触发事件: HelpRequest;
     * // Default: false
     * public bool ShowHelp { get; set; }
     * 
     * // 已经过时; 没有作用;
     * // Default: true
     * public bool ShowNetwork { get; set; }
     * 
     * 
     * // 重置属性为默认值;
     * public override void Reset();
     * 
     * // VSWhidbey 93449: Use PrintDlgEx and PRINTDLGEX on Win2k and newer OS'.
     * protected override bool RunDialog(IntPtr hwndOwner) {
     *     IntSecurity.SafePrinting.Demand();
     *     NativeMethods.WndProc hookProcPtr = new NativeMethods.WndProc(this.HookProc);
     *     
     *     bool returnValue;
     *     if (!UseEXDialog || (Environment.OSVersion.Platform != System.PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)) {
     *         // 使用旧版窗体
     *         NativeMethods.PRINTDLG data = CreatePRINTDLG();
     *         returnValue = ShowPrintDialog(hwndOwner, hookProcPtr, data);
     *     }
     *     else {
     *         // 使用新版窗体
     *         NativeMethods.PRINTDLGEX data = CreatePRINTDLGEX();
     *         returnValue = ShowPrintDialog(hwndOwner, data);
     *     }
     *     return returnValue;
     * }
     * 
     * // VSWhidbey 93449:
     * // Due to the nature of PRINTDLGEX vs PRINTDLG, separate but similar methods are required for showing the print dialog on Win2k and newer OS'.
     * private bool ShowPrintDialog(IntPtr hwndOwner, NativeMethods.WndProc hookProcPtr, NativeMethods.PRINTDLG data) {
     * }
     * 
     * // VSWhidbey 93449:
     * // Due to the nature of PRINTDLGEX vs PRINTDLG, separate but similar methods are required for showing the print dialog on Win2k and newer OS'.
     * private bool ShowPrintDialog(IntPtr hwndOwner, NativeMethods.PRINTDLGEX data) {
     * }
     * 
     * // VSWhidbey 93449:
     * // Due to the nature of PRINTDLGEX vs PRINTDLG, separate but similar methods are required for updating the settings from the structure utilized by the dialog.
     * // Take information from print dialog and put in PrinterSettings.
     * private static void UpdatePrinterSettings(IntPtr hDevMode, IntPtr hDevNames, short copies, int flags, PrinterSettings settings, PageSettings pageSettings) {
     *     // 更新 this.PrinterSettings.DevMode;
     *     settings.SetHdevmode(hDevMode);
     *     settings.SetHdevnames(hDevNames);
     *     
     *     // 更新 this.PageSettings.DevMode;
     *     if (pageSettings!= null)
     *         pageSettings.SetHdevmode(hDevMode);
     *     
     *     // Check for Copies == 1 since we might get the Right number of Copies from hdevMode.dmCopies...
     *     // 更新 this.PrinterSettings.Copies;
     *     if (settings.Copies == 1)
     *         settings.Copies = copies;
     *     
     *     // 更新 this.PrinterSettings.PrintRange;
     *     settings.PrintRange = (PrintRange)(flags & printRangeMask);
     * }
     */
}
