using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
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
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(SelectAllButton, true, "SelectAll", null, null);
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

        private async void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Item.MassUpdateOperations(SetStartDate.Value, SetEndDate.Value, SetCompleted.IsChecked);
            ListView.Items.Refresh();

            SetStartDate.Value = null;
            SetEndDate.Value = null;
            SetCompleted.IsChecked = false;
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
    }
}
