using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* PrinterInfo 汇总说明
     * 
     * PRINTER_INFO_1: General information: Flags/Name/Description/Comment;
     * PRINTER_INFO_2: Detailed information.
     * PRINTER_INFO_3: Security information: SecurityDescriptor;
     * PRINTER_INFO_4: Minimal information: PrinterName/ServerName/Attributes;
     * PRINTER_INFO_5: PrinterName/PortName/Attributes;
     * PRINTER_INFO_6: PrinterStatus;
     * PRINTER_INFO_7: Directory Service: ObjectGUID/Action;
     * PRINTER_INFO_8: Global default printer settings: DevMode;
     * PRINTER_INFO_9: Per-User default printer settings: DevMode;
     */

    /* |================================================================|
     * |                 | ReferenceFields | StructLayoutAttribute.Size |
     * |                 |                 | x86         | x64          |
     * |=================|=================|=============|==============|
     * | PRINTER_INFO_1W | 3               | 16          |              |
     * | PRINTER_INFO_2W | 13              | 84          |              |
     * | PRINTER_INFO_3  | 1               | 4           |              |
     * | PRINTER_INFO_4W | 2               | 12          |              |
     * | PRINTER_INFO_5W | 2               | 20          |              |
     * | PRINTER_INFO_6  | 0               | 4           |              |
     * | PRINTER_INFO_7W | 1               | 8           |              |
     * | PRINTER_INFO_8W | 1               | 4           |              |
     * | PRINTER_INFO_9W | 1               | 4           |              |
     * |================================================================|
     */
    internal interface IPrinterInfo { }
}
