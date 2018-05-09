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
    public partial class EventList : UserControl
    {
        public EventList()
        {
            InitializeComponent();
        }
        private async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Event = ((EventViewModel)ListView.SelectedItem);
                App.vm.Event.BeginEdit();

                await App.vm.LoadChangeLog("Event", App.vm.Event.EventID);

                App.nav.SetLastUri("/Pages/EventEdit.xaml");
                App.nav.NavigateTo("/Pages/EventEdit.xaml", this);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.AutoFitListView(GridListView);
        }
    }
}
