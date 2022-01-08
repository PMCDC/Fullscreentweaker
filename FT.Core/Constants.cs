using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core
{
    public static partial class Constants
    {
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
            /// The window has a thin-line border.
            /// </summary>
            public const long WS_BORDER = 0x00800000L;

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
    }
}
