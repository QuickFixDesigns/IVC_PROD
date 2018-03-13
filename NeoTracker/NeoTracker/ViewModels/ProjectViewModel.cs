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
                CanDelete = !value.Any() && ProjectID != 0;
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
        public void LoadEvents()
        {
            if (ProjectID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    //Users = (from u in context.Users
                    //         join du in context.DepartmentUsers.Where(x => x.DepartmentID == DepartmentID) on u.UserID equals du.UserID
                    //         orderby u.FirstName, u.LastName
                    //         select new UserViewModel()
                    //         {
                    //             UserID = u.UserID,
                    //             Alias = u.Alias,
                    //             CreatedAt = u.CreatedAt,
                    //             Email = u.Email,
                    //             EmailNotifications = u.EmailNotifications,
                    //             FirstName = u.FirstName,
                    //             LastName = u.LastName,
                    //             UpdatedAt = u.UpdatedAt,
                    //             UpdatedBy = u.UpdatedBy
                    //         }).ToList();
                }
            }
        }
        public async void LoadItemsAsync()
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
        public async void Create(string code)
        {
            using (var IvcContext = new IVCLIVEDBEntities())
            using (var context = new NeoTrackerContext())
            {
                var project = GetModel();
                project.IsActive = true;
                var ProjectItems = await IvcContext.Comm2.Where(x => x.No_Com == Code).Select(x => new ProjectItem()
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
            }
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
            bool CanDelete = true;

            var dialog = new QuestionDialog("Do you really want to delete this Project (" + Name + ")?");
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (var context = new NeoTrackerContext())
                {

                    if (CanDelete)
                    {
                        var data = GetModel();
                        context.Entry(data).State = EntityState.Deleted;
                        App.vm.Projects.Remove(this);
                        await context.SaveChangesAsync();
                    }
                }
                EndEdit();
            }
        }
        public void AddEvent()
        {
            using (var context = new NeoTrackerContext())
            {
                //var list = context.Users.Where(x => !context.DepartmentUsers.Any(y => y.DepartmentID == DepartmentID && y.UserID==x.UserID)).ToList().Select(x => new SelectItem()
                //{
                //    Value = x.UserID,
                //    Text = x.LongName
                //}).OrderBy(x => x.Text).ToList();

                //if (list.Any())
                //{
                //    App.vm.SelectItemList = list;
                //    var dialog = new SelectDialog("Select users");
                //    dialog.ShowDialog();

                //    if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                //    {
                //        var result = App.vm.SelectItemList.Where(x => x.IsSelected).ToList();

                //        if (result.Any())
                //        {
                //            foreach (var item in result)
                //            {
                //                context.DepartmentUsers.Add(new DepartmentUser()
                //                {
                //                    DepartmentID = DepartmentID,
                //                    UserID = item.Value
                //                });
                //            }
                //            await context.SaveChangesAsync();
                //            LoadUsers();
                //        }
                //    }
                //}
                //else
                //{
                //    ModernDialog.ShowMessage("No Users to add...", FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
                //}
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
