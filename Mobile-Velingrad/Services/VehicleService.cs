using Microsoft.EntityFrameworkCore;
using Mobile_Velingrad.Data;
using Mobile_Velingrad.Data.Models;
using Mobile_Velingrad.ViewModels;

namespace Mobile_Velingrad.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AppDbContext appDbContext;

        public VehicleService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task CreateAsync(VehicleInputViewModel inputModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await this.appDbContext.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return false;
            }

            var extras = await this.appDbContext.Extras.FindAsync(id);
            this.appDbContext.Extras.Remove(extras);
            this.appDbContext.Vehicles.Remove(vehicle);
            await this.appDbContext.SaveChangesAsync();
            return true;
        }

        public Task<TopVehicleViewModel> GetLastAddedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TopVehicleViewModel> GetTopExpensiveVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<VehiclesViewModel> GetVehiclesAsync(int pageNumber = 1)
        {
            var model = new VehiclesViewModel();

            model.ElementsCount = await this.appDbContext.Vehicles.CountAsync();
            model.PageNumber = pageNumber;

            model.Vehicles = await this.appDbContext.Vehicles.Select(x => new VehicleViewModel()
            {
                Id = x.Id,
                VehicleTypeId = x.VehicleTypeId,
                AdvertDate = x.AdvertDate.ToString("MMMM dd, yyyy"),
                City = x.City.Name,
                Color = x.Color.Name,
                Engine = x.Engine.EngineType.Name,
                Price = x.Price,
                Run = x.Run,
                Brand = x.Model.Brand.Name,
                Tags = x.Tags.Select(t => t.Tag.Name).ToList()
            }).Skip(model.ItemsPerPage * (model.PageNumber - 1)).Take(model.ItemsPerPage).ToListAsync();

            return model;
        }

        public Task<TopVehicleViewModel> SearchByPriceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SearchVehiclesViewModel> SearchByPriceAsync(int minPrice, int maxPrice, int pageNumber)
        {
            throw new NotImplementedException();
        }

        private async Task<Tag> GetOrCreateTag(string tagName)
        {
            var tag = await this.appDbContext.Tags.FirstOrDefaultAsync(x => x.Name == tagName);

            if (tag == null)
            {
                tag = new Tag()
                {
                    Name = tagName
                };
                this.appDbContext.Tags.Add(tag);
                await this.appDbContext.SaveChangesAsync();
            }
            return tag;
        }

        private async Task UpdateTags(int vehicleId)
        {
            var vehicle = await this.appDbContext.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                return;
            }

            vehicle.Tags.Clear();

            if (vehicle.Price > 150000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Висок клас автомобил")
                });
            }

            else if (vehicle.Price < 150000 && vehicle.Price >= 10000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Среден клас автомобил")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Нисък клас автомобил")
                });
            }

            if (vehicle.Engine.HorsePower > 1700)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Много бърз автомобил")
                });
            }

            else if (vehicle.Engine.HorsePower < 1700 && vehicle.Engine.HorsePower >= 1000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Бърз автомобил")
                });
            }

            else if (vehicle.Engine.HorsePower < 1000 && vehicle.Engine.HorsePower >= 500)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Средно бърз автомобил")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Бавен автомобил")
                });
            }

            if (vehicle.Engine.Volume > 3000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Много голям двигател")
                });
            }

            else if (vehicle.Engine.Volume < 3000 && vehicle.Engine.Volume >= 2000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Голям двигател")
                });
            }

            else if (vehicle.Engine.Volume < 2000 && vehicle.Engine.Volume >= 1000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Средно голям двигател")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Малък двигател")
                });
            }

            if (vehicle.Run > 50000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Голям пробег")
                });
            }

            else if (vehicle.Run < 50000 && vehicle.Run >= 20000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Среден пробег")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Малък пробег")
                });
            }

            await this.appDbContext.SaveChangesAsync();
        }
    }
}
