using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    partial class Native
    {
        /* GetDeviceCaps
         * https://msdn.microsoft.com/en-us/library/dd144877(v=vs.85).aspx
         * int GetDeviceCaps(
         *   _In_ HDC hdc,
         *   _In_ int nIndex
         * );
         */
        /// <summary>
        /// The GetDeviceCaps function retrieves device-specific information for the specified device.
        /// </summary>
        /// <param name="hdc">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned.</param>
        /// <returns></returns>
        [DllImport(ExternDll.Gdi32, CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
    }

    /* System.Windows.Forms.Internal.DeviceCapabilities
     */
    enum DeviceCapabilities : int
    {
        /// <summary>
        /// DRIVERVERSION
        /// The device driver version.
        /// </summary>
        DriverVersion = 0,

        /// <summary>
        /// TECHNOLOGY
        /// Device technology.
        /// </summary>
        Technology = 2,

        /// <summary>
        /// HORZSIZE
        /// Width, in millimeters, of the physical screen.
        /// Horizontal size in millimeters.
        /// </summary>
        HorizontalSize = 4,

        /// <summary>
        /// VERTSIZE
        /// Height, in millimeters, of the physical screen.
        /// Vertical size in millimeters.
        /// </summary>
        VerticalSize = 6,

        /// <summary>
        /// HORZRES
        /// Width, in pixels, of the screen; or for printers, the width, in pixels, of the printable area of the page.
        /// Horizontal width in pixels.
        /// </summary>
        HorizontalResolution = 8,

        /// <summary>
        /// VERTRES
        /// Height, in raster lines, of the screen; or for printers, the height, in pixels, of the printable area of the page.
        /// Vertical height in pixels.
        /// </summary>
        VerticalResolution = 10,

        /// <summary>
        /// BITSPIXEL
        /// Number of adjacent color bits for each pixel.
        /// Number of bits per pixel.
        /// </summary>
        BitsPerPixel = 12,

        /// <summary>
        /// PLANES
        /// Number of color planes.
        /// </summary>
        NumberOfColorPlanes = 14,

        /// <summary>
        /// NUMBRUSHES
        /// Number of device-specific brushes.
        /// </summary>
        NumberOfBrushes = 16,

        /// <summary>
        /// NUMPENS
        /// Number of device-specific pens.
        /// </summary>
        NumberOfPens = 18,

        /// <summary>
        /// NUMMARKERS
        /// Number of markers the device has.
        /// </summary>
        NumberOfMarkers = 20,

        /// <summary>
        /// NUMFONTS
        /// Number of device-specific fonts.
        /// </summary>
        NumberOfFonts = 22,

        /// <summary>
        /// NUMCOLORS
        /// Number of entries in the device's color table, if the device has a color depth of no more than 8 bits per pixel.
        /// For devices with greater color depths, 1 is returned.
        /// 
        /// Number of colors the device supports.
        /// </summary>
        NumberOfColors = 24,

        /// <summary>
        /// PDEVICESIZE
        /// Size required for device descriptor.
        /// </summary>
        DeviceSize = 26,

        /// <summary>
        /// CURVECAPS
        /// Value that indicates the curve capabilities of the device.
        /// </summary>
        CurveCapabilities = 28,

        /// <summary>
        /// LINECAPS
        /// Value that indicates the line capabilities of the device.
        /// </summary>
        LineCapabilities = 30,

        /// <summary>
        /// POLYGONALCAPS
        /// Value that indicates the polygon capabilities of the device.
        /// </summary>
        PolygonalCapabilities = 32,

        /// <summary>
        /// TEXTCAPS
        /// Value that indicates the text capabilities of the device.
        /// </summary>
        TextCapabilities = 34,

        /// <summary>
        /// CLIPCAPS
        /// Flag that indicates the clipping capabilities of the device.
        /// If the device can clip to a rectangle, it is 1. Otherwise, it is 0.
        /// </summary>
        ClippingCapabilities = 36,

        /// <summary>
        /// RASTERCAPS
        /// Value that indicates the raster capabilities of the device.
        /// </summary>
        RasterCapabilities = 38,

        /// <summary>
        /// ASPECTX
        /// Relative width of a device pixel used for line drawing.
        /// </summary>
        DeviceWidth = 40,

        /// <summary>
        /// ASPECTY
        /// Relative height of a device pixel used for line drawing.
        /// </summary>
        DeviceHeight = 42,

        /// <summary>
        /// ASPECTXY
        /// Diagonal width of the device pixel used for line drawing.
        /// </summary>
        DeviceDiagonal = 44,

        /// <summary>
        /// Number of pixels per logical inch along the screen width.
        /// In a system with multiple display monitors, this value is the same for all monitors.
        /// 
        /// Logical pixels/inch in X.
        /// </summary>
        LogicalPixelsX = 88,

        /// <summary>
        /// LOGPIXELSY
        /// Number of pixels per logical inch along the screen height.
        /// In a system with multiple display monitors, this value is the same for all monitors.
        /// 
        /// Logical pixels/inch in Y.
        /// </summary>
        LogicalPixelsY = 90,

        /// <summary>
        /// SIZEPALETTE
        /// Number of entries in the system palette.
        /// This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
        /// </summary>
        NumberOfEntries = 104,

        /// <summary>
        /// NUMRESERVED
        /// Number of reserved entries in the system palette.
        /// This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
        /// </summary>
        NumberOfReserved = 106,

        /// <summary>
        /// COLORRES
        /// Actual color resolution of the device, in bits per pixel.
        /// This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the driver is compatible with 16-bit Windows.
        /// </summary>
        ColorResolution = 108,

        #region Printing related DeviceCaps.
        /// <summary>
        /// PHYSICALWIDTH
        /// For printing devices: the width of the physical page, in device units.
        /// For example, a printer set to print at 600 dpi on 8.5-x11-inch paper has a physical width value of 5100 device units.
        /// Note that the physical page is almost always greater than the printable area of the page, and never smaller.
        /// </summary>
        PhysicalWidth = 110,

        /// <summary>
        /// PHYSICALHEIGHT
        /// For printing devices: the height of the physical page, in device units.
        /// For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper has a physical height value of 6600 device units.
        /// Note that the physical page is almost always greater than the printable area of the page, and never smaller.
        /// </summary>
        PhysicalHeight = 111,

        /// <summary>
        /// PHYSICALOFFSETX
        /// For printing devices: the distance from the left edge of the physical page to the left edge of the printable area, in device units.
        /// For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the leftmost 0.25-inch of paper, has a horizontal physical offset of 150 device units.
        /// </summary>
        PhysicalOffsetX = 112,

        /// <summary>
        /// PHYSICALOFFSETY
        /// For printing devices: the distance from the top edge of the physical page to the top edge of the printable area, in device units.
        /// For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the topmost 0.5-inch of paper, has a vertical physical offset of 300 device units.
        /// </summary>
        PhysicalOffsetY = 113,

        /// <summary>
        /// SCALINGFACTORX
        /// Scaling factor for the x-axis of the printer.
        /// </summary>
        ScalingFactorX = 114,

        /// <summary>
        /// SCALINGFACTORY
        /// Scaling factor for the y-axis of the printer.
        /// </summary>
        ScalingFactorY = 115,
        #endregion

        /// <summary>
        /// VREFRESH
        /// For display devices: the current vertical refresh rate of the device, in cycles per second (Hz).
        /// A vertical refresh rate value of 0 or 1 represents the display hardware's default refresh rate.
        /// This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.
        /// </summary>
        VRefresh = 116,

        /// <summary>
        /// DESKTOPVERTRES
        /// Horizontal width of entire desktop in pixels
        /// </summary>
        DesktopVerticalResolution = 117,
        /// <summary>
        /// DESKTOPHORZRES
        /// Vertical height of entire desktop in pixels
        /// </summary>
        DesktopHorizontalResolution = 118,

        /// <summary>
        /// BLTALIGNMENT
        /// Preferred horizontal drawing alignment, expressed as a multiple of pixels.
        /// For best drawing performance, windows should be horizontally aligned to a multiple of this value.
        /// A value of zero indicates that the device is accelerated, and any alignment may be used.
        /// 
        /// BLT: bit block transfer.
        /// </summary>
        HorizontalAlignment = 119,

        /// <summary>
        /// SHADEBLENDCAPS
        /// Value that indicates the shading and blending capabilities of the device.
        /// </summary>
        ShadeBlendCapabilities = 120,

        /// <summary>
        /// COLORMGMTCAPS
        /// Value that indicates the color management capabilities of the device.
        /// </summary>
        ColorManagementCapabilities = 121,
    }
}