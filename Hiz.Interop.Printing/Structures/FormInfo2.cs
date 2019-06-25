using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* FORM_INFO_2
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd144837(v=vs.85).aspx
     * typedef struct _FORM_INFO_2 {
     *   DWORD   Flags;
     *   LPTSTR  pName;
     *   SIZEL   Size;
     *   RECTL   ImageableArea;
     *   LPCSTR  pKeyword;
     *   DWORD   StringType;
     *   LPCTSTR pMuiDll;
     *   DWORD   dwResourceId;
     *   LPCTSTR pDisplayName;
     *   LANGID  wLangId;
     * } FORM_INFO_2, *PFORM_INFO_2;
     * 
     * Minimum supported client: Windows Vista;
     * Minimum supported server: Windows Server 2008;
     */
    /// <summary>
    /// Contains information about a localizable print form.
    /// </summary>
    /// <remarks>
    /// On a call to "AddForm" or "SetForm":
    /// If StringType is STRING_NONE, both "pMuiDll" and "pDisplayName" must be NULL and both "dwResourceId" and "wLangId" must be 0.
    /// If StringType is STRING_MUIDLL, "pDisplayName" must be NULL and "wLangId" must be 0.
    /// If StringType is STRING_LANGPAIR, "pMuiDll" must be NULL and "dwResourceId" must be 0.
    /// 
    /// For a FORM_INFO_2 returned by a call to GetForm or EnumForms:
    /// If StringType is both STRING_MUIDLL and STRING_LANGPAIR, pMuiDll, pDisplayName, dwResourceId, and wLangId will all have valid values.
    /// If StringType is STRING_MUIDLL only, pMuiDll and dwResourceId will have valid values. pDisplayName will be NULL and wLangId will be 0.
    /// If StringType is STRING_LANGPAIR only, pDisplayName and wLangId will have valid values. pMuiDll will be NULL and dwResourceId will be 0.
    /// </remarks>
    [DebuggerDisplay("{Flags}: {pName}")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FormInfo2
    {
        /// <summary>
        /// The form properties. The following values are defined, but only one can be set.
        /// When the FORM_INFO_2 is returned by GetForm or EnumForms, Flags is set to the current value in the forms database.
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
        /// The width and height, in thousandths of millimeters, of the area of the page on which the printer can print.
        /// Unit: 0.001mm.
        /// </summary>
        public RECT ImageableArea;

        /// <summary>
        /// A pointer to a non-localizable string identifier of the form.
        /// When passed to AddForm or SetForm, this gives the caller a means of identifying the form in all locales.
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)] // AnsiString
        public String pKeyword;

        /// <summary>
        /// Specifies how a localized display name for the form is obtained at runtime.
        /// The following values are defined. Only one can be set in any given call to AddForm or SetForm.
        /// Both STRING_MUIDLL and STRING_LANGPAIR can be set in the FORM_INFO_2 (s) returned by GetForm or EnumForms.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public FormInfoStringType StringType;

        /// <summary>
        /// The "Multilingual User Interface" localized resource DLL that contains the localized display name.
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pMuiDll;

        /// <summary>
        /// The resource ID of the form's display name in "pMuiDll".
        /// </summary>
        public int dwResourceId;

        /// <summary>
        /// The form's display name in the language specified by "wLangId".
        /// </summary>
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDisplayName;

        /// <summary>
        /// The language of the "pDisplayName".
        /// </summary>
        [MarshalAs(UnmanagedType.I2)]
        public short wLangId;
    }

    [Flags]
    public enum FormInfoStringType : int
    {
        /// <summary>
        /// STRING_NONE
        /// There is no localized display name.
        /// </summary>
        None = 0x01,
        /// <summary>
        /// STRING_MUIDLL
        /// The display name is extracted from the "Multilingual User Interface" localized resources DLL specified in "pMuiDll".
        /// The ID is in the "dwResourceId" member.
        /// </summary>
        MuiDll = 0x02,
        /// <summary>
        /// STRING_LANGPAIR
        /// The display name and language ID are provided directly by "pDisplayName" and the language is specified by "wLangId".
        /// </summary>
        LangPair = 0x04,
    }
}
