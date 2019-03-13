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
        void RenderOccurences(Track observedTrack, List<Track> OccurenceTracks);
        void RenderTrack(Track updatedTrack);
    }

    public class MonitorConsole : IDisplay
    {
        public void RenderOccurences(Track observedTrack, List<Track> OccurenceTracks)
        {
            Console.WriteLine("WARNING: COLLISIONS MAY OCCOUR!");
            Console.WriteLine($"AIRCRAFT IN COLLISION COURSE: {observedTrack.Tag}");
            Console.WriteLine($"AIRCRAFT(s) IT MAY COLLIDE WITH: ");
            foreach(var track in OccurenceTracks)
            {
                Console.Write($"{observedTrack.Tag}, "); 
            }
            Console.Write("PLEASE ADVISE CAUTION!");

        }

        List<Track> RenderedTracks { get; set; }
        

        public void RenderTrack(Track updatedTrack)
        {
            if (RenderedTracks.Find(x => x.Tag.Contains(updatedTrack.Tag)) != null)
            {
                var track = RenderedTracks.Find(x => x.Tag.Contains(updatedTrack.Tag));
                track.CurrentPositionX = updatedTrack.CurrentPositionX;
                track.CurrentPositionY = updatedTrack.CurrentPositionY;
                track.CurrentAltitude = updatedTrack.CurrentAltitude;
                track.CurrentHorizontalVelocity = updatedTrack.CurrentHorizontalVelocity;
                track.CurrentCompassCourse = updatedTrack.CurrentCompassCourse;
                track.TimeStamp = updatedTrack.TimeStamp; 
            }
            else
                RenderedTracks.Add(updatedTrack); 

            foreach(var track in RenderedTracks)
            {
                track.PrintTrack(); 
            }

        }
    }
}
