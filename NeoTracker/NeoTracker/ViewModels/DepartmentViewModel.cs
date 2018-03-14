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
        private User _HeadOfDepartment;
        public User HeadOfDepartment
        {
            get { return _HeadOfDepartment; }
            set { SetProperty(ref _HeadOfDepartment, value); }
        }
        private string _Msg;
        public string Msg
        {
            get { return _Msg; }
            set { SetProperty(ref _Msg, value); }
        }
        private bool _IsDefault;
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { SetProperty(ref _IsDefault, value); }
        }
        public bool CanAddUsers
        {
            get { return DepartmentID != 0; }
        }
        private List<User> _Users = new List<User>();
        public List<User> Users
        {
            get { return _Users; }
            set
            {
                SetProperty(ref _Users, value);
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
                HeadOfDepartmentID = HeadOfDepartment != null ? HeadOfDepartment.UserID : (int?)null,
                Msg = Msg,
                IsDefault = IsDefault,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy
            };
        }
        public void LoadUsers()
        {
            if (DepartmentID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    Users = context.Users.Where(x => x.DepartmentUsers.Any(u => u.DepartmentID == DepartmentID)).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
                }
            }
        }
        public async void Save()
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
            App.vm.LoadDepartments();
        }
        public async void Delete()
        {
            bool CanDelete = true;

            var dialog = new QuestionDialog("Do you really want to delete this department (" + Name + ")?");
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (var context = new NeoTrackerContext())
                {

                    if (CanDelete)
                    {
                        var data = GetModel();
                        context.Entry(data).State = EntityState.Deleted;
                        App.vm.Departments.Remove(this);
                        await context.SaveChangesAsync();
                        EndEdit();
                    }
                }
            }
        }
        public async void AddUsers()
        {
            using (var context = new NeoTrackerContext())
            {
                var list = context.Users.Where(x => !context.DepartmentUsers.Any(y => y.DepartmentID == DepartmentID && y.UserID == x.UserID)).ToList().Select(x => new SelectItem()
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
                            LoadUsers();
                        }
                    }
                }
                else
                {
                    ModernDialog.ShowMessage("No Users to add...", FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
                }
            }
        }
        public async void RemoveUser(User user)
        {
            var dialog = new QuestionDialog("Do you want to remove this user (" + user.LongName + ")?");
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (var context = new NeoTrackerContext())
                {
                    if (HeadOfDepartment!=null && HeadOfDepartment.UserID == user.UserID)
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
                        LoadUsers();
                    }
                }
            };
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
