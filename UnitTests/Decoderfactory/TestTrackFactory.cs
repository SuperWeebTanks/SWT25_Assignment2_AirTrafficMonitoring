using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT25_Assignment2_AirTrafficMonitoring;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace DecodeFactory.Test.Unit
{
    [TestFixture]
    public class TestTrackFactory
    {
        private SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory.DecodeFactory _uut;
        private string TrackString1;
        private Track TestTrack; 
        private List<string> ListOfStrings;

        [SetUp]
        public void Setup()
        {
            _uut = new TrackFactory();
            TrackString1 = "BTR312;2004;18204;5500;20151006213456789";
            ListOfStrings = new List<string>
            {
                TrackString1
            };
            TestTrack = new Track();

            //String values converted to Track values
            TestTrack.Tag = "BTR312";
            TestTrack.CurrentPositionX = 2004;
            TestTrack.CurrentPositionY = 18204;
            TestTrack.CurrentAltitude = 5500;
            TestTrack.TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56);


        }

        [Test]
        public void CreateTracks_ReceiveStringConvertToTrack_ReturnsTrack()
        {
            //Act - Send 2 strings to be created as tracks 
            var ListOfTracks = _uut.CreateTracks(ListOfStrings);
            var Track = ListOfTracks.Find((x) => x.Tag.Contains("BTR312"));


            //Assert 
            Assert.That(TestTrack == Track, Is.EqualTo(true));

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
