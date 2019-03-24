using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using SWT25_Assignment2_AirTrafficMonitoring;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace AirTrafficMonitor.Unit.Test
{
    [TestFixture]
    public class TestAirTrafficMonitor
    {
        private Air_Traffic_Monitor _uut;
        private IOccurenceDetector _occurenceSource;
        private ISignalForwarder _airport;
        private IDisplay _display;
        private IOccurrenceLogger _logger;
        private IFormat _formatter;
        private Track _observedTrack;
        private Track _occurenceTrack;
        private List<Track> _occurenceTracks;

        [SetUp]
        public void Setup()
        {
            _occurenceSource = Substitute.For<IOccurenceDetector>();

            _display = Substitute.For<IDisplay>();
            _logger = Substitute.For<IOccurrenceLogger>();
            _formatter = Substitute.For<IFormat>();
            _airport = Substitute.For<ISignalForwarder>();

            _observedTrack = new Track();
            _occurenceTrack = new Track();

            _observedTrack.Tag = "Track1";
            _occurenceTrack.Tag = "Track2";

            _observedTrack.CurrentAltitude = 1000;
            _observedTrack.CurrentPositionX = 5000;
            _observedTrack.CurrentPositionY = 5000;

            _occurenceTrack.CurrentAltitude = 1200;
            _occurenceTrack.CurrentPositionX = 7600;
            _occurenceTrack.CurrentPositionY = 7600;

            _uut = new Air_Traffic_Monitor(_airport, _occurenceSource, _display, _logger, _formatter);
        }

        [Test]
        public void AirTrafficMonitor_Occurence_CurrentObservedTrackIsCorrect()
        {
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
                (new OccurrenceEventArgs {ObservedTrack = _observedTrack, OccurenceTrack = null, OccurenceTime = DateTime.Now});

            Assert.That(_uut.ObservedTrack, Is.EqualTo(_observedTrack));
        }

        [Test]
        public void AirTrafficMonitor_Occurrence_CurrentOccurenceTrackIsCorrect()
        {
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
                (new OccurrenceEventArgs { ObservedTrack = _observedTrack, OccurenceTrack = _occurenceTrack, OccurenceTime = DateTime.Now });
            
            Assert.That(_uut.OccurenceTrack, Is.EqualTo(_occurenceTrack));
        }

        [Test]
        public void AirTrafficMonitor_Occurrence_CurrentOccurenceTimeIsCorrect()
        {
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
                (new OccurrenceEventArgs { ObservedTrack = _observedTrack, OccurenceTrack = _occurenceTrack, OccurenceTime = DateTime.Now });

            Assert.That(_uut.OccurrenceTime, Is.EqualTo(_uut.OccurrenceTime));
        }

        [Test]
        public void AirTrafficMonitor_Occurrence_RenderOccurrencesCalled()
        {
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
                (new OccurrenceEventArgs { ObservedTrack = _observedTrack, OccurenceTrack = _occurenceTrack, OccurenceTime = DateTime.Now });

            _display.Received(1).RenderOccurences(Arg.Any<string[]>());
        }

        [Test]
        public void AirTrafficMonitor_Occurrence_LogOccurrencesCalled()
        {
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
            (new OccurrenceEventArgs {ObservedTrack = _observedTrack, OccurenceTrack = _occurenceTrack, OccurenceTime = DateTime.Now});

            _logger.Received(1).LogOccurrences(_uut.ObservedTrack, _uut.OccurenceTrack, _uut.OccurrenceTime);
        }
    }
}