using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.EF.Contexts
{
    public class AppDbContext : AuditableDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CartConfigurations());
            builder.ApplyConfiguration(new CartItemConfigurations());
            builder.ApplyConfiguration(new BillConfigurations());
            builder.ApplyConfiguration(new BillDetailsConfigurations());
            builder.ApplyConfiguration(new ComboConfigurations());
            builder.ApplyConfiguration(new ProductComboConfigurations());
            builder.ApplyConfiguration(new ProductConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());
            builder.ApplyConfiguration(new UserConfigurations());
            builder.ApplyConfiguration(new BillDeliveryConfigurations());
            builder.ApplyConfiguration(new BillNotesConfigurations());
            builder.ApplyConfiguration(new BillPaymentConfigurations());
            builder.ApplyConfiguration(new ComboItemsArchiveConfigurations());
            builder.ApplyConfiguration(new UserRoleConfigurations());
            builder.ApplyConfiguration(new RoleConfigurations());
            builder.ApplyConfiguration(new SizesConfigurations());
            builder.ApplyConfiguration(new CategoriesConfigurations());
            builder.ApplyConfiguration(new StoreConfigurations());


            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetails> BillDetailses { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCombo> productCombos { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }

        public DbSet<BillDelivery> BillDeliveries { get; set; }
        public DbSet<BillNotes> BillNotes { get; set; }
        public DbSet<BillPayment> BillPayments { get; set; }
        public DbSet<ComboItemsArchive> ComboItemsArchives { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Categories> Categories { get; set; }


    }
}
