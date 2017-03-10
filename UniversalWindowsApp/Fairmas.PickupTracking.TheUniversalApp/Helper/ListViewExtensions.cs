using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Fairmas.PickupTracking.TheUniversalApp.Helper
{
    public static class ListViewExtensions
    {
        public static IEnumerable<ListViewItem> GetListViewItems(this ListView listView)
        {
            return FindChildrenOfType<ListViewItem>(listView);
        }

        private static IEnumerable<T> FindChildrenOfType<T>(DependencyObject dependencyObject)
            where T : class
        {
            foreach (var child in GetChildren(dependencyObject))
            {
                T castedChild = child as T;
                if (castedChild != null)
                {
                    yield return castedChild;
                }
                else
                {
                    foreach (var internalChild in FindChildrenOfType<T>(child))
                    {
                        yield return internalChild;
                    }
                }
            }
        }

        private static IEnumerable<DependencyObject> GetChildren(DependencyObject depedencyObject)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(depedencyObject);

            for (int i = 0; i < childCount; i++)
            {
                yield return VisualTreeHelper.GetChild(depedencyObject, i);
            }
        }
        
    }
}
