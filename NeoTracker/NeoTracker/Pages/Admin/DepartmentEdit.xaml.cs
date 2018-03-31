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

namespace NeoTracker.Pages.Admin
{
    /// <summary>
    /// Interaction logic for DepartmentEdit.xaml
    /// </summary>
    public partial class DepartmentEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private DepartmentViewModel vm = new DepartmentViewModel();

        public DepartmentEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(DeleteButton, true, "Delete", null, null);
            btn.SetButton(CancelButton, true, "Cancel", null, null);

            btn.SetButton(AddUserButton, true, "Create", "Add users", "Add users to department");
            btn.SetButton(ClearHeadOfDepartmentCb, false, "Reset", null, null);
        }

        private async Task ApplyButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            await vm.Save();
            App.nav.GoBack(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelEdit();
            App.nav.GoBack(this);
        }

        private async Task DeleteButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            await vm.Delete();
            App.nav.GoBack(this);
        }

        private async Task AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            await vm.AddUsers();
        }
        private void AddOperationButton_Click(object sender, RoutedEventArgs e)
        {

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
            vm = App.vm.Department;
            vm.BeginEdit();
            App.nav.SetLastUri("/Pages/Admin/DepartmentEdit.xaml");

        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            App.vm.Department = null;
        }

        private void ClearHeadOfDepartmentCb_Click(object sender, RoutedEventArgs e)
        {
            vm.HeadOfDepartment = null;
        }
    }
}
