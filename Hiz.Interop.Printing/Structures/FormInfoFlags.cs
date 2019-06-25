using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    public enum FormInfoFlags : int
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

    /* 不能 添加/修改/删除 "内建" 纸张; 其它可以;
     * 
     * 添加纸张存于注册表的位置:
     * HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Forms\
     * Key = FormInfo1.pName; 纸张名称
     * Value = Byte[0x20] {
     *   int[0] = FormInfo1.SIZE.cx;
     *   int[1] = FormInfo1.Size.cy;
     *   
     *   int[2] = FormInfo1.ImageableArea.left;
     *   int[3] = FormInfo1.ImageableArea.top;
     *   int[4] = FormInfo1.ImageableArea.right;
     *   int[5] = FormInfo1.ImageableArea.bottom;
     *   int[6] = 绝对序号;
     *            EnumForms(): 根据序号从小到大输出 (内建优先);
     *            DeleteForm(): 删除某个纸张, 不会改变其它纸张序号;
     *            DeviceCapabilities(DcFields.Papers): 自定纸张输出的值(System.Drawing.Printing.RawKind) = 118 + 相对序号;
     *            例如:
     *            首先添加三张自定纸张: {
     *              "Custom1": 绝对序号 = 1; 相对序号 = 1; RawKind = 118 + 1;
     *              "Custom2": 绝对序号 = 2; 相对序号 = 2; RawKind = 118 + 2;
     *              "Custom3": 绝对序号 = 3; 相对序号 = 3; RawKind = 118 + 3;
     *              EnumForms() 输出结果: 内建纸张[1-118] + { "Custom1", "Custom2", "Custom3" };
     *            }
     *            然后删除纸张 "Custom2": {
     *              "Custom1": 绝对序号 = 1; 相对序号 = 1; RawKind = 118 + 1;
     *              "Custom3": 绝对序号 = 3; 相对序号 = 2; RawKind = 118 + 2;
     *              EnumForms() 输出结果: 内建纸张[1-118] + { "Custom1", "Custom3" };
     *            }
     *   int[7] = FormInfo1.Flags; // 自定纸张 = User/Printer;
     * }
     */
}
