using System.Collections.Generic;
using System.ComponentModel;

namespace Fairmas.PickupTracking.Shared.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _showErrorMessage;
        private string _errorMessage;
        private bool _isLoading;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ShowErrorMessage
        {
            get
            {
                return _showErrorMessage;
            }

            set
            {
                Set(ref _showErrorMessage, value, nameof(ShowErrorMessage));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                Set(ref _errorMessage, value, nameof(ErrorMessage));
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                Set(ref _isLoading, value, nameof(IsLoading));
            }
        }
    }
}
