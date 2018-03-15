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
    public partial class ProjectCreate : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private Utilities util = new Utilities();
        private ProjectViewModel vm;

        public ProjectCreate()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply");
            btn.SetButton(CancelButton, true, "Cancel");
            btn.SetButton(ApplyfilterBtn, true, "Filter");
            btn.SetButton(RemoveFilterBtn, true, "Unfilter");

            App.vm.LoadOrders();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(App.vm.Orders);
            view.Filter = i => UserFilter(i);

            ListView.ItemsSource = view;
        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchBox.Text))
            {
                return true;
            }
            else
            {
                var order = item as OrderViewModel;
                return order.Code.Contains(SearchBox.Text) || order.Po.Contains(SearchBox.Text);
            }
        }


        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                var order = (OrderViewModel)ListView.SelectedItem;
                vm.Create(order.Code);
                App.nav.NavigateTo("/Pages/ProjectEdit.xaml", this);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelEdit();
            App.nav.GoBack(this);
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
            vm = App.vm.Project;
            vm.BeginEdit();
        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ApplyfilterBtn_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }

        private void RemoveFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }
    }
}
