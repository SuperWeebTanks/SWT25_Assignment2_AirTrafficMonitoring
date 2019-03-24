using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
        private Track _track;
        private List<Track> _tracks;
        private IConsoleClear _console;
        private IExceptionHandler _exception;

        [SetUp]
        public void Setup()
        {
            _occurenceSource = Substitute.For<IOccurenceDetector>();

            _display = Substitute.For<IDisplay>();
            _logger = Substitute.For<IOccurrenceLogger>();
            _formatter = Substitute.For<IFormat>();
            _airport = Substitute.For<ISignalForwarder>();
            _console = Substitute.For<IConsoleClear>();
            _exception = Substitute.For<IExceptionHandler>();

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
            _track = new Track
            {
                CurrentAltitude = 600,
                CurrentCompassCourse = 200,
                CurrentHorizontalVelocity = 300,
                CurrentPositionX = 10000,
                CurrentPositionY = 10000,
                Tag = "aaaaaa",
                TimeStamp = new DateTime(2020, 10, 10),
            };
            _tracks=new List<Track>{_track};

            _uut = new Air_Traffic_Monitor(_airport, _occurenceSource, _display, _logger, _formatter,_console,_exception);
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
        public void HandleOccurenceEvent_FormattedOccurenceCalled_WithCorrectArguments()
        {
            DateTime date = DateTime.Now;
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
                (new OccurrenceEventArgs { ObservedTrack = _observedTrack, OccurenceTrack = _occurenceTrack, OccurenceTime = date });

            _formatter.Received(1).FormatOccurence(Arg.Is(_observedTrack), Arg.Is(_occurenceTrack), Arg.Is(date));
        }

        [Test]
        public void HandleOccurenceEvent_LogOccurrencesCalled_WithCorrectArguments()
        {
            DateTime date = DateTime.Now;
            _occurenceSource.OccurenceDetectedEvent += Raise.EventWith<OccurrenceEventArgs>
            (new OccurrenceEventArgs {ObservedTrack = _observedTrack, OccurenceTrack = _occurenceTrack, OccurenceTime = date});

            _logger.Received(1).LogOccurrences(Arg.Is(_observedTrack),Arg.Is(_occurenceTrack), Arg.Is(date));
        }

        [Test]
        public void HandleTrackEvent_FormatTracksCalled_WithCorrectArguments()
        {
            var tracks=new List<Track>{new Track
            {
                CurrentAltitude = 600,
                CurrentCompassCourse = 200,
                CurrentHorizontalVelocity = 300,
                CurrentPositionX = 10000,
                CurrentPositionY = 10000,
                Tag = "aaaaaa",
                TimeStamp = new DateTime(2020, 10, 10),
            }};
            _uut.Tracks = tracks;
            _airport.TrackDataEvent +=
                Raise.EventWith<TrackDataEventArgs>(new TrackDataEventArgs(_tracks));
            _formatter.Received(1).FormatTracks(Arg.Is(_track),Arg.Is<List<Track>>(x=>x.SequenceEqual(tracks)));

        }

        [Test]

        public void HandleTrackEvent_CheckOccurenceCalled_WithCorrectArguments()
        {
            var tracks = new List<Track>{new Track
            {
                CurrentAltitude = 600,
                CurrentCompassCourse = 200,
                CurrentHorizontalVelocity = 300,
                CurrentPositionX = 10000,
                CurrentPositionY = 10000,
                Tag = "aaaaaa",
                TimeStamp = new DateTime(2020, 10, 10),
            }};
            _uut.Tracks = tracks;
            _airport.TrackDataEvent +=
                Raise.EventWith<TrackDataEventArgs>(new TrackDataEventArgs(_tracks));
            _occurenceSource.Received(1).CheckOccurrence(Arg.Is(_track), Arg.Is<List<Track>>(x => x.SequenceEqual(tracks)));

        }

        [Test]
        public void HandleTrackEvent_RenderOccurencesCalled()
        {
            _uut.OccurrenceTracks = new List<string[]> { new string[] { "First", "Second" } };
            _airport.TrackDataEvent +=
                Raise.EventWith<TrackDataEventArgs>(new TrackDataEventArgs(_tracks));

           
            _display.Received(1).RenderOccurences(Arg.Is(_uut.OccurrenceTracks));
        }

        [Test]
        public void HandleTrackEvent_RenderTrackCalled()
        {
            _uut.Tracks = _tracks;
            _airport.TrackDataEvent +=
                Raise.EventWith<TrackDataEventArgs>(new TrackDataEventArgs(_tracks));
            _display.Received(1).RenderTrack(_uut.Tracks);
        }

        [Test]
        public void HandleTrackEvent_NullReferenceException_Thrown()
        {
            _airport.TrackDataEvent +=
                Raise.EventWith<TrackDataEventArgs>(new TrackDataEventArgs(null));
            _exception.Received(1).Handle(Arg.Any<Exception>());
        }

    }
}