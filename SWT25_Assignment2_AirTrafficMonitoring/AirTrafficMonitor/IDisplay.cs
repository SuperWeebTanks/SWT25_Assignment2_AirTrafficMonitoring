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
        void RenderOccurence(Track observedTrack, Track occurenceTrack, DateTime occurenceTime);
        void RenderTrack(Track updatedTrack);
    }

    public class MonitorConsole : IDisplay
    {
        public void RenderOccurence(Track observedTrack, Track occurenceTrack, DateTime occurenceTime)
        {
            Console.WriteLine("WARNING: COLLISION MAY OCCUR!");
            Console.WriteLine($"POTENTIAL COLLISION BETWEEN AIRCRAFTS: {observedTrack.Tag} and {occurenceTrack.Tag} at {occurenceTime}");
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
