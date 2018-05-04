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
    public class ItemViewModel : ViewModelBase, IDataErrorInfo
    {
        public int ItemID { get; set; }
        public int ProjectID { get; set; }
        public int? SortKey { get; set; }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }
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
        private DateTime? _DueDate;
        public DateTime? DueDate
        {
            get { return _DueDate; }
            set { SetProperty(ref _DueDate, value); }
        }
        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { SetProperty(ref _EndDate, value); }
        }
        private StatusViewModel _Status = new StatusViewModel();
        public StatusViewModel Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }
        private List<OperationViewModel> _Operations;
        public List<OperationViewModel> Operations
        {
            get { return _Operations; }
            set
            {
                SetProperty(ref _Operations, value);
            }
        }
        public string OperationCount
        {
            get
            {
                string count = Operations != null ? string.Concat(Operations.Count(x => x.IsCompleted).ToString(), " / ", Operations.Count.ToString()) : App.BlankStr;
                return count;
            }
        }

        //For database
        public Item GetModel()
        {
            return new Item()
            {
                ItemID = ItemID,
                ProjectID = ProjectID,
                Code = Code,
                DueDate = DueDate,
                EndDate = EndDate,
                SortOrder = SortOrder,
                SortKey = SortKey,
                StatusID = Status.StatusID,
                Name = Name,
                IsActive = IsActive,
                CreatedBy = CreatedBy,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public async Task LoadOperations()
        {
            using (var context = new NeoTrackerContext())
            {
                if (ItemID != 0)
                {
                    Operations = await context.Operations.Where(x => x.ItemID == ItemID).Include(x => x.Department).Include(x => x.User).OrderBy(x => x.Department.SortOrder).ThenBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new OperationViewModel()
                    {
                        Department = x.Department,
                        User = x.User,
                        EndDate = x.EndDate,
                        ItemID = x.ItemID,
                        OperationID = x.OperationID,
                        OperationTime = x.OperationTime,
                        IsCompleted = x.IsCompleted,
                        StartDate = x.StartDate,
                        Name = x.Name,
                        SortOrder = x.SortOrder,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy
                    }).ToListAsync();

                    App.vm.SelectItemList = Operations.Select(x => x.Department).Distinct().Select(x => new DropdownItem()
                    {
                        Value = x.DepartmentID,
                        IsSelected = false,
                        Text = x.Name
                    }).ToList();
                }
            }
        }
        public async Task Save()
        {
            try
            {
                using (var context = new NeoTrackerContext())
                {
                    var data = GetModel();
                    if (ItemID == 0)
                    {
                        context.Items.Add(data);
                    }
                    else
                    {
                        context.Entry(data).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
                }
                EndEdit();
                await App.vm.Project.LoadItems();
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
                var dialog = new QuestionDialog("Do you really want to delete this item (" + Name + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {
                        if (CanDelete)
                        {
                            var data = GetModel();
                            context.Entry(data).State = EntityState.Deleted;
                            App.vm.Project.Items.Remove(this);
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

        public async Task MassUpdateOperations(List<Operation> ops, DateTime? StartDate, DateTime? EndDate, bool? Completed)
        {
            if (ItemID != 0 && Operations.Any())
            {
                using (var context = new NeoTrackerContext())
                {
                    foreach (var o in ops)
                    {
                        if (EndDate.HasValue)
                        {
                            o.EndDate = EndDate.Value;
                        }
                        if (Completed.HasValue && Completed.Value)
                        {
                            o.IsCompleted = true;
                        }
                        if (StartDate.HasValue)
                        {
                            o.StartDate = StartDate.Value;
                        }
                        context.Entry(o).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
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
                    if (string.IsNullOrEmpty(Name) || (Name ?? "").Length > 255)
                    {
                        result = "Cannot be empty or more than 255 characters";
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
            if (obj == null || !(obj is ItemViewModel))
                return false;

            return ((ItemViewModel)obj).ItemID == this.ItemID;
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
