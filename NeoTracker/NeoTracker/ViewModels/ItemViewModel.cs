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
    public class ItemViewModel : ViewModelBase, IDataErrorInfo
    {
        private DataService ds = new DataService();

        public int ItemID { get; set; }
        public int ProjectID { get; set; }

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

        private DateTime? _LatestStartDate;
        public DateTime? LatestStartDate
        {
            get { return _LatestStartDate; }
            set { SetProperty(ref _LatestStartDate, value); }
        }

        private DateTime? _DueDate;
        public DateTime? DueDate
        {
            get { return _DueDate; }
            set { SetProperty(ref _DueDate, value); }
        }
        private StatusViewModel _Status = new StatusViewModel();
        public StatusViewModel Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }
        private List<OperationViewModel> _Operations = new List<OperationViewModel>();
        public List<OperationViewModel> Operations
        {
            get { return _Operations; }
            set
            {
                SetProperty(ref _Operations, value);
                CanDelete = !value.Any() && ItemID != 0;
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
                LatestStartDate = LatestStartDate,
                SortOrder = SortOrder,
                StatusID = Status !=null && Status.StatusID !=0 ? Status.StatusID : (int?) null,
                Name = Name,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public async void LoadOperations()
        {
            if (ItemID != 0)
            {
                Operations = await ds.GetOperationList(ProjectID);
            }
        }
        public async void Save()
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
            App.vm.Project.LoadItems();
        }
        public async void Delete()
        {
            bool CanDelete = true;

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
    }
}
