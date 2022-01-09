using FT.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Core.Services
{
    public class CacheService : ICacheService
    {
        private List<WindowInformation> _windowInformations;

        private List<FullscreenizedGameModel> _fullscreenizedGameModels;

        public List<WindowInformation> WindowInformations => _windowInformations;

        public List<FullscreenizedGameModel> FullscreenizedGameModels => _fullscreenizedGameModels;

        public CacheService()
        {
            _windowInformations = new List<WindowInformation>();
            _fullscreenizedGameModels = new List<FullscreenizedGameModel>();
        }

        public void SetCachedWindowInformations(List<WindowInformation> windowInformations)
        {
            _windowInformations = windowInformations;
        }

        public void AddOrUpdateFullscreenizedGame(FullscreenizedGameModel model)
        {
            var existing = _fullscreenizedGameModels.FirstOrDefault(m => m.Game.Pointer == model.Game.Pointer);
            if (existing != null)
            {
                existing = model;
            }
            else
            {
                _fullscreenizedGameModels.Add(model);
            }
        }
    }
}
