using FirstFloor.ModernUI.Windows;
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
    public partial class ItemList : UserControl
    {
        public ItemList()
        {
            InitializeComponent();
        }
        private async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Item = ((ItemViewModel)ListView.SelectedItem);
                App.vm.Item.BeginEdit();

                await App.vm.Item.LoadOperations();
                await App.vm.LoadChangeLog("Item", App.vm.Item.ItemID);

                App.nav.SetLastUri("/Pages/ItemEdit.xaml");
                App.nav.NavigateTo("/Pages/ItemEdit.xaml", this);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.AutoFitListView(GridListView);
        }
    }
}
