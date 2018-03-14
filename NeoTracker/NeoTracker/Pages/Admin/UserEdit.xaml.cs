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
            btn.SetButton(ApplyButton, true, "Apply");
            btn.SetButton(AddDepartmentButton, true, "Create");
            btn.SetButton(DeleteButton, true, "Delete");
            btn.SetButton(CancelButton, true, "Cancel");
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.User.Save();
            App.nav.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.User.CancelEdit();
            App.nav.GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.User.Delete();
            App.nav.GoBack();
        }
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            vm.AddDepartments();
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                vm.RemoveDepartment(((DepartmentViewModel)ListView.SelectedItem));
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
            vm.LoadDepartments();
            util.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
