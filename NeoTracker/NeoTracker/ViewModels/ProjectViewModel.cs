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
using System.Threading.Tasks;
using System.Windows;
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Models
{
    public class ProjectViewModel : ViewModelBase, IDataErrorInfo
    {
        public int ProjectID { get; set; }

        public bool CanCreate
        {
            get { return ProjectID != 0 && !string.IsNullOrEmpty(Name); }
        }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        private int? _Priority;
        public int? Priority
        {
            get { return _Priority; }
            set { SetProperty(ref _Priority, value); }
        }
        private string _Comment;
        public string Comment
        {
            get { return _Comment; }
            set { SetProperty(ref _Comment, value); }
        }
        public bool CanAddItems
        {
            get { return ProjectID != 0; }
        }
        private List<ProjectItemViewModel> _ProjectItems = new List<ProjectItemViewModel>();
        public List<ProjectItemViewModel> ProjectItems
        {
            get { return _ProjectItems; }
            set
            {
                SetProperty(ref _ProjectItems, value);
                CanDelete = !value.Any() && !ProjectEvents.Any() && ProjectID != 0;
            }
        }
        private List<ProjectEventViewModel> _ProjectEvents = new List<ProjectEventViewModel>();
        public List<ProjectEventViewModel> ProjectEvents
        {
            get { return _ProjectEvents; }
            set
            {
                SetProperty(ref _ProjectEvents, value);
                CanDelete = !value.Any() && !ProjectItems.Any() && ProjectID != 0;
            }
        }
        //For database
        public Project GetModel()
        {
            return new Project()
            {
                ProjectID = ProjectID,
                Name = Name,
                Code = Code,
                Comment = Comment,
                Priority = Priority,
                IsActive = IsActive,
            };
        }
        public async void LoadEvents()
        {
            if (ProjectID != 0)
            {
                ProjectEvents = await GetProjectEventList(ProjectID);
            }
        }
        public async void LoadItems()
        {
            if (ProjectID != 0)
            {
                ProjectItems = await GetProjectItemList(ProjectID);
            }
        }
        public async Task<List<ProjectItemViewModel>> GetProjectItemList(int ProjectID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectItems.Include(x => x.Status).Where(x => x.ProjectID == ProjectID).Select(x => new ProjectItemViewModel()
                {
                    Code = x.Code,
                    SortOrder = x.SortOrder,
                    Status = x.Status,
                    DueDate = x.DueDate,
                    LatestStartDate = x.LatestStartDate,
                    Name = x.Name,
                    ProjectItemID = x.ProjectItemID,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async Task<List<ProjectEventViewModel>> GetProjectEventList(int ProjectID)
        {
            using (var context = new NeoTrackerContext())
            {
                return await context.ProjectEvents.Include(x => x.Department).Include(x => x.ProjectItem).Include(x => x.Project).Include(x => x.ProjectEventType).Where(x => x.ProjectID == ProjectID).Select(x => new ProjectEventViewModel()
                {
                    Department = x.Department,
                    Description = x.Description,
                    Project = x.Project,
                    ProjectEventType = x.ProjectEventType,
                    ProjectEventID = x.ProjectEventID,
                    ProjectItem = x.ProjectItem,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToListAsync();
            }
        }
        public async void Create(string code)
        {
            Code = code;
            IsActive = true;
            using (var IvcContext = new IVCLIVEDBEntities())
            using (var context = new NeoTrackerContext())
            {
                var project = GetModel();
                var ProjectItems = await IvcContext.Comm2.Where(x => x.No_Com == code).Select(x => new ProjectItem()
                {
                    Code = x.Item,
                    Name = x.Des,
                    DueDate = x.Dateliv,
                    ProjectID = ProjectID,
                    LatestStartDate = x.DateJobProductionStart,
                    IsActive = true,
                    SortOrder = x.Ligneitm,
                    SortKey = x.Clef,
                }).ToListAsync();
                project.ProjectItems = ProjectItems;

                context.Projects.Add(project);
                await context.SaveChangesAsync();

                ProjectID = project.ProjectID;
                LoadItems();
            }
            EndEdit();
            App.vm.LoadProjects();
        }
        public async void Save()
        {
            using (var context = new NeoTrackerContext())
            {
                var data = GetModel();

                context.Entry(data).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            EndEdit();
            App.vm.LoadProjects();
        }
        public async void Delete()
        {
            var dialog = new QuestionDialog("Do you really want to delete this Project (" + Name + ")?");
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (var context = new NeoTrackerContext())
                {
                    var data = GetModel();
                    context.Entry(data).State = EntityState.Deleted;
                    App.vm.Projects.Remove(this);
                    await context.SaveChangesAsync();
                }
                EndEdit();
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
