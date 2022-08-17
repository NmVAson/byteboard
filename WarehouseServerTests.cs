using System.Linq;
using NUnit.Framework;

namespace Warehouse
{
    [TestFixture]
    public class WarehouseServerTests
    {
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
            server.Vehicles.Add(ada);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Ada", actual[0]);
        }
        
        [Test]
        public void ShouldNotReturnDistancesIncludingTimeStamp()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 1));
            ada.Pings.Add(new Ping(2,2, 10));
            var euler = new Vehicle("Euler");
            euler.Pings.Add(new Ping(0,0, 2));
            euler.Pings.Add(new Ping(1,1, 3));
            server.Vehicles.Add(ada);
            server.Vehicles.Add(euler);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Ada", actual[0]);
        }
        
        [Test]
        public void ShouldNotReturnDistancesPastTimeStamp()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 1));
            ada.Pings.Add(new Ping(1,1, 11));
            var euler = new Vehicle("Euler");
            euler.Pings.Add(new Ping(0,0, 2));
            euler.Pings.Add(new Ping(1,1, 3));
            server.Vehicles.Add(ada);
            server.Vehicles.Add(euler);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Euler", actual[0]);
        }
        
        [Test]
        public void ShouldOrderByTotalDistanceDescending()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 1));
            ada.Pings.Add(new Ping(1,1, 2));
            var euler = new Vehicle("Euler");
            euler.Pings.Add(new Ping(0,0, 2));
            euler.Pings.Add(new Ping(2,2, 3));
            server.Vehicles.Add(ada);
            server.Vehicles.Add(euler);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Euler", actual[0]);
        }
        
        [Test]
        public void ShouldTieBreakOrderByNameAlphabetically()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 1));
            ada.Pings.Add(new Ping(1,1, 2));
            var euler = new Vehicle("Euler");
            euler.Pings.Add(new Ping(0,0, 2));
            euler.Pings.Add(new Ping(1,1, 3));
            server.Vehicles.Add(ada);
            server.Vehicles.Add(euler);

            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Ada", actual[0]);
        }

        [Test]
        public void ShouldNotThrowForZeroVehicles()
        {
            var server = new WarehouseServer();
            
            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.IsEmpty(actual);
        }

        [Test]
        public void ShouldNotThrowForZeroPings()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            server.Vehicles.Add(ada);
            
            var actual = server.GetMostTraveledSince(1, 10);
            
            Assert.AreEqual(1, actual.Length);
        }
    }
}