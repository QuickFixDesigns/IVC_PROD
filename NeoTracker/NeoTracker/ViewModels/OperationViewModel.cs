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
using System.Windows;
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Models
{
    public class OperationViewModel : ViewModelBase, IDataErrorInfo
    {
        public int OperationID { get; set; }
        public int ItemID { get; set; }

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

        private DateTime? _StartDate;
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { SetProperty(ref _StartDate, value); }
        }

        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { SetProperty(ref _EndDate, value); }
        }
        private decimal _Progress;
        public decimal Progress
        {
            get { return _Progress; }
            set { SetProperty(ref _Progress, value); }
        }
        private decimal _OperationTime;
        public decimal OperationTime
        {
            get { return _OperationTime; }
            set { SetProperty(ref _OperationTime, value); }
        }
        private Department _Department = new Department();
        public Department Department
        {
            get { return _Department; }
            set { SetProperty(ref _Department, value); }
        }
        //For database
        public Operation GetModel()
        {
            return new Operation()
            {
                DepartmentID = Department != null && Department.DepartmentID != 0 ? Department.DepartmentID : (int?)null,
                SortOrder = SortOrder,
                Name = Name,
                EndDate = EndDate,
                OperationTime = OperationTime,
                Progress = Progress,
                ItemID = ItemID,
                OperationID = OperationID,
                StartDate = StartDate,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public async void Save()
        {
            using (var context = new NeoTrackerContext())
            {
                var data = GetModel();
                if (OperationID == 0)
                {
                    context.Operations.Add(data);
                }
                else
                {
                    context.Entry(data).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
            EndEdit();
            App.vm.Item.LoadOperations();
        }
        public async void Delete()
        {
            bool CanDelete = true;

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
                        App.vm.Item.Operations.Remove(this);
                        await context.SaveChangesAsync();
                        EndEdit();
                    }
                }
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
                    if (string.IsNullOrEmpty(Name) || (Name ?? "").Length > 25)
                    {
                        result = "Cannot be empty or more than 25 characters";
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
            if (obj == null || !(obj is OperationViewModel))
                return false;

            return ((OperationViewModel)obj).OperationID == this.OperationID;
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
