namespace PMWelfare.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class welfare : DbContext
    {
        public welfare()
            : base("name=welfare")
        {
        }

        public virtual DbSet<activity_logs> activity_logs { get; set; }
        public virtual DbSet<celebration> celebrations { get; set; }
        public virtual DbSet<chat_room> chat_room { get; set; }
        public virtual DbSet<deposit> deposits { get; set; }
        public virtual DbSet<expens> expenses { get; set; }
        public virtual DbSet<member_status> member_status { get; set; }
        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<monthly_summary> monthly_summary { get; set; }
        public virtual DbSet<subscription> subscriptions { get; set; }
        public virtual DbSet<sup_products> sup_products { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<activity_logs>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<activity_logs>()
                .Property(e => e.device_ip)
                .IsUnicode(false);

            modelBuilder.Entity<activity_logs>()
                .Property(e => e.device_mac)
                .IsUnicode(false);

            modelBuilder.Entity<celebration>()
                .Property(e => e.event_name)
                .IsUnicode(false);

            modelBuilder.Entity<celebration>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<celebration>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<chat_room>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<chat_room>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<chat_room>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<deposit>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<deposit>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<deposit>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<deposit>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<expens>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<expens>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<member_status>()
                .Property(e => e.member_status1)
                .IsUnicode(false);

            modelBuilder.Entity<member_status>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<member_status>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<member_status>()
                .HasMany(e => e.members)
                .WithOptional(e => e.member_status1)
                .HasForeignKey(e => e.member_status);

            modelBuilder.Entity<member>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.other_name)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<member>()
                .Property(e => e.updated_by)
                .IsUnicode(false);


            modelBuilder.Entity<monthly_summary>()
                .Property(e => e.closing_balance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<monthly_summary>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<monthly_summary>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<subscription>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<subscription>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<subscription>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<subscription>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<sup_products>()
                .Property(e => e.prod_name)
                .IsUnicode(false);

            modelBuilder.Entity<sup_products>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<sup_products>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<sup_products>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.sup_tel)
                .IsFixedLength();

            modelBuilder.Entity<supplier>()
                .Property(e => e.sup_name)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

        }
    }
}
