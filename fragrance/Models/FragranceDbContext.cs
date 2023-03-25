using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace fragrance.Models
{
    public partial class FragranceDbContext : DbContext
    {
        public FragranceDbContext()
            : base("name=FragranceDbContext1")
        {
        }

        public virtual DbSet<acc_user> acc_user { get; set; }
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<collection> collections { get; set; }
        public virtual DbSet<collection_child> collection_child { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<order_details> order_details { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<product_type> product_type { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user_order> user_order { get; set; }
        public virtual DbSet<message> messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<acc_user>()
                .Property(e => e.name_user)
                .IsUnicode(false);

            modelBuilder.Entity<acc_user>()
                .Property(e => e.email_user)
                .IsUnicode(false);

            modelBuilder.Entity<acc_user>()
                .Property(e => e.phone_user)
                .IsUnicode(false);

            modelBuilder.Entity<acc_user>()
                .Property(e => e.password_user)
                .IsUnicode(false);

            modelBuilder.Entity<acc_user>()
                .HasMany(e => e.user_order)
                .WithOptional(e => e.acc_user)
                .HasForeignKey(e => e.id_order_user);

            modelBuilder.Entity<admin>()
                .Property(e => e.name_ad)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.email_ad)
                .IsUnicode(false);

            modelBuilder.Entity<admin>()
                .Property(e => e.password_ad)
                .IsUnicode(false);

            modelBuilder.Entity<collection>()
                .Property(e => e.name_collect)
                .IsUnicode(false);

            modelBuilder.Entity<collection>()
                .Property(e => e.image_collect)
                .IsUnicode(false);

            modelBuilder.Entity<collection>()
                .HasMany(e => e.collection_child)
                .WithOptional(e => e.collection)
                .HasForeignKey(e => e.id_c_collect);

            modelBuilder.Entity<collection_child>()
                .Property(e => e.name_c_child)
                .IsUnicode(false);

            modelBuilder.Entity<collection_child>()
                .HasMany(e => e.products)
                .WithOptional(e => e.collection_child)
                .HasForeignKey(e => e.id_pro_coll);

            modelBuilder.Entity<product>()
                .Property(e => e.name_pr)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.image_pr)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price_pr)
                .HasPrecision(10, 4);

            modelBuilder.Entity<product>()
                .HasMany(e => e.order_details)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.id_pro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product_type>()
                .Property(e => e.name_prt)
                .IsUnicode(false);

            modelBuilder.Entity<product_type>()
                .Property(e => e.image_prt)
                .IsUnicode(false);

            modelBuilder.Entity<product_type>()
                .Property(e => e.forgender_prt)
                .IsUnicode(false);

            modelBuilder.Entity<product_type>()
                .HasMany(e => e.products)
                .WithOptional(e => e.product_type)
                .HasForeignKey(e => e.id_pro_typeof);

            modelBuilder.Entity<user_order>()
                .Property(e => e.receiver_oder)
                .IsUnicode(false);

            modelBuilder.Entity<user_order>()
                .Property(e => e.status_order)
                .IsUnicode(false);

            modelBuilder.Entity<user_order>()
                .Property(e => e.address_order)
                .IsUnicode(false);

            modelBuilder.Entity<user_order>()
                .Property(e => e.phone_order)
                .IsUnicode(false);

            modelBuilder.Entity<user_order>()
                .HasMany(e => e.order_details)
                .WithRequired(e => e.user_order)
                .HasForeignKey(e => e.id_order_dt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<message>()
                .Property(e => e.fullname_mes)
                .IsUnicode(false);

            modelBuilder.Entity<message>()
                .Property(e => e.email_mes)
                .IsUnicode(false);
        }
    }
}
