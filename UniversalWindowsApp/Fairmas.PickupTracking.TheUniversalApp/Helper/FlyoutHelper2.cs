using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Fairmas.PickupTracking.TheUniversalApp.Helper
{
    public static class FlyoutHelper2
    {
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(FlyoutHelper2), new PropertyMetadata(true, IsOpenChangedCallback));

        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.RegisterAttached("Parent", typeof(FrameworkElement), typeof(FlyoutHelper2), null);

        public static void SetIsOpen(DependencyObject element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static bool GetIsOpen(DependencyObject element)
        {
            return (bool)element.GetValue(IsVisibleProperty);
        }

        private static void IsOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flyoutBase = d as FlyoutBase;

            if (flyoutBase == null)
                return;

            if ((bool)e.NewValue)
            {
                flyoutBase.Closed += OnFlyoutClosed;
                flyoutBase.ShowAt(GetParent(d));
            }
            else
            {
                flyoutBase.Closed -= OnFlyoutClosed;
                flyoutBase.Hide();
            }
        }

        private static void OnFlyoutClosed(object sender, object e)
        {
            // When the flyout is closed, sets its IsOpen attached property to false.
            SetIsOpen(sender as DependencyObject, false);
        }

        public static void SetParent(DependencyObject element, FrameworkElement value)
        {
            element.SetValue(ParentProperty, value);
        }

        public static FrameworkElement GetParent(DependencyObject element)
        {
            return (FrameworkElement)element.GetValue(ParentProperty);
        }
    }
}
