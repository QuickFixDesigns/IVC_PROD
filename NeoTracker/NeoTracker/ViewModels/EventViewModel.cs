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
    public class EventViewModel : ViewModelBase, IDataErrorInfo
    {
        public int EventID { get; set; }
        public int ProjectID { get; set; }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }
        private DepartmentViewModel _Department = new DepartmentViewModel();
        public DepartmentViewModel Department
        {
            get { return _Department; }
            set { SetProperty(ref _Department, value); }
        }
        private ItemViewModel _EventItem = new ItemViewModel();
        public ItemViewModel EventItem
        {
            get { return _EventItem; }
            set { SetProperty(ref _EventItem, value); }
        }
        private EventTypeViewModel _EventType = new EventTypeViewModel();
        public EventTypeViewModel EventType
        {
            get { return _EventType; }
            set { SetProperty(ref _EventType, value); }
        }
        //For database
        public Event GetModel()
        {
            return new Event()
            {
                DepartmentID = Department!=null && Department.DepartmentID!=0 ? Department.DepartmentID : (int?)null,
                Description = Description,
                EventID = EventID,
                EventTypeID = EventType.EventTypeID,
                ItemID = EventItem != null && EventItem.ItemID !=0 ? EventItem.ItemID : (int?)null,
                ProjectID = ProjectID,
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
                if (EventID == 0)
                {
                    context.Events.Add(data);
                }
                else
                {
                    context.Entry(data).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
            App.vm.Project.LoadEvents();
            EndEdit();
        }
        public async void Delete()
        {
            bool CanDelete = true;

            var dialog = new QuestionDialog("Do you really want to delete this Event (" + Description + ")?");
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (var context = new NeoTrackerContext())
                {
                    if (CanDelete)
                    {
                        var data = GetModel();
                        context.Entry(data).State = EntityState.Deleted;
                        App.vm.Project.Events.Remove(this);
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

                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        result = "A Description is required!";
                    }
                }
                return result;
            }
        }
        public string Error
        {
            get { return this[null]; }
        }
    }
}
