using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FT.Core.Services.Models;
using FT.Core.Services.Parameters;
using Windows.Foundation;
using static FT.Core.Constants;

namespace FT.Core.Services
{
    public class ProcessInteractorService : IProcessInteractorService
    {

        #region user32.dll imports

        /// <summary>
        /// Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an application-defined callback function. 
        /// EnumWindows continues until the last top-level window is enumerated or the callback function returns FALSE.
        /// </summary>
        /// <param name="enumProc"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        /// <summary>
        /// Delegate to filter which windows to include.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer. 
        /// If the specified window is a control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another application.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="strText"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        /// <summary>
        /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). 
        /// If the specified window is a control, the function retrieves the length of the text within the control. However, GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// Sends the specified message to a window or windows. 
        /// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// 
        /// To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. 
        /// To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="message"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, uint message, uint wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern uint GetClassLongPtr(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Copies the specified icon from another module to the current module.
        /// </summary>
        /// <param name="hcur"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CopyIcon(IntPtr hcur);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        /// <summary>
        /// Calculates the required size of the window rectangle, based on the desired client-rectangle size. 
        /// The window rectangle can then be passed to the CreateWindow function to create a window whose client area is the desired size.
        /// 
        /// To specify an extended window style, use the AdjustWindowRectEx function.
        /// </summary>
        /// <param name="lpRect"></param>
        /// <param name="dwStyle"></param>
        /// <param name="bMenu"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool AdjustWindowRect(Rect lpRect, uint dwStyle, bool bMenu);

        /// <summary>
        /// Retrieves information about the specified window. The function also retrieves the 32-bit (DWORD) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. 
        /// For a child window, they are relative to the upper-left corner of the parent window's client area.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="redraw"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. 
        /// Keyboard input is directed to the window, and various visual cues are changed for the user. 
        /// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        [DllImport("user32")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        #endregion

        public List<WindowInformation> GetActiveWindows()
        {
            return FindWindows();
        }

        public void SetBorderlessFullscreen(SetBorderlessFullscreenParameter parameter)
        {
            //get rect of the primary screen
            //todo -> let the user select the monitor
            Rect rect = new Rect(
                x: 0,
                y: 0,
                width: Screen.PrimaryScreen.Bounds.Width,
                height: Screen.PrimaryScreen.Bounds.Height
            );

            if (parameter.Is4x3 && parameter.DimensionSettingsFor4x3AspectRatio != null)
            {
                var convertedRatio = Get4x3AspectRatioOfScreen(Screen.PrimaryScreen, parameter.DimensionSettingsFor4x3AspectRatio);
                rect.X = convertedRatio.OffsetOfX;
                rect.Width = convertedRatio.Width;
            }

            //set new style of window
            SetWindowLong(parameter.Window.Pointer, (int)WindowLongFlag.GWL_STYLE, (uint)(WindowStyle.WS_POPUP | WindowStyle.WS_VISIBLE));

            //set new extended style of window (completly remove borders)
            var currentExtendedStyle = GetWindowLong(parameter.Window.Pointer, (int)WindowLongFlag.GWL_EXSTYLE);
            SetWindowLong(parameter.Window.Pointer, (int)WindowLongFlag.GWL_EXSTYLE, currentExtendedStyle
                &= ~((uint)WindowExtendedStyle.WS_EX_DLGMODALFRAME | (uint)WindowExtendedStyle.WS_EX_CLIENTEDGE | (uint)WindowExtendedStyle.WS_EX_STATICEDGE));

            //Adjust window for the new style
            AdjustWindowRect(rect, GetWindowLong(parameter.Window.Pointer, (int)WindowLongFlag.GWL_STYLE), false);

            //add topmost to the extended style
            if (parameter.IsStayOnTop)
            {
                SetWindowLong(parameter.Window.Pointer, (int)WindowLongFlag.GWL_EXSTYLE, (uint)(GetWindowLong(parameter.Window.Pointer, (int)WindowLongFlag.GWL_EXSTYLE) | WindowExtendedStyle.WS_EX_TOPMOST));
            }

            //fit window into primary monitor position
            MoveWindow(parameter.Window.Pointer, (int)rect.Left, (int)rect.Top, (int)rect.Right - (int)rect.Left, (int)rect.Bottom - (int)rect.Top, true);

            //focus back on window (note: required to do "SetForegroundWindow" if the dark overlay is used in order for the game to be on top of it)
            ShowWindow(parameter.Window.Pointer, ShowWindowParameter.SW_SHOW);
            SetForegroundWindow(parameter.Window.Pointer);
        }

        /// <summary>
        /// Find all top level windows
        /// </summary>
        /// <returns></returns>
        private List<WindowInformation> FindWindows()
        {
            IntPtr found = IntPtr.Zero;
            List<WindowInformation> windows = new List<WindowInformation>();

            EnumWindows(delegate (IntPtr hWnd, IntPtr param)
            {
                if (IsWindowVisible(hWnd))
                {
                    string title = GetWindowText(hWnd);

                    if (!string.IsNullOrEmpty(title))
                    {
                        //retreive app icon
                        var hicon = (IntPtr)SendMessage(hWnd, WindowMessage.WM_GETICON, GetIconParameter.ICON_BIG, IntPtr.Zero);

                        //if big icon not found, try the other format
                        if (hicon == IntPtr.Zero)
                        {
                            hicon = (IntPtr)GetClassLongPtr(hWnd, GetClassLongParameter.GCL_HICON);
                        }

                        //if still not found, try to get the small one
                        if (hicon == IntPtr.Zero)
                        {
                            hicon = (IntPtr)GetClassLongPtr(hWnd, GetClassLongParameter.GCL_HICONSM);
                        }

                        //still not found, well null!
                        var icon = hicon != IntPtr.Zero ? Icon.FromHandle(hicon) : (Icon)null;

                        //retreive process id
                        GetWindowThreadProcessId(hWnd, out var processId);

                        windows.Add(new WindowInformation()
                        {
                            Title = GetWindowText(hWnd),
                            Icon = icon,
                            ProcessId = processId,
                            Pointer = hWnd,
                            Index = windows.Count
                        });
                    }
                }

                // return true here so that we iterate all windows
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary>
        /// Get the text for the window pointed to by hWnd
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }

        /// <summary>
        /// Get 4:3 aspect ratio based on the monitor or via a forced setting
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private AspectRatioModel Get4x3AspectRatioOfScreen(Screen screen, DimensionsSettingsModel settings)
        {
            var result = new AspectRatioModel();

            result.WidthRatio = 4;
            result.HeightRatio = 3;

            result.Width = settings.AutoCalculate ? (screen.Bounds.Height / 3) * 4 : settings.ForcedWidth;
            result.Height = screen.Bounds.Height;
            result.OffsetOfX = (screen.Bounds.Width - result.Width) / 2;

            return result;
        }
    }
}
