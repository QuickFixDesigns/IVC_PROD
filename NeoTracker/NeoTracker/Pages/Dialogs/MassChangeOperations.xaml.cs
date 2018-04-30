using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.Content;
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

namespace NeoTracker.Pages.Dialogs
{
    /// <summary>
    /// Interaction logic for QuestionDialog.xaml
    /// </summary>
    public partial class MassChangeOperations : ModernDialog
    {
        private Buttons btn = new Buttons();

        public MassChangeOperations()
        {
            InitializeComponent();
            Title = "Apply to all selected operations ?";
            Buttons = new Button[] { this.YesButton, this.NoButton };
        }
    }
}
