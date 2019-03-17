using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.Airport;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;


namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public abstract class Air_Traffic_Monitor
    {
        private List<Track> tracks;
        private IOccurenceDetector occurenceDetecter;
        private IDisplay display;
        protected Air_Traffic_Monitor(Airport.Airport airport, IOccurenceDetector detector,IDisplay display)
        {
            tracks=new List<Track>();
            this.display = display;
            occurenceDetecter = detector;
            airport.TrackDataEvent += Update;
        }


        public void Update(Object sender, TrackDataEventArgs e)
        {
            var list = e.TrackData;
            foreach (var track in list)
            {
                var listOfTracksInOccurence=//kald til IOccurence funktion 


            }
        }

        
        
    }

    public class Commercial_ATM : Air_Traffic_Monitor
    {
        public List<Track> Tracks { get; set; }
        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurrenceTime { get; set; }

        public Commercial_ATM(IOccurenceDetector detector, IDisplay display, OccurrenceLogger logger)
        {
            Detector = detector;
            Display = display;
            Logger = logger;
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
