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

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }
        private string _PurchaseOrder;
        public string PurchaseOrder
        {
            get { return _PurchaseOrder; }
            set { SetProperty(ref _PurchaseOrder, value); }
        }
        private string _Client;
        public string Client
        {
            get { return _Client; }
            set { SetProperty(ref _Client, value); }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }
        private string _Comment;
        public string Comment
        {
            get { return _Comment; }
            set { SetProperty(ref _Comment, value); }
        }
        private ProjectTypeViewModel _ProjectType;
        public ProjectTypeViewModel ProjectType
        {
            get { return _ProjectType; }
            set { SetProperty(ref _ProjectType, value); }
        }
        private List<OrderViewModel> _Orders = new List<OrderViewModel>();
        public List<OrderViewModel> Orders
        {
            get { return _Orders; }
            set { SetProperty(ref _Orders, value); }
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
                Client = Client,
                CreatedAt = CreatedAt,
                ProjectTypeID = ProjectType.ProjectTypeID,
                PurchaseOrder = PurchaseOrder,
                UpdatedAt = UpdatedAt, 
                UpdatedBy = UpdatedBy,
                IsActive = IsActive,
            };
        }
        public async Task LoadEvents()
        {
            if (ProjectID != 0)
            {
                Events = await ds.GetEventList(ProjectID);
            }
        }
        public async Task LoadOrders()
        {
            Orders = await ds.GetOrderList();
        }
        public async Task LoadItems()
        {
            if (ProjectID != 0)
            {
                Items = await ds.GetItemList(ProjectID);
            }
        }
        public async Task Create(OrderViewModel order)
        {
            try
            {
                Code = order.Code;
                PurchaseOrder = order.Po;
                Client = order.Client;

                IsActive = true;

                using (var IvcContext = new IVCLIVEDBEntities())
                using (var context = new NeoTrackerContext())
                {
                    var project = GetModel();
                    var Operations = await context.DepartmentOperations.Where(x => x.IsActive).ToListAsync();
                    var statusID = await context.Statuses.OrderBy(s => s.SortOrder).ThenBy(s => s.Name).Where(s => s.IsActive).Select(s => s.StatusID).FirstAsync();

                    var Items = await IvcContext.Comm2.Where(x => x.No_Com == Code).Select(x => new Item()
                    {
                        Code = x.Item,
                        Name = x.Des,
                        DueDate = x.Dateliv,
                        ProjectID = ProjectID,
                        IsActive = true,
                        SortOrder = x.Ligneitm,
                        StatusID = statusID,
                        SortKey = x.Clef,
                    }).ToListAsync();

                    foreach (var item in Items)
                    {
                        item.Operations = new List<Operation>();
                        foreach (var o in Operations)
                        {
                            item.Operations.Add(new Operation()
                            {
                                OperationTime = o.OperationTime,
                                DepartmentID = o.DepartmentID,
                                IsActive = true,
                                Name = o.Name,
                                IsCompleted = false,
                                SortOrder = o.SortOrder,
                            });
                        }
                    }
                    project.Items = Items;
                    context.Projects.Add(project);
                    await context.SaveChangesAsync();

                    ProjectID = project.ProjectID;
                    await LoadItems();
                }
                EndEdit();
                Orders.Clear();
                await App.vm.LoadProjects();
            }
            catch (Exception e)
            {
                App.vm.UserMsg = e.Message.ToString();
            }
        }
        public async Task Save()
        {
            try
            {
            using (var context = new NeoTrackerContext())
            {
                var data = GetModel();

                context.Entry(data).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            EndEdit();
            await App.vm.LoadProjects();
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

                if (columnName == "Code")
                {
                    if ( (Code ?? "").Length > 25)
                    {
                        result = "Cannot be empty or more than 25 characters";
                    }
                }
                if (columnName == "PurchaseOrder")
                {
                    if ((PurchaseOrder ?? "").Length > 25)
                    {
                        result = "Cannot be empty or more than 25 characters";
                    }
                }
                if (columnName == "Client")
                {
                    if ((Client ?? "").Length > 255)
                    {
                        result = "Cannot be empty or more than 255 characters";
                    }
                }
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name) || (Name ?? "").Length > 100)
                    {
                        result = "Cannot be empty or more than 100 characters";
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
