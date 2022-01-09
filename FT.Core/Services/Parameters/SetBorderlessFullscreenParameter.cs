using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FT.Core.Services.Models;

namespace FT.Core.Services.Parameters
{
    public class SetBorderlessFullscreenParameter
    {
        public WindowInformation Window { get; set; }

        public bool IsStayOnTop { get; set; }

        public bool Is4x3 { get; set; }

        public DimensionsSettingsModel DimensionSettingsFor4x3AspectRatio { get; set; }
    }
}
