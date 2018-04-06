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
    public class StatusViewModel : ViewModelBase, IDataErrorInfo
    {
        public int StatusID { get; set; }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        private int? _SortOrder;
        public int? SortOrder
        {
            get { return _SortOrder; }
            set { SetProperty(ref _SortOrder, value); }
        }
        private bool _IsDeleted;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { SetProperty(ref _IsDeleted, value); }
        }
        //For database
        public Status GetModel()
        {
            return new Status()
            {
                StatusID = StatusID,
                Name = Name,
                IsDeleted = IsDeleted,
                SortOrder = SortOrder,
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
                if (StatusID == 0)
                {
                    context.Statuses.Add(data);
                }
                else
                {
                    context.Entry(data).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
            EndEdit();
            await App.vm.LoadStatus();
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
            var dialog = new QuestionDialog("Do you really want to delete this Status (" + Name + ")?");
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (var context = new NeoTrackerContext())
                {
                    if (CanDelete)
                    {
                        var data = GetModel();
                        context.Entry(data).State = EntityState.Deleted;
                        App.vm.Statuses.Remove(this);
                        await context.SaveChangesAsync();
                    }
                }
                EndEdit();
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
                    if (string.IsNullOrEmpty(Name) || (Name ?? "").Length > 25)
                    {
                        result = "Cannot be empty or more than 25 characters";
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
                return result;
            }
        }
        public string Error
        {
            get { return this[null]; }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is StatusViewModel))
                return false;

            return ((StatusViewModel)obj).StatusID == this.StatusID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
