using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core.Services.Models
{
    public class WindowInformation
    {
        public string Title { get; set; }

        public IntPtr Pointer { get; set; }

        public int Index { get; set; }

        public Icon Icon { get; set; }
    }
}
