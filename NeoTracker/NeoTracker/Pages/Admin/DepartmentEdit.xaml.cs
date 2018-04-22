using FirstFloor.ModernUI.Windows;
using NeoTracker.Content;
using NeoTracker.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NeoTracker.Pages.Admin
{
    /// <summary>
    /// Interaction logic for DepartmentEdit.xaml
    /// </summary>
    public partial class DepartmentEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();

        public DepartmentEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(DeleteButton, true, "Delete", null, null);
            btn.SetButton(CancelButton, true, "Cancel", null, null);

            btn.SetButton(AddUserButton, true, "Create", "Add users", "Add users to department");
            btn.SetButton(AddOperationButton, true, "Create", "Add operation", "Add operation to department");

            btn.SetButton(ClearHeadOfDepartmentCb, false, "Reset", null, null);
        }

        private async void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Department.Save();
            App.vm.Department = null;
            App.nav.GoBack(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Department.CancelEdit();
            App.vm.Department = null;
            App.nav.GoBack(this);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Department.Delete();
            App.vm.Department = null;
            App.nav.GoBack(this);
        }

        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Department.AddUsers();
        }
        private void AddOperationButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.DepartmentOperation = new DepartmentOperationViewModel()
            {
                DepartmentID = App.vm.Department.DepartmentID,
                IsActive = true,
            };
            App.nav.NavigateTo("/Pages/Admin/DepartmentOperationEdit.xaml", this);
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            App.vm.Department.BeginEdit();
            App.nav.SetLastUri("/Pages/Admin/DepartmentEdit.xaml");

        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        private void ClearHeadOfDepartmentCb_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Department.HeadOfDepartment = null;
        }
    }
}
