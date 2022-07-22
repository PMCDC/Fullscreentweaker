using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core.Services.Models
{
    public class AspectRatioModel
    {
        public string DisplayTitle { get { return $"{WidthRatio}:{HeightRatio}"; } }
        public int WidthRatio { get; set; }
        public int HeightRatio { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int OffsetOfX { get; set; }

        public int ActualMonitorWidth { get; set; }
        public int ActualMonitorHeight { get; set; }
    }
}
