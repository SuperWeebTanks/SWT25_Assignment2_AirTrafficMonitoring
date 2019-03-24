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
        private List<Track> Tracks { get; set; }
        private List<string[]> OccurrenceTracks { get; set; }
        public IOccurenceDetector Detector { get; set; }
        public IDisplay Display { get; set; }
        public ISignalForwarder Airport { get; set; }
        public IOccurrenceLogger Logger { get; set; }
        public IFormat Formatter { get; set; }
        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurrenceTime { get; set; }

        public Air_Traffic_Monitor(ISignalForwarder airport, IOccurenceDetector detector,IDisplay display, IOccurrenceLogger logger, IFormat formatter)
        {
            Tracks=new List<Track>();
            OccurrenceTracks = new List<string[]>();
            Airport = airport;
            Display = display;
            Detector = detector;
            Logger = logger;    
            Formatter = formatter;
            Airport.TrackDataEvent += HandleTrackEvent;
            Detector.OccurenceDetectedEvent += HandleOccurenceEvent;
        }
        
        private void HandleTrackEvent(object sender, TrackDataEventArgs e)
        {
            if (e.TrackData != null)
            {
                Console.Clear();
                OccurrenceTracks.Clear();
                var listOfTracks = e.TrackData;
                foreach (var track in listOfTracks)
                {
                    Formatter.FormatTracks(track, Tracks);
                    Detector.CheckOccurrence(track, Tracks);
                }

                Display.RenderOccurences(OccurrenceTracks);
                Display.RenderTrack(Tracks);
            }
        }

        private void HandleOccurenceEvent(object sender, OccurrenceEventArgs e)
        {
            // Render occurence
            ObservedTrack = e.ObservedTrack;
            OccurenceTrack = e.OccurenceTrack;
            OccurrenceTime = e.OccurenceTime;

            OccurrenceTracks.Add(Formatter.FormatOccurence(ObservedTrack, OccurenceTrack, OccurrenceTime));

            // Log occurence
            Logger.LogOccurrences(ObservedTrack, OccurenceTrack, OccurrenceTime);
        }
    }
}
