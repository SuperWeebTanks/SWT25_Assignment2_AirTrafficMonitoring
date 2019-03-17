using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace Display.Test.Unit
{
    [TestFixture]
    public class MonitorConsoleTests
    {
        #region Properties
        private MonitorConsole _uut { get; set; }
        private CommercialTrack _collisionTrack { get; set; }
        private CommercialTrack _observedTrack { get; set; }

        //Strings to test: 
        private string TrackRenderString { get; set; }
        #endregion

        [SetUp]
        public void Setup()
        {
            _uut = new MonitorConsole();
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
        public void RenderTrack_TrackDoesNotExists_AddTrack()
        {
            //Clear list
            _uut.RenderedTracks.RemoveRange(0, _uut.RenderedTracks.Count-1);

            //Check to see if track was added 
            _uut.RenderTrack(_observedTrack);
            _uut.RenderedTracks.Received(1).Add(_observedTrack);

        }

        public void RenderTrack_PrintAllTracks_AllTracksPrint()
        {
            //Add dummy data
            _uut.RenderedTracks.Add(_collisionTrack);
            _uut.RenderedTracks.Add(_observedTrack);

            //Check if all tracks are printed
            foreach (var track in _uut.RenderedTracks)
            {
                track.Received(1).PrintTrack();
            }
        }

        [Test]
        public void RenderTrack_TrackAlreadyExists_UpdateTrack()
        {
            //Clear list
            _uut.RenderedTracks.RemoveRange(0, _uut.RenderedTracks.Count - 1);

            //Add first time
            _uut.RenderTrack(_observedTrack);

            //Add second time, should update the track 
            //Change altitude
            _observedTrack.CurrentPositionX = 3004;
            //Update track
            _uut.RenderTrack(_observedTrack); 
            //Find track 
            var tempTrack = _uut.RenderedTracks.Find(x => x.Tag.Contains(_observedTrack.Tag)); 
            //Check if track was updated correctly 
            Assert.That(_observedTrack.CurrentPositionX, Is.EqualTo(tempTrack.CurrentPositionX));
            
        }
    }
}
