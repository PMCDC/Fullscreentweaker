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

        public List<WindowInformation> WindowInformations { get => _windowInformations; }

        public CacheService()
        {
            _windowInformations = new List<WindowInformation>();
        }

        public void SetCachedWindowInformations(List<WindowInformation> windowInformations)
        {
            _windowInformations = windowInformations;
        }
    }
}
