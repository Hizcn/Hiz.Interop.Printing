using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    /* Standard Access Rights (WinNT.h)
     * https://msdn.microsoft.com/en-us/library/windows/desktop/aa379607(v=vs.85).aspx
     * 
     * 示例:
     * mscorlib.dll
     * System.Security.AccessControl.FileSystemRights/RegistryRights
     * 
     * System.Core.dll
     * System.IO.Pipes.PipeAccessRights
     * 
     * System.DirectoryServices.dll
     * System.DirectoryServices.ActiveDirectoryRights
     */
    [Flags]
    internal enum StandardAccessRights
    {
        None = 0,

        /// <summary>
        /// DELETE
        /// The right to delete the object.
        /// </summary>
        Delete = 0x00010000,
        /// <summary>
        /// READ_CONTROL (ReadPermissions)
        /// The right to read the information in the object's security descriptor, not including the information in the "System Access Control List" (SACL).
        /// </summary>
        ReadControl = 0x00020000,
        /// <summary>
        /// WRITE_DAC (ChangePermissions)
        /// The right to modify the "Discretionary Access Control List" (DACL) in the object's security descriptor.
        /// </summary>
        WriteDacl = 0x00040000,
        /// <summary>
        /// WRITE_OWNER (TakeOwnership)
        /// The right to change the owner in the object's security descriptor.
        /// </summary>
        WriteOwner = 0x00080000,

        /// <summary>
        /// SYNCHRONIZE
        /// The right to use the object for synchronization.
        /// This enables a thread to wait until the object is in the signaled state.
        /// Some object types do not support this access right.
        /// </summary>
        Synchronize = 0x00100000,

        /// <summary>
        /// STANDARD_RIGHTS_READ = READ_CONTROL;
        /// </summary>
        Read = ReadControl,
        /// <summary>
        /// STANDARD_RIGHTS_WRITE = READ_CONTROL;
        /// </summary>
        Write = ReadControl,
        /// <summary>
        /// STANDARD_RIGHTS_EXECUTE = READ_CONTROL;
        /// </summary>
        Execute = ReadControl,

        /// <summary>
        /// STANDARD_RIGHTS_REQUIRED = 0x000F0000 =  DELETE|READ_CONTROL|WRITE_DAC|WRITE_OWNER;
        /// </summary>
        Required = Delete | ReadControl | WriteDacl | WriteOwner,
        /// <summary>
        /// STANDARD_RIGHTS_ALL = 0x001F0000 = DELETE|READ_CONTROL|WRITE_DAC|WRITE_OWNER | Synchronize;
        /// </summary>
        All = Required | Synchronize,
    }
}
