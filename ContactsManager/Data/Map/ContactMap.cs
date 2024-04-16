using ContactsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Principal;

namespace ContactsManager.Data.Map
{
    public class ContactMap : IEntityTypeConfiguration<ContactModel>
    {
        public void Configure(EntityTypeBuilder<ContactModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.CPF).IsRequired().HasMaxLength(14);
            PropertyBuilder<DateTime?> propertyBuilderBirthDay = builder.Property(x => x.BirthDay).IsRequired();
            PropertyBuilder<bool> propertyBuilderIsActive = builder.Property(x => x.IsActive).IsRequired();

            builder.HasMany(x => x.Phones).WithOne(x => x.Contact).HasForeignKey(x => x.ContactId);
            builder.HasMany(x => x.Emails).WithOne(x => x.Contact).HasForeignKey(x => x.ContactId);
        }
    }

}
