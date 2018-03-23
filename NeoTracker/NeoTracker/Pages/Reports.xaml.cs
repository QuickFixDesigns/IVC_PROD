using ClosedXML.Excel;
using Microsoft.Win32;
using NeoTracker.Content;
using NeoTracker.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        Buttons buttons = new Buttons();

        public Reports()
        {  
            InitializeComponent();
            buttons.SetButton(DepartmentList, true, "Excel");
        }
        private async void DepartmentList_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NeoTrackerContext())
            {
                var data = await context.Departments.Select(x=>x).ToListAsync();

                ExportToExcel(data);
            }
        }

        private void ExportToExcel(IEnumerable<dynamic> data)
        {
            using (var excelFile = new XLWorkbook(XLEventTracking.Disabled))
            {
                using (var worksheet = excelFile.AddWorksheet("data"))
                {
                    worksheet.Cell(1, 1).InsertTable(data);
                    worksheet.Columns().AdjustToContents();
                }
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    Filter = "Excel Files|*.xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        excelFile.SaveAs(dialog.FileName);
                        Process.Start(dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        //App.vm.SendUserMsg(ex.Message.ToString());
                    }
                }
            }
        }
    }
}
