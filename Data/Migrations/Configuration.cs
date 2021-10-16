namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DataModels.CFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.DataModels.CFContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            try
            {
                InitDatabaseProvider.SetupRolesGroups(context);
                InitDatabaseProvider.GrantDefaultRolesForGroups(context);
                InitDatabaseProvider.CreateSuperUser(context);

                //InitDatabaseProvider.CreateSlide(context);
                //InitDatabaseProvider.CreatePage(context);
                //InitDatabaseProvider.CreateContactDetail(context);
                //InitDatabaseProvider.CreateConfigTitle(context);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

        }
    }
}
