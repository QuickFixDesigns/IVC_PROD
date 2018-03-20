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
        private DataService ds = new DataService();
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
        private List<ItemViewModel> _Items = new List<ItemViewModel>();
        public List<ItemViewModel> Items
        {
            get { return _Items; }
            set
            {
                SetProperty(ref _Items, value);
                CanDelete = !value.Any() && !Events.Any() && ProjectID != 0;
            }
        }
        private List<EventViewModel> _Events = new List<EventViewModel>();
        public List<EventViewModel> Events
        {
            get { return _Events; }
            set
            {
                SetProperty(ref _Events, value);
                CanDelete = !value.Any() && !Items.Any() && ProjectID != 0;
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
                Events = await ds.GetEventList(ProjectID);
            }
        }
        public async void LoadItems()
        {
            if (ProjectID != 0)
            {
                Items = await ds.GetItemList(ProjectID);
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
                var Operations = await context.Departments.Where(d => d.IsDefault).ToListAsync();

                var Items = await IvcContext.Comm2.Where(x => x.No_Com == code).Select(x => new Item()
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

                foreach (var item in Items)
                {
                    item.ItemOperations = new List<ItemOperation>();
                    foreach (var o in Operations)
                    {
                        item.ItemOperations.Add(new ItemOperation()
                        {
                            OperationTime = 0,
                            DepartmentID = o.DepartmentID,
                            IsActive = true,
                            Name = "From genius?",
                            Progress = 0,
                            SortOrder = o.SortOrder,
                        });
                    }
                }
                project.Items = Items;
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
