using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Hiz.Interop.Printing
{
    internal static class Error
    {
        /* System Error Codes
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms681381(v=vs.85).aspx
         */
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        public const int ERROR_INVALID_FORM_NAME = 1902;

        public static void ThrowIfLastErrorIsNot(int value)
        {
            var error = Marshal.GetLastWin32Error();
            if (error != value)
                throw new Win32Exception(error);
        }
        public static void ThrowIfLastErrorAreNot(params int[] values)
        {
            var error = Marshal.GetLastWin32Error();
            if (!values.Contains(error))
                throw new Win32Exception(error);
        }

        public static void ThrowIfLastErrorIsNotInsufficientBuffer()
        {
            var error = Marshal.GetLastWin32Error();
            if (error != ERROR_INSUFFICIENT_BUFFER)
                throw new Win32Exception(error);
        }
        public static void ThrowIfLastErrorIsNotInvalidFormName()
        {
            var error = Marshal.GetLastWin32Error();
            if (error != ERROR_INVALID_FORM_NAME)
                throw new Win32Exception(error);
        }

        public static void ThrowLastError()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static Exception ArgumentOutOfRange(string name)
        {
            return new ArgumentOutOfRangeException(name);
        }
        public static Exception ArgumentNull(string name)
        {
            return new ArgumentNullException(name);
        }
        public static Exception Argument(string name, string message = null)
        {
            return new ArgumentException(message, name);
        }
    }
}