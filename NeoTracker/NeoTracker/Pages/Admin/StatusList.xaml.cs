﻿using FirstFloor.ModernUI.Windows;
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

namespace NeoTracker.Pages.Admin
{
    /// <summary>
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class StatusList : UserControl, IContent
    {
        private Buttons btn = new Buttons();

        public StatusList()
        {
            InitializeComponent();
            btn.SetButton(CreateButton, true, "Create", "New status", "Create new status");
            Utilities.AutoFitListView(GridListView);
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.SelectedIndex != -1)
            {
                App.vm.Status = ((StatusViewModel)ListView.SelectedItem);
                App.nav.NavigateTo("/Pages/Admin/StatusEdit.xaml", this);
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Status = new StatusViewModel()
            {
                IsActive = true,
            };
            App.nav.NavigateTo("/Pages/Admin/StatusEdit.xaml", this);
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
            App.nav.SetLastUri("/Pages/Admin/StatusList.xaml");
            Utilities.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
