using FT.Client.Extensions;
using FT.Client.ViewModels.Common;
using FT.Core.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace FT.Client.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProcessInteractorService _processInteractorService;
        private readonly ICacheService _cacheService;

        public int TopLevelCount { get { return WindowInformations.Count; } }

        public string SelectedGameTitle { get { return SelectedWindowInformation?.WindowInformation.Title ?? "Please select a game..."; } }

        public ImageSource SelectedGameIcon { get { return SelectedWindowInformation?.IconImage; } }

        public WindowInformationViewModel SelectedWindowInformation { get; set; }

        public ObservableCollection<WindowInformationViewModel> WindowInformations { get; set; } = new ObservableCollection<WindowInformationViewModel>();

        public DelegateCommand OnRefreshCommand { get; }

        public MainViewModel(IEventAggregator eventAggregator, IProcessInteractorService processInteractorService, ICacheService cacheService)
        {
            _eventAggregator = eventAggregator;
            _processInteractorService = processInteractorService;
            _cacheService = cacheService;
            OnRefreshCommand = new DelegateCommand(OnRefresh);

            _eventAggregator.GetEvent<Events.OnGameSelectedEvent>().Subscribe(OnGameSelected);
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

        private void OnRefresh()
        {
            RefreshWindowInformations();
            _eventAggregator.GetEvent<Events.OnGameSelectedEvent>().Publish(null); //will ensure we reset everything
        }

        private void OnGameSelected(Core.Services.Models.WindowInformation windowInformation)
        {
            SelectedWindowInformation = WindowInformations.FirstOrDefault(wi => wi.WindowInformation.ProcessId == windowInformation?.ProcessId);

            RaisePropertyChanged(nameof(SelectedGameTitle));
            RaisePropertyChanged(nameof(SelectedGameIcon));
            RaisePropertyChanged(nameof(TopLevelCount));
        }
    }
}
