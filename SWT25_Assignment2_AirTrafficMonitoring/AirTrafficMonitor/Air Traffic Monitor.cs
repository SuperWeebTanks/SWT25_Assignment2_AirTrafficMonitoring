using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;


namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public class Air_Traffic_Monitor
    {
        private List<Track> tracks;
        public IOccurenceDetector Detector { get; set; }
        public IDisplay Display { get; set; }
        public ISignalForwarder Airport { get; set; }
        public IOccurrenceLogger Logger { get; set; }
        public IFormat Formatter { get; set; }

        public Air_Traffic_Monitor(ISignalForwarder airport, IOccurenceDetector detector,IDisplay display, IOccurrenceLogger logger, IFormat formatter)
        {
            tracks=new List<Track>();
            Airport = airport;
            Display = display;
            Detector = detector;
            Logger = logger;    
            Formatter = formatter;
            Airport.TrackDataEvent += Update;
            Detector.OccurenceDetectedEvent += HandleOccurenceEvent;
        }
        
        public void Update(Object sender, TrackDataEventArgs e)
        {
            var list = e.TrackData;
            foreach (var track in list)
            {
                //var listOfTracksInOccurence= kald til IOccurence funktion 
            }
        }

        private void HandleOccurenceEvent(object sender, OccurrenceEventArgs e)
        {
            // Render occurence
            ObservedTrack = e.ObservedTrack;
            OccurenceTrack = e.OccurenceTrack;
            OccurrenceTime = e.OccurenceTime;

            Display.RenderOccurences(Formatter.FormatOccurence(ObservedTrack, OccurenceTrack, OccurrenceTime));

            // Log occurence
            Logger.LogOccurrences(ObservedTrack, OccurenceTrack, OccurrenceTime);
        }

        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurrenceTime { get; set; }
    }
}
