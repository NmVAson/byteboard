using System;
using System.Net.NetworkInformation;
using NUnit.Framework;

namespace Warehouse
{
    [TestFixture]
    public class VehicleTests
    {
        [Test]
        public void ShouldCalculateDistanceForASinglePing()
        {
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, DateTime.Now.Millisecond));

            var actual = vehicle.GetTotalDistance();
            
            Assert.That(actual, Is.EqualTo(0));
        }
        
        [Test]
        public void ShouldCalculateDistanceBetweenTwoPoints()
        {
            const double expectedDistance = 1.4;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, DateTime.Now.Millisecond));
            vehicle.Pings.Add(new Ping(1,1, DateTime.Now.Add(new TimeSpan(1)).Millisecond));

            var actual = vehicle.GetTotalDistance();
            
            Assert.AreEqual(expectedDistance, Math.Round(actual, 1));
        }
        
        [Test]
        public void ShouldCalculateDistanceBetweenManyPoints()
        {
            var expectedDistance = Math.Round(1.4 + 2.8, 1);
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, DateTime.Now.Millisecond));
            vehicle.Pings.Add(new Ping(1,1, DateTime.Now.Add(new TimeSpan(1)).Millisecond));
            vehicle.Pings.Add(new Ping(3,3, DateTime.Now.Add(new TimeSpan(2)).Millisecond));

            var actual = vehicle.GetTotalDistance();
            
            Assert.AreEqual(expectedDistance, Math.Round(actual, 1));
        }
        
        [Test]
        public void ShouldCalculateDistanceForAMachineWithZeroPings()
        {
            var vehicle = new Vehicle("Ada");

            var actual = vehicle.GetTotalDistance();
            
            Assert.AreEqual(0, actual);
        }
        
        
        [Test]
        public void ShouldCalculateDistanceForAMachineWithNegativePings()
        {
            var expectedDistance = 101.0;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,-100, DateTime.Now.Millisecond));
            vehicle.Pings.Add(new Ping(-1.5,1, DateTime.Now.Add(new TimeSpan(1)).Millisecond));

            var actual = vehicle.GetTotalDistance();
            
            Assert.AreEqual(expectedDistance, Math.Round(actual, 1));
        }

        [Test]
        public void ShouldCalculateAverageSpeed()
        {
            const double expectedDistance = 0.7;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, 0));
            vehicle.Pings.Add(new Ping(1,1, 2));

            var actual = vehicle.GetAverageSpeed();
            
            Assert.AreEqual(expectedDistance, Math.Round(actual, 1));
            
        }
        
    }
}