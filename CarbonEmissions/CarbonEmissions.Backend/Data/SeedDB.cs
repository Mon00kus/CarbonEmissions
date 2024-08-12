
using CarbonEmissions.Shared.Entities;
using CarbonEmissions.Shared.Enums;

namespace CarbonEmissions.Backend.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCompaniesAsync();
         
        }        

        private async Task CheckCompaniesAsync()
        {
            if (!_context.Companies.Any())
            {
                _context.Companies.Add(new Company
                {
                    CompanyName = "Exxon Mobile",
                    Emissions =
                    [
                        new Emission()
                        {
                            Description = "Gas OC2 producdo en Planta",
                            Quantity = (decimal)1992.24,
                            EmissionType = EmissionType.DirectEmissions,
                            EmissionDate = DateTime.Now,
                        },
                    ]
                });
                _context.Companies.Add(new Company
                {
                    CompanyName = "Chevron",
                    Emissions =
                        [
                            new Emission()
                        {
                            Description = "Gas OC2 producdo en Planta",
                            Quantity = (decimal)1223.76,
                            EmissionType = EmissionType.DirectEmissions,
                            EmissionDate = DateTime.Now,
                        },
                    ]
                });
                _context.Companies.Add(new Company
                {
                    CompanyName = "Haliburton",
                    Emissions =
                        [
                            new Emission()
                        {
                            Description = "Gas OC2 producdo en Planta",
                            Quantity = (decimal)983.09,
                            EmissionType = EmissionType.DirectEmissions,
                            EmissionDate = DateTime.Now,
                        },
                    ]
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
