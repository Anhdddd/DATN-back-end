using System.Text.Json;
using DATN_back_end.Data;
using DATN_back_end.Entities;

namespace DATN_back_end.Data.Seed
{
    public class Seed
    {
        public static void SeedAll(DataContext context)
        {
            SeedOccupation(context);
            SeedCity(context);
        }

        public static void SeedOccupation(DataContext context)
        {
            if (context.Occupations.Any()) return;

            context.Occupations.AddRange(
                new Occupation { Name = "Software Engineer" },
                new Occupation { Name = "Data Scientist" },
                new Occupation { Name = "Business Analyst" },
                new Occupation { Name = "Project Manager" },
                new Occupation { Name = "QA/QC" },
                new Occupation { Name = "DevOps" },
                new Occupation { Name = "System Engineer" },
                new Occupation { Name = "Network Engineer" },
                new Occupation { Name = "Security Engineer" },
                new Occupation { Name = "Frontend Developer" },
                new Occupation { Name = "Backend Developer" },
                new Occupation { Name = "Fullstack Developer" },
                new Occupation { Name = "Mobile Developer" },
                new Occupation { Name = "UI/UX Designer" },
                new Occupation { Name = "Product Manager" },
                new Occupation { Name = "Scrum Master" },
                new Occupation { Name = "HR" },
                new Occupation { Name = "Recruitment" },
                new Occupation { Name = "Marketing" },
                new Occupation { Name = "Sales" },
                new Occupation { Name = "Customer Service" },
                new Occupation { Name = "Accountant" },
                new Occupation { Name = "Finance" },
                new Occupation { Name = "Logistic" },
                new Occupation { Name = "Supply Chain" },
                new Occupation { Name = "Procurement" },
                new Occupation { Name = "Warehouse" },
                new Occupation { Name = "Production" },
                new Occupation { Name = "Manufacturing" },
                new Occupation { Name = "Mechanical Engineer" },
                new Occupation { Name = "Electrical Engineer" },
                new Occupation { Name = "Civil Engineer" },
                new Occupation { Name = "Construction" },
                new Occupation { Name = "Architect" },
                new Occupation { Name = "Interior Designer" },
                new Occupation { Name = "Real Estate" },
                new Occupation { Name = "Property Management" },
                new Occupation { Name = "Legal" },
                new Occupation { Name = "Compliance" },
                new Occupation { Name = "Administration" },
                new Occupation { Name = "Secretary" },
                new Occupation { Name = "Receptionist" },
                new Occupation { Name = "Office Manager" },
                new Occupation { Name = "Personal Assistant" },
                new Occupation { Name = "Driver" });
        }

        public static void SeedCity(DataContext context)
        {
            if (context.ProvinceOrCities.Any()) return;

            var locationText = System.IO.File.ReadAllText("Data\\Seed\\Vietnam-location\\cities.json");
            var cities = JsonSerializer.Deserialize<List<CitySeedDto>>(locationText);
            var cityEntities = new List<ProvinceOrCity>();

            if (cities == null)
            {
                return;
            }
            foreach (var city in cities)
            {
                cityEntities.Add(new ProvinceOrCity()
                {
                    Name = city.name
                });
            }
            context.AddRange(cityEntities);
            context.SaveChanges();
        }
    }

    public class CitySeedDto
    {
        public string? name { get; set; }

        public string? code { get; set; }

        public string? slug { get; set; }

        public string? type { get; set; }

        public string? name_with_type { get; set; }
    }
}