using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    internal enum DcPrintRateUnit
    {
        Unknown = 0,
        // PRINTRATEUNIT_PPM
        PagesPerMinute = 1,
        // PRINTRATEUNIT_CPS
        CharactersPerSecond = 2,
        // PRINTRATEUNIT_LPM
        LinesPerMinute = 3,
        // PRINTRATEUNIT_IPM
        InchesPerMinute = 4,
    }
}
