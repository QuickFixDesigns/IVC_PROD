using FirstFloor.ModernUI.Presentation;
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
using System.Windows.Input;
using static NeoTracker.ViewModels.MainViewModel;

namespace NeoTracker.Models
{
    public class DepartmentViewModel : ViewModelBase, IDataErrorInfo
    {
        public int DepartmentID { get; set; }

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
        private User _HeadOfDepartment = new User();
        public User HeadOfDepartment
        {
            get { return _HeadOfDepartment; }
            set { SetProperty(ref _HeadOfDepartment, value); }
        }
        public bool CanAdd
        {
            get { return DepartmentID != 0; }
        }
        private bool _CanManageProject;
        public bool CanManageProject
        {
            get { return _CanManageProject; }
            set
            {
                SetProperty(ref _CanManageProject, value);
            }
        }
        private List<User> _Users = new List<User>();
        public List<User> Users
        {
            get { return _Users; }
            set
            {
                SetProperty(ref _Users, value);
                CanDelete = !value.Any() && !DepartmentOperations.Any() && DepartmentID != 0;
            }
        }
        private List<DepartmentOperationViewModel> _DepartmentOperations = new List<DepartmentOperationViewModel>();
        public List<DepartmentOperationViewModel> DepartmentOperations
        {
            get { return _DepartmentOperations; }
            set
            {
                SetProperty(ref _DepartmentOperations, value);
                CanDelete = !value.Any() && !Users.Any() && DepartmentID != 0;
            }
        }
        //For database
        public Department GetModel()
        {
            return new Department()
            {
                DepartmentID = DepartmentID,
                Name = Name,
                SortOrder = SortOrder,
                CanManageProject = CanManageProject,
                HeadOfDepartmentID = HeadOfDepartment != null && HeadOfDepartment.UserID != 0 ? HeadOfDepartment.UserID : (int?)null,
                IsActive = IsActive,
                CreatedBy = CreatedBy,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public async Task LoadUsers()
        {
            if (DepartmentID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    Users = await context.Users.Where(x => x.DepartmentUsers.Any(u => u.DepartmentID == DepartmentID)).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToListAsync();
                }
            }
        }
        public async Task LoadOperations()
        {
            if (DepartmentID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    DepartmentOperations = await context.DepartmentOperations.Where(x => x.DepartmentID == DepartmentID).OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new DepartmentOperationViewModel()
                    {
                        CreatedAt = x.CreatedAt,
                        DepartmentID = x.DepartmentID,
                        DepartmentOperationID = x.DepartmentOperationID,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        OperationTime = x.OperationTime,
                        SortOrder = x.SortOrder,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy,
                    }).ToListAsync();
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
                    if (DepartmentID == 0)
                    {
                        context.Departments.Add(data);
                    }
                    else
                    {
                        context.Entry(data).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
                }
                EndEdit();
                await App.vm.LoadDepartments();
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
                var dialog = new QuestionDialog("Do you really want to delete this department (" + Name + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {
                        try
                        {
                            var data = GetModel();
                            context.Entry(data).State = EntityState.Deleted;
                            await context.SaveChangesAsync();
                            App.vm.Departments.Remove(this);
                            EndEdit();
                        }
                        catch (Exception e)
                        {
                            App.vm.UserMsg = e.Message.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                App.vm.UserMsg = e.Message.ToString();
            }
        }
        public async Task AddUsers()
        {
            using (var context = new NeoTrackerContext())
            {
                try
                {
                    var list = context.Users.Where(x => !context.DepartmentUsers.Any(y => y.DepartmentID == DepartmentID && y.UserID == x.UserID)).ToList().Select(x => new DropdownItem()
                    {
                        Value = x.UserID,
                        Text = x.LongName
                    }).OrderBy(x => x.Text).ToList();

                    if (list.Any())
                    {
                        App.vm.SelectItemList = list;
                        var dialog = new SelectDialog("Select users");
                        dialog.ShowDialog();

                        if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                        {
                            var result = App.vm.SelectItemList.Where(x => x.IsSelected).ToList();

                            if (result.Any())
                            {
                                foreach (var item in result)
                                {
                                    context.DepartmentUsers.Add(new DepartmentUser()
                                    {
                                        DepartmentID = DepartmentID,
                                        UserID = item.Value
                                    });
                                }
                                await context.SaveChangesAsync();
                                await LoadUsers();
                            }
                        }
                    }
                    else
                    {
                        App.vm.UserMsg = "No users to add!!!";
                    }
                }
                catch (Exception e)
                {
                    App.vm.UserMsg = e.Message.ToString();
                }
            }
        }
        public async Task RemoveUser(User user)
        {
            try
            {
                var dialog = new QuestionDialog("Do you want to remove this user (" + user.LongName + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {
                        if (HeadOfDepartment != null && HeadOfDepartment.UserID == user.UserID)
                        {
                            HeadOfDepartment = null;
                            context.Entry(GetModel()).State = EntityState.Modified;
                            await context.SaveChangesAsync();
                        }
                        var data = await context.DepartmentUsers.FirstOrDefaultAsync(x => x.UserID == user.UserID && x.DepartmentID == DepartmentID);
                        if (data != null)
                        {
                            context.Entry(data).State = EntityState.Deleted;
                            await context.SaveChangesAsync();
                            await LoadUsers();
                        }
                    }
                };
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
            if (obj == null || !(obj is DepartmentViewModel))
                return false;

            return ((DepartmentViewModel)obj).DepartmentID == this.DepartmentID;
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
