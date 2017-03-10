using Fairmas.PickupTracking.Shared.Services;
using Fairmas.PickupTracking.Shared.ViewModels;
using System;
using System.Windows.Input;

namespace Fairmas.PickupTracking.Shared.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            var model = parameter as LoginViewModel;

            if (model == null)
                return false;

            var loginInProgress = model.LoggingIn;

            return !loginInProgress;
        }

        public async void Execute(Object parameter)
        {
            var viewModel = parameter as LoginViewModel;

            viewModel.ErrorMessage = "";
            viewModel.ShowErrorMessage = false;

            try
            {
                viewModel.ShowErrorMessage = false;
                viewModel.LoginFailed = false;
                viewModel.LoggingIn = true;

                var authenticated = await PickupConnector.Default.ConnectAsync(viewModel.Username, viewModel.Password);

                if (!authenticated)
                {
                    viewModel.ErrorMessage = 
                        "Sorry, we couldn't log you in. Maybe a typo in the username or password?" 
                        + Environment.NewLine + Environment.NewLine
                        + "Try again, we are waiting for you in the app!";

                    viewModel.ShowErrorMessage = true;
                    return;
                }

                viewModel.IsLoggedIn = true;

            }
            catch (Exception exception)
            {
                var error = $"Could not login at server:{Environment.NewLine}{exception.Message}";

                viewModel.ErrorMessage = error;
                viewModel.ShowErrorMessage = true;
            }
            finally
            {
                viewModel.LoggingIn = false;
            }
        }
    }
}
