using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    public enum PrintFormSource
    {
        /// <summary>
        /// Pname: FORM_USER
        /// If this bit flag is set, the form has been defined by the user.
        /// Forms with this flag set are defined in the registry.
        /// </summary>
        User = 0, // 用户

        /// <summary>
        /// Pname: FORM_BUILTIN
        /// If this bit-flag is set, the form is part of the spooler.
        /// Form definitions with this flag set do not appear in the registry.
        /// 
        /// Built-in forms cannot be modified, so this flag should not be set when the structure is passed to AddForm or SetForm.
        /// </summary>
        Builtin = 1, // 内建

        /// <summary>
        /// Pname: FORM_PRINTER
        /// If this bit flag is set, the form is associated with a certain printer, and its definition appears in the registry.
        /// </summary>
        Printer = 2, // 打印设备
    }
}
