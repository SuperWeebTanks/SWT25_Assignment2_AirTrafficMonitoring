using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SWT25_Assignment2_AirTrafficMonitoring;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace Display.Test.Unit
{
    [TestFixture]
    public class MonitorConsoleTests
    {
        #region Properties
        private TrackFormater _uut { get; set; }
        private CommercialTrack _collisionTrack { get; set; }
        private CommercialTrack _observedTrack { get; set; }

        //Strings to test: 
        private string TrackRenderString { get; set; }
        #endregion

        [SetUp]
        public void Setup()
        {
            _uut = new TrackFormater();
            _collisionTrack = new CommercialTrack();
            _observedTrack = new CommercialTrack();
            
            //Setting properties for testing for FakeTracks: 
            _collisionTrack.Tag = "ATR243";
            _collisionTrack.CurrentPositionX = 39045;
            _collisionTrack.CurrentPositionY = 12932;
            _collisionTrack.CurrentAltitude = 14000;
            _collisionTrack.TimeStamp = DateTime.Now;

            _observedTrack.Tag = "BTX924";
            _observedTrack.CurrentPositionX = 29021;
            _observedTrack.CurrentPositionY = 8001;
            _observedTrack.CurrentAltitude = 12892;
            _observedTrack.TimeStamp = DateTime.Now; 
        }

        [Test]
        public void FormatOccurence_ReceiveTwoTracksandDate_ReturnFormatedString()
        {
            //Arrange 
            var timestamp = DateTime.Now; 
            string[] correctStrings = new string[2];
            correctStrings[0] = "WARNING: COLLISION MAY OCCUR!";
            correctStrings[1] =
                $"POTENTIAL COLLISION BETWEEN AIRCRAFTS: {_observedTrack.Tag} and {_collisionTrack.Tag} at {timestamp}";

            //Act
            var formatedString = _uut.FormatOccurence(_observedTrack, _collisionTrack, timestamp); 

            //Assert
            Assert.That(correctStrings, Is.EqualTo(formatedString));
        }

        [Test]
        public void FormatTracks_FormatTrackNewInsertedTrack_TrackisInserted()
        {
            //Arrange 
            //New Empty List
            var ListOfTracks = new List<Track>(); 
            
            //Act
           _uut.FormatTracks(_observedTrack, ListOfTracks);

           //Assert (All tags are unique, if tag exists in List, it has been inserted)
           Assert.That(ListOfTracks.Find(x => x.Tag.Contains(_observedTrack.Tag)), Is.EqualTo(_observedTrack));

        }

        [Test]
        public void FormatTracks_FormatTrackThatIsAlreadyInList_OldTrackisUpdated()
        {
            //Array
            //New Empty List
            var ListOfTracks = new List<Track>(); 
            //Insert UpdatedTrack with default values
            ListOfTracks.Add(_observedTrack);
            //Update _observedTrack properties
            _observedTrack.CurrentPositionX = 2000;
            _observedTrack.CurrentPositionY = 4000; 

            //Act 
            _uut.FormatTracks(_observedTrack, ListOfTracks);

            //Assert
            Assert.That(ListOfTracks.Find(x => x.Tag.Contains(_observedTrack.Tag)), Is.EqualTo(_observedTrack));
        }

    }
}
