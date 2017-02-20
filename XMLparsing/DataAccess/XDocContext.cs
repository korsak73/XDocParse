using System;
using System.Data.Entity;
using XMLParsing.Models;

namespace XMLParsing
{
    public class XDocContext : DbContext
    {
        public XDocContext()
        { }
        /// <summary>
        /// We can specify the database name the context creates or maps.
        /// </summary>
        /// <param name="databaseName">the database name the context creates or maps</param>
        public XDocContext(String databaseName)
            : base(databaseName)
        {
            //Database.SetInitializer<MySchoolContext>(new CustomInitializer<MySchoolContext>());
            //Database.SetInitializer<MySchoolContext>(new DropCreateDatabaseIfModelChanges<MySchoolContext>());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<MySchoolContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}
        //public DbSet<Cart> Carts { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<BillingInfo> Billings { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
    }
    //public class CustomInitializer<T> : DropCreateDatabaseIfModelChanges<MySchoolContext>
    //{
    //    protected override void Seed(MySchoolContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}
