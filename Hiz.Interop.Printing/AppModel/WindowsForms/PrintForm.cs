using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Printing;
using System.Diagnostics;

namespace Hiz.Interop.Printing
{
    /* System.Drawing.Printing.PaperSize (Unit: 1/100 in)
     * System.Printing.PageMediaSize (Unit: 1/96 in)
     * FORM_INFO_1/FORM_INFO_2 (Unit: 0.001 mm)
     */
    [DebuggerDisplay("{Flags}: {Name}")]
    public class PrintForm
    {
        FormInfoFlags _Flags;
        public FormInfoFlags Flags { get { return _Flags; } }

        string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        int _Width;
        public int Width { get { return _Width; } set { _Width = value; } }

        int _Height;
        public int Height { get { return _Height; } set { _Height = value; } }

        // public Size Size { get { return new Size(_Width, _Height); } }

        int _LeftMargin;
        int _TopMargin;
        int _RightMargin;
        int _BottomMargin;
        // public int LeftMargin { get { return _LeftMargin; } set { _LeftMargin = value; } }
        // public int TopMargin { get { return _TopMargin; } set { _TopMargin = value; } }
        // public int RightMargin { get { return _RightMargin; } set { _RightMargin = value; } }
        // public int BottomMargin { get { return _BottomMargin; } set { _BottomMargin = value; } }
        // public Margins Margins { get { return new Margins(_LeftMargin, _RightMargin, _TopMargin, _BottomMargin); } }

        PrintSystemUnit _Unit;
        public PrintSystemUnit Unit { get { return _Unit; } }

        int _RawKind;
        public int RawKind
        {
            get { return _RawKind; }
            set
            {
                _RawKind = value;
            }
        }

        internal PrintForm(FormInfo1 form)
        {
            this._Flags = form.Flags;

            this._Name = form.pName;

            this._Unit = PrintSystemUnit.ThousandthsOfMillimeter;

            var size = form.Size;
            this._Width = size.cx;
            this._Height = size.cy;

            var area = form.ImageableArea;
            this._LeftMargin = area.left;
            this._RightMargin = size.cx - area.right;
            this._TopMargin = area.top;
            this._BottomMargin = size.cy - area.bottom;
        }

        public PrintForm() { }

        public double GetWidth(PrintSystemUnit unit)
        {
            return PrintSystemUnitConvert.Convert(this._Width, _Unit, unit);
        }
        public double GetHeight(PrintSystemUnit unit)
        {
            return PrintSystemUnitConvert.Convert(this._Height, _Unit, unit);
        }
    }
}