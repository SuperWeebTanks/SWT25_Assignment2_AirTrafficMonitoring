using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace DecodeFactory.Test.Unit
{
    [TestFixture]
    public class TestTrack
    {
        private Track _uut;
        private Track TestTrack1;
        private Track TestTrack2;
        private Track TestTrack3;

        [SetUp]
        public void Setup()
        {
            _uut = new Track();

            #region TestTracks
            TestTrack1 = new Track();
            TestTrack2 = new Track();
            TestTrack3 = new Track();
            
            TestTrack1.Tag = "BTR321";
            TestTrack1.CurrentPositionX = 1000;
            TestTrack1.CurrentPositionY = 3000;
            TestTrack1.CurrentAltitude = 1000; 
            TestTrack1.TimeStamp = DateTime.Now;

            TestTrack2.Tag = "BTR321";
            TestTrack2.CurrentPositionX = 2000;
            TestTrack2.CurrentPositionY = 4000;
            TestTrack2.CurrentAltitude = 1000;
            TestTrack2.TimeStamp = DateTime.Now;

            //Same properties as track 1 
            TestTrack3.Tag = "BTR321";
            TestTrack3.CurrentPositionX = 1000;
            TestTrack3.CurrentPositionY = 3000;
            TestTrack3.CurrentAltitude = 1000;
            TestTrack3.TimeStamp = TestTrack1.TimeStamp;

            #endregion
        }

        [Test]
        public void PropertyTag_SetTagForTrack_TagSet()
        {
            _uut.Tag = "BTR321"; 
            Assert.That(_uut.Tag, Is.EqualTo("BTR321"));
        }

        [Test]
        public void Properties_SetInvalidTag_ThrowsException()
        {
            var invalidTag = "ASD21";
            Assert.Throws<ArgumentException>(() => _uut.Tag = invalidTag); 
        }

        [Test]
        public void PropertyCurrentPositionX_SetCurrentPositionXForTrack_CurrentPositionXSet()
        {
            _uut.CurrentPositionX = 1000; 
            Assert.That(_uut.CurrentPositionX, Is.EqualTo(1000));
        }

        [Test]
        public void PropertyCurrentPositionX_SetCurrentPositionYForTrack_CurrentPositionXSet()
        {
            _uut.CurrentPositionY = 1000; 
            Assert.That(_uut.CurrentPositionY, Is.EqualTo(1000));
        }

        [Test]
        public void PropertyCurrentAltitude_SetCurrentAltitudeForTrack_CurrentAltitudeSet()
        {
            _uut.CurrentAltitude = 1000;
            Assert.That(_uut.CurrentAltitude, Is.EqualTo(1000));
        }

        [Test]
        public void PropertyCurrentAltitude_SetInvalidCurrentAltitudeForTrack_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _uut.CurrentAltitude = -1000); 
        }

        [Test]
        public void PropertyCurrentHorizontalVelocity_SetCurrentHorizontalVelocityForTrack_CurrentHorizontalVelocitySet()
        {
            _uut.CurrentHorizontalVelocity = 1000; 
            Assert.That(_uut.CurrentHorizontalVelocity, Is.EqualTo(1000));
        }

        [Test]
        public void PropertyCurrentCompassCourse_SetCurrentCompassCourseForTrack_CurrentCompassCourseSet()
        {
            _uut.CurrentCompassCourse = 100; 
            Assert.That(_uut.CurrentCompassCourse, Is.EqualTo(100));
        }

        [Test]
        public void PropertyCurrentCompassCourse_SetInvalidCurrentCompassCourseForTrack_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _uut.CurrentCompassCourse = 1000); 
        }

        public void OverloadedEqualOperator_TracksAreEqual_ReturnsTrue()
        {
            //Arrange 
            bool result;

            //Act
            result = TestTrack1 == TestTrack3; 

            //Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void OverloadedEqualOperator_TracksAreNotEqual_ReturnsTrue()
        {
            //Arrange 
            bool result;

            //Act
            result = TestTrack1 == TestTrack2;

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void OverloadedNotEqualOperator_TracksAreNotEqual_ReturnsTrue()
        {
            //Arrange 
            bool result = new bool();

            //Act
            result = TestTrack1 != TestTrack2;

            //Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void OverloadedNotEqualOperator_TracksAreEqual_ReturnsFalse()
        {
            //Arrange 
            bool result;

            //Act
            result = TestTrack1 != TestTrack3;

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

    }
}
