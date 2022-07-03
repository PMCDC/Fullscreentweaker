using FT.Core.Services.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media;
using FT.Client.Events;

namespace FT.Client.ViewModels.Common
{
    public class WindowInformationViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Brush _backgroundHoverBrush  = (Brush)new BrushConverter().ConvertFromString("#141414");
        private readonly Brush _backgroundBrush  = (Brush)new BrushConverter().ConvertFromString("#070707");

        public Brush UserControlBackground { get; set; }

        public bool IsSelected { get; set; }

        public ImageSource IconImage { get; set; }

        public WindowInformation WindowInformation { get; set; }

        public DelegateCommand OnGameSelectedCommand { get; }

        public DelegateCommand OnMouseEnterCommand { get; }

        public DelegateCommand OnMouseLeaveCommand { get; }

        public WindowInformationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OnGameSelectedEvent>().Subscribe(GameSelected);

            OnGameSelectedCommand = new DelegateCommand(OnGameSelected);
            OnMouseEnterCommand = new DelegateCommand(OnMouseEnter);
            OnMouseLeaveCommand = new DelegateCommand(OnMouseLeave);

            UserControlBackground = _backgroundBrush;
        }

        public void OnGameSelected()
        {
            _eventAggregator.GetEvent<OnGameSelectedEvent>().Publish(WindowInformation);
        }

        public void GameSelected(WindowInformation windowInformation)
        {
            IsSelected = WindowInformation.ProcessId == windowInformation?.ProcessId;
            OnMouseLeave();
            RaisePropertyChanged(nameof(IsSelected));
        }

        public void OnMouseEnter()
        {
            UserControlBackground = _backgroundHoverBrush;

            RaisePropertyChanged(nameof(UserControlBackground));
        }

        public void OnMouseLeave()
        {
            if(!IsSelected)
                UserControlBackground = _backgroundBrush;

            RaisePropertyChanged(nameof(UserControlBackground));
        }
    }
}
