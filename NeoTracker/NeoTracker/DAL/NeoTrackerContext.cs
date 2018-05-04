namespace NeoTracker.DAL
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Threading.Tasks;

    public class NeoTrackerContext : DbContext
    {
        // Your context has been configured to use a 'NeoTrackerContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NeoTracker.DAL.NeoTrackerContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'NeoTrackerContext' 
        // connection string in the application configuration file.
        public NeoTrackerContext()
            : base("name=NeoTrackerContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public virtual DbSet<ChangeLog> ChangeLogs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUsers { get; set; }
        public virtual DbSet<DepartmentOperation> DepartmentOperations { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();

            foreach (var change in modifiedEntities)
            {
                var now = DateTime.Now; // current datetime
                string user = GetCurrentUserID();

                if (change.State == EntityState.Added)
                {
                    ((EntityBase)change.Entity).CreatedAt = now;
                    ((EntityBase)change.Entity).CreatedBy = user;
                    ((EntityBase)change.Entity).UpdatedAt = now;
                    ((EntityBase)change.Entity).UpdatedBy = user;
                }
                else
                {
                    var entityName = change.Entity.GetType().Name;
                    var primaryKey = GetPrimaryKeyValue(change);
                    var DatabaseValues = change.GetDatabaseValues();

                    foreach (var prop in change.OriginalValues.PropertyNames.Where(x => !x.Equals("UpdatedBy") && !x.Equals("UpdatedAt") && !x.Equals("CreatedBy") && !x.Equals("CreatedAt")))
                    {
                        //if (prop == "ItemID")
                        //{
                        //    //var x = DatabaseValues.GetValue<object>(prop).GetType();
                        //}

                        string originalValue = string.Empty;
                        string currentValue = string.Empty;

                        if (DatabaseValues.GetValue<object>(prop) != null)
                        {
                            switch (Type.GetTypeCode(DatabaseValues.GetValue<object>(prop).GetType()))
                            {
                                case TypeCode.DateTime:
                                    originalValue = string.Format("{0:yyyy-MM-dd}", DatabaseValues.GetValue<object>(prop));
                                    break;
                                case TypeCode.Decimal:
                                    originalValue = string.Format("{0:0.00}", DatabaseValues.GetValue<object>(prop));
                                    break;
                                default:
                                    originalValue = DatabaseValues.GetValue<object>(prop).ToString();
                                    break;
                            }
                        }
                        if(change.CurrentValues[prop] != null)
                        {
                            switch (Type.GetTypeCode(change.CurrentValues[prop].GetType()))
                            {
                                case TypeCode.DateTime:                                  
                                    currentValue = string.Format("{0:yyyy-MM-dd}", change.CurrentValues[prop]);
                                    break;
                                case TypeCode.Decimal:                             
                                    currentValue = string.Format("{0:0.00}", change.CurrentValues[prop]);
                                    break;
                                default:
                                    currentValue = change.CurrentValues[prop].ToString();
                                    break;
                            }
                        }
                        if (originalValue != currentValue) //Only create a log if the value changes
                        {
                            ChangeLogs.Add(new ChangeLog()
                            {
                                EntityName = entityName,
                                //NewValue = currentValue,
                                OldValue = originalValue,
                                PrimaryKeyValue = int.Parse(primaryKey.ToString()),
                                UpdatedAt = now,
                                UpdatedBy = user,
                                PropertyName = prop,
                            });
                        }
                    }
                    ((EntityBase)change.Entity).UpdatedAt = now;
                    ((EntityBase)change.Entity).UpdatedBy = user;
                }
            }
        }
        protected string GetCurrentUserID()
        {
            return App.vm.CurrentUser.Email;
        }
        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}