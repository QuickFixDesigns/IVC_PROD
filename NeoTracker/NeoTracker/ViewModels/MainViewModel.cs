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

        public async void Authentificate()
        {
            IsReady = false;
            CurrentUSer = await ds.GetUser("karrick_Mercier@hotmail.com");
            IsReady = true;
        }

        //Load collections
        public async void LoadDepartments()
        {
            IsReady = false;
            Departments = await ds.GetDepartmentList();
            IsReady = true;
        }
        public void LoadOrders()
        {
            IsReady = false;
            Orders = Task.Run(async () => { return await ds.GetOrderList(); }).Result;
            IsReady = true;
        }
        public async void LoadProjects()
        {
            IsReady = false;
            Projects = await ds.GetProjectList();
            IsReady = true;
        }
        public async void LoadProjectEvents()
        {
            IsReady = false;
            ProjectEvents = await ds.GetProjectEventList(Project.ProjectID);
            IsReady = true;
        }
        public async void LoadProjectItems()
        {
            IsReady = false;
            ProjectItems = await ds.GetProjectItemList(Project.ProjectID);
            IsReady = true;
        }
        public async void LoadStatus()
        {
            IsReady = false;
            Statuses = await ds.GetStatusList();
            IsReady = true;
        }
        public async void LoadUsers()
        {
            IsReady = false;
            Users = await ds.GetUserList();
            IsReady = true;
        }
        public async void LoadProjectEventTypes()
        {
            IsReady = false;
            ProjectEventTypes = await ds.GetProjectEventTypeList();
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
        //ProjectEvents
        private List<ProjectEventViewModel> _ProjectEvents = new List<ProjectEventViewModel>();
        public List<ProjectEventViewModel> ProjectEvents
        {
            get { return _ProjectEvents; }
            set { SetProperty(ref _ProjectEvents, value); }
        }
        private ProjectEventViewModel _ProjectEvent = new ProjectEventViewModel();
        public ProjectEventViewModel ProjectEvent
        {
            get { return _ProjectEvent; }
            set { SetProperty(ref _ProjectEvent, value); }
        }
        //ProjectEventType
        private List<ProjectEventTypeViewModel> _ProjectEventTypes = new List<ProjectEventTypeViewModel>();
        public List<ProjectEventTypeViewModel> ProjectEventTypes
        {
            get { return _ProjectEventTypes; }
            set { SetProperty(ref _ProjectEventTypes, value); }
        }
        private ProjectEventTypeViewModel _ProjectEventType = new ProjectEventTypeViewModel();
        public ProjectEventTypeViewModel ProjectEventType
        {
            get { return _ProjectEventType; }
            set { SetProperty(ref _ProjectEventType, value); }
        }
        //ProjectItems
        private List<ProjectItemViewModel> _ProjectItems = new List<ProjectItemViewModel>();
        public List<ProjectItemViewModel> ProjectItems
        {
            get { return _ProjectItems; }
            set { SetProperty(ref _ProjectItems, value); }
        }
        private ProjectItemViewModel _ProjectItem = new ProjectItemViewModel();
        public ProjectItemViewModel ProjectItem
        {
            get { return _ProjectItem; }
            set { SetProperty(ref _ProjectItem, value); }
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