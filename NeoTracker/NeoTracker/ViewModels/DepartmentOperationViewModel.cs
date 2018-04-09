using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.DAL;
using NeoTracker.Pages.Dialogs;
using NeoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Models
{
    public class DepartmentOperationViewModel : ViewModelBase, IDataErrorInfo
    {
        public int DepartmentOperationID { get; set; }
        public int DepartmentID { get; set; }

        private int? _SortOrder;
        public int? SortOrder
        {
            get { return _SortOrder; }
            set { SetProperty(ref _SortOrder, value); }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        private decimal _OperationTime;
        public decimal OperationTime
        {
            get { return _OperationTime; }
            set { SetProperty(ref _OperationTime, value); }
        }
        //For database
        public DepartmentOperation GetModel()
        {
            return new DepartmentOperation()
            {
                DepartmentOperationID = DepartmentOperationID,
                DepartmentID = DepartmentID,
                SortOrder = SortOrder,
                Name = Name,
                OperationTime = OperationTime,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public async Task Save()
        {
            try
            {
                using (var context = new NeoTrackerContext())
                {
                    var data = GetModel();
                    if (DepartmentOperationID == 0)
                    {
                        context.DepartmentOperations.Add(data);
                    }
                    else
                    {
                        context.Entry(data).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
                }
                EndEdit();
                await App.vm.Department.LoadOperations();
            }
            catch (Exception e)
            {
                App.vm.UserMsg = e.Message.ToString();
            }
        }
        public async Task Delete()
        {
            try
            {
                var dialog = new QuestionDialog("Do you really want to delete this operation (" + Name + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {
                        if (CanDelete)
                        {
                            var data = GetModel();
                            context.Entry(data).State = EntityState.Deleted;
                            App.vm.Department.DepartmentOperations.Remove(this);
                            await context.SaveChangesAsync();
                            EndEdit();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                App.vm.UserMsg = e.Message.ToString();
            }
        }
        //For validation
        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name) || (Name ?? "").Length > 100)
                    {
                        result = "Cannot be empty or more than 100 characters";
                    }
                }
                if (columnName == "SortOrder")
                {
                    Regex regex = new Regex("[^0-9]+");
                    if (regex.IsMatch(SortOrder.ToString()))
                    {
                        result = "Cannot be a text value";
                    }
                }
                if (columnName == "OperationTime")
                {
                    Regex regex = new Regex("[^0-9]+");
                    if (regex.IsMatch(OperationTime.ToString()))
                    {
                        result = "Cannot be a text value";
                    }
                }
                return result;
            }
        }
        public string Error
        {
            get { return this[null]; }
        }


        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DepartmentOperationViewModel))
                return false;

            return ((DepartmentOperationViewModel)obj).DepartmentOperationID == DepartmentOperationID;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
