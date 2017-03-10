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
    public class ShowMonthlyPickupCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(MonthlyViewPage));
        }
    }
}
