using FT.Client.Extensions;
using FT.Client.ViewModels.Common;
using FT.Core.Services;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace FT.Client.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProcessInteractorService _processInteractorService;
        private readonly ICacheService _cacheService;

        public int TopLevelCount { get { return WindowInformations.Count; } }
        public ObservableCollection<WindowInformationViewModel> WindowInformations { get; set; } = new ObservableCollection<WindowInformationViewModel>();

        public MainViewModel(IEventAggregator eventAggregator, IProcessInteractorService processInteractorService, ICacheService cacheService)
        {
            _eventAggregator = eventAggregator;
            _processInteractorService = processInteractorService;
            _cacheService = cacheService;

            RefreshWindowInformations();
        }

        public void RefreshWindowInformations()
        {
            var windows = _processInteractorService.GetActiveWindows();
            _cacheService.SetCachedWindowInformations(windows);

            WindowInformations.Clear();
            foreach (var window in windows)
            {
                WindowInformations.Add(new WindowInformationViewModel(_eventAggregator)
                {
                    IconImage = window.Icon.ToImageSource(),
                    WindowInformation = window
                });
            }

            RaisePropertyChanged(nameof(WindowInformations));
        }
    }
}
