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
    public class UserViewModel : ViewModelBase, IDataErrorInfo
    {
        public int UserID { get; set; }

        private string _Alias;
        public string Alias
        {
            get { return _Alias; }
            set { SetProperty(ref _Alias, value); }
        }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { SetProperty(ref _FirstName, value); }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value); }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }
        private bool _IsAdmin;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { SetProperty(ref _IsAdmin, value); }
        }
        private bool _EmailNotifications;
        public bool EmailNotifications
        {
            get { return _EmailNotifications; }
            set { SetProperty(ref _EmailNotifications, value); }
        }
        public string LongName
        {
            get
            {
                return string.Concat(LastName, ", ", FirstName, " (", Alias, ")");
            }
        }
        public bool CanAddDepartments
        {
            get { return UserID != 0; }
        }
        private List<DepartmentViewModel> _Departments = new List<DepartmentViewModel>();
        public List<DepartmentViewModel> Departments
        {
            get { return _Departments; }
            set
            {
                SetProperty(ref _Departments, value);
            }
        }
        public User GetModel()
        {
            return new User()
            {
                UserID = UserID,
                Alias = Alias,
                Email = Email,
                EmailNotifications = EmailNotifications,
                FirstName = FirstName,
                LastName = LastName,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public async Task LoadDepartments()
        {
            if (UserID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    Departments = await (from d in context.Departments.Include(x => x.HeadOfDepartment)
                                         join du in context.DepartmentUsers.Where(x => x.UserID == UserID) on d.DepartmentID equals du.DepartmentID
                                         orderby d.SortOrder, d.Name
                                         select new DepartmentViewModel()
                                         {
                                             DepartmentID = d.DepartmentID,
                                             Name = d.Name,
                                             SortOrder = d.SortOrder,
                                             HeadOfDepartment = d.HeadOfDepartment,
                                             IsActive = d.IsActive,
                                             CreatedAt = d.CreatedAt,
                                             UpdatedAt = d.UpdatedAt,
                                             UpdatedBy = d.UpdatedBy,
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
                    if (UserID == 0)
                    {
                        context.Users.Add(data);
                    }
                    else
                    {
                        context.Entry(data).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
                }
                EndEdit();
                await App.vm.LoadUsers();
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
                var dialog = new QuestionDialog("Do you really want to delete this user (" + LongName + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {
                        if (CanDelete)
                        {
                            var data = GetModel();
                            context.Entry(data).State = EntityState.Deleted;
                            await context.SaveChangesAsync();
                            App.vm.Users.Remove(this);
                            EndEdit();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                App.vm.UserMsg = e.Message.ToString();
            }
        }
        public async Task AddDepartments()
        {
            try
            {
                using (var context = new NeoTrackerContext())
                {
                    var list = context.Departments.Where(x => !context.DepartmentUsers.Any(y => y.UserID == UserID && y.DepartmentID == x.DepartmentID)).ToList().Select(x => new SelectItem()
                    {
                        Value = x.DepartmentID,
                        Text = x.Name
                    }).OrderBy(x => x.Text).ToList();

                    if (list.Any())
                    {
                        App.vm.SelectItemList = list;
                        var dialog = new SelectDialog("Select departments");
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
                                        DepartmentID = item.Value,
                                        UserID = UserID
                                    });
                                }
                                await context.SaveChangesAsync();
                                await LoadDepartments();
                            }
                        }
                    }
                    else
                    {
                        App.vm.UserMsg = "There's no departments to add!!!";
                    }
                }
            }
            catch (Exception e)
            {
                App.vm.UserMsg = e.Message.ToString();
            }
        }
        public async Task RemoveDepartment(DepartmentViewModel department)
        {
            try
            {
                var dialog = new QuestionDialog("Do you want to remove this department (" + department.Name + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {
                        var data = await context.DepartmentUsers.FirstOrDefaultAsync(x => x.DepartmentID == department.DepartmentID && x.UserID == UserID);
                        if (data != null)
                        {
                            context.Entry(data).State = EntityState.Deleted;
                            await context.SaveChangesAsync();
                            await LoadDepartments();
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

                if (columnName == "Alias")
                {
                    if (string.IsNullOrEmpty(Alias) || (Alias ?? "").Length > 25)
                    {
                        result = "Cannot be empty or more than 25 characters";
                    }
                }
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName) || (FirstName ?? "").Length > 100)
                    {
                        result = "Cannot be empty or more than 100 characters";
                    }
                }
                if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName) || (LastName ?? "").Length > 100)
                    {
                        result = "Cannot be empty or more than 100 characters";
                    }
                }
                if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email) || (Email ?? "").Length > 320)
                    {
                        result = "Cannot be empty or more than 320 characters";
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
