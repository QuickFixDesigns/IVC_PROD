using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.Content;
using NeoTracker.DAL;
using NeoTracker.Models;
using NeoTracker.ViewModels;
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

namespace NeoTracker.Pages
{
    /// <summary>
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class ProjectList : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private Utilities util = new Utilities();

        public ProjectList()
        {
            InitializeComponent();
            btn.SetButton(CreateButton, true, "Create", "Add project", "Add new project");
            util.AutoFitListView(GridListView);
        }
        private async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Project = ((ProjectViewModel)ListView.SelectedItem);
                await App.vm.Project.LoadEvents();
                await App.vm.Project.LoadItems();
                App.nav.NavigateTo("/Pages/ProjectEdit.xaml", this);
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Project = new ProjectViewModel();
            await App.vm.Project.LoadOrders();
            App.nav.NavigateTo("/Pages/ProjectCreate.xaml", this);
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
            App.nav.SetLastUri("/Pages/ProjectList.xaml");
            util.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
