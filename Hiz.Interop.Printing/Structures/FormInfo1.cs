using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* FORM_INFO_1
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd144836(v=vs.85).aspx
     * typedef struct _FORM_INFO_1 {
     *   DWORD  Flags;
     *   LPTSTR pName;
     *   SIZEL  Size;
     *   RECTL  ImageableArea;
     * } FORM_INFO_1, *PFORM_INFO_1;
     * 
     * Minimum supported client: Windows 2000 Professional;
     * Minimum supported server: Windows 2000 Server;
     */
    /// <summary>
    /// The FORM_INFO_1 structure contains information about a print form.
    /// The information includes the print form's origin, its name, its dimensions, and the dimensions of its printable area.
    /// </summary>
    [DebuggerDisplay("{Flags}: {pName}")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct FormInfo1
    {
        /// <summary>
        /// The form properties. The following values are defined, but only one can be set.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public FormInfoFlags Flags;

        /// <summary>
        /// Pointer to a null-terminated string that specifies the name of the form.
        /// The form name cannot exceed 31 characters.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public String pName;

        /// <summary>
        /// The width and height, in thousandths of millimeters, of the form.
        /// Unit: 0.001mm.
        /// </summary>
        public SIZE Size;

        /// <summary>
        /// The width and height, in thousandths of millimeters, of the form.
        /// Unit: 0.001mm.
        /// 
        /// 经过测试, 所有内建纸张数据:
        ///   this.ImageableArea.X/Y == 0;
        ///   this.ImageableArea.Size == this.Size;
        /// </summary>
        public RECT ImageableArea;
    }
}
