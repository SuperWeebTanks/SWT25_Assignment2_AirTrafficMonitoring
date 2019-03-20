using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory
{
    public interface DecodeFactory
    {
        /// <summary>
        /// Abstract Factory Class 
        /// </summary>
        /// <param name="transponderInfo"></param>
        /// <param name="time"></param>
        /// <returns>A list of Tracks</returns>
         List<Track> CreateTracks(List<string> transponderInfo);
    }

    public class TrackFactory : DecodeFactory
    {
        /// <summary>
        /// Factorymethod for Commercial Aircraft Tracks
        /// </summary>
        /// <param name="transponderInfo"></param>
        /// <returns>A list of CommercialTracks</returns>
        #region FactoryMethod
        public List<Track> CreateTracks(List<string> transponderInfo)
        {
            if (transponderInfo.Count > 0)
            {
                List<Track> commercialTracks = new List<Track>();

                try
                {
                    #region String Conversion Into Track

                    foreach (var info in transponderInfo)
                    {
                        string[] properties = info.Split(';');

                        if (properties.Length == 5)
                        {
                            //Missing check if properties contain the correct amount of information
                            //Perhaps insert try/catch block 
                            Track c_Track = new Track();
                            c_Track.Tag = properties[0];
                            c_Track.CurrentPositionX = int.Parse(properties[1]);
                            c_Track.CurrentPositionY = int.Parse(properties[2]);
                            c_Track.CurrentAltitude = int.Parse(properties[3]);
                            c_Track.TimeStamp = new DateTime(int.Parse(properties[4].Substring(0, 4)),
                                int.Parse(properties[4].Substring(4, 2)),
                                int.Parse(properties[4].Substring(6, 2)), int.Parse(properties[4].Substring(8, 2)),
                                int.Parse(properties[4].Substring(10, 2))
                                , int.Parse(properties[4].Substring(12, 2)));

                            commercialTracks.Add(c_Track);
                        }
                        else
                            throw new ArgumentException("Invalid String");
                    }

                    #endregion
                }
                catch (ArgumentException e) when(e.ParamName == "Invalid Tag")
                {
                    Console.WriteLine(e.Message);
                }

                return commercialTracks;
            }
            else
                return null;
        }
        #endregion
    }
}
