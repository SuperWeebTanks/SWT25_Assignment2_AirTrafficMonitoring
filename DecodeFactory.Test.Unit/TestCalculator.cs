using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace DecodeFactory.Test.Unit
{
    [TestFixture]
    public class TestCalculator
    {
        private Track OldTrack { get; set; } = new Track();
        private Track UpdatedTrack { get; set; } = new Track();

        [SetUp]
        public void SetUp()
        {
        }

        #region CalculateHorizontalDistance
        [Test]
        public void CalculateHorizontalDistance_FindHorizontalDistanceBetweenTwoTracks_ReturnsDistance()
        {
            //Arrange
            double result; 
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 4000; 
            UpdatedTrack.CurrentPositionX = 2200;
            UpdatedTrack.CurrentPositionY = 4400; 

            //Act
            result = Calculator.CalculateHorizontalDistance(OldTrack, UpdatedTrack);

            //Assert 
            Assert.That(result, Is.EqualTo(447.213).Within(0.01));

        }

        [Test]
        public void CalculateHorizontalDistance_FindHorizontalDistanceBetweenTwoTracksOneWithNegativePositions_ReturnsDistance()
        {
            //Arrange
            double result;
            OldTrack.CurrentPositionX = -2000;
            OldTrack.CurrentPositionY = -4000;
            UpdatedTrack.CurrentPositionX = 2200;
            UpdatedTrack.CurrentPositionY = 4400;

            //Act
            result = Calculator.CalculateHorizontalDistance(OldTrack, UpdatedTrack);

            //Assert 
            Assert.That(result, Is.EqualTo(9391.4855).Within(0.01));

        }

        [Test]
        public void CalculateHorizontalDistance_FindHorizontalDistancenBetweenTwoTracksWithNegativePosition_ReturnsDistance()
        {
            //Arrange
            double result;
            OldTrack.CurrentPositionX = -2000;
            OldTrack.CurrentPositionY = -4000;
            UpdatedTrack.CurrentPositionX = -2200;
            UpdatedTrack.CurrentPositionY = -4400;

            //Act
            result = Calculator.CalculateHorizontalDistance(OldTrack, UpdatedTrack);

            //Assert 
            Assert.That(result, Is.EqualTo(447.213).Within(0.01));
        }

        [Test]
        public void CalculateHorizontalDistance_FindHorizontalDistanceBetweenTwoEqualTracks_ReturnsDistance()
        {
            //Arrange
            double result;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 4000;
            UpdatedTrack.CurrentPositionX = 2000;
            UpdatedTrack.CurrentPositionY = 4000;

            //Act
            result = Calculator.CalculateHorizontalDistance(OldTrack, UpdatedTrack);

            //Assert 
            Assert.That(result, Is.EqualTo(0).Within(0.01));
        }
        #endregion

        #region CalculateHorizontalVelocity
        [Test]
        public void CalculateHorizontalVelocity_FindHorizontalDistanceBetweenTwoTracks_ReturnsVelocity()
        {
            //Arrange
            double result;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 4000;
            UpdatedTrack.CurrentPositionX = 7000;
            UpdatedTrack.CurrentPositionY = 9000;

            OldTrack.TimeStamp = new DateTime(2019, 01, 01, 10, 22, 33);
            UpdatedTrack.TimeStamp = new DateTime(2019, 01, 01, 10, 23, 39);

            //Act
            result = Calculator.CalculateHorizontalVelocity(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(result, Is.EqualTo(107.137).Within(0.01));
        }

        [Test]
        public void CalculateHorizontalVelocity_FindHorizontalDistanceBetweenTwoEqualTracks_ReturnsZero()
        {
            //Arrange
            double result;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 4000;
            UpdatedTrack.CurrentPositionX = 2000;
            UpdatedTrack.CurrentPositionY = 4000;

            OldTrack.TimeStamp = new DateTime(2019, 01, 01, 10, 22, 33);
            UpdatedTrack.TimeStamp = new DateTime(2019, 01, 01, 10, 23, 39);

            //Act
            result = Calculator.CalculateHorizontalVelocity(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateHorizontalVelocity_FindHorizotanlDistanceBetweenInvalidTimeStampTracks_ReturnsZero()
        {
            //Arrange
            double result;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 4000;
            UpdatedTrack.CurrentPositionX = 7000;
            UpdatedTrack.CurrentPositionY = 9000;

            OldTrack.TimeStamp = new DateTime(2019, 01, 01, 10, 22, 33);
            //Time travelling
            UpdatedTrack.TimeStamp = new DateTime(2019, 01, 01, 10, 01, 39);

            //Act
            result = Calculator.CalculateHorizontalVelocity(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        #endregion


        /// <summary>
        /// Tests for calculating the compass course of a track
        /// NorthEastRegion 0-90 degress (See it as a unit circle)
        /// NorthWestRegion 90-180 degress
        /// SouthWestRegion 180-270 degrees
        /// SouthEastRegion 270-359 degrees
        /// North = 90, West = 180, East = 0, South = 270
        /// </summary>
        #region CalculateCompassCourse
        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackNorthWestRegion_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 4000;
            UpdatedTrack.CurrentPositionX = 7000;
            UpdatedTrack.CurrentPositionY = 5000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(11.3).Within(0.01));
        }

        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackNorthEastRegion_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 3000;
            OldTrack.CurrentPositionY = 1000;
            UpdatedTrack.CurrentPositionX = 1000;
            UpdatedTrack.CurrentPositionY = 2000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(153.43).Within(0.01));
        }

        [Test]
        public void CalculateCompassCourse_CalculateComppassCourseForTrackSouthEastRegion_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 3000;
            UpdatedTrack.CurrentPositionX = 1000;
            UpdatedTrack.CurrentPositionY = 1000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(243.43).Within(0.01));

        }

        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackSouthWestRegion_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 3000;
            OldTrack.CurrentPositionY = 3000;
            UpdatedTrack.CurrentPositionX = 2999;
            UpdatedTrack.CurrentPositionY = 1000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(269.97).Within(0.01));
        }

        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackNorth_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 1000;
            OldTrack.CurrentPositionY = 1000;
            UpdatedTrack.CurrentPositionX = 1000;
            UpdatedTrack.CurrentPositionY = 3000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(90.00).Within(0.01));
        }

        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackWest_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 3000;
            OldTrack.CurrentPositionY = 2000;
            UpdatedTrack.CurrentPositionX = 1000;
            UpdatedTrack.CurrentPositionY = 2000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(180.00).Within(0.01));
        }

        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackEast_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 1000;
            OldTrack.CurrentPositionY = 2000;
            UpdatedTrack.CurrentPositionX = 3000;
            UpdatedTrack.CurrentPositionY = 2000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(0.00).Within(0.01));
        }

        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForTrackSouth_ReturnsCourse()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 3000;
            UpdatedTrack.CurrentPositionX = 2000;
            UpdatedTrack.CurrentPositionY = 1000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(270.00).Within(0.01));
        }
       
        [Test]
        public void CalculateCompassCourse_CalculateCompassCourseForEqualTracks_ReturnsZero()
        {
            //Arrange
            double angle;
            OldTrack.CurrentPositionX = 2000;
            OldTrack.CurrentPositionY = 3000;
            UpdatedTrack.CurrentPositionX = 2000;
            UpdatedTrack.CurrentPositionY = 3000;

            //Assert 
            angle = Calculator.CalculateCompassCourse(UpdatedTrack, OldTrack);

            //Assert
            Assert.That(angle, Is.EqualTo(0).Within(0.01));
        }
        #endregion

    }


}
