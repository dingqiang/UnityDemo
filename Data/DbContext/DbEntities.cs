using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Entities;

namespace Data.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }

        public virtual DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}