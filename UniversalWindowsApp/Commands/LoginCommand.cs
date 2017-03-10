using Fairmas.FairPayroll.PnL.OnlinePlanner;
using Fairmas.PickupTracking.TheUniversalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Fairmas.PickupTracking.TheUniversalApp.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            var loginModel = parameter as LoginViewModel;
            var baseAddress = "https://put-development.fairplanner.net/";
            var client = new WebApi(baseAddress, loginModel.Username, loginModel.Password);

            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(HotelListPage));
        }
    }
}
