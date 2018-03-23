namespace NeoTracker.DAL
{
    using Models;
    using System;
    using System.Data.Entity;
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

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUsers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
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
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.Now; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedAt = now;
                }
                ((EntityBase)entity.Entity).UpdatedAt = now;
                ((EntityBase)entity.Entity).UpdatedBy = GetCurrentUserID();
            }
        }
        protected string GetCurrentUserID()
        {
            return App.vm.CurrentUSer.Email;
        }
    }
}