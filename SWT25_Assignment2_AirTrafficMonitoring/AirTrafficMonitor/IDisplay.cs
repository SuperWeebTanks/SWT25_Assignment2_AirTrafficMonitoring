using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public interface IDisplay
    {
        void RenderOccurences(string[] occurences);
        void RenderTrack(List<Track> ListOfTracks);
    }
    
    public class MonitorConsole : IDisplay
    {
        public void RenderOccurences(string[] occurences)
        {
            foreach (var textline in occurences)
            {
                Console.WriteLine(textline);
            }
        }

        public void RenderTrack(List<Track> ListOfTracks)
        {
            foreach (var track in ListOfTracks)
            {
                Console.WriteLine($"Tag: {track.Tag}");
                Console.WriteLine($"Current altitude: x:{track.CurrentPositionX.ToString()}, y:{track.CurrentPositionY.ToString()}");
                Console.WriteLine($"Current altitude (Meters): {track.CurrentAltitude.ToString()}");
                Console.WriteLine($"Current Horizontal Velocity (m/s): {track.CurrentHorizontalVelocity.ToString()}");
                Console.WriteLine($"Current Compass Course: {track.CurrentCompassCourse}");
                Console.WriteLine($"Timestamp: {track.TimeStamp.ToString()}\n");
            }
        }
    }
}
