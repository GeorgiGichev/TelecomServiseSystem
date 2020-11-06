namespace TelecomServiceSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TelecomServiceSystem.Data.Models;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> appUser)
        {
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasIndex(u => u.EGN)
                .IsUnique();

            appUser
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public void Configure(EntityTypeBuilder<Customer> customer)
        {
            customer.HasIndex(c => c.Email)
                .IsUnique();

            customer.HasIndex(c => c.PersonalNumber)
                .IsUnique();
        }

        public void Configure(EntityTypeBuilder<Order> order)
        {
            order.HasIndex(o => o.EnginieringTaskId)
                .IsUnique();
        }

        public void Configure(EntityTypeBuilder<EnginieringTask> task)
        {
            task.HasIndex(t => t.OrderId)
                .IsUnique();
        }
    }
}
