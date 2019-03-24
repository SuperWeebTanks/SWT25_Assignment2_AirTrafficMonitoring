using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring
{
    public interface IFormat
    {
        string[] FormatOccurence(Track observedTrack, Track occurenceTrack, DateTime occurenceTime);
        void FormatTracks(Track updatedTrack, List<Track> allTracks);

    }
    public class TrackFormater : IFormat
    {
        public string[] FormatOccurence(Track observedTrack, Track occurenceTrack, DateTime occurenceTime)
        {

            var occurences = new string[2];

            //Format information into string array
            occurences[0] = "WARNING: COLLISION MAY OCCUR!";
            occurences[1] =
                $"POTENTIAL COLLISION BETWEEN AIRCRAFTS: {observedTrack.Tag} and {occurenceTrack.Tag} at {occurenceTime}";

            return occurences;
        }

        public void FormatTracks(Track updatedTrack, List<Track> ListOfTracks)
        {

            //Check to see if track already exists in list of tracks
            if (!object.ReferenceEquals(ListOfTracks.Find(x => x.Tag.Contains(updatedTrack.Tag)), null))
            {
                var track = ListOfTracks.Find(x => x.Tag.Contains(updatedTrack.Tag));
                //Update these, compare to previous location 
                track.CurrentHorizontalVelocity = Calculator.CalculateHorizontalVelocity(updatedTrack, track);

                track.CurrentCompassCourse = (int)Calculator.CalculateCompassCourse(updatedTrack, track);

                //Update all properties for that track
                track.CurrentPositionX = updatedTrack.CurrentPositionX;
                track.CurrentPositionY = updatedTrack.CurrentPositionY;
                track.CurrentAltitude = updatedTrack.CurrentAltitude;
                track.TimeStamp = updatedTrack.TimeStamp;
                
            }
            //Track does not exist, therefore it must be rendered as a new track
            else
            {
                ListOfTracks.Add(updatedTrack);
            }
        }
    }
}
