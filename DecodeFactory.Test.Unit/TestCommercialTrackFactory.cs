using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SWT25_Assignment2_AirTrafficMonitoring;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace DecodeFactory.Test.Unit
{
    [TestFixture]
    public class TestCommercialTrackFactory
    {
        private SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory.DecodeFactory _uut;
        private string TrackString1;
        private string TrackString2;
        private List<string> ListOfStrings;

        [SetUp]
        public void Setup()
        {
            _uut = new CommercialTrackFactory();
            TrackString1 = "BTR312;2004;18204;5500;20151006213456789";
            TrackString2 = "AQM312;3200;18602;5500;20151006213456789";
            ListOfStrings = new List<string>
            {
                TrackString1,
                TrackString2
            };
        }

        [Test]
        [Ignore("Not Implmented yet")]
        public void CreateTracks_ReceiveStringConvertToTrack_ReturnsTrack()
        {
            //Act
            var track = _uut.CreateTracks(ListOfStrings);

            //Assert

        }

        [Test]
        public void CreateTrack_ReceiveInvalidTrack_ThrowException()
        {
            //Arrange
            var invalidTrack = "asdf;slg12;3020313;204022140;2561123";
            var InvalidListOfTracks = new List<string>();
            InvalidListOfTracks.Add(invalidTrack);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => _uut.CreateTracks(InvalidListOfTracks));
        }

        [Test]
        public void CreateTrack_StringContainsInvalidTagName_CatchesException()
        {
            //Arrange
            var invalidTrack = "asdf;2005;3020313;2040221402561123";
            var InvalidTag = new List<string>();
            InvalidTag.Add(invalidTrack);

            //Act & Assert
            Assert.Catch<ArgumentException>(() => _uut.CreateTracks(InvalidTag)); 
        }

        [Test]
        public void CreateTrack_ReceiveEmptyList_ReturnNull()
        {
            //Arrange
            var emptyList = new List<string>();

            //Act
            var Track = _uut.CreateTracks(emptyList); 

            //Assert
            Assert.That(Track, Is.EqualTo(null));
        }
    }
}
