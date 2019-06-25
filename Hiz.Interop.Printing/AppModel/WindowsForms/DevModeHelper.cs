using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;

namespace Hiz.Interop.Printing
{
    internal static class DevModeHelper
    {
        /* 逻辑参考:
         * System.Drawing.Printing.PrinterSettings.PaperSizeFromMode();
         */
        public static PaperSize GetPaperSize(DevMode mode)
        {
            if ((mode.dmFields & DevModeFields.PaperSize) == DevModeFields.PaperSize)
            {
                // TODO: 输入打印设备名称
                var sizes = PrintSpooler.GetPrinterPapers("");
                for (var i = 0; i < sizes.Length; i++)
                {
                    var s = sizes[i];
                    if (s.RawKind == mode.dmPaperSize)
                        return s;
                }
            }
            return new PaperSize("custom",
                (int)PrintSystemUnitConvert.Convert(mode.dmPaperWidth, PrintSystemUnit.TenthsOfMillimeter, PrintSystemUnit.HundredthsOfInch),
                (int)PrintSystemUnitConvert.Convert(mode.dmPaperWidth, PrintSystemUnit.TenthsOfMillimeter, PrintSystemUnit.HundredthsOfInch)
                );
        }
    }
}
