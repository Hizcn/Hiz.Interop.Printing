using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* MS.Internal.Printing.NativeMethods.DEVMODE
     * MS.Internal.Printing.Configuration.DevMode
     * 
     * System.Drawing.SafeNativeMethods.DEVMODE (System.Drawing.Printing 打印使用)
     * System.Windows.Forms.NativeMethods.DEVMODE
     * 
     * System.Printing.PrintTicket
     */

    /* Per-User DEVMODE
     * A user can specify the default document settings for a printer.
     * This is called the per-user DEVMODE because it only affects the defaults for a particular user, and the information for each printer is defined in a separate DEVMODE structure.
     * 
     * To set the per-user DEVMODE, call "SetPrinter" with either the PRINTER_INFO_2 or the PRINTER_INFO_9 structure.
     * To reset the per-user DEVMODE to the global default DEVMODE, call "SetPrinter" with the PRINTER_INFO_8 structure.
     */

    /* DEVMODE
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd183565(v=vs.85).aspx
     * https://msdn.microsoft.com/en-us/library/windows/hardware/ff552837(v=vs.85).aspx
     * http://www.undocprint.org/winspool/devmode
     * 
     * DM_SPECVERSION
     * >= Windows 2000: 0x0401;
     * Windows NT 4.0 : 0x0400;
     * Windows NT 3.x : 0x0320;
     * 
     * typedef struct _devicemode {
     *   TCHAR dmDeviceName[CCHDEVICENAME];
     *   WORD  dmSpecVersion;
     *   WORD  dmDriverVersion;
     *   WORD  dmSize;
     *   WORD  dmDriverExtra;
     *   DWORD dmFields;
     *   union {
     *     // printer only fields
     *     struct {
     *       short dmOrientation;
     *       short dmPaperSize;
     *       short dmPaperLength;
     *       short dmPaperWidth;
     *       short dmScale;
     *       short dmCopies;
     *       short dmDefaultSource;
     *       short dmPrintQuality;
     *     };
     *     // display only fields
     *     struct {
     *       POINTL dmPosition;
     *       DWORD  dmDisplayOrientation;
     *       DWORD  dmDisplayFixedOutput;
     *     };
     *   };
     *   short dmColor;
     *   short dmDuplex;
     *   short dmYResolution;
     *   short dmTTOption;
     *   short dmCollate;
     *   TCHAR dmFormName[CCHFORMNAME];
     *   WORD  dmLogPixels;
     *   DWORD dmBitsPerPel;
     *   DWORD dmPelsWidth;
     *   DWORD dmPelsHeight;
     *   union {
     *     // printer only fields
     *     DWORD dmDisplayFlags;
     *     // display only fields
     *     DWORD dmNup;
     *   };
     *   DWORD dmDisplayFrequency;
     * #if (DM_SPECVERSION >= 0x0400)
     *   DWORD dmICMMethod;
     *   DWORD dmICMIntent;
     *   DWORD dmMediaType;
     *   DWORD dmDitherType;
     *   DWORD dmReserved1;
     *   DWORD dmReserved2;
     * #endif
     * #if (DM_SPECVERSION >= 0x0401)
     *   DWORD dmPanningWidth;
     *   DWORD dmPanningHeight;
     * #endif
     * } DEVMODE, *PDEVMODE, *LPDEVMODE;
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct DevMode
    {
        /* if (WINVER >= 0x0500): 0x0401; // >= Windows 2000
         * if (WINVER >= 0x0400): 0x0400;
         * else: 0x0320;
         */
        public const int DM_SPECVERSION = 0x0401;
        const int CCHDEVICENAME = 0x20;
        const int CCHFORMNAME = 0x20;

        #region Header
        /// <summary>
        /// A zero-terminated character array that specifies the "friendly" name of the printer or display; for example, "PCL/HP LaserJet" in the case of PCL/HP LaserJet.
        /// This string is unique among device drivers.
        /// Note that this name may be truncated to fit in the dmDeviceName array.
        /// 
        /// 打印机名; 本地: "{PrinterName}"; 外部: "\\{MachineName}\{PrinterName}";
        /// 超过最大长度将被截断, 因此可能并不完整.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;

        /// <summary>
        /// The version number of the initialization data specification on which the structure is based.
        /// To ensure the correct version is used for any operating system, use DM_SPECVERSION.
        /// 
        /// 规范版本; >= Windows 2000: 0x0401;
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public short dmSpecVersion;

        /// <summary>
        /// The driver version number assigned by the driver developer.
        /// For a printer, specifies the printer driver version number assigned by the printer driver developer.
        /// Display drivers can set this member to DM_SPECVERSION.
        /// 
        /// 驱动版本;
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public short dmDriverVersion;

        /// <summary>
        /// Specifies the size, in bytes, of the DEVMODE structure, not including any private driver-specific data that might follow the structure's public members.
        /// Set this member to sizeof (DEVMODE) to indicate the version of the DEVMODE structure being used.
        /// 
        /// 标准数据大小; if (dmSpecVersion == 0x0401 && Unicode): 220 Bytes;
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public short dmSize;

        /// <summary>
        /// Contains the number of bytes of private driver-data that follow this structure.
        /// If a device driver does not use device-specific information, set this member to zero.
        /// 
        /// 私有数据大小;
        /// </summary>
        [MarshalAs(UnmanagedType.U2)]
        public short dmDriverExtra;

        /// <summary>
        /// Specifies whether certain members of the DEVMODE structure have been initialized.
        /// If a member is initialized, its corresponding bit is set, otherwise the bit is clear.
        /// A driver supports only those DEVMODE members that are appropriate for the printer or display technology.
        /// 
        /// 标准数据设值情况;
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DevModeFields dmFields;
        #endregion

        #region Printer only fields
        /// <summary>
        /// For printer devices only, selects the orientation of the paper.
        /// 
        /// 纸张方向;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModeOrientation dmOrientation;

        /// <summary>
        /// For printer devices only, selects the size of the paper to print on.
        /// This member can be set to zero if the length and width of the paper are both set by the dmPaperLength and dmPaperWidth members.
        /// Otherwise, the dmPaperSize member can be set to a device specific value greater than or equal to DMPAPER_USER or to one of the following predefined values.
        /// 
        /// DMPAPER_USER = 256 = 0x100;
        /// 
        /// 纸张编号; 内建: [1-118]
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short dmPaperSize;

        /// <summary>
        /// For printer devices only, overrides the length of the paper specified by the dmPaperSize member, either for custom paper sizes or for devices such as dot-matrix printers that can print on a page of arbitrary length.
        /// These values, along with all other values in this structure that specify a physical length, are in tenths of a millimeter.
        /// (PaperHeight)
        /// 
        /// 纸张高度; Unit: 0.1mm;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short dmPaperLength;

        /// <summary>
        /// For printer devices only, overrides the width of the paper specified by the dmPaperSize member.
        /// 
        /// 纸张宽度; Unit: 0.1mm;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short dmPaperWidth;

        /// <summary>
        /// Specifies the factor by which the printed output is to be scaled.
        /// The apparent page size is scaled from the physical page size by a factor of dmScale /100. For example, a letter-sized page with a dmScale value of 50 would contain as much data as a page of 17- by 22-inches because the output text and graphics would be half their original height and width.
        /// 
        /// 打印缩放比例; Unit: %; Default: 100;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short dmScale;

        /// <summary>
        /// Selects the number of copies printed if the device supports multiple-page copies.
        /// 
        /// 打印分数;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short dmCopies;

        /// <summary>
        /// Specifies the paper source. To retrieve a list of the available paper sources for a printer, use the DeviceCapabilities function with the DC_BINS flag.
        /// This member can be one of the following values, or it can be a device-specific value greater than or equal to DMBIN_USER.
        /// If the specified constant is DMBIN_FORMSOURCE, the input bin should be selected automatically.
        /// 
        /// 纸张来源;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModePaperSources dmDefaultSource;

        /// <summary>
        /// Specifies the printer resolution.
        /// 
        /// If a positive value is specified, it specifies the number of dots per inch (DPI) and is therefore device dependent.
        /// 
        /// 打印质量; 水平方向每英寸的点数; (XResolution)
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModeResolutions dmPrintQuality;
        #endregion

        #region Display only fields
        // /// <summary>
        // /// For display devices only, a POINTL structure that indicates the positional coordinates of the display device in reference to the desktop area.
        // /// The primary display device is always located at coordinates (0,0).
        // /// </summary>
        // public POINT dmPosition;

        // /// <summary>
        // /// For display devices only, the orientation at which images should be presented.
        // /// If DM_DISPLAYORIENTATION is not set, this member must be zero.
        // /// 
        // /// To determine whether the display orientation is portrait or landscape orientation, check the ratio of dmPelsWidth to dmPelsHeight.
        // /// </summary>
        // [MarshalAs(UnmanagedType.I4)]
        // public DevModeDisplayOrientation dmDisplayOrientation;

        // /// <summary>
        // /// For fixed-resolution display devices only, how the display presents a low-resolution mode on a higher-resolution display.
        // /// For example, if a display device's resolution is fixed at 1024 x 768 pixels but its mode is set to 640 x 480 pixels, the device can either display a 640 x 480 image somewhere in the interior of the 1024 x 768 screen space or stretch the 640 x 480 image to fill the larger screen space.
        // /// If DM_DISPLAYFIXEDOUTPUT is not set, this member must be zero.
        // /// </summary>
        // [MarshalAs(UnmanagedType.I4)]
        // public DevModeDisplayFixedOutput dmDisplayFixedOutput;
        #endregion

        /// <summary>
        /// Switches between color and monochrome on color printers.
        /// 
        /// 打印颜色; 黑白; 彩色;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModeColor dmColor;

        /// <summary>
        /// Selects duplex or double-sided printing for printers capable of duplex printing.
        /// 
        /// 双面打印;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModeDuplex dmDuplex;

        /// <summary>
        /// Specifies the y-resolution, in dots per inch, of the printer.
        /// If the printer initializes this member, the dmPrintQuality member specifies the x-resolution, in dots per inch, of the printer.
        /// 
        /// 打印质量; 垂直方向每英寸的点数;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short dmYResolution;

        /// <summary>
        /// Specifies how TrueType fonts should be printed.
        /// 
        /// 打印全真字体处理方式;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModeTrueTypeOption dmTTOption;

        /// <summary>
        /// Specifies whether collation should be used when printing multiple copies.
        /// This member is ignored unless the printer driver indicates support for collation by setting the dmFields member to DM_COLLATE.
        /// 
        /// 是否逐份打印;
        /// 比如: 共计三张 打印两份; 是的输出顺序: 1-2-3, 1-2-3; 否的输出顺序: 1-1, 2-2, 3-3;
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public DevModeCollate dmCollate;

        /// <summary>
        /// A zero-terminated character array that specifies the name of the form to use; for example, "Letter" or "Legal".
        /// A complete set of names can be retrieved by using the EnumForms function.
        /// 
        /// 纸张名称;
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;

        /// <summary>
        /// The number of pixels per logical inch.
        /// Printer drivers do not use this member.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [MarshalAs(UnmanagedType.U2)]
        private short dmLogPixels; // 数据占位

        /// <summary>
        /// Specifies the color resolution, in bits per pixel, of the display device (for example: 4 bits for 16 colors, 8 bits for 256 colors, or 16 bits for 65,536 colors).
        /// Display drivers use this member, for example, in the "ChangeDisplaySettings“ function.
        /// Printer drivers do not use this member.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [MarshalAs(UnmanagedType.U4)]
        private int dmBitsPerPel; // 数据占位

        /// <summary>
        /// Specifies the width, in pixels, of the visible device surface.
        /// Display drivers use this member, for example, in the ChangeDisplaySettings function.
        /// Printer drivers do not use this member.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [MarshalAs(UnmanagedType.U4)]
        private int dmPelsWidth; // 数据占位

        /// <summary>
        /// Specifies the height, in pixels, of the visible device surface.
        /// Display drivers use this member, for example, in the ChangeDisplaySettings function.
        /// Printer drivers do not use this member.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [MarshalAs(UnmanagedType.U4)]
        private int dmPelsHeight; // 数据占位

        #region Display only fields
        // /// <summary>
        // /// Specifies the device's display mode.
        // /// 
        // /// Display drivers use this member, for example, in the "ChangeDisplaySettings" function.
        // /// Printer drivers do not use this member.
        // /// </summary>
        // [MarshalAs(UnmanagedType.U4)]
        // public DevModeDisplayFlags dmDisplayFlags;
        #endregion

        #region Printer only fields
        /// <summary>
        /// Specifies whether the print system handles "N-up" printing (playing multiple EMF logical pages onto a single physical page).
        /// 
        /// 合并打印处理方式: 系统处理; 程序处理;
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DevModeNUp dmNup;
        #endregion

        /// <summary>
        /// Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode.
        /// This value is also known as the display device's vertical refresh rate.
        /// Display drivers use this member.
        /// It is used, for example, in the ChangeDisplaySettings function.
        /// Printer drivers do not use this member.
        /// 
        /// When you call the EnumDisplaySettings function, the dmDisplayFrequency member may return with the value 0 or 1.
        /// These values represent the display hardware's default refresh rate.
        /// This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [MarshalAs(UnmanagedType.U4)]
        private int dmDisplayFrequency; // 数据占位

        #region 0x0400
        /// <summary>
        /// Specifies how ICM is handled. For a non-ICM application, this member determines if ICM is enabled or disabled.
        /// For ICM applications, the system examines this member to determine how to handle ICM support.
        /// This member can be one of the following predefined values, or a driver-defined value greater than or equal to the value of DMICMMETHOD_USER.
        /// 
        /// The printer driver must provide a user interface for setting this member.
        /// Most printer drivers support only the DMICMMETHOD_SYSTEM or DMICMMETHOD_NONE value.
        /// Drivers for PostScript printers support all values.
        /// 
        /// ICM: Image Color Management
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DevModeICMMethod dmICMMethod;

        /// <summary>
        /// Specifies which color matching method, or intent, should be used by default.
        /// This member is primarily for non-ICM applications. ICM applications can establish intents by using the ICM functions.
        /// This member can be one of the following predefined values, or a driver defined value greater than or equal to the value of DMICM_USER.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DevModeICMIntent dmICMIntent;

        /// <summary>
        /// Specifies the type of media being printed on.
        /// The member can be one of the following predefined values, or a driver-defined value greater than or equal to the value of DMMEDIA_USER.
        /// 
        /// To retrieve a list of the available media types for a printer, use the "DeviceCapabilities" function with the DC_MEDIATYPES flag.
        /// 
        /// 纸质类型;
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DevModeMediaType dmMediaType;

        /// <summary>
        /// Specifies how dithering is to be done.
        /// The member can be one of the following predefined values, or a driver-defined value greater than or equal to the value of DMDITHER_USER.
        /// 
        /// 抖动方式; 图像处理?
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public DevModeDitherType dmDitherType;

        /// <summary>
        /// Not used; must be zero.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int dmICCManufacturer;
        /// <summary>
        /// Not used; must be zero.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int dmICCModel;
        #endregion

        #region 0x0401
        /// <summary>
        /// This member must be zero.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int dmPanningWidth;
        /// <summary>
        /// This member must be zero.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int dmPanningHeight;
        #endregion
    }

    /* MS.Internal.Printing.Configuration.DevModeFields
     * System.Drawing.Printing.ModeField
     */
    [Flags]
    internal enum DevModeFields : int
    {
        #region Count = 30
        /// <summary>
        /// DM_ORIENTATION: dmOrientation
        /// </summary>
        Orientation = 0x01,
        /// <summary>
        /// DM_PAPERSIZE: dmPaperSize
        /// </summary>
        PaperSize = 0x02,
        /// <summary>
        /// DM_PAPERLENGTH: dmPaperLength
        /// </summary>
        PaperLength = 0x04,
        /// <summary>
        /// DM_PAPERWIDTH: dmPaperWidth
        /// </summary>
        PaperWidth = 0x08,
        /// <summary>
        /// DM_SCALE: dmScale
        /// </summary>
        Scale = 0x10,
        /// <summary>
        /// DM_POSITION: dmPosition
        /// </summary>
        Position = 0x20,
        /// <summary>
        /// DM_NUP: dmNup
        /// </summary>
        Nup = 0x40,
        /// <summary>
        /// DM_DISPLAYORIENTATION: dmDisplayOrientation
        /// </summary>
        DisplayOrientation = 0x80,
        /// <summary>
        /// DM_COPIES: dmCopies
        /// </summary>
        Copies = 0x0100,
        /// <summary>
        /// DM_DEFAULTSOURCE: dmDefaultSource
        /// </summary>
        DefaultSource = 0x0200,
        /// <summary>
        /// DM_PRINTQUALITY: dmPrintQuality
        /// </summary>
        PrintQuality = 0x0400,
        /// <summary>
        /// DM_COLOR: dmColor
        /// </summary>
        Color = 0x0800,
        /// <summary>
        /// DM_DUPLEX: dmDuplex
        /// </summary>
        Duplex = 0x1000,
        /// <summary>
        /// DM_YRESOLUTION: dmYResolution
        /// </summary>
        YResolution = 0x2000,
        /// <summary>
        /// DM_TTOPTION: dmTTOption
        /// </summary>
        TTOption = 0x4000,
        /// <summary>
        /// DM_COLLATE: dmCollate
        /// </summary>
        Collate = 0x8000,
        /// <summary>
        /// DM_FORMNAME: dmFormName
        /// </summary>
        FormName = 0x010000,
        /// <summary>
        /// DM_LOGPIXELS: dmLogPixels
        /// </summary>
        LogPixels = 0x020000,
        /// <summary>
        /// DM_BITSPERPEL: dmBitsPerPel
        /// </summary>
        BitsPerPel = 0x040000,
        /// <summary>
        /// DM_PELSWIDTH: dmPelsWidth
        /// </summary>
        PelsWidth = 0x080000,
        /// <summary>
        /// DM_PELSHEIGHT: dmPelsHeight
        /// </summary>
        PelsHeight = 0x100000,
        /// <summary>
        /// DM_DISPLAYFLAGS: dmDisplayFlags
        /// </summary>
        DisplayFlags = 0x200000,
        /// <summary>
        /// DM_DISPLAYFREQUENCY: dmDisplayFrequency
        /// </summary>
        DisplayFrequency = 0x400000,
        /// <summary>
        /// DM_ICMMETHOD: dmICMMethod
        /// </summary>
        ICMMethod = 0x800000,
        /// <summary>
        /// DM_ICMINTENT: dmICMIntent
        /// </summary>
        ICMIntent = 0x01000000,
        /// <summary>
        /// DM_MEDIATYPE: dmMediaType
        /// </summary>
        MediaType = 0x02000000,
        /// <summary>
        /// DM_DITHERTYPE: dmDitherType
        /// </summary>
        DitherType = 0x04000000,
        /// <summary>
        /// DM_PANNINGWIDTH: dmPanningWidth
        /// </summary>
        PanningWidth = 0x08000000,
        /// <summary>
        /// DM_PANNINGHEIGHT: dmPanningHeight
        /// </summary>
        PanningHeight = 0x10000000,
        /// <summary>
        /// DM_DISPLAYFIXEDOUTPUT: dmDisplayFixedOutput
        /// </summary>
        DisplayFixedOutput = 0x20000000,
        #endregion

        /// <summary>
        /// 打印设备 可设置的字段
        /// </summary>
        PrinterMask = Orientation | PaperSize | PaperLength | PaperWidth | Scale | Copies | DefaultSource | PrintQuality
            | Color | Duplex | YResolution | TTOption | Collate | FormName | Nup
            | ICMMethod | ICMIntent | MediaType | DitherType,
        // | PanningWidth | PanningHeight,

        /// <summary>
        /// 显示设备 可设置的字段
        /// </summary>
        DisplayMask = Position | DisplayOrientation | DisplayFixedOutput
            | LogPixels | BitsPerPel | PelsWidth | PelsHeight | DisplayFlags | DisplayFrequency
            | ICMMethod | ICMIntent | MediaType | DitherType,
        // | PanningWidth | PanningHeight,

        All = 0x3FFFFFFF,
    }

    // MS.Internal.Printing.Configuration.DevModeOrientation
    internal enum DevModeOrientation : short
    {
        /// <summary>
        /// DMORIENT_PORTRAIT
        /// 竖版
        /// </summary>
        Portrait = 1,
        /// <summary>
        /// DMORIENT_LANDSCAPE
        /// 横板
        /// </summary>
        Landscape = 2,
    }

    /* MS.Internal.Printing.Configuration.DevModePaperSources
     * System.Drawing.Printing.PaperSourceKind;
     */
    internal enum DevModePaperSources : short
    {
        /// <summary>
        /// DMBIN_UPPER = DMBIN_ONLYONE
        /// </summary>
        Upper = 1,
        /// <summary>
        /// DMBIN_LOWER
        /// </summary>
        Lower = 2,
        /// <summary>
        /// DMBIN_MIDDLE
        /// </summary>
        Middle = 3,

        /// <summary>
        /// DMBIN_MANUAL
        /// 手动送纸
        /// </summary>
        Manual = 4,

        /// <summary>
        /// DMBIN_ENVELOPE
        /// </summary>
        Envelope = 5,
        /// <summary>
        /// DMBIN_ENVMANUAL
        /// </summary>
        ManualFeed = 6,
        /// <summary>
        /// DMBIN_AUTO
        /// </summary>
        AutomaticFeed = 7,

        /// <summary>
        /// DMBIN_TRACTOR
        /// 滚动送纸
        /// </summary>
        TractorFeed = 8,

        /// <summary>
        /// DMBIN_SMALLFMT
        /// </summary>
        SmallFormat = 9,
        /// <summary>
        /// DMBIN_LARGEFMT
        /// </summary>
        LargeFormat = 10,
        /// <summary>
        /// DMBIN_LARGECAPACITY
        /// </summary>
        LargeCapacity = 11,
        /// <summary>
        /// DMBIN_CASSETTE
        /// </summary>
        Cassette = 14,

        /// <summary>
        /// DMBIN_FORMSOURCE
        /// 自动选择
        /// </summary>
        FormSource = 15,

        /// <summary>
        /// DMBIN_USER
        /// </summary>
        User = 0x0100,
    }

    /* MS.Internal.Printing.Configuration.DevModeResolutions
     * System.Drawing.Printing.PrinterResolutionKind;
     */
    internal enum DevModeResolutions : short
    {
        /// <summary>
        /// DMRES_HIGH
        /// </summary>
        High = -4,
        /// <summary>
        /// DMRES_MEDIUM
        /// </summary>
        Medium = -3,
        /// <summary>
        /// DMRES_LOW
        /// </summary>
        Low = -2,
        /// <summary>
        /// DMRES_DRAFT
        /// </summary>
        Draft = -1,
    }

    /* System.Windows.Forms.ScreenOrientation
     */
    internal enum DevModeDisplayOrientation : int
    {
        /// <summary>
        /// DMDO_DEFAULT
        /// The display orientation is the natural orientation of the display device; it should be used as the default.
        /// </summary>
        Default = 0,

        /// <summary>
        /// DMDO_90
        /// The display orientation is rotated 90 degrees (measured clockwise) from DMDO_DEFAULT.
        /// </summary>
        Rotated90 = 1,

        /// <summary>
        /// DMDO_180
        /// The display orientation is rotated 180 degrees (measured clockwise) from DMDO_DEFAULT.
        /// </summary>
        Rotated180 = 2,

        /// <summary>
        /// DMDO_270
        /// The display orientation is rotated 270 degrees (measured clockwise) from DMDO_DEFAULT.
        /// </summary>
        Rotated270 = 3,
    }
    internal enum DevModeDisplayFixedOutput : int
    {
        /// <summary>
        /// DMDFO_DEFAULT
        /// The display's default setting.
        /// </summary>
        Default = 0,

        /// <summary>
        /// DMDFO_STRETCH
        /// The low-resolution image is stretched to fill the larger screen space.
        /// </summary>
        Stretch = 1,

        /// <summary>
        /// DMDFO_CENTER
        /// The low-resolution image is centered in the larger screen space.
        /// </summary>
        Center = 2,
    }

    // MS.Internal.Printing.Configuration.DevModeColor
    internal enum DevModeColor : short
    {
        /// <summary>
        /// DMCOLOR_MONOCHROME
        /// 黑白
        /// </summary>
        Monochrome = 1,

        /// <summary>
        /// DMCOLOR_COLOR
        /// 彩色
        /// </summary>
        Color = 2,
    }

    /* MS.Internal.Printing.Configuration.DevModeDuplex
     * System.Drawing.Printing.Duplex;
     */
    internal enum DevModeDuplex : short
    {
        /// <summary>
        /// DMDUP_SIMPLEX
        /// Normal (nonduplex) printing.
        /// 
        /// 单面打印
        /// </summary>
        Simplex = 1,

        /// <summary>
        /// DMDUP_VERTICAL
        /// Long-edge binding, that is, the long edge of the page is vertical.
        /// </summary>
        Vertical = 2,

        /// <summary>
        /// DMDUP_HORIZONTAL
        /// Short-edge binding, that is, the long edge of the page is horizontal.
        /// </summary>
        Horizontal = 3,
    }

    /* MS.Internal.Printing.Configuration.DevModeTrueTypeOption
     * System.Printing.TrueTypeFontMode
     */
    internal enum DevModeTrueTypeOption : short
    {
        /// <summary>
        /// DMTT_BITMAP
        /// Prints TrueType fonts as graphics. This is the default action for dot-matrix printers.
        /// </summary>
        Bitmap = 1,

        /// <summary>
        /// DMTT_DOWNLOAD
        /// Downloads TrueType fonts as soft fonts.
        /// This is the default action for Hewlett-Packard printers that use Printer Control Language (PCL).
        /// </summary>
        Download = 2,

        /// <summary>
        /// DMTT_SUBDEV
        /// Substitutes device fonts for TrueType fonts. This is the default action for PostScript printers.
        /// </summary>
        SubstituteDevice = 3,

        /// <summary>
        /// DMTT_DOWNLOAD_OUTLINE
        /// Downloads TrueType fonts as outline soft fonts.
        /// </summary>
        DownloadOutline = 4,
    }

    // MS.Internal.Printing.Configuration.DevModeCollate
    internal enum DevModeCollate : short
    {
        /// <summary>
        /// DMCOLLATE_FALSE
        /// Do not collate when printing multiple copies.
        /// </summary>
        False = 0,

        /// <summary>
        /// DMCOLLATE_TRUE
        /// Collate when printing multiple copies.
        /// </summary>
        True = 1,
    }

    [Flags]
    internal enum DevModeDisplayFlags : int
    {
        /// <summary>
        /// DM_GRAYSCALE
        /// Specifies that the display is a noncolor device.
        /// If this flag is not set, color is assumed.
        /// 
        /// This flag is no longer valid.
        /// </summary>
        Grayscale = 0x01,

        /// <summary>
        /// DM_INTERLACED
        /// Specifies that the display mode is interlaced.
        /// If the flag is not set, noninterlaced is assumed.
        /// </summary>
        Interlaced = 0x02,

        /// <summary>
        /// DMDISPLAYFLAGS_TEXTMODE
        /// </summary>
        TextMode = 0x04,
    }

    // MS.Internal.Printing.Configuration.DevModeNUp
    internal enum DevModeNUp : int
    {
        /// <summary>
        /// DMNUP_SYSTEM
        /// The print spooler does the NUP.
        /// </summary>
        System = 1,

        /// <summary>
        /// DMNUP_ONEUP
        /// The application does the NUP.
        /// </summary>
        OneUp = 2,
    }

    // MS.Internal.Printing.Configuration.DevModeICMMethod
    internal enum DevModeICMMethod : int
    {
        /// <summary>
        /// DMICMMETHOD_NONE
        /// Specifies that ICM is disabled.
        /// </summary>
        None = 1,

        /// <summary>
        /// DMICMMETHOD_SYSTEM
        /// Specifies that ICM is handled by Windows.
        /// </summary>
        System = 2,

        /// <summary>
        /// DMICMMETHOD_DRIVER
        /// Specifies that ICM is handled by the device driver.
        /// </summary>
        Driver = 3,

        /// <summary>
        /// DMICMMETHOD_DEVICE
        /// Specifies that ICM is handled by the destination device.
        /// </summary>
        Device = 4,

        /// <summary>
        /// DMICMMETHOD_USER
        /// Device-specific methods start here.
        /// </summary>
        User = 0x0100,
    }

    // MS.Internal.Printing.Configuration.DevModeICMIntents
    internal enum DevModeICMIntent : int
    {
        /// <summary>
        /// DMICM_SATURATE
        /// Color matching should optimize for color saturation.
        /// This value is the most appropriate choice for business graphs when dithering is not desired.
        /// </summary>
        Saturate = 1,

        /// <summary>
        /// DMICM_CONTRAST
        /// Color matching should optimize for color contrast.
        /// This value is the most appropriate choice for scanned or photographic images when dithering is desired.
        /// </summary>
        Contrast = 2,

        /// <summary>
        /// DMICM_COLORIMETRIC
        /// Color matching should optimize to match the exact color requested.
        /// This value is most appropriate for use with business logos or other images when an exact color match is desired.
        /// </summary>
        Colorimetric = 3,

        /// <summary>
        /// DMICM_ABS_COLORIMETRIC
        /// Color matching should optimize to match the exact color requested without white point mapping.
        /// This value is most appropriate for use with proofing.
        /// </summary>
        AbsColorimetric = 4,

        /// <summary>
        /// DMICM_USER
        /// Device-specific intents start here.
        /// </summary>
        User = 0x0100,
    }

    /* MS.Internal.Printing.Configuration.DevModeMediaTypes
     * 
     * System.Printing.PageMediaType
     */
    internal enum DevModeMediaType : int
    {
        /// <summary>
        /// DMMEDIA_STANDARD
        /// Plain paper.
        /// </summary>
        Standard = 1,

        /// <summary>
        /// DMMEDIA_TRANSPARENCY
        /// Transparent film.
        /// </summary>
        Transparency = 2,

        /// <summary>
        /// DMMEDIA_GLOSSY
        /// Glossy paper.
        /// </summary>
        Glossy = 3,

        /// <summary>
        /// DMMEDIA_USER
        /// Device-specific media start here.
        /// </summary>
        User = 0x0100,
    }

    // MS.Internal.Printing.Configuration.DevModeDitherTypes
    internal enum DevModeDitherType : int
    {
        /// <summary>
        /// DMDITHER_NONE
        /// No dithering.
        /// </summary>
        None = 1,

        /// <summary>
        /// DMDITHER_COARSE
        /// Dithering with a coarse brush.
        /// </summary>
        Coarse = 2,

        /// <summary>
        /// DMDITHER_FINE
        /// Dithering with a fine brush.
        /// </summary>
        Fine = 3,

        /// <summary>
        /// DMDITHER_LINEART
        /// Line art dithering, a special dithering method that produces well defined borders between black, white, and gray scaling.
        /// It is not suitable for images that include continuous graduations in intensity and hue, such as scanned photographs.
        /// </summary>
        LineArt = 4,

        /* LineArt dithering:
         * 
         * DMDITHER_ERRORDIFFUSION = 5
         * DMDITHER_RESERVED6 = 6
         * DMDITHER_RESERVED7 = 7
         * DMDITHER_RESERVED8 = 8
         * DMDITHER_RESERVED9 = 9
         */

        /// <summary>
        /// DMDITHER_GRAYSCALE
        /// Device does gray scaling.
        /// </summary>
        Grayscale = 10,

        /// <summary>
        /// DMDITHER_USER
        /// Device-specific dithers start here.
        /// </summary>
        User,
    }
}
