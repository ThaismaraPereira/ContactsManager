using ContactsManager.Data;
using ContactsManager.Repositories.Interfaces;
using ContactsManager.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactsManager.Models;
using EmailsManager.Repositories;
using System.Text.Json.Serialization;

namespace ContactsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ContactsManagerContext>(options =>
                options.UseInMemoryDatabase("ContactsManager"));

            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
            builder.Services.AddScoped<IEmailRepository, EmailRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            InitializeDatabase(app);

            app.Run();
        }

        public static void InitializeDatabase(IApplicationBuilder app) // Populating database in memory
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ContactsManagerContext>();

                var contacts = new List<ContactModel>
                {
                    new ContactModel { Id = 1, Name = "Nelson Lucas Costa", CPF = "815.317.239-50", BirthDay = DateTime.Now },
                    new ContactModel { Id = 2, Name = "Pedro Bruno Cláudio Duarte", CPF = "913.590.139-03", BirthDay = DateTime.Parse("18/03/1972") },
                    new ContactModel { Id = 3, Name = "Gustavo Raimundo Ribeiro", CPF = "800.194.959-19", BirthDay = DateTime.Parse("09/01/1978") },
                    new ContactModel { Id = 4, Name = "Francisco Giovanni Benício Bernardes", CPF = "731.897.831-44", BirthDay = DateTime.Parse("19/02/1994") },
                    new ContactModel { Id = 5, Name = "Henry Anderson Breno Oliveira", CPF = "083.823.809-21", BirthDay = DateTime.Parse("20/03/1993") },
                    new ContactModel { Id = 6, Name = "Augusto Vitor Matheus da Cunha", CPF = "391.125.300-18", BirthDay = DateTime.Parse("22/01/1983") },
                    new ContactModel { Id = 7, Name = "Lucca Fernando Viana", CPF = "794.986.214-97", BirthDay = DateTime.Parse("14/02/1950") },
                    new ContactModel { Id = 8, Name = "Elaine Mariah Fogaça", CPF = "885.174.538-24", BirthDay = DateTime.Parse("19/01/1986") },
                    new ContactModel { Id = 9, Name = "Cláudio Mateus Kevin da Luz", CPF = "560.783.120-57", BirthDay = DateTime.Parse("21/01/1971") },
                    new ContactModel { Id = 10, Name = "Yasmin Sara Sara Nogueira", CPF = "476.369.945-85", BirthDay = DateTime.Parse("14/02/1947") },
                };
                context.Contacts.AddRange(contacts);

                var phones = new List<PhoneModel>
                {
                    new PhoneModel { PhoneId = 1, ContactId = 1, PhoneNumber = "(44) 2770-2364", PhoneNumberType = Util.Enums.PhoneNumberType.Residencial },
                    new PhoneModel { PhoneId = 2, ContactId = 2, PhoneNumber = "(71) 2731-3946", PhoneNumberType = Util.Enums.PhoneNumberType.Residencial },
                    new PhoneModel { PhoneId = 3, ContactId = 2, PhoneNumber = "(71) 99439-0757", PhoneNumberType = Util.Enums.PhoneNumberType.Celular },
                    new PhoneModel { PhoneId = 4, ContactId = 3, PhoneNumber = "(65) 2710-4861", PhoneNumberType = Util.Enums.PhoneNumberType.Residencial },
                    new PhoneModel { PhoneId = 5, ContactId = 4, PhoneNumber = "(61) 3892-9744", PhoneNumberType = Util.Enums.PhoneNumberType.Comercial },
                    new PhoneModel { PhoneId = 6, ContactId = 5, PhoneNumber = "(82) 99877-1748", PhoneNumberType = Util.Enums.PhoneNumberType.Comercial },
                    new PhoneModel { PhoneId = 7, ContactId = 6, PhoneNumber = "(61) 98993-8635", PhoneNumberType = Util.Enums.PhoneNumberType.Celular },
                    new PhoneModel { PhoneId = 8, ContactId = 7, PhoneNumber = "(54) 98760-8716", PhoneNumberType = Util.Enums.PhoneNumberType.Celular },
                    new PhoneModel { PhoneId = 9, ContactId = 8, PhoneNumber = "(66) 3756-5883", PhoneNumberType = Util.Enums.PhoneNumberType.Residencial },
                    new PhoneModel { PhoneId = 10, ContactId = 8, PhoneNumber = "(66) 98685-1791", PhoneNumberType = Util.Enums.PhoneNumberType.Celular },
                    new PhoneModel { PhoneId = 11, ContactId = 9, PhoneNumber = "(27) 3649-0231", PhoneNumberType = Util.Enums.PhoneNumberType.Comercial },
                    new PhoneModel { PhoneId = 12, ContactId =  10, PhoneNumber = "(21) 2918-7812", PhoneNumberType = Util.Enums.PhoneNumberType.Comercial },
                    new PhoneModel { PhoneId = 13, ContactId =  10, PhoneNumber = "(21) 99307-8484", PhoneNumberType = Util.Enums.PhoneNumberType.Celular },
                };
                context.Phones.AddRange(phones);

                var emails = new List<EmailModel>
                {
                    new EmailModel { EmailId = 1, ContactId = 1, EmailAddress = "nelson_lucas_costa@br.com.br", EmailAddressType = Util.Enums.EmailAddressType.Pessoal },
                    new EmailModel { EmailId = 2, ContactId = 1, EmailAddress = "nelsonlucascosta@br.com.br", EmailAddressType = Util.Enums.EmailAddressType.Corporativo },
                    new EmailModel { EmailId = 3, ContactId = 2, EmailAddress = "pedro_duarte@toyota.com.br", EmailAddressType = Util.Enums.EmailAddressType.Corporativo },
                    new EmailModel { EmailId = 4, ContactId = 3, EmailAddress = "gustavo_ribeiro@destaco.com", EmailAddressType = Util.Enums.EmailAddressType.Estudantil },
                    new EmailModel { EmailId = 5, ContactId = 4, EmailAddress = "franciscogiovannibernardes@enox.com.br", EmailAddressType = Util.Enums.EmailAddressType.Pessoal },
                    new EmailModel { EmailId = 6, ContactId = 5, EmailAddress = "henry-oliveira76@expressoforte.com.br", EmailAddressType = Util.Enums.EmailAddressType.Estudantil },
                    new EmailModel { EmailId = 7, ContactId = 6, EmailAddress = "augustovitordacunha@bemarius.com.br", EmailAddressType = Util.Enums.EmailAddressType.Pessoal },
                    new EmailModel { EmailId = 8, ContactId = 7, EmailAddress = "lucca_fernando_viana@emitec.com.br", EmailAddressType = Util.Enums.EmailAddressType.Corporativo },
                    new EmailModel { EmailId = 9, ContactId = 8, EmailAddress = "elaine-fogaca93@splicenet.com.br", EmailAddressType = Util.Enums.EmailAddressType.Pessoal },
                    new EmailModel { EmailId = 10, ContactId = 9, EmailAddress = "claudio.mateus.daluz@utbr.com.br", EmailAddressType = Util.Enums.EmailAddressType.Estudantil },
                };
                context.Emails.AddRange(emails);

                context.SaveChanges();
            }
        }
    }
}
