using Bogus;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Models;
using ProjektCmentarz.Data;

namespace ProjektCmentarz.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GraveyardContext(
                serviceProvider.GetRequiredService<DbContextOptions<GraveyardContext>>()))
            {
                
              //  if (context.Deceaseds.Any())
               // {
                  //  return;  
              //  }

               
                var deceasedFaker = new Faker<Deceased>("pl") 
                    .RuleFor(d => d.FirstName, f => f.Name.FirstName())
                    .RuleFor(d => d.Surname, f => f.Name.LastName())
                    .RuleFor(d => d.BirthDate, f => f.Date.Past(80, DateTime.Now.AddYears(-20)))
                    .RuleFor(d => d.DeathDate, (f, d) => f.Date.Between(d.BirthDate, DateTime.Now));

                
                var deceasedList = deceasedFaker.Generate(50);

               
                context.Deceaseds.AddRange(deceasedList);
                context.SaveChanges();
            }
        }
    }
}
