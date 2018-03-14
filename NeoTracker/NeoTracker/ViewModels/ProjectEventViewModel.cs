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
    public class ProjectEventViewModel : ViewModelBase, IDataErrorInfo
    {
        public int ProjectEventID { get; set; }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }
        private Project _Project = new Project();
        public Project Project
        {
            get { return _Project; }
            set { SetProperty(ref _Project, value); }
        }
        private Department _Department = new Department();
        public Department Department
        {
            get { return _Department; }
            set { SetProperty(ref _Department, value); }
        }
        private ProjectItem _ProjectItem = new ProjectItem();
        public ProjectItem ProjectItem
        {
            get { return _ProjectItem; }
            set { SetProperty(ref _ProjectItem, value); }
        }
        private ProjectEventType _ProjectEventType = new ProjectEventType();
        public ProjectEventType ProjectEventType
        {
            get { return _ProjectEventType; }
            set { SetProperty(ref _ProjectEventType, value); }
        }
        //For database
        public ProjectEvent GetModel()
        {
            return new ProjectEvent()
            {
                DepartmentID = Department.DepartmentID!=0 ? Department.DepartmentID : (int?)null,
                Description = Description,
                ProjectEventID = ProjectEventID,
                ProjectEventTypeID = ProjectEventType.ProjectEventTypeID,
                ProjectItemID = ProjectItem.ProjectItemID!=0 ? ProjectItem.ProjectItemID : (int?)null,
                ProjectID = Project.ProjectID,
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
                if (ProjectEventID == 0)
                {
                    context.ProjectEvents.Add(data);
                }
                else
                {
                    context.Entry(data).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
            EndEdit();
            App.vm.LoadDepartments();
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
                        App.vm.ProjectEvents.Remove(this);
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
                    if (string.IsNullOrEmpty(Description) || (Description ?? "").Length > 255)
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
    }
}
