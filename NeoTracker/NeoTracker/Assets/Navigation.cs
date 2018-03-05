using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.Models;
using NeoTracker.Pages;
using NeoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NeoTracker.Assets
{

    public class Navigation
    {
        private List<string> LastUri = new List<string>();
        private ModernFrame frame;

        //use in mainWindow
        public void InitNavigation()
        {
            frame = GetDescendantFromName(Application.Current.MainWindow, "ContentFrame") as ModernFrame;
        }

        public void GoBack()
        {
            LastUri.RemoveAt(LastUri.Count - 1);
            NavigationCommands.GoToPage.Execute(LastUri.Last(), frame);

        }
        public void SetLastUri(string uri)
        {
            LastUri.Add(uri);
        }
        public void NavigateTo(string uri)
        {
            try
            {
                NavigationCommands.GoToPage.Execute(uri, frame);
            }
            catch (Exception error)
            {
                ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            return null;
        }
    }
}
