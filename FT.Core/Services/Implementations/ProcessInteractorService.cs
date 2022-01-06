using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FT.Core.Services.Models;

namespace FT.Core.Services
{
    public class ProcessInteractorService : IProcessInteractorService
    {
        private const int WM_GETICON = 0x007F;
        private const int WM_SETICON = 0x80;
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;
        private const int GCL_HICON = -14;
        private const int GCL_HICONSM = -34;

        public List<WindowInformation> GetActiveWindows()
        {
            return FindWindows();
        }

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        // Delegate to filter which windows to include 
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int message, int wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        static extern uint GetClassLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        static extern IntPtr GetClassLong64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CopyIcon(IntPtr hcur);

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
                        var hicon = (IntPtr)SendMessage(hWnd, WM_GETICON, ICON_SMALL, IntPtr.Zero);

                        if (hicon == IntPtr.Zero)
                        {
                            hicon = GetClassLongPtr(hWnd, GCL_HICONSM);
                        }

                        if (hicon == IntPtr.Zero)
                        {
                            hicon = GetClassLongPtr(hWnd, GCL_HICON);
                        }

                        var icon = hicon != IntPtr.Zero ?  Icon.FromHandle(hicon) : (Icon)null;

                        windows.Add(new WindowInformation()
                        {
                            Title = GetWindowText(hWnd),
                            Icon = icon,
                            Pointer = hWnd
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
        /// 64 bit version maybe loses significant 64-bit specific information
        /// </summary>
        static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return new IntPtr((long)GetClassLong32(hWnd, nIndex));
            else
                return GetClassLong64(hWnd, nIndex);
        }
    }
}
