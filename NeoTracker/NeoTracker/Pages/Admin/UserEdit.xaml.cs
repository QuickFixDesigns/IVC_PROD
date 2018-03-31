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

namespace NeoTracker.Pages.Admin
{
    /// <summary>
    /// Interaction logic for DepartmentEdit.xaml
    /// </summary>
    public partial class UserEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private Utilities util = new Utilities();
        private UserViewModel vm;

        public UserEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(DeleteButton, true, "Delete", null, null);
            btn.SetButton(CancelButton, true, "Cancel", null, null);

            btn.SetButton(AddDepartmentButton, true, "Create", "Add deparment", "Add departments to user");
        }

        private async Task ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.User.Save();
            App.nav.GoBack(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.User.CancelEdit();
            App.nav.GoBack(this);
        }

        private async Task DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.User.Delete();
            App.nav.GoBack(this);
        }
        private async Task AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            await vm.AddDepartments();
        }
        private async Task ListView_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                await vm.RemoveDepartment(((DepartmentViewModel)ListView.SelectedItem));
            }
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
            App.nav.SetLastUri("/Pages/Admin/UserEdit.xaml");
            vm = App.vm.User;
            vm.BeginEdit();
            util.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            App.vm.User = null;
        }
    }
}
