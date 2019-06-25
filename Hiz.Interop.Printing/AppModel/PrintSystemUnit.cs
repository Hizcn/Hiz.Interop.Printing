using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* System.Drawing.Printing.PrinterUnit
     */
    public enum PrintSystemUnit
    {
        None = 0,

        #region Metric Units
        /// <summary>
        /// 千分之一毫米; 0.001mm; (FormInfo)
        /// </summary>
        ThousandthsOfMillimeter = 0x01,

        /// <summary>
        /// 百分之一毫米; 0.01mm; (PAGESETUPDLG: 页面设置窗体)
        /// </summary>
        HundredthsOfMillimeter = 0x02,

        /// <summary>
        /// 十分之一毫米; 0.1mm; (DeviceCapabilities.PaperSize) (DevMode.dmPaperLength/dmPaperWidth)
        /// </summary>
        TenthsOfMillimeter = 0x04,

        /// <summary>
        /// 毫米; mm; (System.Windows.Forms.PageSetupDialog: EnableMetric = true)
        /// </summary>
        Millimeter = 0x08,

        /// <summary>
        /// 厘米; cm; (Windows 打印服务器属性对话框: 公制单位)
        /// </summary>
        Centimeter = 0x10,
        #endregion

        #region Imperial Units
        /// <summary>
        /// 千分之一英寸; 0.001in; (PAGESETUPDLG: 页面设置窗体);
        /// </summary>
        ThousandthsOfInch = 0x0100,

        /// <summary>
        /// 百分之一英寸; 0.01in; (System.Drawing.Printing.PaperSize)
        /// </summary>
        HundredthsOfInch = 0x0200,

        /// <summary>
        /// 英寸; in; (System.Windows.Forms.PageSetupDialog: EnableMetric = false)
        /// </summary>
        Inch = 0x0800,
        #endregion
    }
}