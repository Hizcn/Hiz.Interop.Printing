using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    // TODO: 待测试: 定义为 struct 会报错.
    // Update: 2014-12-05D 增加: 显式转换
    /* System.Windows.Forms.NativeMethods.POINT (Type = Class)
     * System.Windows.Forms.NativeMethods.POINTL (Type = Class)
     * System.Windows.Forms.NativeMethods.tagPOINTF (Type = Class)
     * System.Windows.Forms.NativeMethods.POINTSTRUCT (Type = Struct)
     */
    /* POINT
     * https://msdn.microsoft.com/en-us/library/dd162805(v=vs.85).aspx
     * typedef struct tagPOINT {
     *   LONG x;
     *   LONG y;
     * } POINT, *PPOINT;
     * 
     * POINTS
     * https://msdn.microsoft.com/en-us/library/dd162808(v=vs.85).aspx
     * typedef struct tagPOINTS {
     *   SHORT x;
     *   SHORT y;
     * } POINTS, *PPOINTS;
     * 
     * POINTL
     * https://msdn.microsoft.com/en-us/library/dd162807(v=vs.85).aspx
     * typedef struct _POINTL {
     *   LONG x;
     *   LONG y;
     * } POINTL, *PPOINTL;
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;

        // public POINT() { }
        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsEmpty { get { return this.x == 0 && this.y == 0; } }
    }
}
