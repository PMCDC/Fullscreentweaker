using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FT.Core.Services.Models;

namespace FT.Core.Services.Models
{
    public class FullscreenizedGameModel
    {
        public WindowInformation Game { get; set; }

        public WindowInformation DarkOverlay{ get; set; }
    }
}
