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
    public class EventTypeViewModel : ViewModelBase, IDataErrorInfo
    {
        public int EventTypeID { get; set; }
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
        private bool _Notificate;
        public bool Notificate
        {
            get { return _Notificate; }
            set
            {
                SetProperty(ref _Notificate, value);
            }
        }
        private bool _IsPriceChange;
        public bool IsPriceChange
        {
            get { return _IsPriceChange; }
            set { SetProperty(ref _IsPriceChange, value); }
        }
        private bool _IsDueDateChange;
        public bool IsDueDateChange
        {
            get { return _IsDueDateChange; }
            set { SetProperty(ref _IsDueDateChange, value); }
        }
        //For database
        public EventType GetModel()
        {
            return new EventType()
            {
                EventTypeID = EventTypeID,
                Name = Name,
                Notificate = Notificate,
                IsDueDateChange = IsDueDateChange,
                IsPriceChange = IsPriceChange,
                SortOrder = SortOrder,
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
                    if (EventTypeID == 0)
                    {
                        context.EventTypes.Add(data);
                    }
                    else
                    {
                        context.Entry(data).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
                }
                EndEdit();
                await App.vm.LoadEventTypes();
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
                var dialog = new QuestionDialog("Do you really want to delete this EventType (" + Name + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {

                        if (CanDelete)
                        {
                            var data = GetModel();
                            context.Entry(data).State = EntityState.Deleted;
                            App.vm.EventTypes.Remove(this);
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
                return result;
            }
        }
        public string Error
        {
            get { return this[null]; }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EventTypeViewModel))
                return false;

            return ((EventTypeViewModel)obj).EventTypeID == this.EventTypeID;
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
