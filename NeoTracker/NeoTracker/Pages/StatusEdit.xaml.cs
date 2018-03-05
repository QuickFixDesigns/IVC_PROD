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
    public partial class StatusEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private Utilities util = new Utilities();
        private StatusViewModel vm;

        public StatusEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply");
            btn.SetButton(DeleteButton, true, "Delete");
            btn.SetButton(CancelButton, true, "Cancel");
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Save();
            App.nav.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelEdit();
            App.nav.GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Delete();
            App.nav.GoBack();
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
            App.nav.SetLastUri("/Pages/StatusEdit.xaml");
            vm = App.vm.Status;
            vm.BeginEdit();
        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
