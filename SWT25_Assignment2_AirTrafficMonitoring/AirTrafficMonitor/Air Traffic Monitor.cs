using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public abstract class Air_Traffic_Monitor
    {
        public IOccurenceDetector Detector { get; set; }
        public IDisplay Display { get; set; }
    }

    public class Commercial_ATM : Air_Traffic_Monitor
    {
        public List<Track> Tracks { get; set; }
        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurrenceTime { get; set; }

        public Commercial_ATM(IOccurenceDetector detector, IDisplay display)
        {
            Detector = detector;
            Display = display;
            Detector.OccurenceDetectedEvent += HandleOccurenceEvent;
        }

        private void HandleOccurenceEvent(object sender, OccurrenceEventArgs e)
        {
            // Render occurence
            ObservedTrack = e.ObservedTrack;
            OccurenceTrack = e.OccurenceTrack;
            OccurrenceTime = e.OccurenceTime;

            Display.RenderOccurence(ObservedTrack, OccurenceTrack, OccurrenceTime);
            
            // Log occurence
        }
    }

}
