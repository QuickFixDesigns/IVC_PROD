﻿using FirstFloor.ModernUI.Windows;
using NeoTracker.Content;
using NeoTracker.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NeoTracker.Pages
{
    /// <summary>
    /// Interaction logic for DepartmentEdit.xaml
    /// </summary>
    public partial class ProjectEdit : UserControl, IContent
    {
        private Buttons btn = new Buttons();

        public ProjectEdit()
        {
            InitializeComponent();
            btn.SetButton(ApplyButton, true, "Apply", null, null);
            btn.SetButton(DeleteButton, true, "Delete", null, null);
            btn.SetButton(CancelButton, true, "Cancel", null, null);

            btn.SetButton(AddEventButton, true, "Create", "Add event", "Add event to project");
        }

        private async void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Project.Save();
            App.vm.Project = null;
            App.nav.GoBack(this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.vm.Project.CancelEdit();
            App.vm.Project = null;
            App.nav.GoBack(this);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await App.vm.Project.Delete();
            App.vm.Project = null;
            App.nav.GoBack(this);
        }
        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedSource = new Uri("Pages/EventList.xaml", UriKind.Relative);

            App.vm.Event = new EventViewModel()
            {
                ProjectID = App.vm.Project.ProjectID,
                IsActive = true,
                Status = App.vm.Statuses.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).FirstOrDefault(),
                EventType = App.vm.EventTypes.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).FirstOrDefault(),
            };
            App.nav.NavigateTo("/Pages/EventEdit.xaml", this);
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }
        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }
        public async void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            App.nav.SetLastUri("/Pages/ProjectEdit.xaml");
            App.vm.Project.BeginEdit();

            await App.vm.Project.LoadEvents();
            await App.vm.Project.LoadItems();
            await App.vm.LoadChangeLog("Project", App.vm.Project.ProjectID);
        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}
