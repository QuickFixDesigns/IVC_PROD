using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.DAL;
using NeoTracker.Models;
using NeoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DataService ds = new DataService();

        public async Task Authentificate()
        {
            IsReady = false;
            CurrentUSer = await ds.GetUser("karrick_Mercier@hotmail.com");
            IsReady = true;
        }

        //Load collections
        public async Task LoadDepartments()
        {
            IsReady = false;
            Departments = await ds.GetDepartmentList();
            IsReady = true;
        }
        public async Task LoadOrders()
        {
            IsReady = false;
            Orders = await ds.GetOrderList();
            IsReady = true;
        }
        public async Task LoadProjects()
        {
            IsReady = false;
            Projects = await ds.GetProjectList();
            IsReady = true;
        }
        public async Task LoadStatus()
        {
            IsReady = false;
            Statuses = await ds.GetStatusList();
            IsReady = true;
        }
        public async Task LoadUsers()
        {
            IsReady = false;
            Users = await ds.GetUserList();
            IsReady = true;
        }
        public async Task LoadEventTypes()
        {
            IsReady = false;
            EventTypes = await ds.GetEventTypeList();
            IsReady = true;
        }

        //base attributes
        private User _CurrentUSer = new User();
        public User CurrentUSer
        {
            get { return _CurrentUSer; }
            set { SetProperty(ref _CurrentUSer, value); }
        }
        private bool _IsReady = false;
        public bool IsReady
        {
            get { return _IsReady; }
            set { SetProperty(ref _IsReady, value && CurrentUSer != null); }
        }

        private string _UserMsg = string.Empty;
        public string UserMsg
        {
            get { return _UserMsg; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ModernDialog.ShowMessage(value, "User message", System.Windows.MessageBoxButton.OK);
                }
            }
        }


        //GENIUS DB
        private List<OrderViewModel> _Orders = new List<OrderViewModel>();
        public List<OrderViewModel> Orders
        {
            get { return _Orders; }
            set { SetProperty(ref _Orders, value); }
        }


        //Departments
        private List<DepartmentViewModel> _Departments = new List<DepartmentViewModel>();
        public List<DepartmentViewModel> Departments
        {
            get { return _Departments; }
            set { SetProperty(ref _Departments, value); }
        }
        private DepartmentViewModel _Department = new DepartmentViewModel();
        public DepartmentViewModel Department
        {
            get { return _Department; }
            set { SetProperty(ref _Department, value); }
        }
        //Projects
        private List<ProjectViewModel> _Projects = new List<ProjectViewModel>();
        public List<ProjectViewModel> Projects
        {
            get { return _Projects; }
            set { SetProperty(ref _Projects, value); }
        }
        private ProjectViewModel _Project = new ProjectViewModel();
        public ProjectViewModel Project
        {
            get { return _Project; }
            set { SetProperty(ref _Project, value); }
        }
        private EventViewModel _Event = new EventViewModel();
        public EventViewModel Event
        {
            get { return _Event; }
            set { SetProperty(ref _Event, value); }
        }
        private ItemViewModel _Item = new ItemViewModel();
        public ItemViewModel Item
        {
            get { return _Item; }
            set { SetProperty(ref _Item, value); }
        }
        private OperationViewModel _Operation = new OperationViewModel();
        public OperationViewModel Operation
        {
            get { return _Operation; }
            set { SetProperty(ref _Operation, value); }
        }
        //EventType
        private List<EventTypeViewModel> _EventTypes = new List<EventTypeViewModel>();
        public List<EventTypeViewModel> EventTypes
        {
            get { return _EventTypes; }
            set { SetProperty(ref _EventTypes, value); }
        }
        private EventTypeViewModel _EventType = new EventTypeViewModel();
        public EventTypeViewModel EventType
        {
            get { return _EventType; }
            set { SetProperty(ref _EventType, value); }
        }
        //Status
        private List<StatusViewModel> _Statuses = new List<StatusViewModel>();
        public List<StatusViewModel> Statuses
        {
            get { return _Statuses; }
            set { SetProperty(ref _Statuses, value); }
        }
        private StatusViewModel _Status = new StatusViewModel();
        public StatusViewModel Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }
        //users
        private List<UserViewModel> _Users = new List<UserViewModel>();
        public List<UserViewModel> Users
        {
            get { return _Users; }
            set { SetProperty(ref _Users, value); }
        }
        private UserViewModel _User = new UserViewModel();
        public UserViewModel User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }

        //for dropdowns and selection dialog
        private List<SelectItem> _SelectItemList = new List<SelectItem>();
        public List<SelectItem> SelectItemList
        {
            get { return _SelectItemList; }
            set { SetProperty(ref _SelectItemList, value); }
        }
    }
}