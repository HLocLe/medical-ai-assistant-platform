using MedMateAI.Domain.Entities;
using MedMateAI.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedMateAI.Infrastructure.Persistence.FluentAPiConfiguration;

public sealed class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("PaymentTransaction");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("PaymentTransactionId").ValueGeneratedOnAdd();

        builder.Property(x => x.TransactionReference)
            .HasMaxLength(100);

        builder.Property(x => x.ProviderTransactionId)
            .HasMaxLength(100);

        builder.Property(x => x.ProviderResponseCode)
            .HasMaxLength(20);

        builder.Property(x => x.ProviderTransactionStatus)
            .HasMaxLength(20);

        builder.Property(x => x.BankCode)
            .HasMaxLength(50);

        builder.Property(x => x.CardType)
            .HasMaxLength(50);

        builder.Property(x => x.OrderInfo)
            .HasMaxLength(500);

        builder.Property(x => x.RawResponse)
            .HasColumnType("text");

        builder.HasOne(x => x.Payment)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>()
            .WithMany(x => x.PaymentTransactions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UserSubscription)
            .WithMany(x => x.PaymentTransactions)
            .HasForeignKey(x => x.UserSubscriptionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PaymentId);
        builder.HasIndex(x => x.TransactionReference);
        builder.HasIndex(x => x.ProviderTransactionId);
    }
}
