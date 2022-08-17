using System.Linq;
using NUnit.Framework;

namespace Warehouse
{
    [TestFixture]
    public class WarehouseServerTests
    {
        [Test]
        public void ShouldNotReturnDistancesPastTimeStamp()
        {
            var server = new WarehouseServer();
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(1,1, 10));
            vehicle.Pings.Add(new Ping(0,0, 11));
            server.Vehicles.Add(vehicle);

            var actual = server.GetMostTraveledSince(2, 10);
            
            Assert.AreEqual(1, actual.Length);
        }

        [Test]
        public void ShouldNotReturnMoreThanNumberSpecified()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 1));
            var euler = new Vehicle("Euler");
            euler.Pings.Add(new Ping(0,0, 2));
            server.Vehicles.Add(ada);
            server.Vehicles.Add(euler);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            
        }

        [Test]
        public void ShouldReturnNamesOfVehicles()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 1));
            var euler = new Vehicle("Euler");
            euler.Pings.Add(new Ping(0,0, 2));
            server.Vehicles.Add(ada);
            server.Vehicles.Add(euler);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Euler", actual[0]);
            
        }
    }
}