using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* WinGdi.h
     * 
     * ReachFramework.dll
     * MS.Internal.Printing.Configuration.WinSpoolPrinterCapabilities
     */
    internal enum PrinterCapabilities : short
    {
        /// <summary>
        /// DC_FIELDS
        /// Returns the dmFields member of the printer driver's DEVMODE structure.
        /// The dmFields member indicates which members in the device-independent portion of the structure are supported by the printer driver.
        /// </summary>
        Fields = 1,

        /// <summary>
        /// DC_PAPERS
        /// Retrieves a list of supported paper sizes.
        /// The pOutput buffer receives an array of WORD values that indicate the available paper sizes for the printer.
        /// The return value indicates the number of entries in the array.
        /// For a list of the possible array values, see the description of the dmPaperSize member of the DEVMODE structure.
        /// If pOutput is NULL, the return value indicates the required number of entries in the array.
        /// </summary>
        Papers = 2,

        /// <summary>
        /// DC_PAPERSIZE
        /// Retrieves the dimensions, in tenths of a millimeter, of each supported paper size.
        /// The pOutput buffer receives an array of POINT structures.
        /// Each structure contains the width (x-dimension) and length (y-dimension) of a paper size as if the paper were in the DMORIENT_PORTRAIT orientation.
        /// The return value indicates the number of entries in the array.
        /// </summary>
        PaperSize = 3,

        /// <summary>
        /// DC_MINEXTENT
        /// Returns the minimum paper size that the dmPaperLength and dmPaperWidth members of the printer driver's DEVMODE structure can specify.
        /// The LOWORD of the return value contains the minimum dmPaperWidth value, and the HIWORD contains the minimum dmPaperLength value.
        /// </summary>
        MinExtent = 4,

        /// <summary>
        /// DC_MAXEXTENT
        /// Returns the maximum paper size that the dmPaperLength and dmPaperWidth members of the printer driver's DEVMODE structure can specify.
        /// The LOWORD of the return value contains the maximum dmPaperWidth value, and the HIWORD contains the maximum dmPaperLength value.
        /// </summary>
        MaxExtent = 5,

        /// <summary>
        /// DC_BINS
        /// Retrieves a list of available paper bins.
        /// The pOutput buffer receives an array of WORD values that indicate the available paper sources for the printer.
        /// The return value indicates the number of entries in the array. For a list of the possible array values, see the description of the dmDefaultSource member of the DEVMODE structure.
        /// If pOutput is NULL, the return value indicates the required number of entries in the array.
        /// </summary>
        Bins = 6,

        /// <summary>
        /// DC_DUPLEX
        /// If the printer supports duplex printing, the return value is 1; otherwise, the return value is zero.
        /// The pOutput parameter is not used.
        /// </summary>
        Duplex = 7,

        /// <summary>
        /// DC_SIZE
        /// Returns the dmSize member of the printer driver's DEVMODE structure.
        /// </summary>
        Size = 8,

        /// <summary>
        /// DC_EXTRA
        /// Returns the number of bytes required for the device-specific portion of the DEVMODE structure for the printer driver.
        /// </summary>
        Extra = 9,

        /// <summary>
        /// DC_VERSION
        /// Returns the specification version to which the printer driver conforms.
        /// </summary>
        Version = 10,

        /// <summary>
        /// DC_DRIVER
        /// Returns the version number of the printer driver.
        /// </summary>
        Driver = 11,

        /// <summary>
        /// DC_BINNAMES
        /// Retrieves the names of the printer's paper bins.
        /// The pOutput buffer receives an array of string buffers.
        /// Each string buffer is 24 characters long and contains the name of a paper bin.
        /// The return value indicates the number of entries in the array.
        /// The name strings are null-terminated unless the name is 24 characters long.
        /// If pOutput is NULL, the return value is the number of bin entries required.
        /// </summary>
        BinNames = 12,

        /// <summary>
        /// DC_ENUMRESOLUTIONS
        /// Retrieves a list of the resolutions supported by the printer.
        /// The pOutput buffer receives an array of LONG values. For each supported resolution, the array contains a pair of LONG values that specify the x and y dimensions of the resolution, in dots per inch.
        /// The return value indicates the number of supported resolutions.
        /// If pOutput is NULL, the return value indicates the number of supported resolutions.
        /// </summary>
        EnumResolutions = 13,

        /// <summary>
        /// DC_FILEDEPENDENCIES
        /// Retrieves the names of any additional files that need to be loaded when a driver is installed.
        /// The pOutput buffer receives an array of string buffers. Each string buffer is 64 characters long and contains the name of a file.
        /// The return value indicates the number of entries in the array.
        /// The name strings are null-terminated unless the name is 64 characters long. If pOutput is NULL, the return value is the number of files.
        /// </summary>
        FileDependencies = 14,

        /// <summary>
        /// DC_TRUETYPE
        /// Retrieves the abilities of the driver to use TrueType fonts.
        /// For DC_TRUETYPE, the pOutput parameter should be NULL.
        /// The return value can be one or more of the following:
        /// DCTT_BITMAP: Device can print TrueType fonts as graphics.
        /// DCTT_DOWNLOAD: Device can download TrueType fonts.
        /// DCTT_SUBDEV: Device can substitute device fonts for TrueType fonts.
        /// </summary>
        TrueType = 15,

        /// <summary>
        /// DC_PAPERNAMES
        /// Retrieves a list of supported paper names (for example, Letter or Legal).
        /// The pOutput buffer receives an array of string buffers.
        /// Each string buffer is 64 characters long and contains the name of a paper form.
        /// The return value indicates the number of entries in the array.
        /// The name strings are null-terminated unless the name is 64 characters long.
        /// If pOutput is NULL, the return value is the number of paper forms.
        /// </summary>
        PaperNames = 16,

        /// <summary>
        /// DC_ORIENTATION
        /// Returns the relationship between portrait and landscape orientations for a device, in terms of the number of degrees that portrait orientation is rotated counterclockwise to produce landscape orientation.
        /// The return value can be one of the following:
        ///   0: No landscape orientation.
        ///  90: Portrait is rotated 90 degrees to produce landscape.
        /// 270: Portrait is rotated 270 degrees to produce landscape.
        /// </summary>
        Orientation = 17,

        /// <summary>
        /// DC_COPIES
        /// Returns the number of copies the device can print.
        /// </summary>
        Copies = 18,

        // DC_BINADJUST
        BinAdjust = 19,
        /// <summary>
        /// DC_EMF_COMPLIANT
        /// 是否支持 emf 图元文件; (Windows 95)
        /// </summary>
        EmfCompliant = 20,
        /// <summary>
        /// DC_DATATYPE_PRODUCED
        /// Only Raw; (Windows 95)
        /// </summary>
        DatatypeProduced = 21,

        /// <summary>
        /// DC_COLLATE
        /// If the printer supports collating, the return value is 1; otherwise, the return value is zero.
        /// The pOutput parameter is not used.
        /// </summary>
        Collate = 22,

        // DC_MANUFACTURER
        Manufacturer = 23,
        // DC_MODEL
        Model = 24,

        /// <summary>
        /// DC_PERSONALITY
        /// Retrieves a list of printer description languages supported by the printer.
        /// The pOutput buffer receives an array of string buffers.
        /// Each buffer is 32 characters long and contains the name of a printer description language.
        /// The return value indicates the number of entries in the array. The name strings are null-terminated unless the name is 32 characters long.
        /// If pOutput is NULL, the return value indicates the required number of array entries.
        /// </summary>
        Personality = 25,

        /// <summary>
        /// DC_PRINTRATE
        /// The return value indicates the printer's print rate.
        /// The value returned for DC_PRINTRATEUNIT indicates the units of the DC_PRINTRATE value.
        /// The pOutput parameter is not used.
        /// </summary>
        PrintRate = 26,

        /// <summary>
        /// DC_PRINTRATEUNIT
        /// The return value is one of the following values that indicate the print rate units for the value returned for the DC_PRINTRATE flag.
        /// The pOutput parameter is not used.
        /// 
        /// PRINTRATEUNIT_CPS: Characters per second.
        /// PRINTRATEUNIT_IPM: Inches per minute.
        /// PRINTRATEUNIT_LPM: Lines per minute.
        /// PRINTRATEUNIT_PPM: Pages per minute.
        /// </summary>
        PrintRateUnit = 27,

        /// <summary>
        /// DC_PRINTERMEM
        /// The return value is the amount of available printer memory, in kilobytes.
        /// The pOutput parameter is not used.
        /// </summary>
        PrinterMemory = 28,

        /// <summary>
        /// DC_MEDIAREADY
        /// Retrieves the names of the paper forms that are currently available for use.
        /// The pOutput buffer receives an array of string buffers. Each string buffer is 64 characters long and contains the name of a paper form.
        /// The return value indicates the number of entries in the array.
        /// The name strings are null-terminated unless the name is 64 characters long.
        /// If pOutput is NULL, the return value is the number of paper forms.
        /// </summary>
        MediaReady = 29,

        /// <summary>
        /// DC_STAPLE
        /// If the printer supports stapling, the return value is a nonzero value; otherwise, the return value is zero.
        /// The pOutput parameter is not used.
        /// </summary>
        Staple = 30,

        /// <summary>
        /// DC_PRINTRATEPPM
        /// The return value indicates the printer's print rate, in pages per minute.
        /// The pOutput parameter is not used.
        /// </summary>
        PrintRatePagesPerMinute = 31,

        /// <summary>
        /// DC_COLORDEVICE
        /// If the printer supports color printing, the return value is 1; otherwise, the return value is zero.
        /// The pOutput parameter is not used.
        /// </summary>
        ColorDevice = 32,

        /// <summary>
        /// DC_NUP
        /// Retrieves an array of integers that indicate that printer's ability to print multiple document pages per printed page.
        /// The pOutput buffer receives an array of DWORD values. Each value represents a supported number of document pages per printed page.
        /// The return value indicates the number of entries in the array.
        /// If pOutput is NULL, the return value indicates the required number of entries in the array.
        /// </summary>
        NUp = 33,

        /// <summary>
        /// DC_MEDIATYPENAMES
        /// Retrieves the names of the supported media types. The pOutput buffer receives an array of string buffers.
        /// Each string buffer is 64 characters long and contains the name of a supported media type. The return value indicates the number of entries in the array.
        /// The strings are null-terminated unless the name is 64 characters long.
        /// If pOutput is NULL, the return value is the number of media type names required.
        /// </summary>
        MediaTypeNames = 34,

        /// <summary>
        /// DC_MEDIATYPES
        /// Retrieves a list of supported media types. The pOutput buffer receives an array of DWORD values that indicate the supported media types.
        /// The return value indicates the number of entries in the array.
        /// For a list of possible array values, see the description of the dmMediaType member of the DEVMODE structure.
        /// If pOutput is NULL, the return value indicates the required number of entries in the array.
        /// </summary>
        MediaTypes = 35,
    }
}
