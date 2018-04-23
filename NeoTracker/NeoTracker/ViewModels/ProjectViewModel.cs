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
                CreatedBy = CreatedBy,
                UpdatedAt = UpdatedAt,
                UpdatedBy = UpdatedBy,
                IsActive = IsActive,
            };
        }
        public async Task LoadEvents()
        {
            if (ProjectID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    Events = await context.Events.Where(x => x.ProjectID == ProjectID).Include(x => x.Status).Include(x => x.Item).Include(x => x.EventType).Include(x => x.Department).OrderByDescending(x => x.CreatedAt).Select(x => new EventViewModel()
                    {
                        EventID = x.EventID,
                        Department = x.DepartmentID.HasValue ? new DepartmentViewModel()
                        {
                            //CreatedAt = x.Department.CreatedAt,
                            DepartmentID = x.Department.DepartmentID,
                            //HeadOfDepartment = x.Department.HeadOfDepartment,
                            //IsActive = x.Department.IsActive,
                            //IsDefault = x.Department.IsDefault,
                            //Msg = x.Department.Msg,
                            Name = x.Department.Name,
                            //SortOrder = x.Department.SortOrder,
                            //UpdatedAt = x.Department.UpdatedAt,
                            //UpdatedBy = x.Department.UpdatedBy,
                        } : null,
                        Description = x.Description,
                        ProjectID = x.ProjectID,
                        EventType = new EventTypeViewModel()
                        {
                            EventTypeID = x.EventTypeID,
                            Name = x.EventType.Name,
                            //Notificate = x.EventType.Notificate,
                            //SortOrder = x.EventType.SortOrder,
                            //IsActive = x.EventType.IsActive,
                            //CreatedAt = x.EventType.CreatedAt,
                            //UpdatedAt = x.EventType.UpdatedAt,
                            //UpdatedBy = x.EventType.UpdatedBy,
                        },
                        EventItem = x.ItemID.HasValue ? new ItemViewModel()
                        {
                            //IsActive = x.Item.IsActive,
                            Code = x.Item.Code,
                            //CreatedAt = x.Item.CreatedAt,
                            //UpdatedBy = x.Item.UpdatedBy,
                            //UpdatedAt = x.Item.UpdatedAt,
                            //SortOrder = x.Item.SortOrder,
                            //DueDate = x.Item.DueDate,
                            //LatestStartDate = x.Item.LatestStartDate,
                            Name = x.Item.Name,
                            //Project = x.Item.Project,
                            ItemID = x.Item.ItemID,
                            Status = new StatusViewModel()
                            {
                                //CreatedAt = x.Item.Status.CreatedAt,
                                //IsActive = x.Item.Status.IsActive,
                                //IsDeleted = x.Item.Status.IsDeleted,
                                Name = x.Item.Status.Name,
                                //SortOrder = x.Item.Status.SortOrder,
                                StatusID = x.Item.Status.StatusID,
                                //UpdatedAt = x.Item.Status.UpdatedAt,
                                //UpdatedBy = x.Item.Status.UpdatedBy,
                            },
                        } : null,
                        Status = new StatusViewModel()
                        {
                            //CreatedAt = x.Item.Status.CreatedAt,
                            //IsActive = x.Item.Status.IsActive,
                            //IsDeleted = x.Item.Status.IsDeleted,
                            Name = x.Status.Name,
                            //SortOrder = x.Item.Status.SortOrder,
                            StatusID = x.Status.StatusID,
                            //UpdatedAt = x.Item.Status.UpdatedAt,
                            //UpdatedBy = x.Item.Status.UpdatedBy,
                        },
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy
                    }).ToListAsync();
                }
            }
        }
        public async Task LoadOrders()
        {
            using (var context = new NeoTrackerContext())
            using (var Genius = new IVCLIVEDBEntities())
            {
                var projects = context.Projects.Select(x => x.Code).ToArray();

                Orders = await Genius.Comms.Where(x => x.Datecli > DateTime.Today && !projects.Contains(x.No_Com)).OrderByDescending(x => x.Datecli).ThenBy(x => x.No_Com).Select(x => new OrderViewModel()
                {
                    Code = x.No_Com,
                    Client = x.Fact_A1,
                    Date = x.Datecli,
                    Po = x.No_Po,
                }).ToListAsync();
            }
        }
        public async Task LoadItems()
        {
            if (ProjectID != 0)
            {
                using (var context = new NeoTrackerContext())
                {
                    Items = await context.Items.Where(x => x.ProjectID == ProjectID).Include(x => x.Project).Include(x => x.Operations).Include(x => x.Status).OrderBy(x => x.SortOrder).ThenBy(x => x.Name).Select(x => new ItemViewModel()
                    {
                        ItemID = x.ItemID,
                        Code = x.Code,
                        DueDate = x.DueDate,
                        EndDate = x.EndDate,
                        Status = new StatusViewModel()
                        {
                            //CreatedAt = x.Status.CreatedAt,
                            //IsActive = x.Status.IsActive,
                            //IsDeleted = x.Status.IsDeleted,
                            Name = x.Status.Name,
                            //SortOrder = x.Status.SortOrder,
                            StatusID = x.Status.StatusID,
                            //UpdatedAt = x.Status.UpdatedAt,
                            //UpdatedBy = x.Status.UpdatedBy,
                        },
                        ProjectID = x.ProjectID,
                        Name = x.Name,
                        Operations = x.Operations.Select(o => new OperationViewModel()
                        {
                            IsCompleted = o.IsCompleted
                        }).ToList(),
                        SortOrder = x.SortOrder,
                        IsActive = x.IsActive,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy
                    }).ToListAsync();
                }
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
                    if ((Code ?? "").Length > 25)
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
