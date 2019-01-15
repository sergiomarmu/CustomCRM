using System.Data.Entity;

namespace CompanyCRM
{
    public class CompanyCRMContext:DbContext
    {
        public CompanyCRMContext() : base("name=companycrmdb")
        {

        }

        public virtual DbSet<Productes> productsItem { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Factura> Invoice { get; set; }
        public virtual DbSet<Factura_Detail> Invoice_Detail { get; set; }
        public virtual DbSet<LoginUser> Login { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("account");
            modelBuilder.Entity<Factura_Detail>().HasKey<int>(l => l.n_Factura);
            modelBuilder.Entity<Factura_Detail>().HasKey<int>(l => l.id_Producte);
        }
    }
}
