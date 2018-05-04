using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.Assets;
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
using System.Windows.Input;
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Models
{
    public class OperationViewModel : ViewModelBase, IDataErrorInfo
    {
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(async () => await Save(), true));
            }
        }
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
        private decimal _OperationTime;
        public decimal OperationTime
        {
            get { return _OperationTime; }
            set { SetProperty(ref _OperationTime, value); }
        }
        private bool _IsCompleted;
        public bool IsCompleted
        {
            get { return _IsCompleted; }
            set { SetProperty(ref _IsCompleted, value); }
        }

        private Department _Department = new Department();
        public Department Department
        {
            get { return _Department; }
            set { SetProperty(ref _Department, value); }
        }
        private User _User = new User();
        public User User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }

        //For database
        public Operation GetModel()
        {
            return new Operation()
            {
                DepartmentID = Department.DepartmentID,
                SortOrder = SortOrder,
                Name = Name,
                EndDate = EndDate,
                OperationTime = OperationTime,
                IsCompleted = IsCompleted,
                ItemID = ItemID,
                OperationID = OperationID,
                StartDate = StartDate,
                UserID = User != null && User.UserID != 0 ? User.UserID : (int?)null,
                IsActive = IsActive,
                CreatedBy = CreatedBy,
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
                await App.vm.Item.LoadOperations();
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
                        var data = GetModel();
                        context.Entry(data).State = EntityState.Deleted;
                        App.vm.Item.Operations.Remove(this);

                        var changeLogs = App.vm.ChangeLog.Where(x => x.EntityName == "Operation" && x.PrimaryKeyValue == data.OperationID).ToList();
                        changeLogs.ForEach(x => context.Entry(x).State = EntityState.Deleted);

                        await context.SaveChangesAsync();
                        EndEdit();
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
