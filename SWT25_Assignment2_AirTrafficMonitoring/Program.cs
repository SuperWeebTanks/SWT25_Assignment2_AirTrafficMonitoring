using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
           
            #region DecodeFactory Test
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
            #endregion


            Console.ReadLine();
        }

    }
}
