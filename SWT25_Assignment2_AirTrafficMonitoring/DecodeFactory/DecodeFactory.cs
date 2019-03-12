using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory
{
    public abstract class DecodeFactory
    {
        public abstract List<Track> CreateTracks(List<string> transponderInfo, DateTime time);
    }

    public class CommercialTrackFactory : DecodeFactory
    {
        public override List<Track> CreateTracks(List<string> transponderInfo, DateTime time)
        {
            if (transponderInfo != null)
            {
                List<Track> commercialTracks = new List<Track>();
                foreach (var info in transponderInfo)
                {
                    string[] properties = info.Split(';');

                    //Missing check if properties contain the correct amount of information
                    Track c_Track = new CommercialTrack();
                    c_Track.Tag = properties[0];
                    c_Track.CurrentPositionX = int.Parse(properties[1]);
                    c_Track.CurrentPositionY = int.Parse(properties[2]);
                    c_Track.CurrentAltitude = int.Parse(properties[3]);
                    c_Track.CurrentHorizontalVelocity = int.Parse(properties[4]);
                    c_Track.CurrentCompassCourse = properties[5];
                    c_Track.TimeStamp = time;
                    commercialTracks.Add(c_Track); 
                }
                return commercialTracks; 
            }
            else
                return null; 
        }
    }
}
