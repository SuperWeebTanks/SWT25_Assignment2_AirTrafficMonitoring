using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
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

        
        [SetUp]
        public void Setup()
        {
            _airspace = new AirSpace {Height_from = 500, Height_to = 20000, X = 80000, Y = 80000};
            _decoderMock = Substitute.For<DecodeFactory>();
            _transponderReceiverMock = Substitute.For<ITransponderReceiver>();
            _airport=new SWT25_Assignment2_AirTrafficMonitoring.Airport(_transponderReceiverMock,_decoderMock,_airspace);
        }

        [Test]
        public void FilterTracks_EmptyList()
        {

        }
    }

    [TestFixture]
    public class TrackDataEventUnitTest
    {
        [Test]
        public void ConstructorBuilderTest()
        {
            var track = new CommercialTrack
            {
                CurrentAltitude = 600, CurrentCompassCourse = 200, CurrentHorizontalVelocity = 300,
                CurrentPositionX = 10000, CurrentPositionY = 10000,
                Tag="aaaaaa",
                TimeStamp = new DateTime(2020, 10, 10),
            };
            var track1 = new CommercialTrack
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
