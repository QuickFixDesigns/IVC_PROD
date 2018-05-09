using FirstFloor.ModernUI.Windows.Controls;
using NeoTracker.Assets;
using NeoTracker.DAL;
using NeoTracker.Models;
using NeoTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NeoTracker.ViewModels
{
    public class MainViewModel : ViewModelBase, IAsyncInitialization
    {
        public MainViewModel()
        {
            Initialization = InitializeAsync();
        }

        //interface IAsyncInit
        public Task Initialization { get; private set; }

        public async Task InitializeAsync()
        {
            IsReady = false;

            if (await Authentificate())
            {
                await LoadDepartments();
                await LoadProjects();
                await LoadStatus();
                await LoadUsers();
                await LoadEventTypes();
                await LoadProjectTypes();
            }
            IsReady = true;
        }
        public async Task<bool> Authentificate()
        {
            CurrentUser = await DataService.GetUser("karrick_mercier@hotmail.com");
            return CurrentUser != null;
        }

        //Load collections
        public async Task LoadDepartments()
        {
            Departments = await DataService.GetDepartmentList();
        }
        public async Task LoadProjects()
        {
            Projects = await DataService.GetProjectList();
        }
        public async Task LoadStatus()
        {
            Statuses = await DataService.GetStatusList();
        }
        public async Task LoadUsers()
        {
            Users = await DataService.GetUserList();
        }
        public async Task LoadEventTypes()
        {
            EventTypes = await DataService.GetEventTypeList();
        }
        public async Task LoadProjectTypes()
        {
            ProjectTypes = await DataService.GetProjectTypeList();
        }
        public async Task LoadChangeLog(string entityName, int pk)
        {
            ChangeLog.Clear();

            using (var context = new NeoTrackerContext())
            {
                var logs = await context.ChangeLogs.Where(x => x.EntityName == entityName && x.PrimaryKeyValue == pk).OrderByDescending(x => x.UpdatedAt).ToListAsync();
                foreach (var log in logs.Where(x=>x.PropertyName.Contains("ID")))
                {
                    switch (log.PropertyName)
                    {
                        case "StatusID":
                            //log.NewValue = !string.IsNullOrEmpty(log.NewValue) ? context.Statuses.Find(int.Parse(log.NewValue)).Name : string.Empty;
                            log.OldValue = !string.IsNullOrEmpty(log.OldValue) ? context.Statuses.Find(int.Parse(log.OldValue)).Name : string.Empty;
                            break;
                        case "ItemID":
                            //log.NewValue = !string.IsNullOrEmpty(log.NewValue) ? context.Items.Find(int.Parse(log.NewValue)).Name : string.Empty;
                            log.OldValue = !string.IsNullOrEmpty(log.OldValue) ? context.Items.Find(int.Parse(log.OldValue)).Name : string.Empty;
                            break;
                        case "UserID":
                            log.OldValue = !string.IsNullOrEmpty(log.OldValue) ? context.Users.Find(int.Parse(log.OldValue)).LongName : string.Empty;
                            break;
                        case "DepartmentID":
                            //log.NewValue = !string.IsNullOrEmpty(log.NewValue) ? context.Departments.Find(int.Parse(log.NewValue)).Name : string.Empty;
                            log.OldValue = !string.IsNullOrEmpty(log.OldValue) ? context.Departments.Find(int.Parse(log.OldValue)).Name : string.Empty;
                            break;
                        case "EventTypeID":
                            //log.NewValue = !string.IsNullOrEmpty(log.NewValue) ? context.EventTypes.Find(int.Parse(log.NewValue)).Name : string.Empty;
                            log.OldValue = !string.IsNullOrEmpty(log.OldValue) ? context.EventTypes.Find(int.Parse(log.OldValue)).Name : string.Empty;
                            break;
                        case "ProjectTypeID":
                            //log.NewValue = !string.IsNullOrEmpty(log.NewValue) ? context.ProjectTypes.Find(int.Parse(log.NewValue)).Name : string.Empty;
                            log.OldValue = !string.IsNullOrEmpty(log.OldValue) ? context.ProjectTypes.Find(int.Parse(log.OldValue)).Name : string.Empty;
                            break;
                        default:
                            break;
                    }
                }
                ChangeLog = logs;
            }
        }
        //Commands
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _LoginCommand ?? (_LoginCommand = new CommandHandler(async () => await InitializeAsync(), true));
            }
        }
        private ICommand _AdminCommand;
        public ICommand AdminCommand
        {
            get
            {
                return _AdminCommand ?? (_AdminCommand = new CommandHandler(null, true));
            }
        }

        //base attributes
        private UserViewModel _CurrentUser;
        public UserViewModel CurrentUser
        {
            get { return _CurrentUser; }
            set { SetProperty(ref _CurrentUser, value); }
        }
        private bool _IsReady = false;
        public bool IsReady
        {
            get { return _IsReady; }
            set
            {
                IsPending = !value;
                SetProperty(ref _IsReady, value);
            }
        }
        private bool _IsPending;
        public bool IsPending
        {
            get { return _IsPending; }
            set { SetProperty(ref _IsPending, value); }
        }

        private string _UserMsg = string.Empty;
        public string UserMsg
        {
            get { return _UserMsg; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ModernDialog.ShowMessage(value, "User message", MessageBoxButton.OK);
                }
            }
        }
        private List<ChangeLog> _ChangeLog = new List<ChangeLog>();
        public List<ChangeLog> ChangeLog
        {
            get { return _ChangeLog; }
            set { SetProperty(ref _ChangeLog, value); }
        }
        //Departments
        private List<DepartmentViewModel> _Departments;
        public List<DepartmentViewModel> Departments
        {
            get { return _Departments; }
            set { SetProperty(ref _Departments, value); }
        }
        private DepartmentViewModel _Department;
        public DepartmentViewModel Department
        {
            get { return _Department; }
            set { SetProperty(ref _Department, value); }
        }
        private DepartmentOperationViewModel _DepartmentOperation;
        public DepartmentOperationViewModel DepartmentOperation
        {
            get { return _DepartmentOperation; }
            set { SetProperty(ref _DepartmentOperation, value); }
        }
        //Projects
        private List<ProjectViewModel> _Projects;
        public List<ProjectViewModel> Projects
        {
            get { return _Projects; }
            set { SetProperty(ref _Projects, value); }
        }
        private ProjectViewModel _Project;
        public ProjectViewModel Project
        {
            get { return _Project; }
            set { SetProperty(ref _Project, value); }
        }
        private EventViewModel _Event;
        public EventViewModel Event
        {
            get { return _Event; }
            set { SetProperty(ref _Event, value); }
        }
        private ItemViewModel _Item;
        public ItemViewModel Item
        {
            get { return _Item; }
            set { SetProperty(ref _Item, value); }
        }
        private OperationViewModel _Operation;
        public OperationViewModel Operation
        {
            get { return _Operation; }
            set { SetProperty(ref _Operation, value); }
        }
        //ProjectType
        private List<ProjectTypeViewModel> _ProjectTypes;
        public List<ProjectTypeViewModel> ProjectTypes
        {
            get { return _ProjectTypes; }
            set { SetProperty(ref _ProjectTypes, value); }
        }
        private ProjectTypeViewModel _ProjectType;
        public ProjectTypeViewModel ProjectType
        {
            get { return _ProjectType; }
            set { SetProperty(ref _ProjectType, value); }
        }
        //EventType
        private List<EventTypeViewModel> _EventTypes;
        public List<EventTypeViewModel> EventTypes
        {
            get { return _EventTypes; }
            set { SetProperty(ref _EventTypes, value); }
        }
        private EventTypeViewModel _EventType;
        public EventTypeViewModel EventType
        {
            get { return _EventType; }
            set { SetProperty(ref _EventType, value); }
        }
        //Status
        private List<StatusViewModel> _Statuses;
        public List<StatusViewModel> Statuses
        {
            get { return _Statuses; }
            set { SetProperty(ref _Statuses, value); }
        }
        private StatusViewModel _Status;
        public StatusViewModel Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }
        //users
        private List<UserViewModel> _Users;
        public List<UserViewModel> Users
        {
            get { return _Users; }
            set { SetProperty(ref _Users, value); }
        }
        private UserViewModel _User;
        public UserViewModel User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }

        //for dropdowns and selection dialog
        private List<DropdownItem> _SelectItemList;
        public List<DropdownItem> SelectItemList
        {
            get { return _SelectItemList; }
            set { SetProperty(ref _SelectItemList, value); }
        }
    }
}