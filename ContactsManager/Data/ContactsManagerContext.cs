using ContactsManager.Data.Map;
using ContactsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Data
{
    public class ContactsManagerContext : DbContext
    {
        public ContactsManagerContext(DbContextOptions<ContactsManagerContext> options) : base(options) { }

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<EmailModel> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new PhoneMap());
            modelBuilder.ApplyConfiguration(new EmailMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
