using FirstFloor.ModernUI.Windows.Controls;
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
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Pages.Dialogs
{
    /// <summary>
    /// Interaction logic for ListOfChoices.xaml
    /// </summary>
    public partial class OrderDialog : ModernDialog
    {
        public OrderDialog(string title)
        {
            InitializeComponent();
            DataContext = App.vm.Orders;
            Title = title;

            App.vm.LoadOrders();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(App.vm.Orders);
            view.Filter = i => UserFilter(i);

            ListView.ItemsSource = view;

            Button okBtn = this.OkButton;
            okBtn.Click += (s, ee) => { UpdateProjectCode(); };
            this.Buttons = new Button[] { okBtn, this.CancelButton };
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
        private void UpdateProjectCode()
        {
            if (ListView.SelectedIndex != -1)
            {
                var order = (OrderViewModel)ListView.SelectedItem;
                App.vm.Project.Initialize(order.Code);
            }
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

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListView.ItemsSource).Refresh();
        }
    }
}
