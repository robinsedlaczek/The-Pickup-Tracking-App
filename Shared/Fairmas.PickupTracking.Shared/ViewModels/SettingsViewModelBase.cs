using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public abstract class SettingsViewModelBase : ViewModelBase
    {
        private SettingsViewModel _settingsViewModel;

        public SettingsViewModel SettingsViewModel
        {
            get { return _settingsViewModel; }
            private set { Set(ref _settingsViewModel, value, nameof(SettingsViewModel)); }
        }

        public SettingsViewModelBase(SettingsViewModel settingsViewModel)
        {
            SettingsViewModel = settingsViewModel;
        }
    }
}
