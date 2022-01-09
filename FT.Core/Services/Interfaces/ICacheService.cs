using FT.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core.Services
{
    public interface ICacheService
    {
        List<FullscreenizedGameModel> FullscreenizedGameModels { get; }

        List<WindowInformation> WindowInformations { get; }

        void SetCachedWindowInformations(List<WindowInformation> windowInformations);

        void AddOrUpdateFullscreenizedGame(FullscreenizedGameModel model);
    }
}
