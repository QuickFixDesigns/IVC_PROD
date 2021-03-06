﻿using FirstFloor.ModernUI.Windows;
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

namespace NeoTracker.Pages.Admin
{
    /// <summary>
    /// Interaction logic for ItemList.xaml
    /// </summary>
    public partial class DepartmentUserList : UserControl, IContent
    {
        public DepartmentUserList()
        {
            InitializeComponent();
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
            Utilities.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                await App.vm.Department.RemoveUser(((User)ListView.SelectedItem));
            }
        }
    }
}
