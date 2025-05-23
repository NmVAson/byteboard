﻿using System.Linq;
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

            var actual = server.GetMostTraveledSince(1, 0);
            
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
        public void ShouldReturnDistancesIncludingTimeStamp()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 10));
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
        public void ShouldNotReturnDistancesBeforeTimeStamp()
        {
            var server = new WarehouseServer();
            var ada = new Vehicle("Ada");
            ada.Pings.Add(new Ping(0,0, 11));
            ada.Pings.Add(new Ping(1,1, 12));
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

            var actual = server.GetMostTraveledSince(1, 0);
            
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
            server.Vehicles.Add(euler);
            server.Vehicles.Add(ada);

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

        [Test]
        public void ShouldRetrieveForkliftsWithAggressiveAcceleration()
        {
            var server = new WarehouseServer();
            var aggressive = new Vehicle("Ada");
            aggressive.Pings.Add(new Ping(0,0, 1));
            aggressive.Pings.Add(new Ping(10,10, 2));
            aggressive.Pings.Add(new Ping(11,11, 3));
            var normalVehicle = new Vehicle("Euler");
            normalVehicle.Pings.Add(new Ping(0,0, 2));
            normalVehicle.Pings.Add(new Ping(1,1, 3));
            server.Vehicles.Add(aggressive);
            server.Vehicles.Add(normalVehicle);

            var actual = server.CheckForDamage();
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Ada", actual[0]);
        }

        [Test]
        public void ShouldNotThrowIfThereIsNotEnoughPings()
        {
            var server = new WarehouseServer();
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, 1));
            server.Vehicles.Add(vehicle);

            var actual = server.CheckForDamage();
            
            Assert.AreEqual(0, actual.Length);
        }

        [Test]
        public void ShouldRetrieveForkliftsWithAggressiveDeceleration()
        {
            var server = new WarehouseServer();
            var aggressive = new Vehicle("Ada");
            aggressive.Pings.Add(new Ping(10,10, 1));
            aggressive.Pings.Add(new Ping(0,0, 2));
            aggressive.Pings.Add(new Ping(1,1, 3));
            var normalVehicle = new Vehicle("Euler");
            normalVehicle.Pings.Add(new Ping(0,0, 1));
            normalVehicle.Pings.Add(new Ping(1,1, 2));
            server.Vehicles.Add(aggressive);
            server.Vehicles.Add(normalVehicle);

            var actual = server.CheckForDamage();
            
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual("Ada", actual[0]);
        }

        [Test]
        public void ShouldRetrieveForkliftsWithCollisions()
        {
            var server = new WarehouseServer();
            var aggressive = new Vehicle("Ada");
            aggressive.Pings.Add(new Ping(1,1, 3));
            var normalVehicle = new Vehicle("Euler");
            normalVehicle.Pings.Add(new Ping(1,1, 3));
            server.Vehicles.Add(aggressive);
            server.Vehicles.Add(normalVehicle);

            var actual = server.CheckForDamage();
            
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual("Ada", actual[0]);
            Assert.AreEqual("Euler", actual[1]);
        }
    }
}