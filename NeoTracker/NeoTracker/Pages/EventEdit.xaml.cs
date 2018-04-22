using FirstFloor.ModernUI.Windows;
using NeoTracker.Content;
using NeoTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Navigation;
using NeoTracker.DAL;
using System.Data.Entity;
using static NeoTracker.ViewModels.MainViewModel;
using NeoTracker.Pages.Dialogs;

namespace NeoTracker.Pages
{
    /// <summary>
    /// Interaction logic for DepartmentEdit.xaml
    /// </summary>
    public partial class EventEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private Utilities util = new Utilities();

        public EventEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(DeleteButton, true, "Delete", null, "Delete event");
            btn.SetButton(CancelButton, true, "Cancel", null, null);

            btn.SetButton(ClearDepartment, false, "Reset", null, null);
            btn.SetButton(ClearItem, false, "Reset", null, null);
            btn.SetButton(ClearStatus, false, "Reset", null, null);
        }

        private async void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Event.Save();
            App.vm.Event = null;
            App.nav.GoBack(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Event.CancelEdit();
            App.vm.Event = null;
            App.nav.GoBack(this);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Event.Delete();
            App.vm.Event = null;
            App.nav.GoBack(this);
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            App.nav.SetLastUri("/Pages/EventEdit.xaml");
            App.vm.Event.BeginEdit();
        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
        private void ClearDepartment_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Event.Department = null;
        }

        private void ClearItem_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Event.EventItem = null ;
        }

        private void ClearStatus_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Event.Status = null;
        }
    }
}
