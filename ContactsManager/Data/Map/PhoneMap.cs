using ContactsManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class PhoneMap : IEntityTypeConfiguration<PhoneModel>
{
    public void Configure(EntityTypeBuilder<PhoneModel> builder)
    {
        builder.HasKey(x => x.PhoneId);
        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15)
               .HasAnnotation("RegularExpression", @"^\+?[1-9]\d{1,14}$");
        builder.Property(x => x.PhoneNumberType).IsRequired();
        builder.Property(x => x.ContactId).IsRequired();
    }
}
