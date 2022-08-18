using System;
using System.Net.NetworkInformation;
using NUnit.Framework;

namespace Warehouse
{
    [TestFixture]
    public class VehicleTests
    {
        private static readonly DateTime Now = DateTime.Now;

        [Test]
        public void ShouldCalculateDistanceForASinglePing()
        {
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, Now.Second));

            var actual = vehicle.GetTotalDistance();
            
            Assert.That(actual, Is.EqualTo(0));
        }
        
        [Test]
        public void ShouldCalculateDistanceBetweenTwoPoints()
        {
            const double expectedDistance = 1.4;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, Now.Second));
            vehicle.Pings.Add(new Ping(1,1, Now.AddSeconds(1).Second));

            var actual = vehicle.GetTotalDistance();
            
            Assert.AreEqual(expectedDistance, Math.Round(actual, 1));
        }
        
        [Test]
        public void ShouldCalculateDistanceBetweenManyPoints()
        {
            var expectedDistance = Math.Round(1.4 + 2.8, 1);
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, Now.Second));
            vehicle.Pings.Add(new Ping(1,1, Now.AddSeconds(1).Second));
            vehicle.Pings.Add(new Ping(3,3, Now.AddSeconds(2).Second));

            var actual = vehicle.GetTotalDistance();
            
            Assert.AreEqual(expectedDistance, Math.Round(actual, 1));
        }
        
        [Test]
        public void ShouldCalculateDistanceSinceTimeInclusively()
        {
            const double expectedDistance = 0.0;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, Now.Second));
            vehicle.Pings.Add(new Ping(1,1, Now.AddSeconds(1).Second));
            vehicle.Pings.Add(new Ping(3,3, Now.AddSeconds(2).Second));
            vehicle.Pings.Add(new Ping(3,3, Now.AddSeconds(3).Second));

            var actual = vehicle.GetTotalDistanceSince(Now.AddSeconds(2).Second);
            
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
            vehicle.Pings.Add(new Ping(0,-100, Now.Second));
            vehicle.Pings.Add(new Ping(-1.5,1, Now.AddSeconds(1).Second));

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

        [Test]
        public void ShouldCalculateMaxAcceleration()
        {
            const double expectedAcceleration = 5.7;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, 0));
            vehicle.Pings.Add(new Ping(1,1, 1));
            vehicle.Pings.Add(new Ping(3,3, 2));

            var actual = vehicle.GetMaxAcceleration();
            
            Assert.AreEqual(expectedAcceleration, Math.Round(actual, 1));
        }

        [Test]
        public void ShouldNotThrowMaxAccelerationIfDataIsLacking()
        {
            const double expectedAcceleration = 0.0;
            var vehicle = new Vehicle("Ada");
            vehicle.Pings.Add(new Ping(0,0, 0));

            var actual = vehicle.GetMaxAcceleration();
            
            Assert.AreEqual(expectedAcceleration, Math.Round(actual, 1));
            
        }
    }
}