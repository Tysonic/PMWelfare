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

        public virtual DbSet<ActivityLogs> ActivityLogs { get; set; }
        public virtual DbSet<Celebration> Celebrations { get; set; }
        public virtual DbSet<ChatRoom> chatRoom { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Celebrant> Celebrants { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<MemberStatus> Member_Status { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MonthlySummary> Monthlysummary { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SupProducts> SupProducts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ActivityLogs>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ActivityLogs>()
                .Property(e => e.DeviceIp)
                .IsUnicode(false);

            modelBuilder.Entity<ActivityLogs>()
                .Property(e => e.DeviceMac)
                .IsUnicode(false);

            modelBuilder.Entity<Celebration>()
                .Property(e => e.EventName)
                .IsUnicode(false);

            modelBuilder.Entity<Celebration>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Celebration>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ChatRoom>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<ChatRoom>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<ChatRoom>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<Deposit>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Deposit>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Deposit>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Deposit>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Expense>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Expense>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MemberStatus>()
                .Property(e => e.MemberStatus1)
                .IsUnicode(false);

            modelBuilder.Entity<MemberStatus>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity < MemberStatus > ()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MemberStatus>()
                .HasMany(e => e.Members)
                .WithOptional(e => e.MemberStatus1)
                .HasForeignKey(e => e.MemberStatus);

            modelBuilder.Entity<Member>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.OtherName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);


            modelBuilder.Entity<MonthlySummary>()
                .Property(e => e.ClosingBalance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MonthlySummary>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MonthlySummary>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SupProducts>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<SupProducts>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SupProducts>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SupProducts>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.SupTel)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.SupName)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

        }
    }
}
