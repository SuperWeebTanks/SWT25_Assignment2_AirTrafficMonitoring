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
        public List<Track> Tracks { get; set; }
        public List<string[]> OccurrenceTracks { get; set; }
        public IOccurenceDetector Detector { get; set; }
        public IDisplay Display { get; set; }
        public ISignalForwarder Airport { get; set; }
        public IOccurrenceLogger Logger { get; set; }
        public IFormat Formatter { get; set; }
        public Track ObservedTrack { get; set; }
        public Track OccurenceTrack { get; set; }
        public DateTime OccurrenceTime { get; set; }
        public IConsoleClear Console { get; set; }
        public IExceptionHandler Exception { get; set; }

        public Air_Traffic_Monitor(ISignalForwarder airport, IOccurenceDetector detector,IDisplay display, IOccurrenceLogger logger, IFormat formatter,IConsoleClear console,IExceptionHandler exc)
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
            Console = console;
            Exception = exc;
        }

        
        private void HandleTrackEvent(object sender, TrackDataEventArgs e)
        {
            
            Console.ClearConsole();
            OccurrenceTracks.Clear();
            try
            {
                if(e.TrackData==null)
                    throw new NullReferenceException("empty Track list");
                
            

            var listOfTracks = e.TrackData;
            foreach (var track in listOfTracks)
            {
                Formatter.FormatTracks(track, Tracks);
                Detector.CheckOccurrence(track, Tracks);
            }
            Display.RenderOccurences(OccurrenceTracks);
            Display.RenderTrack(Tracks);
            }
            catch (NullReferenceException ex)
            { Exception.Handle(ex); }
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

    public interface IConsoleClear
    {
        void ClearConsole();

    }
    public class ConsoleClear
    {
        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
