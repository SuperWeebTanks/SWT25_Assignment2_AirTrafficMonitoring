using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using TransponderReceiver;
using System.Threading;

namespace SWT25_Assignment2_AirTrafficMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dependencies for Airport
            DecodeFactory.DecodeFactory trackFactory = new TrackFactory();
            AirSpace airspace = new AirSpace
            {
                Height_from = 500,
                Height_to = 20000,
                X = 80000,
                Y = 80000
            };
            IExceptionHandler exceptionHandler = new NullReferenceExceptionHandler();

            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            //// Dependency injection with the real TDR
            //var system = new TransponderReceiverUser.TransponderReceiverClient(receiver);

            //// Let the real TDR execute in the background
            //while (true)
            //    Thread.Sleep(1000);

            var Airport = new Airport(receiver, trackFactory, airspace, exceptionHandler);

            //Dependencies for ATM 
            IOccurenceDetector Detector = new TrackOccurrenceDetector();
            IDisplay Display = new MonitorConsole();
            IOccurrenceLogger Logger = new OccurrenceLogger();
            IFormat Formatter = new TrackFormater();

            Air_Traffic_Monitor ATM = new Air_Traffic_Monitor(Airport, Detector, Display, Logger, Formatter);


            #region DecodeFactory Test
            /*
            var Aircrafts = new List<string>
            {
                "BTR312;2004;18204;5500;20151006213456789",
                "BTR312;3200;18602;5500;20151006213456789",
                "BTR312;4200;18802;5500;20151006213456789"
            };

            var factory = new TrackFactory();
            var tracks = factory.CreateTracks(Aircrafts); 
            
            foreach(var track in tracks)
            {
                Console.WriteLine($"Tag: {track.Tag}");
                Console.WriteLine($"Current altitude: x:{track.CurrentPositionX.ToString()}, y:{track.CurrentPositionY.ToString()}");
                Console.WriteLine($"Current altitude (Meters): {track.CurrentAltitude.ToString()}");
                Console.WriteLine($"Current Horizontal Velocity (m/s): {track.CurrentHorizontalVelocity.ToString()}");
                Console.WriteLine($"Current Compass Course: {track.CurrentCompassCourse}");
                Console.WriteLine( $"Timestamp: {track.TimeStamp.ToString()}\n");
                
            }
            */
            #endregion


            Console.ReadLine();
        }

    }
}
