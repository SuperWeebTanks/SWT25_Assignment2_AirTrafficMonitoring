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

    }
}
