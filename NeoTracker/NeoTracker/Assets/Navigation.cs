using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
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
        private List<string> historic = new List<string>();

        public void GoBack(FrameworkElement source)
        {
            if (historic.Count > 1)
            {
                historic.RemoveAt(historic.Count - 1);
            }
            NavigateTo(historic.Last(), source);
        }
        public void SetLastUri(string uri)
        {
            if(historic.Count == 0 || historic.Last() != uri)
            {
                historic.Add(uri);
            }
        }
        public void NavigateTo(string uri, FrameworkElement source)
        {
            try
            {
                BBCodeBlock bbBlock = new BBCodeBlock();
                bbBlock.LinkNavigator.Navigate(new Uri(uri, UriKind.Relative), source, NavigationHelper.FrameTop);
            }
            catch (Exception error)
            {
                ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
            }
        }
    }
}
