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
    public class ProjectTypeViewModel : ViewModelBase, IDataErrorInfo
    {
        public int ProjectTypeID { get; set; }
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
        //For database
        public ProjectType GetModel()
        {
            return new ProjectType()
            {
                ProjectTypeID = ProjectTypeID,
                Name = Name,
                SortOrder = SortOrder,
                IsActive = IsActive,
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
                    if (ProjectTypeID == 0)
                    {
                        context.ProjectTypes.Add(data);
                    }
                    else
                    {
                        context.Entry(data).State = EntityState.Modified;
                    }
                    await context.SaveChangesAsync();
                }
                EndEdit();
                await App.vm.LoadProjectTypes();
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
                var dialog = new QuestionDialog("Do you really want to delete this Project type (" + Name + ")?");
                dialog.ShowDialog();
                if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
                {
                    using (var context = new NeoTrackerContext())
                    {

                        if (CanDelete)
                        {
                            var data = GetModel();
                            context.Entry(data).State = EntityState.Deleted;
                            App.vm.ProjectTypes.Remove(this);
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
                if (columnName == "SortOrder")
                {
                    Regex regex = new Regex("[^0-9]+");
                    if (regex.IsMatch(SortOrder.ToString()))
                    {
                        result = "Cannot be a text value";
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
            if (obj == null || !(obj is ProjectTypeViewModel))
                return false;

            return ((ProjectTypeViewModel)obj).ProjectTypeID == this.ProjectTypeID;
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
