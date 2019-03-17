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

    public class Commercial_OD : IOccurenceDetector
    {
        public event EventHandler<OccurrenceEventArgs> OccurenceDetectedEvent;

        private double _altitudeDistance;
        private double _horizontalDistance;

        public double CalculateHorizontalDistance(Track track1, Track track2)
        {
            return Math.Sqrt(Math.Pow((track2.CurrentPositionX - track1.CurrentPositionX), 2) +
                             Math.Pow((track2.CurrentPositionY - track1.CurrentPositionY), 2));
        }

        public void CheckOccurrence(Track track, List<Track> tracks)
        {
            foreach (var t in tracks)
            {
                _altitudeDistance = (track.CurrentAltitude - t.CurrentAltitude > 0) ?
                    (track.CurrentAltitude - t.CurrentAltitude) : (t.CurrentAltitude - track.CurrentAltitude);

                _horizontalDistance = CalculateHorizontalDistance(track, t);

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
