using FT.Client.Extensions;
using FT.Client.ViewModels.Common;
using FT.Core.Services;
using FT.Core.Services.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Interop;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace FT.Client.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProcessInteractorService _processInteractorService;
        private readonly ICacheService _cacheService;
        private readonly BackgroundWorker _backgroundWorker;

        private bool _is4By3Game;
        private bool _isAutoWidth = true;
        private AspectRatioModel _aspectRatioModel;

        public int TopLevelCount { get { return WindowInformations.Count; } }

        public string SelectedGameTitle { get { return SelectedWindowInformation?.WindowInformation.Title ?? "Please select a game..."; } }

        public bool IsStayOnTop { get; set; } = true;

        public bool RemoveTitleBar { get; set; }

        public bool Is4By3Game
        {
            get { return _is4By3Game; }
            set 
            { 
                SetProperty(ref _is4By3Game, value);
                RaisePropertyChanged(nameof(Is4By3Game));
            }
        }

        public bool IsAutoWidth
        {
            get { return _isAutoWidth; }
            set
            {
                SetProperty(ref _isAutoWidth, value);
                RaisePropertyChanged(nameof(IsAutoWidth));
                RaisePropertyChanged(nameof(IsForcedWidth));
            }
        }


        public bool IsForcedWidth { get { return !this.IsAutoWidth; } }

        public int ForcedWidthValue { get; set; }

        public ImageSource SelectedGameIcon { get { return SelectedWindowInformation?.IconImage; } }

        public WindowInformationViewModel SelectedWindowInformation { get; set; }

        public ObservableCollection<WindowInformationViewModel> WindowInformations { get; set; } = new ObservableCollection<WindowInformationViewModel>();

        public DelegateCommand OnRefreshCommand { get; }

        public DelegateCommand OnFullscreenizeCommand { get; }

        public string AspectRatioInformation 
        { 
            get 
            {
                if(_aspectRatioModel == null)
                    return string.Empty;

                return $"Actual Monitor: {_aspectRatioModel.ActualMonitorWidth}x{_aspectRatioModel.ActualMonitorHeight}{Environment.NewLine}"
                     + $"Auto Calculation: {_aspectRatioModel.Width}x{_aspectRatioModel.Height}"; 
            } 
        }

        public MainViewModel(IEventAggregator eventAggregator, IProcessInteractorService processInteractorService, ICacheService cacheService)
        {
            _eventAggregator = eventAggregator;
            _processInteractorService = processInteractorService;
            _cacheService = cacheService;
            OnRefreshCommand = new DelegateCommand(OnRefresh);
            OnFullscreenizeCommand = new DelegateCommand(OnFullscreenize);

            _eventAggregator.GetEvent<Events.OnGameSelectedEvent>().Subscribe(OnGameSelected);

            _aspectRatioModel = _processInteractorService.Get4x3AspectRatioOfScreen(Screen.PrimaryScreen, new Core.Services.Models.DimensionsSettingsModel() { AutoCalculate = true });
            ForcedWidthValue = _aspectRatioModel.Width;
            RaisePropertyChanged(nameof(AspectRatioInformation));
            RaisePropertyChanged(nameof(ForcedWidthValue));
            
            RefreshWindowInformations();

            _backgroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = false,
                WorkerReportsProgress = false
            };

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        private async void _backgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    var fullscreenizedGamesToRemove = new List<FullscreenizedGameModel>();
                    var availableProcess = _processInteractorService.GetActiveWindows();

                    //get terminated process/game
                    foreach (var cached in _cacheService.FullscreenizedGameModels)
                    {
                        if (!availableProcess.Any(w => w.Pointer == cached.Game.Pointer))
                        {
                            fullscreenizedGamesToRemove.Add(cached);
                        }
                    }

                    //remove game from cache (and close dark overlay as well)
                    foreach (var fullscreenizedGame in fullscreenizedGamesToRemove)
                    {
                        //be sure darkoverlay still exist
                        if (fullscreenizedGame.DarkOverlay != null && availableProcess.Any(w => w.Pointer == fullscreenizedGame.DarkOverlay.Pointer))
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(() => 
                            {
                                var darkOverlay = (Views.Common.DarkOverlay)(HwndSource.FromHwnd(fullscreenizedGame.DarkOverlay.Pointer).RootVisual);
                                darkOverlay.Close();
                            });
                        }
                        _cacheService.FullscreenizedGameModels.Remove(fullscreenizedGame);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"VerifyProcess error: {ex.Message}");
                }
            }
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

        private void OnFullscreenize()
        {
            try
            {
                //verify if there is a process selected
                if (SelectedWindowInformation == null)
                {
                    MessageBox.Show("Please select a game first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //verify that the force width is not zero for the 4x3 option
                if (Is4By3Game && IsForcedWidth && ForcedWidthValue <= 0)
                {
                    MessageBox.Show(@"The ""Forced Width"" should be above 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //get the selected process and validate that it does still exist
                var window = _cacheService.WindowInformations.FirstOrDefault(w => w.Index == SelectedWindowInformation.WindowInformation.Index);
                var windows = _processInteractorService.GetActiveWindows();
                if (!windows.Any(w => w.Pointer == window?.Pointer))
                {
                    MessageBox.Show("The process does no longer exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //get cached process model or instanciate (for cache)
                var cachedProcessModel = _cacheService.FullscreenizedGameModels.FirstOrDefault(fpm => fpm.Game.Pointer == window.Pointer);
                if (cachedProcessModel == null)
                {
                    cachedProcessModel = new FullscreenizedGameModel();
                }

                //Apply a dark overlay for 4:3 game?
                if (Is4By3Game)
                {
                    //instanciate the overlay form (or get existing one from cache)
                    Views.Common.DarkOverlay darkOverlay = cachedProcessModel.DarkOverlay != null ?
                                                    (Views.Common.DarkOverlay)(HwndSource.FromHwnd(cachedProcessModel.DarkOverlay.Pointer).RootVisual) :
                                                    new Views.Common.DarkOverlay();

                    //if the cached dark overlay was forcely closed, we will create a new one
                    if (darkOverlay == null)
                    {
                        darkOverlay = new Views.Common.DarkOverlay();
                    }

                    darkOverlay.Show();

                    //created window information
                    var darkWindow = new WindowInformation()
                    {
                        Index = window.Index,
                        Pointer = new WindowInteropHelper(darkOverlay).Handle,
                        Title = "Dark Overlay"
                    };


                    //display the overlay
                    _processInteractorService.SetBorderlessFullscreen(new Core.Services.Parameters.SetBorderlessFullscreenParameter()
                    {
                        Window = darkWindow,
                        IsStayOnTop = IsStayOnTop
                    });

                    //set to the cache
                    cachedProcessModel.DarkOverlay = darkWindow;
                }

                //apply borderless fullscreen
                _processInteractorService.SetBorderlessFullscreen(new Core.Services.Parameters.SetBorderlessFullscreenParameter()
                {
                    Window = window,
                    IsStayOnTop = IsStayOnTop,
                    Is4x3 = Is4By3Game,
                    RemoveTitleBar = RemoveTitleBar,
                    DimensionSettingsFor4x3AspectRatio = new DimensionsSettingsModel()
                    {
                        AutoCalculate = IsAutoWidth,
                        ForcedWidth = ForcedWidthValue
                    }
                });

                //set to the cache object
                cachedProcessModel.Game = window;

                //add to cache
                _cacheService.AddOrUpdateFullscreenizedGame(cachedProcessModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
