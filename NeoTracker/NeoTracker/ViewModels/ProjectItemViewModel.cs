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
    public class ProjectItemViewModel : ViewModelBase, IDataErrorInfo
    {
        public int ProjectItemID { get; set; }

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
        private Project _Project = new Project();
        public Project Project
        {
            get { return _Project; }
            set { SetProperty(ref _Project, value); }
        }
        private Status _Status = new Status();
        public Status Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }

        //For database
        public ProjectItem GetModel()
        {
            return new ProjectItem()
            {
                ProjectItemID = ProjectItemID,
                ProjectID = Project.ProjectID,
                Code = Code,
                DueDate = DueDate,
                LatestStartDate = LatestStartDate,
                SortOrder = SortOrder,
                StatusID = Status.StatusID,
                Name = Name,
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
                if (ProjectItemID == 0)
                {
                    context.ProjectItems.Add(data);
                }
                else
                {
                    context.Entry(data).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
            EndEdit();
            App.vm.LoadProjectItems();
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
                        App.vm.ProjectItems.Remove(this);
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
    }
}
