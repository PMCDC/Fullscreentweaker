using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core
{
    public static partial class Constants
    {
        public static class FullscreenTweaker 
        {
            public const string VERSION = "1.2.0";
        }

        public static class Worker 
        {
            /// <summary>
            /// Default interval between callbacks. Default 2 seconds (2000mm)
            /// </summary>
            public const int DEFAULT_INTERVAL = 2000;
        }

        public static class WindowStyle
        {
            /// <summary>
            /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
            /// </summary>
            public const long WS_OVERLAPPED = 0x00000000L;

            /// <summary>
            /// The window is a pop-up window. This style cannot be used with the WS_CHILD style.
            /// </summary>
            public const long WS_POPUP = 0x80000000L;

            /// <summary>
            /// The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.
            /// </summary>
            public const long WS_VISIBLE = 0x10000000L;

            /// <summary>
            /// The window has a title bar (includes the WS_BORDER style).
            /// </summary>
            public const long WS_CAPTION = 0x00C00000L;

            /// <summary>
            /// The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
            /// </summary>
            public const long WS_SYSMENU = 0x00080000L;

            /// <summary>
            /// The window has a sizing border. Same as the WS_SIZEBOX style.
            /// </summary>
            public const long WS_THICKFRAME = 0x00040000L;

            /// <summary>
            /// The window has a thin-line border.
            /// </summary>
            public const long WS_BORDER = 0x00800000L;

            /// <summary>
            /// The window is initially minimized. Same as the WS_ICONIC style.
            /// </summary>
            public const long WS_MINIMIZE = 0x20000000L;

            /// <summary>
            /// The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
            /// </summary>
            public const long WS_MAXIMIZEBOX = 0x00010000L;

            /// <summary>
            /// The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
            /// </summary>
            public const long WS_MINIMIZEBOX = 0x00020000L;
        }

        public static class WindowExtendedStyle
        {
            /// <summary>
            /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. 
            /// To add or remove this style, use the SetWindowPos function.
            /// </summary>
            public const long WS_EX_TOPMOST = 0x00000008L;

            /// <summary>
            /// The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
            /// </summary>
            public const long WS_EX_DLGMODALFRAME = 0x00000001L;

            /// <summary>
            /// The window has a border with a sunken edge.
            /// </summary>
            public const long WS_EX_CLIENTEDGE = 0x00000200L;

            /// <summary>
            /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
            /// </summary>
            public const long WS_EX_STATICEDGE = 0x00020000L;
        }

        public static class WindowLongFlag
        {
            /// <summary>
            /// Sets a new window style.
            /// </summary>
            public const long GWL_STYLE = -16;

            /// <summary>
            /// Sets a new extended window style.
            /// </summary>
            public const long GWL_EXSTYLE = -20;
        }

        public static class WindowMessage
        {
            /// <summary>
            /// Sent to a window to retrieve a handle to the large or small icon associated with a window. 
            /// The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
            /// </summary>
            public const int WM_GETICON = 0x007F;

            /// <summary>
            /// Sent to a window to retrieve a handle to the large or small icon associated with a window. 
            /// The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
            /// </summary>
            public const int WM_SETICON = 0x80;
        }

        public static class GetIconParameter
        {
            /// <summary>
            /// Retrieve the large icon for the window.
            /// </summary>
            public const int ICON_BIG = 1;

            /// <summary>
            /// Retrieve the small icon for the window.
            /// </summary>
            public const int ICON_SMALL = 0;

            /// <summary>
            /// Retrieves the small icon provided by the application. If the application does not provide one, the system uses the system-generated icon for that window.
            /// </summary>
            public const int ICON_SMALL2 = 2;
        }

        public static class GetClassLongParameter
        {
            /// <summary>
            /// Retrieves a handle to the icon associated with the class
            /// </summary>
            public const int GCL_HICON = -14;

            /// <summary>
            /// Retrieves a handle to the small icon associated with the class.
            /// </summary>
            public const int GCL_HICONSM = -34;
        }

        public static class ShowWindowParameter
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            public const int SW_HIDE = 0;

            /// <summary>
            /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when displaying the window for the first time.
            /// </summary>
            public const int SW_SHOWNORMAL = 1;

            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            public const int SW_SHOWMINIMIZED = 2;

            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>
            public const int SW_SHOWMAXIMIZED = 3;

            /// <summary>
            /// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
            /// </summary>
            public const int SW_SHOWNOACTIVATE = 4;

            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            public const int SW_SHOW = 5;

            /// <summary>
            /// Minimizes the specified window and activates the next top-level window in the Z order.
            /// </summary>
            public const int SW_MINIMIZE = 6;

            /// <summary>
            /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
            /// </summary>
            public const int SW_SHOWMINNOACTIVE = 7;

            /// <summary>
            /// Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.
            /// </summary>
            public const int SW_SHOWNA = 8;

            /// <summary>
            /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            public const int SW_RESTORE = 9;

            /// <summary>
            /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
            /// </summary>
            public const int SW_SHOWDEFAULT = 10;

            /// <summary>
            /// Minimizes a window, even if the thread that owns the window is not responding. 
            /// This flag should only be used when minimizing windows from a different thread.
            /// </summary>
            public const int SW_FORCEMINIMIZE = 11;
        }
    }
}
