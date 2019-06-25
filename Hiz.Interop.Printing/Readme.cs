using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hiz.Interop.Printing
{
    // Interop = Interoperability

    /* Documents and Printing
     * https://msdn.microsoft.com/en-us/library/windows/desktop/ff686798(v=vs.85).aspx
     * 
     * Print Spooler API
     * https://msdn.microsoft.com/en-us/library/windows/desktop/ff686807(v=vs.85).aspx
     */

    /* HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler {
     *   "Performance"
     *   "Security"
     * }
     * 
     * HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print {
     *   "Environments" {
     *     "Windows NT x86"
     *     "Windows x64" {
     *       "Drivers" {
     *         "Version-3"
     *         "Version-4"
     *       }
     *       "Print Processors"
     *     }
     *   }
     *   "Forms" // 保存所有自定纸张
     *   "Monitors" {
     *     "Local Port"
     *     "Microsoft Shared Fax Monitor"
     *     "Standard TCP/IP Port" {
     *       "Ports"
     *     }
     *     "USB Monitor"
     *     "WSD Port" {
     *       "OfflinePorts"
     *     }
     *   }
     *   "PendingUpgrades" {
     *     "Version-3"
     *   }
     *   "Printers" // 符号链接 // 本地打印设备
     *   "Providers" {
     *     "Internet Print Provider"
     *     "LanMan Print Services" {
     *       "PortNames"
     *       "Servers"
     *     }
     *   }
     * }
     * 
     * HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Print // 符号链接
     * HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print {
     *   "Cluster"
     *   "Connections"
     *   "OfflinePrinterExtensions"
     *   "PackageInstallation"
     *   "PackagesToAdd"
     *   "PrinterMigration"
     *   "Printers" { // 本地打印设备
     *     "{PrinterName}"...
     *   }
     *   "Providers" {
     *     "Client Side Rendering Print Provider" {
     *       "{...}" {
     *         "Printers" {
     *           "Connections" { // 已链接的远程打印设备
     *             ",,{MachineName},{PrinterName}"... {
     *               "DefaultDevMode"
     *               "RemotePrinterCache"
     *             }
     *           }
     *         }
     *       }
     *       "Servers" {
     *         "{MachineName}" { // 远程打印服务主机
     *           "Forms" // 远程主机上的所有纸张(包含内建以及自定)
     *           "Monitors" {
     *             "Client Side Port"
     *           }
     *           "Printers" { // 已链接的远程打印设备
     *             "{PrinterGuid}"...
     *           }
     *           "Providers"
     *         }
     *       }
     *     }
     *   }
     *   "V4 Connections"
     * }
     * 
     * HKEY_CURRENT_USER\Printers {
     *   "Connections" { // 已链接的远程打印设备
     *     ",,{MachineName},{PrinterName}"... {
     *       "DevMode"
     *       "GuidPrinter"
     *       "Server"
     *     }
     *   }
     *   "Defaults" // 默认打印设备
     *   "DevModePerUser"
     *   "DevModes2" {
     *     "{PrinterName}"... // 本地打印设备名称
     *     "\\{MachineName}\{PrinterName}"... // 远程打印设备名称
     *   }
     *   "Settings"
     * }
     * 
     * HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Devices
     * HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows NT\CurrentVersion\PrinterPorts
     */

    /* System.Drawing.Printing.PageSettings.CopyToHdevmode()/SetHdevmode();
     */

    /* UnmanagedType 字符串类型:
     * 
     * AnsiBStr: 有长度前缀的 Ansi
     * BStr: 有长度前缀的 Unicode
     * TBStr: 有长度前缀的字符串; 在 Windows 98 上为 Ansi; 在 Windows NT 上为 Unicode;
     * 
     * LPStr: 空终止的 Ansi
     * LPWStr: 空终止的 Unicode
     * LPTStr: 在 Windows 98 上为 Ansi; 在 Windows NT 上为 Unicode;
     * 
     * ByValTStr: 结构中出现的内联定长字符数组; 类型由 StructLayout(CharSet.Ansi/Unicode) 来确定;
     * 
     * 
     * C++ 字符串类型:
     * 
     * CHAR: char
     * WCHAR: wchar_t
     * TCHAR: char/wchar_t 取决于宏定义..
     * 
     * LPSTR: 即 char*, 指向以'\0'结尾的(单字节) Ansi 字符数组指针..
     * LPWSTR: 即 wchar_t*, 指向以'\0'结尾的(双字节) Unicode 字符数组指针..
     * LPTSTR: LPSTR/LPWSTR 取决于宏定义..
     * 
     * LPCSTR: 即 const char*
     * LPCWSTR: 即 const wchar_t*
     * LPCTSTR: LPCSTR/LPCWSTR 取决于宏定义..
     * 
     * 
     * MBCS(Ansi): Multi‐Byte Charater Set
     * 
     * public fixed char Name[32];
     * 
     * [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
     * public string Name;
     */

    /* Locales and Languages
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd318716(v=vs.85).aspx
     * 
     * LCID = Locale Identifier
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd373763(v=vs.85).aspx
     * 
     * LANGID = Language Identifier
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd318691(v=vs.85).aspx
     * Language Identifier Constants and Strings
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd318693(v=vs.85).aspx
     * 
     * WinNT.h
     * typedef DWORD  LCID; // 低16位: Language ID; 中04位: Sort ID; 高12位: Reserved;
     * typedef WORD   LANGID; // 低10位: Primary Language ID; 高06位: SubLanguage ID;
     * 
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd373908(v=vs.85).aspx
     * #define MAKELANGID(p, s) => ((WORD)s << 10) | (WORD)p;
     * #define PRIMARYLANGID(lgid) => (WORD)lgid & 0x3ff;
     * #define SUBLANGID(lgid) => (WORD)lgid >> 10;
     * 
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd319052(v=vs.85).aspx
     * #define MAKELCID(lgid, srtid) => ((DWORD)srtid << 16) | (DWORD)lgid;
     */

    /* Windows 10
     * 控制面板 > 硬件和声音 > 设备和打印机 > 打印服务器属性 > 安全
     * 
     * 常规权限: {
     *   打印
     *   管理打印机
     *   管理文档
     *   查看服务器
     *   管理服务器
     * }
     * 
     * 默认:
     * Everyone: 打印 | 查看服务器;
     * ALL APPLICATION PACKAGES: 打印 | 管理文档 | 查看服务器;
     * CREATOR OWNER: 管理文档;
     * Administrators: 全部;
     * Guests: 没有任何权限;
     * INTERACTIVE: 查看服务器 | 管理服务器;
     */

    /* Marshal.AllocHGlobal VS Marshal.AllocCoTaskMem; Marshal.SizeOf VS sizeof;
     * http://stackoverflow.com/questions/1887288/marshal-allochglobal-vs-marshal-alloccotaskmem-marshal-sizeof-vs-sizeof
     */

    /* Marshal.AllocHGlobal
     * https://msdn.microsoft.com/zh-cn/library/system.runtime.interopservices.marshal.allochglobal(v=vs.100).aspx
     * LocalAlloc
     * https://msdn.microsoft.com/en-us/library/windows/desktop/aa366723(v=vs.85).aspx
     * LocalFree
     * https://msdn.microsoft.com/en-us/library/windows/desktop/aa366730(v=vs.85).aspx
     * 
     * 
     * Marshal.AllocCoTaskMem
     * https://msdn.microsoft.com/zh-cn/library/system.runtime.interopservices.marshal.alloccotaskmem(v=vs.100).aspx
     * CoTaskMemAlloc
     * https://msdn.microsoft.com/en-us/library/windows/desktop/ms692727(v=vs.85).aspx
     * CoTaskMemFree
     * https://msdn.microsoft.com/en-us/library/windows/desktop/ms680722(v=vs.85).aspx
     * 
     * stackalloc
     * https://msdn.microsoft.com/zh-cn/library/cx9s2sy4(v=vs.100).aspx
     */

    /* 复制和锁定
     * https://msdn.microsoft.com/zh-cn/library/23acw07k(v=vs.100).aspx
     */

    /* http://www.programarts.com/cfree_en/wingdi_h.html
     */

    /* Paper Sizes
     * https://msdn.microsoft.com/en-us/library/windows/desktop/dd319099(v=vs.85).aspx
     * 
     * System.Drawing.Printing.PaperKind
     * System.Printing.PageMediaSizeName
     * 
     * |=============================================================================================|
     * | Type                                  |Value| Meaning                                       |
     * |=======================================|=====|===============================================|
     * | DMPAPER_LETTER                        |   1 | Letter 8 1/2 x 11 in                          | 信纸
     * | DMPAPER_LETTERSMALL                   |   2 | Letter Small 8 1/2 x 11 in                    | 小号信纸
     * | DMPAPER_TABLOID                       |   3 | Tabloid 11 x 17 in                            | Tabloid
     * | DMPAPER_LEDGER                        |   4 | Ledger 17 x 11 in                             | Ledger
     * | DMPAPER_LEGAL                         |   5 | Legal 8 1/2 x 14 in                           | 法律专用纸
     * | DMPAPER_STATEMENT                     |   6 | Statement 5 1/2 x 8 1/2 in                    | Statement
     * | DMPAPER_EXECUTIVE                     |   7 | Executive 7 1/4 x 10 1/2 in                   | Executive
     * | DMPAPER_A3                            |   8 | A3 297 x 420 mm                               | A3
     * | DMPAPER_A4                            |   9 | A4 210 x 297 mm                               | A4
     * | DMPAPER_A4SMALL                       |  10 | A4 Small 210 x 297 mm                         | A4 小号
     * | DMPAPER_A5                            |  11 | A5 148 x 210 mm                               | A5
     * | DMPAPER_B4                            |  12 | B4 (JIS) 250 x 354                            | B4 (JIS)
     * | DMPAPER_B5                            |  13 | B5 (JIS) 182 x 257 mm                         | B5 (JIS)
     * | DMPAPER_FOLIO                         |  14 | Folio 8 1/2 x 13 in                           | Folio
     * | DMPAPER_QUARTO                        |  15 | Quarto 215 x 275 mm                           | Quarto
     * | DMPAPER_10X14                         |  16 | 10x14 in                                      | 10x14
     * | DMPAPER_11X17                         |  17 | 11x17 in                                      | 11x17
     * | DMPAPER_NOTE                          |  18 | Note 8 1/2 x 11 in                            | 便签
     * | DMPAPER_ENV_9                         |  19 | Envelope #9 3 7/8 x 8 7/8                     | 信封 #9
     * | DMPAPER_ENV_10                        |  20 | Envelope #10 4 1/8 x 9 1/2                    | 信封 #10
     * | DMPAPER_ENV_11                        |  21 | Envelope #11 4 1/2 x 10 3/8                   | 信封 #11
     * | DMPAPER_ENV_12                        |  22 | Envelope #12 4 \276 x 11                      | 信封 #12
     * | DMPAPER_ENV_14                        |  23 | Envelope #14 5 x 11 1/2                       | 信封 #14
     * | DMPAPER_CSHEET                        |  24 | C size sheet                                  | C size sheet
     * | DMPAPER_DSHEET                        |  25 | D size sheet                                  | D size sheet
     * | DMPAPER_ESHEET                        |  26 | E size sheet                                  | E size sheet
     * | DMPAPER_ENV_DL                        |  27 | Envelope DL 110 x 220mm                       | 信封 DL
     * | DMPAPER_ENV_C5                        |  28 | Envelope C5 162 x 229 mm                      | 信封 C5
     * | DMPAPER_ENV_C3                        |  29 | Envelope C3  324 x 458 mm                     | 信封 C3
     * | DMPAPER_ENV_C4                        |  30 | Envelope C4  229 x 324 mm                     | 信封 C4
     * | DMPAPER_ENV_C6                        |  31 | Envelope C6  114 x 162 mm                     | 信封 C6
     * | DMPAPER_ENV_C65                       |  32 | Envelope C65 114 x 229 mm                     | 信封 C65
     * | DMPAPER_ENV_B4                        |  33 | Envelope B4  250 x 353 mm                     | 信封 B4
     * | DMPAPER_ENV_B5                        |  34 | Envelope B5  176 x 250 mm                     | 信封 B5
     * | DMPAPER_ENV_B6                        |  35 | Envelope B6  176 x 125 mm                     | 信封 B6
     * | DMPAPER_ENV_ITALY                     |  36 | Envelope 110 x 230 mm                         | 信封
     * | DMPAPER_ENV_MONARCH                   |  37 | Envelope Monarch 3.875 x 7.5 in               | 信封 Monarch
     * | DMPAPER_ENV_PERSONAL                  |  38 | 6 3/4 Envelope 3 5/8 x 6 1/2 in               | 6 3/4 信封
     * | DMPAPER_FANFOLD_US                    |  39 | US Std Fanfold 14 7/8 x 11 in                 | 美国标准 Fanfold
     * | DMPAPER_FANFOLD_STD_GERMAN            |  40 | German Std Fanfold 8 1/2 x 12 in              | 德国标准 Fanfold
     * | DMPAPER_FANFOLD_LGL_GERMAN            |  41 | German Legal Fanfold 8 1/2 x 13 in            | 德国法律专用纸 Fanfold
     * |=======================================|=====|===============================================|
     * | DMPAPER_ISO_B4                        |  42 | B4 (ISO) 250 x 353 mm                         | B4 (ISO)
     * | DMPAPER_JAPANESE_POSTCARD             |  43 | Japanese Postcard 100 x 148 mm                | 日式明信片
     * | DMPAPER_9X11                          |  44 | 9 x 11 in                                     | 9x11
     * | DMPAPER_10X11                         |  45 | 10 x 11 in                                    | 10x11
     * | DMPAPER_15X11                         |  46 | 15 x 11 in                                    | 15x11
     * | DMPAPER_ENV_INVITE                    |  47 | Envelope Invite 220 x 220 mm                  | 信封邀请函
     * | DMPAPER_RESERVED_48                   |  48 | RESERVED--DO NOT USE                          | Reserved48
     * | DMPAPER_RESERVED_49                   |  49 | RESERVED--DO NOT USE                          | Reserved49
     * | DMPAPER_LETTER_EXTRA                  |  50 | Letter Extra 9 \275 x 12 in                   | 特大信纸
     * | DMPAPER_LEGAL_EXTRA                   |  51 | Legal Extra 9 \275 x 15 in                    | 特大法律专用纸
     * | DMPAPER_TABLOID_EXTRA                 |  52 | Tabloid Extra 11.69 x 18 in                   | Tabloid 特大
     * | DMPAPER_A4_EXTRA                      |  53 | A4 Extra 9.27 x 12.69 in                      | A4 特大
     * | DMPAPER_LETTER_TRANSVERSE             |  54 | Letter Transverse 8 \275 x 11 in              | 信纸横向
     * | DMPAPER_A4_TRANSVERSE                 |  55 | A4 Transverse 210 x 297 mm                    | A4 横向
     * | DMPAPER_LETTER_EXTRA_TRANSVERSE       |  56 | Letter Extra Transverse 9\275 x 12 in         | 特大信纸 横向
     * | DMPAPER_A_PLUS                        |  57 | SuperA/SuperA/A4 227 x 356 mm                 | Super A
     * | DMPAPER_B_PLUS                        |  58 | SuperB/SuperB/A3 305 x 487 mm                 | Super B
     * | DMPAPER_LETTER_PLUS                   |  59 | Letter Plus 8.5 x 12.69 in                    | 信纸加大
     * | DMPAPER_A4_PLUS                       |  60 | A4 Plus 210 x 330 mm                          | A4 加大
     * | DMPAPER_A5_TRANSVERSE                 |  61 | A5 Transverse 148 x 210 mm                    | A5 横向
     * | DMPAPER_B5_TRANSVERSE                 |  62 | B5 (JIS) Transverse 182 x 257 mm              | B5 (JIS) 横向
     * | DMPAPER_A3_EXTRA                      |  63 | A3 Extra 322 x 445 mm                         | A3 特大
     * | DMPAPER_A5_EXTRA                      |  64 | A5 Extra 174 x 235 mm                         | A5 特大
     * | DMPAPER_B5_EXTRA                      |  65 | B5 (ISO) Extra 201 x 276 mm                   | B5 (ISO) 特大
     * | DMPAPER_A2                            |  66 | A2 420 x 594 mm                               | A2
     * | DMPAPER_A3_TRANSVERSE                 |  67 | A3 Transverse 297 x 420 mm                    | A3 横向
     * | DMPAPER_A3_EXTRA_TRANSVERSE           |  68 | A3 Extra Transverse 322 x 445 mm              | A3 特大横向
     * |=======================================|=====|===============================================|
     * | DMPAPER_DBL_JAPANESE_POSTCARD         |  69 | Japanese Double Postcard 200 x 148 mm         | 日式往返明信片
     * | DMPAPER_A6                            |  70 | A6 105 x 148 mm                               | A6
     * | DMPAPER_JENV_KAKU2                    |  71 | Japanese Envelope Kaku #2                     | 日式信封 Kaku #2
     * | DMPAPER_JENV_KAKU3                    |  72 | Japanese Envelope Kaku #3                     | 日式信封 Kaku #3
     * | DMPAPER_JENV_CHOU3                    |  73 | Japanese Envelope Chou #3                     | 日式信封 Chou #3
     * | DMPAPER_JENV_CHOU4                    |  74 | Japanese Envelope Chou #4                     | 日式信封 Chou #4
     * | DMPAPER_LETTER_ROTATED                |  75 | Letter Rotated 11 x 8 1/2 11 in               | 信纸旋转
     * | DMPAPER_A3_ROTATED                    |  76 | A3 Rotated 420 x 297 mm                       | A3 旋转
     * | DMPAPER_A4_ROTATED                    |  77 | A4 Rotated 297 x 210 mm                       | A4 旋转
     * | DMPAPER_A5_ROTATED                    |  78 | A5 Rotated 210 x 148 mm                       | A5 旋转
     * | DMPAPER_B4_JIS_ROTATED                |  79 | B4 (JIS) Rotated 364 x 257 mm                 | B4 (JIS) 旋转
     * | DMPAPER_B5_JIS_ROTATED                |  80 | B5 (JIS) Rotated 257 x 182 mm                 | B5 (JIS) 旋转
     * | DMPAPER_JAPANESE_POSTCARD_ROTATED     |  81 | Japanese Postcard Rotated 148 x 100 mm        | 日式明信片旋转
     * | DMPAPER_DBL_JAPANESE_POSTCARD_ROTATED |  82 | Double Japanese Postcard Rotated 148 x 200 mm | 双层日式明信片旋转
     * | DMPAPER_A6_ROTATED                    |  83 | A6 Rotated 148 x 105 mm                       | A6 旋转
     * | DMPAPER_JENV_KAKU2_ROTATED            |  84 | Japanese Envelope Kaku #2 Rotated             | 日式信封 Kaku #2 旋转
     * | DMPAPER_JENV_KAKU3_ROTATED            |  85 | Japanese Envelope Kaku #3 Rotated             | 日式信封 Kaku #3 旋转
     * | DMPAPER_JENV_CHOU3_ROTATED            |  86 | Japanese Envelope Chou #3 Rotated             | 日式信封 Chou #3 旋转
     * | DMPAPER_JENV_CHOU4_ROTATED            |  87 | Japanese Envelope Chou #4 Rotated             | 日式信封 Chou #4 旋转
     * | DMPAPER_B6_JIS                        |  88 | B6 (JIS) 128 x 182 mm                         | B6 (JIS)
     * | DMPAPER_B6_JIS_ROTATED                |  89 | B6 (JIS) Rotated 182 x 128 mm                 | B6 (JIS) 旋转
     * | DMPAPER_12X11                         |  90 | 12 x 11 in                                    | 12x11
     * | DMPAPER_JENV_YOU4                     |  91 | Japanese Envelope You #4                      | 日式信封 You #4
     * | DMPAPER_JENV_YOU4_ROTATED             |  92 | Japanese Envelope You #4 Rotated              | 日式信封 You #4 旋转
     * | DMPAPER_P16K                          |  93 | PRC 16K 146 x 215 mm                          | PRC 16K
     * | DMPAPER_P32K                          |  94 | PRC 32K 97 x 151 mm                           | PRC 32K
     * | DMPAPER_P32KBIG                       |  95 | PRC 32K(Big) 97 x 151 mm                      | PRC 32K(Big)
     * | DMPAPER_PENV_1                        |  96 | PRC Envelope #1 102 x 165 mm                  | PRC 信封 #1
     * | DMPAPER_PENV_2                        |  97 | PRC Envelope #2 102 x 176 mm                  | PRC 信封 #2
     * | DMPAPER_PENV_3                        |  98 | PRC Envelope #3 125 x 176 mm                  | PRC 信封 #3
     * | DMPAPER_PENV_4                        |  99 | PRC Envelope #4 110 x 208 mm                  | PRC 信封 #4
     * | DMPAPER_PENV_5                        | 100 | PRC Envelope #5 110 x 220 mm                  | PRC 信封 #5
     * | DMPAPER_PENV_6                        | 101 | PRC Envelope #6 120 x 230 mm                  | PRC 信封 #6
     * | DMPAPER_PENV_7                        | 102 | PRC Envelope #7 160 x 230 mm                  | PRC 信封 #7
     * | DMPAPER_PENV_8                        | 103 | PRC Envelope #8 120 x 309 mm                  | PRC 信封 #8
     * | DMPAPER_PENV_9                        | 104 | PRC Envelope #9 229 x 324 mm                  | PRC 信封 #9
     * | DMPAPER_PENV_10                       | 105 | PRC Envelope #10 324 x 458 mm                 | PRC 信封 #10
     * | DMPAPER_P16K_ROTATED                  | 106 | PRC 16K Rotated                               | PRC 16K 旋转
     * | DMPAPER_P32K_ROTATED                  | 107 | PRC 32K Rotated                               | PRC 32K 旋转
     * | DMPAPER_P32KBIG_ROTATED               | 108 | PRC 32K(Big) Rotated                          | PRC 32K(Big) 旋转
     * | DMPAPER_PENV_1_ROTATED                | 109 | PRC Envelope #1 Rotated 165 x 102 mm          | PRC 信封 #1 旋转
     * | DMPAPER_PENV_2_ROTATED                | 110 | PRC Envelope #2 Rotated 176 x 102 mm          | PRC 信封 #2 旋转
     * | DMPAPER_PENV_3_ROTATED                | 111 | PRC Envelope #3 Rotated 176 x 125 mm          | PRC 信封 #3 旋转
     * | DMPAPER_PENV_4_ROTATED                | 112 | PRC Envelope #4 Rotated 208 x 110 mm          | PRC 信封 #4 旋转
     * | DMPAPER_PENV_5_ROTATED                | 113 | PRC Envelope #5 Rotated 220 x 110 mm          | PRC 信封 #5 旋转
     * | DMPAPER_PENV_6_ROTATED                | 114 | PRC Envelope #6 Rotated 230 x 120 mm          | PRC 信封 #6 旋转
     * | DMPAPER_PENV_7_ROTATED                | 115 | PRC Envelope #7 Rotated 230 x 160 mm          | PRC 信封 #7 旋转
     * | DMPAPER_PENV_8_ROTATED                | 116 | PRC Envelope #8 Rotated 309 x 120 mm          | PRC 信封 #8 旋转
     * | DMPAPER_PENV_9_ROTATED                | 117 | PRC Envelope #9 Rotated 324 x 229 mm          | PRC 信封 #9 旋转
     * | DMPAPER_PENV_10_ROTATED               | 118 | PRC Envelope #10 Rotated 458 x 324 mm         | PRC 信封 #10 旋转
     * |=======================================|=====|===============================================|
     * | DMPAPER_USER                          | 256 |                                               |
     * |=============================================================================================|
     */

    /* OleView
     * C:\Program Files (x86)\Windows Kits\8.1\bin\x86\oleview.exe
     * C:\Program Files (x86)\Windows Kits\8.1\bin\x64\oleview.exe
     */
}