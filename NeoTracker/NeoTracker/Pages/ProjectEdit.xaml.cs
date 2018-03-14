﻿using FirstFloor.ModernUI.Windows;
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
    public partial class ProjectEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();
        private Utilities util = new Utilities();
        private ProjectViewModel vm;

        public ProjectEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply");
            btn.SetButton(DeleteButton, true, "Delete");
            btn.SetButton(CancelButton, true, "Cancel");
            btn.SetButton(AddEventButton, true, "Create");
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Save();
            App.nav.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelEdit();
            App.nav.GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Delete();
            App.nav.GoBack();
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
            App.nav.SetLastUri("/Pages/ProjectEdit.xaml");
            vm = App.vm.Project;
            App.vm.Project.LoadItems();
            App.vm.Project.LoadEventsAsync();
            vm.BeginEdit();
        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedSource = new Uri("Pages/ProjectEventList.xaml", UriKind.Relative);

            App.vm.ProjectEvent = new ProjectEventViewModel()
            {
                Project = vm.GetModel()
            };
            App.nav.NavigateTo("/Pages/ProjectEventEdit.xaml");
        }
    }
}
