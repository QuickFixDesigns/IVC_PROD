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
    public partial class ProjectCreate : UserControl
    {
        private Buttons btn = new Buttons();

        public ProjectCreate()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(CancelButton, true, "Cancel", null, null);

            btn.SetButton(RemoveFilterBtn, false, "Reset", "Filter", "Reset filter");

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(App.vm.Project.Orders);
            view.Filter = i => UserFilter(i);

            ListView.ItemsSource = view;
        }
        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(SearchBox.Text))
            {
                return true;
            }
            else
            {
                var order = item as OrderViewModel;
                return Utilities.Contains(order.Code, SearchBox.Text) || Utilities.Contains(order.Po, SearchBox.Text) || Utilities.Contains(order.Client, SearchBox.Text);
            }
        }
        private async void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                await App.vm.Project.Create((OrderViewModel)ListView.SelectedItem);
                App.nav.NavigateTo("/Pages/ProjectEdit.xaml", this);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Project.CancelEdit();
            App.vm.Project = null;
            App.nav.GoBack(this);
        }
        private void RemoveFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
            ListView.SelectedIndex = -1;
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.vm.Project != null)
            {
                App.nav.SetLastUri("/Pages/ProjectList.xaml");
                App.vm.Project.BeginEdit();
            }
        }
    }
}
