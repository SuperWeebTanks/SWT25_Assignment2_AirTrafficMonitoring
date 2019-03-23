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
    public abstract class Air_Traffic_Monitor
    {
        private List<Track> tracks;
        public IOccurenceDetector Detector { get; set; }
        public IDisplay Display { get; set; }
        public Airport Airport { get; set; }
        public OccurrenceLogger Logger { get; set; }

        protected Air_Traffic_Monitor(Airport airport, IOccurenceDetector detector,IDisplay display, OccurrenceLogger logger)
        {
            tracks=new List<Track>();
            Airport = airport;
            Display = display;
            Detector = detector;
            Logger = logger;                    
        }
        
        public void Update(Object sender, TrackDataEventArgs e)
        {
            var list = e.TrackData;
            foreach (var track in list)
            {
                //var listOfTracksInOccurence= kald til IOccurence funktion 
            }
        }
    }

    public class Commercial_ATM : Air_Traffic_Monitor
    {
        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurrenceTime { get; set; }

        public Commercial_ATM(Airport airport, IOccurenceDetector detector, IDisplay display, OccurrenceLogger logger)
        : base(airport, detector, display, logger)
        {
            Airport.TrackDataEvent += Update;
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
            Logger.LogOccurrences(ObservedTrack, OccurenceTrack, OccurrenceTime);
        }
    }
}
