using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeoTracker.Content
{
    public class Utilities
    {
        public bool GetDialogYesNo(string title, string content)
        {
            var dlg = new ModernDialog()
            {
                Title = title,
                Content = content,
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.NoButton };
            dlg.ShowDialog();

            return dlg.DialogResult.HasValue && dlg.DialogResult.Value;
        }
        public void AutoFitListView(GridView gv)
        {
            foreach (GridViewColumn c in gv.Columns)
            {
                if (double.IsNaN(c.Width))
                {
                    c.Width = c.ActualWidth;
                }
                c.Width = double.NaN;
            }
        }
        public void AutoFitPanel(Panel p)
        {
            if (double.IsNaN(p.Width))
            {
                p.Width = p.ActualWidth;
            }
            p.Width = double.NaN;

        }
    }
}
