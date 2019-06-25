using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PrintDocument
     * https://msdn.microsoft.com/zh-cn/library/system.drawing.printing.printdocument(v=vs.100).aspx
     * 
     * 备注:
     * 通常创建 PrintDocument 类的实例，设置属性（例如 DocumentName 和 PrinterSettings），并调用 Print 方法来开始打印过程。
     * 通过使用 PrintPageEventArgs 中包含的 Graphics 来处理用于指定打印输出的 PrintPage 事件。
     * 
     * 打印设置取至: this.PrinterSettings; Default: 当前系统默认打印设备;
     * 页面设置取至: this.DefaultPageSettings; Default: 默认页面设置 (取至: 默认打印设备)
     */

    /* PrintDocument
     * 
     * public PrintDocument() {
     *     printerSettings = new PrinterSettings();
     *     defaultPageSettings = new PageSettings(printerSettings);
     * }
     * 
     * // Default: new PrinterSettings();
     * public PrinterSettings PrinterSettings {
     *     get { return printerSettings; }
     *     set {
     *         if (value == null)
     *             value = new PrinterSettings();
     *         printerSettings = value;
     *         
     *         // Reset the PageSettings that match the PrinterSettings only if we have created the defaultPageSettings..
     *         if (!userSetPageSettings)
     *             defaultPageSettings = printerSettings.DefaultPageSettings;
     *     }
     * }
     * 
     * // Default: new PageSettings(this.PrinterSettings);
     * // 默认修改互不影响: this.DefaultPageSettings != this.PrinterSettings.DefaultPageSettings;
     * // 但是如果赋值一次 PrinterSettings 属性, 并且没有赋值 DefaultPageSettings 属性, 那么: this.DefaultPageSettings == this.PrinterSettings.DefaultPageSettings;
     * // 注意: 文档打印时取: this.DefaultPageSettings; 无关: this.PrinterSettings.DefaultPageSettings;
     * public PageSettings DefaultPageSettings {
     *     get { return defaultPageSettings; }
     *     set {
     *         if (value == null)
     *             value = new PageSettings();
     *         defaultPageSettings = value;
     *         userSetPageSettings = true;
     *     }
     * }
     * 
     * // Default: "document"
     * public string DocumentName { get; set; }
     * 
     * // Default: false
     * public bool OriginAtMargins { get; set; }
     * 
     * // Default: new System.Windows.Forms.PrintControllerWithStatusDialog(new System.Drawing.Printing.StandardPrintController());
     * public PrintController PrintController { get; set; }
     * 
     * public event PrintEventHandler BeginPrint;
     * public event PrintEventHandler EndPrint;
     * public event QueryPageSettingsEventHandler QueryPageSettings;
     * public event PrintPageEventHandler PrintPage;
     * 
     * // 以下方法只触发对应的事件.
     * protected virtual void OnBeginPrint(PrintEventArgs e);
     * protected virtual void OnEndPrint(PrintEventArgs e);
     * protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e);
     * protected virtual void OnPrintPage(PrintPageEventArgs e);
     * 
     * // 以下方法供给 PrintController 调用.
     * internal void _OnBeginPrint(PrintEventArgs e);
     * internal void _OnEndPrint(PrintEventArgs e);
     * internal void _OnQueryPageSettings(QueryPageSettingsEventArgs e);
     * internal void _OnPrintPage(PrintPageEventArgs e);
     * 
     * public void Print() {
     *     this.PrintController.Print(this);
     * }
     */

    /* PrintController
     * 
     * void Print(PrintDocument document) {
     *     PrintAction printAction;
     *     if (this.IsPreview) {
     *         printAction = PrintAction.PrintToPreview;
     *     }
     *     else {
     *         printAction = document.PrinterSettings.PrintToFile ? PrintAction.PrintToFile : PrintAction.PrintToPrinter;
     *     }
     *     var e = new PrintEventArgs(printAction);
     *     
     *     document._OnBeginPrint(e); // 触发起始打印事件
     *     if (e.Cancel) {
     *         document._OnEndPrint(e); // 触发结束打印事件
     *         return;
     *     }
     *     
     *     this.OnStartPrint(document, e); // 创建句柄
     *     if (e.Cancel) {
     *         document._OnEndPrint(e);
     *         this.OnEndPrint(document, e); // 释放句柄
     *         return;
     *     }
     *     
     *     var canceled = this.PrintLoop(document); // 循环打印每页
     *     document._OnEndPrint(e);
     *     e.Cancel |= canceled;
     *     this.OnEndPrint(document, e);
     * }
     * 
     * bool PrintLoop(PrintDocument document) {
     *     // 使用文档默认页面设置创建事件
     *     var query = new QueryPageSettingsEventArgs((PageSettings)document.DefaultPageSettings.Clone());
     *     
     *     while (true) {
     *         document._OnQueryPageSettings(query); // 触发每页询问设置事件; 可以修改当前页面打印设置;
     *         if (query.Cancel)
     *             return true;
     *         
     *         var e = this.CreatePrintPageEvent(query.PageSettings); // 创建每页打印事件数据
     *         var g = this.OnStartPage(document, e); // 创建绘图 Graphics;
     *         e.SetGraphics(g);
     *         
     *         try {
     *             document._OnPrintPage(e); // 触发每页打印事件
     *             this.OnEndPage(document, e); // 释放绘图 Graphics;
     *         } finally {
     *             e.Dispose(); // 释放绘图 Graphics;
     *         }
     *         
     *         if (e.Cancel) {
     *             return true;
     *         }
     *         if (!e.HasMorePages) {
     *             return false;
     *         }
     *     }
     * }
     * 
     * PrintPageEventArgs CreatePrintPageEvent(PageSettings pageSettings) {
     *     var pageBounds = pageSettings.GetBounds(modeHandle);
     *     var marginBounds = new Rectangle(pageSettings.Margins.Left,
     *                                      pageSettings.Margins.Top,
     *                                      pageBounds.Width - (pageSettings.Margins.Left + pageSettings.Margins.Right),
     *                                      pageBounds.Height - (pageSettings.Margins.Top + pageSettings.Margins.Bottom));
     *     return new PrintPageEventArgs(null, marginBounds, pageBounds, pageSettings);
     * }
     * 
     * // 创建句柄: 重置默认页面设置; // 整个打印过程调用一次;
     * public virtual void OnStartPrint(PrintDocument document, PrintEventArgs e) {
     *     modeHandle = (SafeDeviceModeHandle)document.PrinterSettings.GetHdevmode(document.DefaultPageSettings);
     * }
     * 
     * // 释放句柄; // 整个打印过程调用一次;
     * public virtual void OnEndPrint(PrintDocument document, PrintEventArgs e) {
     *     if (modeHandle != null) {
     *         modeHandle.Close();
     *     }
     * }
     * 
     * // 创建绘图
     * public virtual Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e) {
     *     return null;
     * }
     * 
     * // 释放绘图
     * public virtual void OnEndPage(PrintDocument document, PrintPageEventArgs e) {
     * }
     */
}
