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
    public partial class OperationEdit : UserControl
    {
        private Buttons btn = new Buttons();

        public OperationEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(DeleteButton, true, "Delete", null, null);
            btn.SetButton(CancelButton, true, "Cancel", null, null);
            btn.SetButton(ClearUser, false, "Reset", "User", null);
        }

        private async void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Operation.Save();
            App.vm.Operation = null;
            App.nav.GoBack(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Operation.CancelEdit();
            App.vm.Operation = null;
            App.nav.GoBack(this);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Operation.Delete();
            App.vm.Operation = null;
            App.nav.GoBack(this);
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.vm.Operation != null)
            {
                App.nav.SetLastUri("/Pages/OperationEdit.xaml");
                App.vm.Operation.BeginEdit();

                using (var context = new NeoTrackerContext())
                {
                    var data = await context.DepartmentUsers.Where(x => x.DepartmentID == App.vm.Operation.Department.DepartmentID).Include(x => x.User).Select(x => x.User).ToListAsync();
                    UserCb.ItemsSource = data;
                }
                await App.vm.LoadChangeLog("Operation", App.vm.Operation.OperationID);
            }
        }

        private void ClearUser_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Operation.User = null;
        }
    }
}
