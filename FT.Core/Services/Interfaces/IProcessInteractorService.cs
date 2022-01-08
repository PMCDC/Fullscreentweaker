using FT.Core.Services.Models;
using FT.Core.Services.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core.Services
{
    public interface IProcessInteractorService
    {
        List<WindowInformation> GetActiveWindows();

        void SetBorderlessFullscreen(SetBorderlessFullscreenParameter parameter);
    }
}
