using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    partial class Native
    {
        /* DeviceCapabilities
         * https://msdn.microsoft.com/en-us/library/windows/desktop/dd183552(v=vs.85).aspx
         * DWORD DeviceCapabilities(
         *   _In_        LPCTSTR pDevice,
         *   _In_        LPCTSTR pPort,
         *   _In_        WORD    fwCapability,
         *   _Out_       LPTSTR  pOutput,
         *   _In_  const DEVMODE *pDevMode
         * );
         */
        /// <summary>
        /// The DeviceCapabilities function retrieves the capabilities of a printer driver.
        /// </summary>
        /// <param name="pDevice">
        /// A pointer to a null-terminated string that contains the name of the printer.
        /// Note that this is the name of the printer, not of the printer driver.
        /// </param>
        /// <param name="pPort">A pointer to a null-terminated string that contains the name of the port to which the device is connected, such as LPT1.</param>
        /// <param name="fwCapability">The capabilities to be queried. This parameter can be one of the following values.</param>
        /// <param name="pOutput">
        /// A pointer to an array. The format of the array depends on the setting of the fwCapability parameter.
        /// If pOutput is NULL, DeviceCapabilities returns the number of bytes required for the output data.
        /// </param>
        /// <param name="pDevMode">
        /// A pointer to a DEVMODE structure.
        /// If this parameter is NULL, DeviceCapabilities retrieves the current default initialization values for the specified printer driver.
        /// Otherwise, the function retrieves the values contained in the structure to which pDevMode points.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value depends on the setting of the fwCapability parameter.
        /// A return value of zero generally indicates that, while the function completed successfully, there was some type of failure, such as a capability that is not supported. For more details, see the descriptions for the fwCapability values.
        /// 
        /// If the function returns -1, this may mean either that the capability is not supported or there was a general function failure.
        /// </returns>
        [DllImport(ExternDll.WinSpool, CharSet = CharSet.Unicode, EntryPoint = "DeviceCapabilitiesW", SetLastError = true)]
        internal static extern int DeviceCapabilities(string pDevice, string pPort, PrinterCapabilities fwCapability, IntPtr pOutput, IntPtr pDevMode);
    }
}