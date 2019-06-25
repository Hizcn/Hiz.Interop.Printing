using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    internal enum DcTrueType : int
    {
        /// <summary>
        /// DCTT_BITMAP
        /// </summary>
        Bitmap = 0x01,
        /// <summary>
        /// DCTT_DOWNLOAD
        /// </summary>
        Download = 0x02,
        /// <summary>
        /// DCTT_SUBDEV
        /// </summary>
        SubstituteDevice = 0x04,
        /// <summary>
        /// DCTT_DOWNLOAD_OUTLINE
        /// </summary>
        DownloadOutline = 0x08,
    }
}
