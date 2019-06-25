using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    // Update: 2014-12-05D 增加: 显式转换
    /* System.Windows.Forms.NativeMethods.RECT (Type = Struct)
     * System.Windows.Forms.NativeMethods.COMRECT (Type = Class)
     * System.Drawing.SafeNativeMethods.RECT (Type = Struct)
     */
    /* RECT
     * https://msdn.microsoft.com/en-us/library/dd162897(v=vs.85).aspx
     * typedef struct _RECT {
     *   LONG left;
     *   LONG top;
     *   LONG right;
     *   LONG bottom;
     * } RECT, *PRECT;
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public Int32 left;
        public Int32 top;
        public Int32 right;
        public Int32 bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public int X { get { return this.left; } set { this.left = value; } }
        public int Y { get { return this.top; } set { this.top = value; } }
        public int Width { get { return this.right - this.left; } }
        public int Height { get { return this.bottom - this.top; } }
        public POINT Location { get { return new POINT(this.left, this.top); } }
        public SIZE Size { get { return new SIZE(this.right - this.left, this.bottom - this.top); } }

        public bool IsEmpty { get { return left == 0 && top == 0 && right == 0 && bottom == 0; } }
    }
}
