using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using NeoTracker.Content;
using NeoTracker.Models;
using NeoTracker.Pages.Dialogs;
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

namespace NeoTracker.Pages
{
    /// <summary>
    /// Interaction logic for ItemList.xaml
    /// </summary>
    public partial class OperationList : UserControl, IContent
    {
        private Utilities util = new Utilities();
        private Buttons btn = new Buttons();
        private bool SelectAll = true;

        public OperationList()
        {
            InitializeComponent();

            btn.SetButton(MassChangeOperationsDialog, true, "MassChange", "Mass changes", "Open dialog to set changes to all selected operations");
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Operation = ((OperationViewModel)ListView.SelectedItem);
                App.nav.NavigateTo("/Pages/OperationEdit.xaml", this);
            }
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            util.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectAll)
            {
                ListView.SelectAll();
                SelectAll = false;
            }
            else
            {
                ListView.UnselectAll();
                SelectAll = true;
            }
        }

        private async void MassChangeOperationsDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MassChangeOperations();
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                var ops = new List<Operation>();

                foreach (var o in ListView.SelectedItems)
                {
                    var vm = o as OperationViewModel;
                    ops.Add(vm.GetModel());
                }
                await App.vm.Item.MassUpdateOperations(ops, dialog.SetStartDate.Value, dialog.SetEndDate.Value, dialog.SetCompleted.IsChecked);
                ListView.Items.Refresh();

            }
        }
    }
}
