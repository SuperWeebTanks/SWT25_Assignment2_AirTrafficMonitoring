using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT25_Assignment2_AirTrafficMonitoring;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;


namespace Airport.Unit.Tests
{
    [TestFixture]
    public class AirportUnitTests
    {
        private SWT25_Assignment2_AirTrafficMonitoring.Airport _airport;
        private DecodeFactory _decoderMock;
        private ITransponderReceiver _transponderReceiverMock;
        private AirSpace _airspace;
        private List<Track> _tracks;
        private Track _track;
        private List<Track> _trackEventArgs;

        [SetUp]
        public void Setup()
        {
            _airspace = new AirSpace {Height_from = 500, Height_to = 20000, X = 80000, Y = 80000};
            _decoderMock = Substitute.For<DecodeFactory>();
            _transponderReceiverMock = Substitute.For<ITransponderReceiver>();
            _airport=new SWT25_Assignment2_AirTrafficMonitoring.Airport(_transponderReceiverMock,_decoderMock,_airspace);
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
            _trackEventArgs = null;
            _tracks = new List<Track> { _track };

            _airport.TrackDataEvent += (o, args) => { _trackEventArgs = args.TrackData; };

        }

        [Test]
        public void TrackDataEvent_DoesFire()
        {
            
            _decoderMock.CreateTracks(Arg.Any<List<string>>()).Returns(_tracks);
        
            _airport.AirportReceiverHandler(null, new RawTransponderDataEventArgs(new List<string> { "null" }));
            Assert.That(_trackEventArgs,Is.EquivalentTo(_tracks));
        }

        [Test]
        public void FilterTracks_EmptyList()
        {
            var tracks = new List<Track>();
            var list=_airport.FilterTracks(tracks);
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void Filtertracks_HeightOverHighestLimit_NotAllowed()
        {
            _track.CurrentAltitude = 20001;
          
            var list = _airport.FilterTracks(_tracks);
            Assert.IsTrue(list.IsNullOrEmpty());

        }

        [Test]
        public void FilterTracks_HeigtUnderHighestLimit_Allowed()
        {
            _track.CurrentAltitude = 20000;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsFalse(list.IsNullOrEmpty());
        }

        [Test]
        public void Filtertracks_HeightUnderLowestLimit_NotAllowed()
        {
            _track.CurrentAltitude = 499;

            var list = _airport.FilterTracks(_tracks);
            Assert.IsTrue(list.IsNullOrEmpty());

        }

        [Test]
        public void Filtertracks_HeightOverAndEqualToLowestLimit_Allowed()
        {
            _track.CurrentAltitude = 500;

            var list = _airport.FilterTracks(_tracks);
            Assert.IsFalse(list.IsNullOrEmpty());

        }

        [Test]
        public void FilterTracks_XCorOver80000_NotAllowed()
        {
            _track.CurrentPositionX = 80001;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_XCorUnderAndEqualTo8000_Allowed()
        {
            _track.CurrentPositionX = 80000;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsFalse(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_XCorUnder0_NotAllowed()
        {
            _track.CurrentPositionX = -1;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_XCorOverAndEqualTo0_Allowed()
        {
            _track.CurrentPositionX = 0;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsFalse(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_YCorOver80000_NotAllowed()
        {
            _track.CurrentPositionY = 80001;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_YCorUnderAndEqualTo80000_Allowed()
        {
            _track.CurrentPositionY = 80000;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsFalse(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_YCorUnder0_NotAllowed()
        {
            _track.CurrentPositionY =-1;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        [Test]
        public void FilterTracks_YCorOverAndEqualTo0_Allowed()
        {
            _track.CurrentPositionY = 0;
            var list = _airport.FilterTracks(_tracks);
            Assert.IsFalse(list.IsNullOrEmpty());
        }


    }

    [TestFixture]
    public class TrackDataEventUnitTest
    {
        [Test]
        public void ConstructorBuilderTest()
        {
            var track = new Track
            {
                CurrentAltitude = 600, CurrentCompassCourse = 200, CurrentHorizontalVelocity = 300,
                CurrentPositionX = 10000, CurrentPositionY = 10000,
                Tag="aaaaaa",
                TimeStamp = new DateTime(2020, 10, 10),
            };
            var track1 = new Track
            {
                CurrentAltitude = 800,
                CurrentCompassCourse = 300,
                CurrentHorizontalVelocity = 200,
                CurrentPositionX = 5000,
                CurrentPositionY = 200,
                Tag = "bbbbbb",
                TimeStamp = new DateTime(2020, 10, 11),
            };
            var list = new List<Track> {track,track1};
            var uut=new TrackDataEventArgs(list);
            Assert.That(uut.TrackData,Is.EquivalentTo(list));
        }
    }
}
