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
    /// Interaction logic for ProjectItemList.xaml
    /// </summary>
    public partial class ProjectEventList : UserControl, IContent
    {
        private Utilities util = new Utilities();

        public ProjectEventList()
        {
            InitializeComponent();
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //NavigationWindow nav = Application.Current.MainWindow as NavigationWindow;
            //nav.Navigate(new System.Uri("/Pages/ProjectEventEdit.xaml", UriKind.RelativeOrAbsolute));

            if (ListView.SelectedIndex != -1)
            {
                App.vm.ProjectEvent = ((ProjectEventViewModel)ListView.SelectedItem);
                App.nav.NavigateTo("/Pages/ProjectEventEdit.xaml", this);
                //BBCodeBlock bbBlock = new BBCodeBlock();
                //bbBlock.LinkNavigator.Navigate(new Uri(url, UriKind.Relative), this, NavigationHelper.FrameTop);
            }
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            util.AutoFitListView(GridListView);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
