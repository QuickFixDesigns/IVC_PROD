using FirstFloor.ModernUI.Windows.Controls;
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
    public partial class SelectDialog : ModernDialog
    {
        public SelectDialog(string title)
        {
            InitializeComponent();
            DataContext = App.vm;
            Title = title;

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            App.vm.SelectItemList.ForEach(x => x.IsSelected = SelectAllCheckBox.IsChecked.Value);
        }
    }
}
