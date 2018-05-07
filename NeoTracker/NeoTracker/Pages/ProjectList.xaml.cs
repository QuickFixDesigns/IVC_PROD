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

        public ProjectList()
        {
            InitializeComponent();
            btn.SetButton(CreateButton, true, "Create", "Add project", "Add new project");
            btn.SetButton(ClearProjectTypeFilter, false, "Reset", null, null);
        }
        private bool ProjectFilter(object item)
        {
            if (string.IsNullOrEmpty(SearchBox.Text) && ProjecTypeFilter.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                var project = item as ProjectViewModel;
                return Utilities.Contains(project.Code, SearchBox.Text) || Utilities.Contains(project.Name, SearchBox.Text) || Utilities.Contains(project.PurchaseOrder, SearchBox.Text) || Utilities.Contains(project.Client, SearchBox.Text);
            }
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Project = ((ProjectViewModel)ListView.SelectedItem);
                App.nav.NavigateTo("/Pages/ProjectEdit.xaml", this);
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Project = new ProjectViewModel()
            {
                ProjectType = App.vm.ProjectTypes.OrderBy(x=>x.SortOrder).ThenBy(x=>x.Name).FirstOrDefault()
            };
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
            Utilities.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }

        private void ProjecTypeFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }

        private void ClearProjectTypeFilter_Click(object sender, RoutedEventArgs e)
        {
            ProjecTypeFilter.SelectedIndex=-1;
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.vm.Projects != null)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(App.vm.Projects);
                if (view != null)
                {
                    view.Filter = i => ProjectFilter(i);
                }

                ListView.ItemsSource = view;
            }
        }
    }
}
