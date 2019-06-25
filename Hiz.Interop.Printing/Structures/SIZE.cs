using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* SIZE
     * https://msdn.microsoft.com/en-us/library/dd145106(v=vs.85).aspx
     * typedef struct tagSIZE {
     *   LONG cx;
     *   LONG cy;
     * } SIZE, *PSIZE;
     */
    [DebuggerDisplay("{cx}, {cy}")]
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;

        public int cy;

        // public SIZE() { }
        public SIZE(int cx, int cy)
        {
            this.cx = cx;
            this.cy = cy;
        }

        public bool IsEmpty { get { return this.cx == 0 && this.cy == 0; } }
    }
}
