using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* https://msdn.microsoft.com/en-us/library/cc244669.aspx
     */
    [Flags]
    internal enum PrinterFlags : int
    {
        None = 0,

        /// <summary>
        /// PRINTER_ENUM_DEFAULT
        /// </summary>
        Default = 0x01,

        /// <summary>
        /// PRINTER_ENUM_LOCAL
        /// If the PRINTER_ENUM_NAME flag is not also passed, the function ignores the Name parameter, and enumerates the locally installed printers.
        /// If PRINTER_ENUM_NAME is also passed, the function enumerates the local printers on Name.
        /// </summary>
        Local = 0x02,

        /// <summary>
        /// PRINTER_ENUM_CONNECTIONS = PRINTER_ENUM_FAVORITE = 0x04;
        /// The function enumerates the list of printers to which the user has made previous connections.
        /// </summary>
        Connections = 0x04,

        /// <summary>
        /// PRINTER_ENUM_NAME
        /// The function enumerates the printer identified by "Name".
        /// This can be a server, a domain, or a print provider.
        /// If "Name" is NULL, the function enumerates available print providers.
        /// </summary>
        Name = 0x08,

        /// <summary>
        /// PRINTER_ENUM_REMOTE
        /// The function enumerates network printers and print servers in the computer's domain.
        /// This value is valid only if Level is 1.
        /// </summary>
        Remote = 0x10,

        /// <summary>
        /// PRINTER_ENUM_SHARED
        /// The function enumerates printers that have the shared attribute.
        /// Cannot be used in isolation; use an OR operation to combine with another PRINTER_ENUM type.
        /// </summary>
        Shared = 0x20,

        /// <summary>
        /// PRINTER_ENUM_NETWORK
        /// The function enumerates network printers in the computer's domain.
        /// This value is valid only if Level is 1.
        /// </summary>
        Network = 0x40,

        /// <summary>
        /// PRINTER_ENUM_EXPAND
        /// A print provider can set this flag as a hint to a calling application to enumerate this object further if default expansion is enabled.
        /// For example, when domains are enumerated, a print provider might indicate the user's domain by setting this flag.
        /// </summary>
        Expand = 0x4000,

        /// <summary>
        /// PRINTER_ENUM_CONTAINER
        /// If this flag is set, the printer object may contain enumerable objects.
        /// For example, the object may be a print server that contains printers.
        /// </summary>
        Container = 0x8000,

        /// <summary>
        /// PRINTER_ENUM_ICON1
        /// Indicates that, where appropriate, an application should display an icon identifying the object as a top-level network name, such as Microsoft Windows Network.
        /// </summary>
        Icon1 = 0x00010000,

        /// <summary>
        /// PRINTER_ENUM_ICON2
        /// Indicates that, where appropriate, an application should display an icon that identifies the object as a network domain.
        /// </summary>
        Icon2 = 0x00020000,

        /// <summary>
        /// PRINTER_ENUM_ICON3
        /// Indicates that, where appropriate, an application should display an icon that identifies the object as a print server.
        /// </summary>
        Icon3 = 0x00040000,

        /* Reserved:
         * PRINTER_ENUM_ICON4 = 0x00080000;
         * PRINTER_ENUM_ICON5 = 0x00100000;
         * PRINTER_ENUM_ICON6 = 0x00200000;
         * PRINTER_ENUM_ICON7 = 0x00400000;
         * 
         * PRINTER_ENUM_ICONMASK = 0x00FF0000 = Icon1 | Icon2 | Icon3 | Icon4 | Icon5 | Icon6 | Icon7 | Icon8;
         */

        /// <summary>
        /// PRINTER_ENUM_ICON8
        /// Indicates that, where appropriate, an application should display an icon that identifies the object as a printer.
        /// </summary>
        Icon8 = 0x00800000,

        /// <summary>
        /// PRINTER_ENUM_ICONMASK = 0x00FF0000
        /// </summary>
        IconMask = 0x00FF0000,

        /// <summary>
        /// PRINTER_ENUM_HIDE
        /// Indicates that an application cannot display the printer object.
        /// </summary>
        Hide = 0x01000000,

        #region >= Windows8.1
        /// <summary>
        /// PRINTER_ENUM_CATEGORY_ALL
        /// The function enumerates all print devices, including 3D printers.
        /// </summary>
        CategoryAll = 0x02000000,

        /// <summary>
        /// PRINTER_ENUM_CATEGORY_3D
        /// The function enumerates only 3D printers.
        /// </summary>
        Category3D = 0x04000000,
        #endregion
    }
}
