using Data.Entities;
using Data.Enum;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DbContext.DbEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.DbContext.DbEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Student.AddOrUpdate(s => s.Id, new Student()
            {
                Age = 22,
                CreateTime = DateTime.Now,
                DeleteTime = InitTime.Time,
                Id = Guid.NewGuid(),
                IsDel = false,
                Name = "ะกรื",
                Sex = (byte)Sex.ฤะ
            });
            //
        }
    }
}
