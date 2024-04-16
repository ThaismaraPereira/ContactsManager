using ContactsManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class EmailMap : IEntityTypeConfiguration<EmailModel>
{
    public void Configure(EntityTypeBuilder<EmailModel> builder)
    {
        builder.HasKey(x => x.EmailId);
        builder.Property(x => x.ContactId).IsRequired();
        builder.Property(x => x.EmailAddress)
               .IsRequired()
               .HasMaxLength(256)
               .HasAnnotation("RegularExpression", @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        builder.Property(x => x.EmailAddressType).IsRequired();
    }
}
