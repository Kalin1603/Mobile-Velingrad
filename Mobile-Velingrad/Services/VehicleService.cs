﻿using Microsoft.EntityFrameworkCore;
using Mobile_Velingrad.Data;
using Mobile_Velingrad.Data.Models;
using Mobile_Velingrad.Importer;
using Mobile_Velingrad.ViewModels;
using Newtonsoft.Json;

namespace Mobile_Velingrad.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly AppDbContext appDbContext;

        public VehicleService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task CreateAsync(VehicleInputViewModel inputModel)
        {
            bool isInvalid = string.IsNullOrWhiteSpace(inputModel.Brand) || string.IsNullOrWhiteSpace(inputModel.Model) || string.IsNullOrWhiteSpace(inputModel.City) || string.IsNullOrWhiteSpace(inputModel.Country);

            if (isInvalid)
            {
                throw new ArgumentException("Invalid vehicle data!");
            }

            var brand = await this.appDbContext.Brands.FirstOrDefaultAsync(b => b.Name == inputModel.Brand);

            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = inputModel.Brand
                };
            }

            var model = await this.appDbContext.Models.FirstOrDefaultAsync(m => m.Name == inputModel.Model && m.Brand.Name == inputModel.Brand);

            if (model == null)
            {
                model = new Model()
                {
                    Name = inputModel.Model,
                    Brand = brand
                };
            }

            var engineType = await this.appDbContext.EngineTypes.FirstOrDefaultAsync(et => et.Name == inputModel.EngineType);

            if (engineType == null)
            {
                engineType = new EngineType()
                {
                    Name = inputModel.EngineType
                };
            }

            var engine = await this.appDbContext.Engines.FirstOrDefaultAsync(e => e.HorsePower == inputModel.HorsePower && e.Volume == inputModel.EngineVolume && e.EngineType.Name == inputModel.EngineType);

            if (engine == null)
            {
                engine = new Engine()
                {
                    HorsePower = inputModel.HorsePower,
                    Volume = inputModel.EngineVolume,
                    EngineType = engineType
                };
            }

            var country = await this.appDbContext.Countries.FirstOrDefaultAsync(c => c.Name == inputModel.Country);

            if (country == null)
            {
                country = new Country()
                {
                    Name = inputModel.Country
                };
            }

            var city = await this.appDbContext.Cities.FirstOrDefaultAsync(c => c.Name == inputModel.City && c.Country.Name == inputModel.Country);

            if (city == null)
            {
                city = new City()
                {
                    Name = inputModel.City,
                    Country = country,
                    ZipCode = inputModel.ZipCode
                };
            }

            var color = await this.appDbContext.Colors.FirstOrDefaultAsync(c => c.Name == inputModel.Color);

            if (color == null)
            {
                color = new Color()
                {
                    Name = inputModel.Color
                };
            }

            var extrasPackage = new ExtrasPackage()
            {
                HasABS = inputModel.HasABS,
                HasAllWheelDriveSystem = inputModel.HasAllWheelDriveSystem,
                HasCentralLock = inputModel.HasCentralLock,
                HasClimatronic = inputModel.HasClimatronic,
                HasCruiseControl = inputModel.HasCruiseControl,
                HasDVD = inputModel.HasDVD,
                HasElectricWindows = inputModel.HasElectricWindows,
                HasParkAssist = inputModel.HasParkAssist,
                HasRadioBluetooth = inputModel.HasRadioBluetooth,
                HasStabilityControl = inputModel.HasStabilityControl
            };

            var vehicleType = await this.appDbContext.VehicleTypes.FirstOrDefaultAsync(vt => vt.Name == inputModel.VehicleType);

            if (vehicleType == null)
            {
                vehicleType = new VehicleType()
                {
                    Name = inputModel.VehicleType
                };
            }

            var vehicle = new Vehicle()
            {
                Price = inputModel.Price,
                Run = inputModel.Run,
                Engine = engine,
                City = city,
                Model = model,
                Color = color,
                ExtrasPackage = extrasPackage,
                VehicleType = vehicleType,
                AdvertDate = inputModel.AdvertDate
            };

           await this.appDbContext.AddAsync(vehicle);
           await this.appDbContext.SaveChangesAsync();
           await this.UpdateTags(vehicle.Id);
        }

        private async Task<bool> VehicleExistsAsync(VehicleInputViewModel inputModel)
        {
            return await this.appDbContext.Vehicles.AnyAsync(v =>
                v.Model.Name == inputModel.Model &&
                v.Model.Brand.Name == inputModel.Brand &&
                v.City.Name == inputModel.City &&
                v.AdvertDate == inputModel.AdvertDate);
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

        public async Task<TopVehicleViewModel> GetLastAddedVehiclesAsync()
        {
            var model = new TopVehicleViewModel();

            model.VehicleViewModels = await this.appDbContext.Vehicles.OrderByDescending(x => x.AdvertDate).Take(6).Select(x => new VehicleViewModel()
            {
                AdvertDate = x.AdvertDate,
                City = x.City.Name,
                Color = x.Color.Name,
                Engine = x.Engine.EngineType.Name,
                Model = x.Model.Name,
                Price = x.Price,
                Run = x.Run,
                Brand = x.Model.Brand.Name
            }).ToListAsync();

            return model;
        }

        public async Task<TopVehicleViewModel> GetTopExpensiveVehiclesAsync()
        {
            var model = new TopVehicleViewModel();

            model.VehicleViewModels = await this.appDbContext.Vehicles.OrderByDescending(x => x.Price).Take(6).Select(x => new VehicleViewModel()
            {
                AdvertDate = x.AdvertDate,
                City = x.City.Name,
                Color = x.Color.Name,
                Engine = x.Engine.EngineType.Name,
                Model = x.Model.Name,
                Price = x.Price,
                Run = x.Run,
                Brand = x.Model.Brand.Name
            }).ToListAsync();

            return model;
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
                AdvertDate = x.AdvertDate,
                City = x.City.Name,
                Color = x.Color.Name,
                Engine = x.Engine.EngineType.Name,
                ExtrasPackageId = x.ExtrasPackageId,
                Model = x.Model.Name,
                Price = x.Price,
                Run = x.Run,
                Brand = x.Model.Brand.Name,
                Tags = x.Tags.Select(t => t.Tag.Name).ToList()
            }).Skip(model.ItemsPerPage * (model.PageNumber - 1)).Take(model.ItemsPerPage).ToListAsync();

            return model;
        }

        public async Task<TopVehicleViewModel> SearchByPriceAsync()
        {
            var model = new TopVehicleViewModel();

            model.VehicleViewModels = await this.appDbContext.Vehicles.Where(x => x.Price > 0).OrderBy(x => x.Price).Take(6).Select(x => new VehicleViewModel()
            {
                AdvertDate = x.AdvertDate,
                City = x.City.Name,
                Color = x.Color.Name,
                Engine = x.Engine.EngineType.Name,
                Model = x.Model.Name,
                Price = x.Price,
                Run = x.Run,
                Brand = x.Model.Brand.Name
            }).ToListAsync();

            return model;
        }

        public async Task<SearchVehiclesViewModel> SearchByPriceAsync(int minPrice, int maxPrice, int pageNumber)
        {
            var model = new SearchVehiclesViewModel();

            model.ElementsCount = await this.appDbContext.Vehicles.CountAsync();
            model.PageNumber = pageNumber;
            model.MinPrice = minPrice;
            model.MaxPrice = maxPrice;

            model.Vehicles = await this.appDbContext.Vehicles.OrderBy(x => x.Price).Where(x => x.Price >= minPrice && x.Price <= maxPrice).Select(x => new VehicleViewModel()
            {
                Id = x.Id,
                VehicleTypeId = x.VehicleTypeId,
                AdvertDate = x.AdvertDate,
                City = x.City.Name,
                Color = x.Color.Name,
                Engine = x.Engine.EngineType.Name,
                ExtrasPackageId = x.ExtrasPackageId,
                Model = x.Model.Name,
                Price = x.Price,
                Run = x.Run,
                Brand = x.Model.Brand.Name,
                Tags = x.Tags.Select(t => t.Tag.Name).ToList()
            }).Skip(model.ItemsPerPage * (model.PageNumber - 1)).Take(model.ItemsPerPage).ToListAsync();

            return model;
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
                    Tag = await this.GetOrCreateTag("High class car")
                });
            }

            else if (vehicle.Price < 150000 && vehicle.Price >= 10000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Mid-range car")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("A low class car")
                });
            }

            if (vehicle.Engine.HorsePower > 1700)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("A very fast car")
                });
            }

            else if (vehicle.Engine.HorsePower < 1700 && vehicle.Engine.HorsePower >= 1000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Fast car")
                });
            }

            else if (vehicle.Engine.HorsePower < 1000 && vehicle.Engine.HorsePower >= 500)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Medium fast car")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Slow car")
                });
            }

            if (vehicle.Engine.Volume > 3000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("A very large engine")
                });
            }

            else if (vehicle.Engine.Volume < 3000 && vehicle.Engine.Volume >= 2000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Big engine")
                });
            }

            else if (vehicle.Engine.Volume < 2000 && vehicle.Engine.Volume >= 1000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Medium sized engine")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Small engine")
                });
            }

            if (vehicle.Run > 50000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Great mileage")
                });
            }

            else if (vehicle.Run < 50000 && vehicle.Run >= 20000)
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Average mileage")
                });
            }

            else
            {
                vehicle.Tags.Add(new TagCars
                {
                    Tag = await this.GetOrCreateTag("Low mileage")
                });
            }

            await this.appDbContext.SaveChangesAsync();
        }
    }
}
