using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public class OccurrenceEventArgs : EventArgs
    {
        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurenceTime { get; set; }
    }

    public interface IOccurenceDetector
    {
        void CheckOccurrence(Track track, List<Track> tracks);
        event EventHandler<OccurrenceEventArgs> OccurenceDetectedEvent;
    }

    public class TrackOccurrenceDetector : IOccurenceDetector
    {
        public event EventHandler<OccurrenceEventArgs> OccurenceDetectedEvent;

        private double _altitudeDistance;
        private double _horizontalDistance;


        public void CheckOccurrence(Track track, List<Track> tracks)
        {
            foreach (var t in tracks)
            {
                if (track.Tag == t.Tag)
                    return;

                _altitudeDistance = (track.CurrentAltitude - t.CurrentAltitude > 0) ?
                    (track.CurrentAltitude - t.CurrentAltitude) : (t.CurrentAltitude - track.CurrentAltitude);

                _horizontalDistance = Calculator.CalculateHorizontalDistance(track, t);

                if ((_altitudeDistance < 300) && (_horizontalDistance < 5000))
                    OnOccurenceDetectedEvent(new OccurrenceEventArgs { ObservedTrack = track, OccurenceTrack = t, OccurenceTime = DateTime.Now});
            }
        }

        protected virtual void OnOccurenceDetectedEvent(OccurrenceEventArgs e)
        {
            OccurenceDetectedEvent?.Invoke(this, e);
        }
    }
}
