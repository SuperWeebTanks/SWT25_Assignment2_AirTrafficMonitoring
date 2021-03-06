﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace OccurrenceDetector.Unit.Test
{
    public class TestOccurenceDetector
    {
            private IOccurenceDetector _uut;
            private OccurrenceEventArgs _receivedOccurenceEventArgs;
            private Track _observedTrack;
            private Track _occurenceTrack;
            private List<Track> _occurenceTracks;
            
            [SetUp]
            public void SetUp()
            {
                _receivedOccurenceEventArgs = null;
                _uut = new TrackOccurrenceDetector();
                _observedTrack = new  Track();
                _occurenceTrack = new Track();
                _occurenceTracks = new List<Track>();

                _observedTrack.Tag = "Track1";
                _occurenceTrack.Tag = "Track2";

                _observedTrack.CurrentAltitude = 1000;
                _observedTrack.CurrentPositionX = 5000;
                _observedTrack.CurrentPositionY = 5000;

               _uut.OccurenceDetectedEvent +=
                    (o, args) => { _receivedOccurenceEventArgs = args; };
            }

            [Test]
            public void CheckOccurrence_BothDistancesBelowThreshold_EventFired()
            {
                _occurenceTrack.CurrentAltitude = 1200;
                _occurenceTrack.CurrentPositionX = 5000;
                _occurenceTrack.CurrentPositionY = 5000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Not.Null);
            }

            [Test]
            public void CheckOccurrence_BothDistancesAboveThreshold_EventNotFired()
            {
                _occurenceTrack.CurrentAltitude = 18000;
                _occurenceTrack.CurrentPositionX = 32000;
                _occurenceTrack.CurrentPositionY = 72000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Null);
            }

            [Test]
            public void CheckOccurrence_OnlyHorizontalDistanceBelowThreshold_EventNotFired()
            {
                _occurenceTrack.CurrentAltitude = 18000;
                _occurenceTrack.CurrentPositionX = 5000;
                _occurenceTrack.CurrentPositionY = 5000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Null);
            }

            [Test]
            public void CheckOccurrence_OnlyAltitudeDistanceBelowThreshold_EventNotFired()
            {
                _occurenceTrack.CurrentAltitude = 1000;
                _occurenceTrack.CurrentPositionX = 80000;
                _occurenceTrack.CurrentPositionY = 31500;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Null);
            }

            [Test]
            public void CheckOccurrence_BothDistancesBelowThreshold_CorrectObservedTrackReceived()
            {
                _occurenceTrack.CurrentAltitude = 1200;
                _occurenceTrack.CurrentPositionX = 5000;
                _occurenceTrack.CurrentPositionY = 5000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs.ObservedTrack.Tag,
                    Is.EqualTo("Track1"));
            }

            [Test]
            public void CheckOccurrences_BothDistancesBelowThreshold_CorrectOccurenceTrackReceived()
            {
                _occurenceTrack.CurrentAltitude = 1000;
                _occurenceTrack.CurrentPositionX = 7000;
                _occurenceTrack.CurrentPositionY = 9000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs.OccurenceTrack.Tag,
                    Is.EqualTo("Track2"));
            }

            [Test]
            public void CheckOccurrences_AltitudeDistanceJustBelowThreshold_EventFired()
            {
                _occurenceTrack.CurrentAltitude = 1299;
                _occurenceTrack.CurrentPositionX = 5000;
                _occurenceTrack.CurrentPositionY = 5000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Not.Null);
            }

            [Test]
            public void CheckOccurrences_AltitudeDistanceJustAboveThreshold_EventNotFired()
            {
                _occurenceTrack.CurrentAltitude = 1300;
                _occurenceTrack.CurrentPositionX = 5000;
                _occurenceTrack.CurrentPositionY = 5000;
                _occurenceTracks.Add(_occurenceTrack);

                _uut.CheckOccurrence(_observedTrack, _occurenceTracks);

                Assert.That(_receivedOccurenceEventArgs, Is.Null);
            }
    }
}
