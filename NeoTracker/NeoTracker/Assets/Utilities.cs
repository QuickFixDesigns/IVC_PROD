using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeoTracker.Content
{
    public static class Utilities
    {
        public static bool Contains(string toComp, string comp)
        {
            return (toComp.ToUpper() ?? "").Contains(comp.ToUpper());
        }
        public static bool GetDialogYesNo(string title, string content)
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
        public static void AutoFitListView(GridView gv)
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
        public static void AutoFitPanel(Panel p)
        {
            if (double.IsNaN(p.Width))
            {
                p.Width = p.ActualWidth;
            }
            p.Width = double.NaN;

        }
    }
}
