using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    public static class PrintSystemUnitConvert
    {
        /// <summary>
        /// 公制单位
        /// </summary>
        public const PrintSystemUnit MetricUnits = PrintSystemUnit.ThousandthsOfMillimeter | PrintSystemUnit.HundredthsOfMillimeter | PrintSystemUnit.TenthsOfMillimeter | PrintSystemUnit.Millimeter | PrintSystemUnit.Centimeter;
        /// <summary>
        /// 英制单位
        /// </summary>
        public const PrintSystemUnit ImperialUnits = PrintSystemUnit.ThousandthsOfInch | PrintSystemUnit.HundredthsOfInch | PrintSystemUnit.Inch;

        /// <summary>
        /// 长度单位转换
        /// </summary>
        /// <param name="value">长度数值</param>
        /// <param name="source">原始单位</param>
        /// <param name="target">目标单位</param>
        /// <returns></returns>
        /// <remarks>
        /// 个别情况转换可能存在误差, 例如:
        /// 1). System.Drawing.Printing.PaperSize; Unit = 0.01 in; 并且值类型为整数;
        ///     A4 = { Width = 827, Height = 1169 };
        ///     将单位转换为 0.001 mm;
        ///     A4 = { Width = 210058, Height = 296926 }; 跟实际值不符: { Width = 210000, Height = 297000 };
        /// 
        /// 2). 反向计算
        ///     A4 = { Width = 210000, Height = 297000 } Unit: 0.001 mm;
        ///     将单位转换为 0.01 in;
        ///     A4 = { Width = 826.77165354330714, Height = 1169.2913385826771 };
        ///     调用 Math.Round(double) 之后:
        ///     A4 = { Width = 827, Height = 1169 }; Unit: 0.01 in;
        /// </remarks>
        public static double Convert(double value, PrintSystemUnit source, PrintSystemUnit target)
        {
            switch (source)
            {
                case PrintSystemUnit.ThousandthsOfMillimeter:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 0.1; // ÷10;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 0.01; // ÷100;
                        case PrintSystemUnit.Millimeter:
                            return value * 0.001; // ÷1000;
                        case PrintSystemUnit.Centimeter:
                            return value * 0.0001; // ÷10000;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value / 25.4;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value / 254d;
                        case PrintSystemUnit.Inch:
                            return value / 25400d;
                    }
                    break;
                case PrintSystemUnit.HundredthsOfMillimeter:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 10d;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 0.1; // ÷10;
                        case PrintSystemUnit.Millimeter:
                            return value * 0.01; // ÷100;
                        case PrintSystemUnit.Centimeter:
                            return value * 0.0001; // ÷1000;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value / 2.54;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value / 25.4;
                        case PrintSystemUnit.Inch:
                            return value / 2540d;
                    }
                    break;
                case PrintSystemUnit.TenthsOfMillimeter:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 100d;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 10d;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value;
                        case PrintSystemUnit.Millimeter:
                            return value * 0.1; // ÷10;
                        case PrintSystemUnit.Centimeter:
                            return value * 0.01d; // ÷100;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value / 0.254;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value / 2.54;
                        case PrintSystemUnit.Inch:
                            return value / 254d;
                    }
                    break;
                case PrintSystemUnit.Millimeter:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 1000d;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 100d;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 10d;
                        case PrintSystemUnit.Millimeter:
                            return value;
                        case PrintSystemUnit.Centimeter:
                            return value * 0.1; // ÷10;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value / 0.0254;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value / 0.254;
                        case PrintSystemUnit.Inch:
                            return value / 25.4;
                    }
                    break;
                case PrintSystemUnit.Centimeter:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 10000d;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 1000d;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 100d;
                        case PrintSystemUnit.Millimeter:
                            return value * 10d;
                        case PrintSystemUnit.Centimeter:
                            return value;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value / 0.00254;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value / 0.0254;
                        case PrintSystemUnit.Inch:
                            return value / 2.54;
                    }
                    break;
                case PrintSystemUnit.ThousandthsOfInch:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 25.4;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 2.54;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 0.254;
                        case PrintSystemUnit.Millimeter:
                            return value * 0.0254;
                        case PrintSystemUnit.Centimeter:
                            return value * 0.00254;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value * 0.1; // ÷10;
                        case PrintSystemUnit.Inch:
                            return value * 0.001; // ÷1000;
                    }
                    break;
                case PrintSystemUnit.HundredthsOfInch:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 254d;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 25.4;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 2.54;
                        case PrintSystemUnit.Millimeter:
                            return value * 0.254;
                        case PrintSystemUnit.Centimeter:
                            return value * 0.0254;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value * 10d;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value;
                        case PrintSystemUnit.Inch:
                            return value * 0.01; // ÷100;
                    }
                    break;
                case PrintSystemUnit.Inch:
                    switch (target)
                    {
                        case PrintSystemUnit.ThousandthsOfMillimeter:
                            return value * 25400d;
                        case PrintSystemUnit.HundredthsOfMillimeter:
                            return value * 2540d;
                        case PrintSystemUnit.TenthsOfMillimeter:
                            return value * 254d;
                        case PrintSystemUnit.Millimeter:
                            return value * 25.4;
                        case PrintSystemUnit.Centimeter:
                            return value * 2.54;
                        case PrintSystemUnit.ThousandthsOfInch:
                            return value * 1000d;
                        case PrintSystemUnit.HundredthsOfInch:
                            return value * 100d;
                        case PrintSystemUnit.Inch:
                            return value;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("source");
            }
            throw new ArgumentOutOfRangeException("target");
        }
    }

    /* Microsoft.Reporting.WinForms.Global
     */
    internal static class Global
    {
        public static int ToPixels(double inMM, double dpi)
        {
            return (int)(inMM * dpi / 25.4);
        }

        public static float ToMillimeters(int pixels, double dpi)
        {
            return (float)((double)pixels * 25.4 / dpi);
        }

        public static float ToMillimeters(float pixels, double dpi)
        {
            return (float)((double)pixels * 25.4 / dpi);
        }

        public static int InchToPixels(float value, float dpi)
        {
            return (int)(value * dpi);
        }
    }
}
