using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory
{
    public abstract class DecodeFactory
    {
        /// <summary>
        /// Abstract Factory Class 
        /// </summary>
        /// <param name="transponderInfo"></param>
        /// <param name="time"></param>
        /// <returns>A list of Tracks</returns>
        public abstract List<Track> CreateTracks(List<string> transponderInfo);
    }

    public class CommercialTrackFactory : DecodeFactory
    {
        /// <summary>
        /// Factorymethod for Commercial Aircraft Tracks
        /// </summary>
        /// <param name="transponderInfo"></param>
        /// <returns>A list of CommercialTracks</returns>
        #region FactoryMethod
        public override List<Track> CreateTracks(List<string> transponderInfo)
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
                    c_Track.TimeStamp = new DateTime(int.Parse(properties[4].Substring(0,4)), int.Parse(properties[4].Substring(4,2)),
                        int.Parse(properties[4].Substring(6, 2)), int.Parse(properties[4].Substring(8, 2)), int.Parse(properties[4].Substring(10, 2))
                        , int.Parse(properties[4].Substring(12, 2)), int.Parse(properties[4].Substring(14,3)));

                    commercialTracks.Add(c_Track); 
                }
                return commercialTracks; 
            }
            else
                return null; 
        }
        #endregion
    }
}
