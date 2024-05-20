using Microsoft.EntityFrameworkCore;
using Mobile_Velingrad.Data;
using Mobile_Velingrad.Data.Models;
using Mobile_Velingrad.Services;
using Moq;
using NUnit.Framework;

namespace Mobile_Velingrad.Tests
{
    [TestFixture]
    public class VehicleServiceTest
    {
        IQueryable<Vehicle> data = new List<Vehicle>
        {
            new Vehicle {Id = 1, Price = 10000, Model = new Model(){Name = "BMW"}, City = new City(){Name = "Blagoevgrad"}, Run = 10000, Engine = new Engine(){Volume = 2000}},
            new Vehicle {Id=2, Price = 20000, Model = new Model() {Name = "Audi"}, City = new City() {Name="Pazarjik"}, Run = 11000, Engine = new Engine(){ Volume = 2500}},
            new Vehicle {Id=3, Price = 30000, Model = new Model() {Name = "GF"}, City = new City() {Name="Plovdiv"}, Run = 10400, Engine = new Engine(){ Volume = 2030}},
            new Vehicle {Id=4, Price = 40000,Model = new Model() {Name = "VW"}, City = new City() {Name="Varna"}, Run = 10000, Engine = new Engine(){ Volume = 3000}},
            new Vehicle {Id=5, Price = 50000, Model = new Model() {Name = "Opel"}, City = new City() {Name="Burgas"}, Run = 10300, Engine = new Engine(){ Volume = 4000}},
            new Vehicle {Id=6, Price = 60000, Model = new Model() { Name = "BMW"}, City = new City() {Name="Velingrad"}, Run = 10000, Engine = new Engine(){ Volume = 2000}},
            new Vehicle {Id=7, Price = 70000, Model = new Model() {Name = "Audi"}, City = new City() {Name="Sofia"}, Run = 11000, Engine = new Engine(){ Volume = 2500} },
            new Vehicle {Id=8, Price = 80000, Model = new Model() {Name = "GF"}, City = new City() {Name="Veliko Tyrnovo"}, Run = 10400, Engine = new Engine(){ Volume = 2030}},
            new Vehicle {Id=9, Price = 90000,Model = new Model() {Name = "VW"}, City = new City() {Name="Pamporovo"}, Run = 10000, Engine = new Engine(){ Volume = 3000}},
            new Vehicle {Id=10, Price = 100000, Model = new Model() {Name = "Opel"}, City = new City() {Name="Ahtopol"}, Run = 10300, Engine = new Engine(){ Volume = 4000}},
            new Vehicle {Id=11, Price = 110000,Model = new Model() {Name = "VW"}, City = new City() {Name="Dobrich"}, Run = 10000, Engine = new Engine(){ Volume = 3000}},
            new Vehicle {Id=12, Price = 120000, Model = new Model() {Name = "Opel"}, City = new City() {Name="Ilinden"}, Run = 10300, Engine = new Engine(){ Volume = 4000}}
        }.AsQueryable();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task TestGetVehiclesFirstPage()
        {
            var data = new List<Vehicle>()
            {
                new Vehicle {Price = 10000, Model = new Model(){Name = "BMW"}, City = new City(){Name = "Blagoevgrad"}, Run = 10000, Engine = new Engine(){Volume = 2000}},
                new Vehicle {Price = 20000, Model = new Model() {Name = "Audi"}, City = new City() {Name="Pazarjik"}, Run = 11000, Engine = new Engine(){ Volume = 2500}},
                new Vehicle {Price = 30000, Model = new Model() {Name = "GF"}, City = new City() {Name="Plovdiv"}, Run = 10400, Engine = new Engine(){ Volume = 2030}},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Vehicle>>();
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(v => v.Vehicles).Returns(mockSet.Object);

            var service = new VehicleService(mockContext.Object);
            var vehicles = await service.GetVehiclesAsync();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(10, vehicles.Vehicles.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, vehicles.Vehicles.First().Id);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, vehicles.Vehicles.Skip(1).First().Id);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(3, vehicles.Vehicles.Skip(2).First().Id);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(10, vehicles.Vehicles.Last().Id);


        }
    }
}
