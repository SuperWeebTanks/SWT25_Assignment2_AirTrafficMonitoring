using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace AirTrafficMonitor.Unit.Test
{
    public class OccurenceDetector
    {
            private IOccurenceDetector _uut;
            private OccurrenceEventArgs _receivedOccurenceEventArgs;
            private Track _observedTrack;
            private List<Track> _occurenceTracks;
            private Track _occurenceTrack;

            [SetUp]
            public void SetUp()
            {
                _receivedOccurenceEventArgs = null;
                _uut = new Commercial_OD();
                _observedTrack = new CommercialTrack();
                _occurenceTrack = new CommercialTrack();
                _occurenceTracks = new List<Track>();

                _uut.OccurenceDetectedEvent +=
                    (o, args) => { _receivedOccurenceEventArgs = args; };
            }

            [Test]
            public void OccurenceDetector_OccurenceTracks_EventFired()
            {
                _observedTrack.Tag = "Test01";
                _observedTrack.CurrentAltitude = 400;
                _observedTrack.CurrentPositionX = 5000;
                _observedTrack.CurrentPositionY = 5000;
                
                _occurenceTrack.Tag = "Test02";
                _occurenceTrack.CurrentAltitude = 400;
                _occurenceTrack.CurrentPositionX = 5000;
                _occurenceTrack.CurrentPositionY = 5000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Not.Null);
            }
    }
}
