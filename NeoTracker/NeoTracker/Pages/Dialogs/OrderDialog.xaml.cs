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
    public partial class OrderDialog : ModernDialog
    {
        public OrderDialog(string title)
        {
            InitializeComponent();

            App.vm.LoadOrders();
            DataContext = App.vm;
            Title = title;

            Button okBtn = this.OkButton;
            okBtn.Click += (s, ee) => { UpdateProjectCode(); };
            this.Buttons = new Button[] { okBtn, this.CancelButton };
        }

        private EventHandler SaveOrderCode()
        {
            throw new NotImplementedException();
        }

        private void UpdateProjectCode()
        {
            App.vm.Project.Code = "";
            Close();
        }
    }
}
