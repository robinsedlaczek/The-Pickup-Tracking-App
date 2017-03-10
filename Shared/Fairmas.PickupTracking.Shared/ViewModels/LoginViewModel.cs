using Fairmas.PickupTracking.Shared.Commands;
using System.Windows.Input;

namespace Fairmas.PickupTracking.Shared.ViewModels
{

    public class LoginViewModel : ViewModelBase
    {
        private bool _loginFailed;
        private bool _loggingIn;
        private bool _isLoggedIn;

        public LoginViewModel()
        {
            Username = "robin.sedlaczek@live.de";
            Password = "Fairmas2017";
            LoginFailed = false;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool LoggingIn
        {
            get
            {
                return _loggingIn;
            }

            set
            {
                Set(ref _loggingIn, value, nameof(LoggingIn));
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new LoginCommand();
            }
        }

        public  bool LoginFailed
        {
            get
            {
                return _loginFailed;
            }

            internal set
            {
                Set(ref _loginFailed, value, nameof(LoginFailed));
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }

            set
            {
                Set(ref _isLoggedIn, value, nameof(IsLoggedIn));
            }
        }
    }
}
