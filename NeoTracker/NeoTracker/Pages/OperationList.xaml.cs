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
    public partial class OperationList : UserControl
    {
        private Buttons btn = new Buttons();
        private bool SelectAll = true;
        private string EmptyCbText = "No selection";

        public OperationList()
        {
            InitializeComponent();
            btn.SetButton(MassChangeOperationsDialog, true, "MassChange", "Edit operations", "Open dialog to set changes to all selected operations");
            btn.SetButton(ClearFilter, false, "Reset", "Filter", null);
            btn.SetButton(SelectAllButton, true, "SelectAll", "All/None", "Select / Unselect all operations");

            MassChangeOperationsDialog.Margin = new Thickness() { Left = 10, Right = 10 };
            SelectAllButton.Margin = new Thickness() { Left = 10, Right = 10 };
            ClearFilter.Margin = new Thickness() { Bottom=5 };
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Operation = ((OperationViewModel)ListView.SelectedItem);
                App.nav.NavigateTo("/Pages/OperationEdit.xaml", this);
            }
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }
        private async void RefreshListView()
        {
            if (App.vm.Item != null && App.vm.Item.Operations != null)
            {
                await App.vm.Item.LoadOperations();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(App.vm.Item.Operations);
                if (view != null)
                {
                    view.Filter = i => Filter(i);
                }
                ListView.ItemsSource = view;
                Utilities.AutoFitListView(GridListView);
            }
        }
        private bool Filter(object item)
        {
            var selection = App.vm.SelectItemList.Where(x => x.IsSelected).Select(x => x.Value);
            OperationViewModel op = item as OperationViewModel;

            if (!selection.Any())
            {
                cbDeparmtneFilter.Text = EmptyCbText;
                return true;
            }
            else
            {
                cbDeparmtneFilter.Text = string.Concat(App.vm.SelectItemList.Count(x => x.IsSelected).ToString(), " of ", App.vm.SelectItemList.Count());
                return selection.Contains(op.Department.DepartmentID) || op.Department == null;
            }
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
                RefreshListView();
            }
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            App.vm.SelectItemList.ForEach(x => x.IsSelected = false);
            cbDeparmtneFilter.Text = EmptyCbText;
            RefreshListView();
        }

        private void cbDeparmtneFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListView();
        }
    }
}
