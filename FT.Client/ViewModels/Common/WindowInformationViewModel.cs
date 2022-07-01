using FT.Core.Services.Models;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media;

namespace FT.Client.ViewModels.Common
{
    public class WindowInformationViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public string Name { get { return WindowInformation.Title; } }

        public ImageSource IconImage { get; set; }

        public WindowInformation WindowInformation { get; set; } 

        public WindowInformationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
